using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using TaggerApi.DTOs;
using TaggerApi.Models;

namespace TaggerApi.Services.DB_Services;

public class TagService : ITagService
{

    private readonly PostgresContext _context;

    public TagService(PostgresContext context){
        _context = context;
    }

    public Task<TagDTO> AddTag(VideoDTO videoDTO)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DelTag(long id)
    {
        throw new NotImplementedException();
    }

    public Task<TagDTO> RetrieveTag(long id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<TagDTO>> RetrieveTags()
    {
        throw new NotImplementedException();
    }

    public Task<TagDTO> UpdateTag(long id, VideoDTO videoDTO)
    {
        throw new NotImplementedException();
    }
}