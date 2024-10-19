using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using TaggerApi.DTOs;
using TaggerApi.Models;
using TaggerApi.Pagination;
using TaggerApi.Services.ErrorServices;
using TaggerApi.Services.Interfaces;


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
            Medialink = "",
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
            throw new NotFoundException("Tag not found");
        }

        var video = await _context.Videos.FindAsync(tag.IdVideo);
         
        if(userUid != tag.IdUser && userUid != video.IdUser){
            throw new UnauthorizedAccessException("Tag not your property.");
        }        

        _context.Tags.Remove(tag);
        await _context.SaveChangesAsync();

        return true;        
    }


    public async Task<TagDTO> UpdateTag(long id, UpdateTagDTO tagDTO,string userUid)
    {
        var tag = await _context.Tags.FindAsync(id);

        if(tag == null){
            throw new NotFoundException("Tag not found.");
        }

        if(userUid != tag.IdUser){
            throw new UnauthorizedAccessException("Tag not user property.");
        }        

        tag.Content = tagDTO.Content;
        
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
       }              

       return listDto;
    }
}