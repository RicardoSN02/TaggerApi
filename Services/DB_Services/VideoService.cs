using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using TaggerApi.DTOs;
using TaggerApi.Models;
using TaggerApi.Services.ErrorServices;

namespace TaggerApi.Services.DB_Services;

public class VideoService : IVideoService
{
    
    private readonly PostgresContext _context;

    public VideoService(PostgresContext context){
        _context = context;
    }

    public async Task<VideoDTO> AddVideo(VideoDTO videoDTO)
    {
        var video = new Video{
            Id = videoDTO.Id,
            Name = videoDTO.Name,
            Link = videoDTO.Link,
            Description = videoDTO.Description,
            IdUser = videoDTO.IdUser
        };
            
        _context.Videos.Add(video);
        await _context.SaveChangesAsync();

        return VideoToDTO(video);
    }

    public async Task<bool> DelVideo(long id)
    {
        var video = await _context.Videos.FindAsync(id);
        
        if (video == null)
        {
            return false;
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

    public async Task<VideoDTO> UpdateVideo(long id,VideoDTO videoDTO)
    {
        var video = await _context.Videos.FindAsync(id);

        if(video == null){
            throw new NotFoundException("Video not found.");
        }

        video.Name = videoDTO.Name;
        video.Link = videoDTO.Link;
        video.Description = videoDTO.Description;
        video.IdUser = videoDTO.IdUser;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        when (!VideoExists(id))
        {
           throw new NotFoundException("Video not found");
        }

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
        Description = video.Description,
        IdUser = video.IdUser
    };    
}