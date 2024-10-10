namespace TaggerApi.Services.DB_Services;

using TaggerApi.DTOs;
using TaggerApi.Models;
public interface ITagService{

    Task<TagDTO> AddTag(TagDTO tagDTO);

    Task<TagDTO> UpdateTag(long id,TagDTO tagDTO);

    Task<IEnumerable<TagDTO>> RetrieveTags();

    Task<TagDTO> RetrieveTag(long id);

    Task<bool> DelTag(long id);

}