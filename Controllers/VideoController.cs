using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaggerApi.Models;
using TaggerApi.DTOs;
using TaggerApi.Services.DB_Services;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using FirebaseAdmin.Auth;
using TaggerApi.Pagination;
using TaggerApi.Services.Interfaces;
using TaggerApi.Services.ErrorServices;

namespace TaggerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoController : ControllerBase
    {
        private readonly IVideoService _videoService;

        public VideoController(IVideoService videoService)
        {
            _videoService = videoService;
        }

        [Authorize]
        [Route("owner")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VideoDTO>>> GetVideosUser(
          [FromQuery] PaginationParams paginationQuery
        )
        {          
            var userUid = User.FindFirst("user_id")?.Value;
            
            if(userUid == null){
              throw new NotFoundException("User not found.");
            }

            return Ok(await _videoService.GetByUserPag(paginationQuery, userUid));
        }

        [HttpGet("{token}")]
        public async Task<ActionResult<IEnumerable<VideoDTO>>> GetVideosUser(string token)
        {         
            return Ok(await _videoService.GetSharedVideo(token));
        }        

        // PUT: api/Video/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<VideoDTO>> PutVideo(long id, VideoDTO videoDTO)
        { 
          var userUid = User.FindFirst("user_id")?.Value;

          if(userUid == null){
            throw new NotFoundException("User not found.");
          }

          var video = await _videoService.UpdateVideo(id,videoDTO,userUid);

          return Ok(video);

        }

    
        // POST: api/Video
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<VideoDTO>> PostVideo(VideoDTO videoDTO)
        {
   
          var userUid = User.FindFirst("user_id")?.Value;

          if(userUid == null){
            throw new NotFoundException("User Not Found.");
          }

          var video = await _videoService.AddVideo(videoDTO,userUid);
          
          return Ok(video);
          
        }

        // DELETE: api/Video/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVideo(long id)
        {
          var userUid = (User.FindFirst("user_id")?.Value) ?? throw new NotFoundException("User not found");
            
          bool result = await _videoService.DelVideo(id,userUid);

          return Ok("Video deleted");
        }
    }
}
