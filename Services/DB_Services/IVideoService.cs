namespace TaggerApi.Services.DB_Services;

using TaggerApi.DTOs;
using TaggerApi.Models;
public interface IVideoService{

    Task<VideoDTO> AddVideo(VideoDTO videoDTO);

    Task<VideoDTO> UpdateVideo(long id,VideoDTO videoDTO);

    Task<IEnumerable<VideoDTO>> RetrieveVideos();

    Task<VideoDTO> RetrieveVideo(long id);

    Task<bool> DelVideo(long id);

}