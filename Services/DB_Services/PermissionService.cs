using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using TaggerApi.DTOs;
using TaggerApi.Models;
using TaggerApi.Pagination;
using TaggerApi.Extensions;
using TaggerApi.Services.ErrorServices;
using TaggerApi.Services.Interfaces;

namespace TaggerApi.Services.DB_Services;

public class PermissionService : IPermissionService
{
    private readonly PostgresContext _context;
    public PermissionService(PostgresContext context){
        _context = context;
    }    


    public Task<string> CreatePermission(PermissionDTO perDTO, string userUid)
    {
        throw new NotImplementedException();
    }

    public Task<PermissionDTO> DelPermissions(string token)
    {
        throw new NotImplementedException();
    }

    public Task<PermissionDTO> GetPermissions(int idvideo)
    {
        throw new NotImplementedException();
    }
}    