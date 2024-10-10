using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using TaggerApi.DTOs;
using TaggerApi.Models;
using TaggerApi.Services.ErrorServices;

namespace TaggerApi.Services.DB_Services;

public class TagService : ITagService
{

    private readonly PostgresContext _context;

    public TagService(PostgresContext context){
        _context = context;
    }

    public async Task<TagDTO> AddTag(TagDTO tagDTO)
    {
        var  tag = new Tag{
            Id = tagDTO.Id,
            Content = tagDTO.Content,
            Timestamp = tagDTO.Timestamp,
            Medialink = tagDTO.Medialink,
            IdUser = tagDTO.IdUser,
            IdVideo = tagDTO.IdVideo
        };

        _context.Tags.Add(tag);
        await _context.SaveChangesAsync();

        return TagToDTO(tag);
    }

    public async Task<bool> DelTag(long id)
    {
        var tag = await _context.Tags.FindAsync(id);
        if (tag == null)
        {
            return false;
        }

        _context.Tags.Remove(tag);
        await _context.SaveChangesAsync();

        return true;        
    }

    public async Task<TagDTO> RetrieveTag(long id)
    {
        var tag = await _context.Tags.FindAsync(id);

        if (tag == null)
        {
            throw new NotFoundException("Video not found.");
        }

        return TagToDTO(tag);
    }

    public async Task<IEnumerable<TagDTO>> RetrieveTags()
    {
        return await _context.Tags
            .Select(x => TagToDTO(x))
            .ToListAsync();
    }

    public async Task<TagDTO> UpdateTag(long id, TagDTO tagDTO)
    {
        var tag = await _context.Tags.FindAsync(id);

        if(tag == null){
            throw new NotFoundException("Video not found.");
        }

        tag.Content = tagDTO.Content;
        tag.Timestamp = tagDTO.Timestamp;
        tag.Medialink = tagDTO.Medialink;
        tag.IdUser = tagDTO.IdUser;
        tag.IdVideo = tagDTO.IdVideo;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        when (!TagExists(id))
        {
            throw new NotFoundException("Video not found.");
        }

        return TagToDTO(tag);
    }

    private bool TagExists(long id)
    {
        return _context.Tags.Any(e => e.Id == id);
    }    

    private static TagDTO TagToDTO(Tag tag) => 
        new TagDTO
    {
        Id = tag.Id,
        Content = tag.Content,
        Timestamp = tag.Timestamp,
        Medialink = tag.Medialink,
        IdUser = tag.IdUser,
        IdVideo = tag.IdVideo
    };
}