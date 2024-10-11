using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using TaggerApi.DTOs;
using TaggerApi.Models;
using TaggerApi.Pagination;
using TaggerApi.Extensions;
using TaggerApi.Services.ErrorServices;

namespace TaggerApi.Services.DB_Services;

public class VideoService : IVideoService
{
    
    private readonly PostgresContext _context;
    private readonly IPagedList _pageList;

    public VideoService(PostgresContext context, IPagedList pagination){
        _pageList = pagination;
        _context = context;
    }
          
    public async Task<VideoDTO> AddVideo(VideoDTO videoDTO,string userUid)
    {
        
        var video = new Video{
            Id = videoDTO.Id,
            Name = videoDTO.Name,
            Link = videoDTO.Link,
            Description = videoDTO.Description,
            IdUser = userUid
        };
            
        _context.Videos.Add(video);
        await _context.SaveChangesAsync();

        return VideoToDTO(video);
    }

    public async Task<bool> DelVideo(long id,string userUid)
    {
        var video = await _context.Videos.FindAsync(id);
        
        if (video == null)
        {
            return false;
        }

        if(userUid != video.IdUser){
            throw new UnauthorizedAccessException("Video not your property.");
        }

        _context.Videos.Remove(video);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<VideoDTO> RetrieveVideo(long id)
    {
        var video = await _context.Videos.FindAsync(id);

        if (video == null)
        {
            throw new NotFoundException("Video not found."); 
        }

        return VideoToDTO(video);
    }

    public async Task<IEnumerable<VideoDTO>> RetrieveVideos()
    {
        return await _context.Videos
                .Select(x => VideoToDTO(x))
                .ToListAsync();
    }

    public async Task<IEnumerable<VideoDTO>> GetByUser(string userUid)
    {
        return await _context.Videos
                .Where(b => b.IdUser == userUid)
                .Select(x => VideoToDTO(x))
                .ToListAsync();
    }    

    public async Task<VideoDTO> UpdateVideo(long id,VideoDTO videoDTO,string userUid)
    {
        var video = await _context.Videos.FindAsync(id);
        
        if(video == null){
            throw new NotFoundException("Video not found.");
        }

        if(userUid != video.IdUser){
            throw new UnauthorizedAccessException("Video not your property.");
        }

        video.Name = videoDTO.Name;
        video.Link = videoDTO.Link;
        video.Description = videoDTO.Description;

        await _context.SaveChangesAsync();
        
        return VideoToDTO(video);
    }

    private bool VideoExists(long id)
    {
        return _context.Videos.Any(e => e.Id == id);
    }

    private static VideoDTO VideoToDTO(Video video) =>
        new VideoDTO
    {
        Id = video.Id,
        Name = video.Name,
        Link = video.Link,
        Description = video.Description
    };

    public async Task<IEnumerable<VideoDTO>> GetByUserPag(PaginationParams request,string userUid)
    {
       var query =  _context.Videos
                .Where(b => b.IdUser == userUid);
                //.Select(x => VideoToDTO(x));

       var resultPagination = await _pageList.CreatePagedGenericResults<Video>(query,
           request.PageNumber,
           request.PageSize,
           request.OrderBy!,
           request.OrderAsc
       );

       var listDto = new List<VideoDTO>();

       foreach (var item in resultPagination.Result){
            listDto.Add(VideoToDTO(item));
       }

       return listDto;
    }
}