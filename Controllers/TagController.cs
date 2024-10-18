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
using TaggerApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using TaggerApi.Pagination;
using TaggerApi.Services.ErrorServices;

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


        [Authorize]
        [Route("videos")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VideoDTO>>> GetVideosUser(
          [FromQuery] PaginationParams paginationQuery, [FromQuery]int idvideo
        )
        {        
            return Ok(await _tagService.GetTagsPag(paginationQuery, idvideo));
        }        


        // PUT: api/Tag/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<TagDTO>> PutTag(long id, TagDTO tagDTO)
        {
            var userUid = User.FindFirst("user_id")?.Value;

            if(userUid == null){
               return NotFound("User not found");
            }

            var tag = await _tagService.UpdateTag(id,tagDTO,userUid);
            
            return Ok(tag);


        }

        // POST: api/Tag
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<TagDTO>> PostTag(TagDTO tagDTO)
        {
            var userUid = User.FindFirst("user_id")?.Value;

            if(userUid == null){
              throw new NotFoundException("User not found");
            }

            var tag = await _tagService.AddTag(tagDTO,userUid);

            return  Ok(tag);

        }

        // DELETE: api/Tag/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTag(long id)
        {
            var userUid = User.FindFirst("user_id")?.Value;

            if(userUid == null){
              return NotFound("User not found");
            }

            bool result = await _tagService.DelTag(id,userUid);

            return Ok("Tag deleted properly");

        }
        
    }
}
