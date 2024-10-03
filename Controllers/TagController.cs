using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaggerApi.Models;
using TaggerApi.DTOs;

namespace TaggerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly PostgresContext _context;

        public TagController(PostgresContext context)
        {
            _context = context;
        }

        // GET: api/Tag
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TagDTO>>> GetTags()
        {
            return await _context.Tags
                 .Select(x => TagToDTO(x))
                 .ToListAsync();
        }

        // GET: api/Tag/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TagDTO>> GetTag(long id)
        {
            var tag = await _context.Tags.FindAsync(id);

            if (tag == null)
            {
                return NotFound();
            }

            return TagToDTO(tag);
        }

        // PUT: api/Tag/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTag(long id, TagDTO tagDTO)
        {
            if (id != tagDTO.Id)
            {
                return BadRequest();
            }

            var tag = await _context.Tags.FindAsync(id);

            if(tag == null){
                return NotFound();
            }

            tag.Content = tagDTO.Content;
            tag.Timestamp = tagDTO.Timestamp;
            tag.Medialink = tagDTO.Medialink;
            tag.IdUser = tagDTO.IdUser;
            tag.IdVideo = tagDTO.IdVideo;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            when (!TagExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/Tag
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TagDTO>> PostTag(TagDTO tagDTO)
        {
            var  tag = new Tag{
                Id = tagDTO.Id,
                Content = tagDTO.Content,
                Timestamp = tagDTO.Timestamp,
                Medialink = tagDTO.Medialink,
                IdUser = tagDTO.IdUser,
                IdVideo = tagDTO.IdVideo
            };

            _context.Tags.Add(tag);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTag), new { id = tag.Id }, tag);
        }

        // DELETE: api/Tag/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTag(long id)
        {
            var tag = await _context.Tags.FindAsync(id);
            if (tag == null)
            {
                return NotFound();
            }

            _context.Tags.Remove(tag);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TagExists(long id)
        {
            return _context.Tags.Any(e => e.Id == id);
        }

        private static TagDTO TagToDTO(Tag tag) => 
           new TagDTO
        {
           Id = tag.Id,
           Content = tag.Content,
           Timestamp = tag.Timestamp,
           Medialink = tag.Medialink,
           IdUser = tag.IdUser,
           IdVideo = tag.IdVideo
        };
        
    }
}
