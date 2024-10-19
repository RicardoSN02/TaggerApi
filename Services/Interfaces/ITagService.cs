namespace TaggerApi.Services.Interfaces;

using TaggerApi.DTOs;
using TaggerApi.Models;
using TaggerApi.Pagination;

public interface ITagService{

    Task<TagDTO> AddTag(TagDTO tagDTO, string userUid);

    Task<TagDTO> UpdateTag(long id,UpdateTagDTO tagDTO,string userUid);

    Task<bool> DelTag(long id,string userUid);

    Task<IEnumerable<TagDTO>> GetTagsPag(PaginationParams request,int idvideo);

}