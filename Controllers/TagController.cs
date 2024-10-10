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

namespace TaggerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly ITagService _tagService;

        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }

        // GET: api/Tag
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TagDTO>>> GetTags()
        {
            try{
              return Ok(await _tagService.RetrieveTags());
            }catch(Exception e){
                return BadRequest(e.Message);
            }
        }

        // GET: api/Tag/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TagDTO>> GetTag(long id)
        {
            try{

                var tag = await _tagService.RetrieveTag(id);
                return tag;

            }catch(Exception e){
                if(e.Message.Contains("not found")){
                    return NotFound();
                }else{
                    return BadRequest(e.Message);
                }
            }
        }

        // PUT: api/Tag/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<TagDTO>> PutTag(long id, TagDTO tagDTO)
        {
            if (id != tagDTO.Id)
            {
                return BadRequest();
            }

            try{
                var tag = await _tagService.UpdateTag(id,tagDTO);
                return tag;

            }catch(Exception e){
                if(e.Message.Contains("not found")){
                    return NotFound();
                }else{
                    return BadRequest(e.Message);
                }
            }
        }

        // POST: api/Tag
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TagDTO>> PostTag(TagDTO tagDTO)
        {
            try{
                var tag = await _tagService.AddTag(tagDTO);
                return CreatedAtAction(nameof(GetTag), new { id = tag.Id }, tag);
            }catch(Exception e){
                return BadRequest(e.Message);
            }
        }

        // DELETE: api/Tag/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTag(long id)
        {
            try{
                bool result = await _tagService.DelTag(id);
                if(result == false){
                    return NotFound();
                }

                return NoContent();
            }catch(Exception e){
                return BadRequest(e.Message);
            }
        }
        
    }
}
