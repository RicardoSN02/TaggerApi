namespace TaggerApi.Services.DB_Services;

using TaggerApi.DTOs;
using TaggerApi.Models;
public interface ITagService{

    Task<TagDTO> AddTag(VideoDTO videoDTO);

    Task<TagDTO> UpdateTag(long id,VideoDTO videoDTO);

    Task<IEnumerable<TagDTO>> RetrieveTags();

    Task<TagDTO> RetrieveTag(long id);

    Task<bool> DelTag(long id);

}