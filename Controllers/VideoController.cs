using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaggerApi.Models;
using TaggerApi.DTOs;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Authorization;

namespace TaggerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoController : ControllerBase
    {
        private readonly PostgresContext _context;

        public VideoController(PostgresContext context)
        {
            _context = context;
        }

        // GET: api/Video
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VideoDTO>>> GetVideos()
        {
            return await _context.Videos
                    .Select(x => VideoToDTO(x))
                    .ToListAsync();
        }

        // GET: api/Video/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VideoDTO>> GetVideo(long id)
        {
            var video = await _context.Videos.FindAsync(id);

            if (video == null)
            {
                return NotFound();
            }

            return VideoToDTO(video);
        }

        // PUT: api/Video/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVideo(long id, VideoDTO videoDTO)
        {
            if (id != videoDTO.Id)
            {
                return BadRequest();
            }

            var video = await _context.Videos.FindAsync(id);

            if(video == null){
                return NotFound();
            }

            video.Name = videoDTO.Name;
            video.Link = videoDTO.Link;
            video.Description = videoDTO.Description;
            video.IdUser = videoDTO.IdUser;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            when (!VideoExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

    
        // POST: api/Video
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<VideoDTO>> PostVideo(VideoDTO videoDTO)
        {
            var video = new Video{
                Id = videoDTO.Id,
                Name = videoDTO.Name,
                Link = videoDTO.Link,
                Description = videoDTO.Description,
                IdUser = videoDTO.IdUser
            };
            
            _context.Videos.Add(video);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetVideo), new { id = video.Id }, video);
        }

        // DELETE: api/Video/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVideo(long id)
        {
            var video = await _context.Videos.FindAsync(id);
            if (video == null)
            {
                return NotFound();
            }

            _context.Videos.Remove(video);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VideoExists(long id)
        {
            return _context.Videos.Any(e => e.Id == id);
        }

        private static VideoDTO VideoToDTO(Video video) =>
           new VideoDTO
        {
           Id = video.Id,
           Name = video.Name,
           Link = video.Link,
           Description = video.Description,
           IdUser = video.IdUser
        };
    }
}
