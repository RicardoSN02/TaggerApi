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

        // GET: api/Video/5
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

        // PUT: api/Video/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<VideoDTO>> PutVideo(long id, VideoDTO videoDTO)
        {
            if (id != videoDTO.Id)
            {
                return BadRequest();
            }

            try{
               var video = await _videoService.UpdateVideo(id,videoDTO);

               return video;
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
        [HttpPost]
        public async Task<ActionResult<VideoDTO>> PostVideo(VideoDTO videoDTO)
        {
            try{
               var video = await _videoService.AddVideo(videoDTO);
               return CreatedAtAction(nameof(GetVideo), new { id = video.Id }, video);
            }catch(Exception e){
                return BadRequest(e.Message);
            }
           
        }

        // DELETE: api/Video/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVideo(long id)
        {
            try{
                bool result = await _videoService.DelVideo(id);
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
