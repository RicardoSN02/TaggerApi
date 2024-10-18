namespace TaggerApi.Services.Interfaces;

using TaggerApi.DTOs;
using TaggerApi.Models;
using TaggerApi.Services;
using TaggerApi.Extensions;
using TaggerApi.Pagination;
public interface IVideoService{

    Task<VideoDTO> AddVideo(VideoDTO videoDTO,string userUid);

    Task<VideoDTO> UpdateVideo(long id,VideoDTO videoDTO,string userUid);

    Task<bool> DelVideo(long id,string userUid);

    Task<IEnumerable<VideoDTO>> GetByUserPag(PaginationParams request,string userUid);

    Task<VideoDTO> GetSharedVideo(string token);    



}