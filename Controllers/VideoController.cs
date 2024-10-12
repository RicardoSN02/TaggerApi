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

        /*
        // GET: api/Video
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VideoDTO>>> GetVideos()
        {
            try{
              return Ok(await _videoService.RetrieveVideos());
            }catch(Exception e){
              return BadRequest(e.Message);
            }

        }
        */

        /*
        [Authorize]
        [Route("owner")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VideoDTO>>> GetVideosUser()
        {          
            var userUid = User.FindFirst("user_id")?.Value;
            
            if(userUid == null){
              return NotFound("User not found");
            }

            try{
              return Ok(await _videoService.GetByUser(userUid));
            }catch(Exception e){
              return BadRequest(e.Message);
            }

        }
       */

        [Authorize]
        [Route("owner")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VideoDTO>>> GetVideosUser(
          [FromQuery] PaginationParams paginationQuery
        )
        {          
            var userUid = User.FindFirst("user_id")?.Value;
            
            if(userUid == null){
              return NotFound("User not found");
            }

            try{
              return Ok(await _videoService.GetByUserPag(paginationQuery, userUid));
            }catch(Exception e){
              return BadRequest(e.Message);
            }

        }
        
        /*
        //TODO: title search
        //remove endpoint 
        // GET: api/Video/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<VideoDTO>> GetVideo(long id)
        {
            try{

               var video = await _videoService.RetrieveVideo(id);

               return video;

            }catch(Exception e){
                if(e.Message.Contains("not found")){

                  return NotFound();

                }else{
                  return BadRequest(e.Message);

                }

            }  
        }
        */

        // PUT: api/Video/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<VideoDTO>> PutVideo(long id, VideoDTO videoDTO)
        {
          var userUid = User.FindFirst("user_id")?.Value;

          if(userUid == null){
            return NotFound("User not found");
          }

          if (id != videoDTO.Id)
          {
            return BadRequest();
          }

          try{
            var video = await _videoService.UpdateVideo(id,videoDTO,userUid);

            return Ok(video);
          }catch(Exception e){
            if(e.Message.Contains("not found")){

              return NotFound();

            }else{
              return BadRequest(e.Message);

            }                
          }
        }

    
        // POST: api/Video
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<VideoDTO>> PostVideo(VideoDTO videoDTO)
        {
          var userUid = User.FindFirst("user_id")?.Value;

          if(userUid == null){
            return NotFound("User not found");
          }

          try{
            var video = await _videoService.AddVideo(videoDTO,userUid);
            return Ok(video);
          }catch(Exception e){
              return BadRequest(e.Message);
          }  
        }

        // DELETE: api/Video/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVideo(long id)
        {
          var userUid = User.FindFirst("user_id")?.Value;

          if(userUid == null){
            return NotFound("User not found");
          }

            try{
                bool result = await _videoService.DelVideo(id,userUid);
                if (result == false)
                {
                  return NotFound();
                }

                return NoContent();

            }catch(Exception e){
                return BadRequest(e.Message);
            }
 
        }
    }
}
