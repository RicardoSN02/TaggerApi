using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using TaggerApi.DTOs;
using TaggerApi.Models;
using TaggerApi.Pagination;
using TaggerApi.Extensions;
using TaggerApi.Services.ErrorServices;
using TaggerApi.Services.Interfaces;
using NuGet.Protocol;
using NuGet.Common;
using Microsoft.AspNetCore.Http.HttpResults;

namespace TaggerApi.Services.DB_Services;

public class PermissionService : IPermissionService
{
    private readonly PostgresContext _context;

    public PermissionService(PostgresContext context){
        _context = context;
    }    


    public async Task<string> CreatePermission(PermissionDTO perDTO, string userUid)
    {
        var permissionFind = await _context.Permissions.Include(p => p.IdVideoNavigation)
                                                   .Where(b => b.IdVideo == perDTO.IdVideo)
                                                   .FirstOrDefaultAsync();        

        if(permissionFind != null){
           throw new Exception("Link already exists");
        }   

        DateTime date = DateTime.UtcNow;
        date = date.AddDays(15);

        if(perDTO.Role == "viewer" || perDTO.Role == "editor" ){
           var permission = new Permission{
               IdVideo = perDTO.IdVideo,
               Role = perDTO.Role,  
               Expire = date
           };

        

           var video = await _context.Videos.FindAsync(perDTO.IdVideo);
        
           if(video == null){
               throw new NotFoundException("Video not found.");
           }

           if(userUid != video.IdUser){
               throw new UnauthorizedAccessException("Video not your property.");
           }        

           var result = await _context.Permissions.AddAsync(permission);
           await _context.SaveChangesAsync();

           return result.Entity.Token.ToString();
        }else{
            throw new Exception("Invalid role");
        }
    }

    public async Task<bool> DelPermissions(int idvideo,string userUid)
    {

        var permission = await _context.Permissions.Include(p => p.IdVideoNavigation)
                                                   .Where(b => b.IdVideo == idvideo)
                                                   .FirstOrDefaultAsync();

        if(permission == null){
            throw new NotFoundException("Permission not found");
        }

        if(permission.IdVideoNavigation.IdUser != userUid){
            throw new UnauthorizedAccessException("Video not your property");
        }

        _context.Permissions.Remove(permission);
        await _context.SaveChangesAsync();

        return true;        
        
    }

    public async Task<string> GetPermission(int idvideo, string userUid)
    {
        var permission = await _context.Permissions.Include(p => p.IdVideoNavigation)
                                                   .Where(b => b.IdVideo == idvideo)
                                                   .FirstOrDefaultAsync();
         
        if(permission==null){
            throw new NotFoundException("Permission link not found");
        }

        if(permission.IdVideoNavigation.IdUser != userUid){
            throw new UnauthorizedAccessException("Video not your property");
        }

        return permission.Token.ToString();

    }
}    