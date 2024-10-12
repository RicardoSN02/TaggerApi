using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using TaggerApi.DTOs;
using TaggerApi.Models;
using TaggerApi.Pagination;
using TaggerApi.Services.ErrorServices;


namespace TaggerApi.Services.DB_Services;

public class TagService : ITagService
{

    private readonly PostgresContext _context;
    private readonly IPagedList _pageList;

    public TagService(PostgresContext context, IPagedList pageList){
        _context = context;
        _pageList = pageList;
    }

    public async Task<TagDTO> AddTag(TagDTO tagDTO,string userUid)
    {
        var  tag = new Tag{
            Id = tagDTO.Id,
            Content = tagDTO.Content,
            Timestamp = tagDTO.Timestamp,
            Medialink = tagDTO.Medialink,
            IdUser = userUid,
            IdVideo = tagDTO.IdVideo
        };

        _context.Tags.Add(tag);
        await _context.SaveChangesAsync();

        return TagToDTO(tag);
    }

    public async Task<bool> DelTag(long id,string userUid)
    {
        var tag = await _context.Tags.FindAsync(id);
        
        if (tag == null)
        {
            return false;
        }

        var video = await _context.Videos.FindAsync(tag.IdVideo);

        if (video == null)
        {
            throw new NotFoundException("Video not found");
        }
         
        if(userUid != tag.IdUser && userUid != video.IdUser){
            throw new UnauthorizedAccessException("Tag not your property.");
        }        

        _context.Tags.Remove(tag);
        await _context.SaveChangesAsync();

        return true;        
    }

    //TODO: No use
    public async Task<TagDTO> RetrieveTag(long id)
    {
        var tag = await _context.Tags.FindAsync(id);

        if (tag == null)
        {
            throw new NotFoundException("Video not found.");
        }

        return TagToDTO(tag);
    }
    

    //TODO: No use
    public async Task<IEnumerable<TagDTO>> RetrieveTags()
    {
        return await _context.Tags
            .Select(x => TagToDTO(x))
            .ToListAsync();
    }


    public async Task<TagDTO> UpdateTag(long id, TagDTO tagDTO,string userUid)
    {
        var tag = await _context.Tags.FindAsync(id);

        if(tag == null){
            throw new NotFoundException("Tag not found.");
        }

        if(userUid != tag.IdUser){
            throw new UnauthorizedAccessException("Tag not your property.");
        }        

        tag.Content = tagDTO.Content;
        tag.Timestamp = tagDTO.Timestamp;
        tag.Medialink = tagDTO.Medialink;
        
        await _context.SaveChangesAsync();

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
        IdVideo = tag.IdVideo
    };

    public async Task<IEnumerable<TagDTO>> GetTagsPag(PaginationParams request,int idvideo)
    {
       var query = _context.Tags.Where(b => b.IdVideo == idvideo);

       var resultPagination = await _pageList.CreatePagedGenericResults<Tag>(query,
           request.PageNumber,
           request.PageSize,
           request.OrderBy!,
           request.OrderAsc
       );

       var listDto = new List<TagDTO>();
       
       foreach (var item in resultPagination.Result){
            listDto.Add(TagToDTO(item));
            Console.WriteLine(item);
       }              

       return listDto;
    }
}