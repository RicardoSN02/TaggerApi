namespace TaggerApi.Services.DB_Services;

using TaggerApi.DTOs;
using TaggerApi.Models;
using TaggerApi.Pagination;

public interface ITagService{

    Task<TagDTO> AddTag(TagDTO tagDTO, string userUid);

    Task<TagDTO> UpdateTag(long id,TagDTO tagDTO,string userUid);

    Task<IEnumerable<TagDTO>> RetrieveTags();

    Task<TagDTO> RetrieveTag(long id);

    Task<bool> DelTag(long id,string userUid);

    Task<IEnumerable<TagDTO>> GetTagsPag(PaginationParams request,int idvideo);

}