using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaggerApi.Models;
using TaggerApi.Services.Interfaces;
using TaggerApi.DTOs;
using Microsoft.AspNetCore.Authorization;
using TaggerApi.Services.ErrorServices;

namespace TaggerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionController : ControllerBase
    {
        private readonly IPermissionService _perService;

        public PermissionController(IPermissionService permissionService)
        {
            _perService = permissionService;
        }

        
        // GET: api/Permission
        [Authorize]
        [HttpGet("{idvideo}")]
        public async Task<ActionResult<string>> GetPermissions(int idvideo)
        {
            var userUid = User.FindFirst("user_id")?.Value;
            
            if(userUid == null){
              return NotFound("User not found");
            }

            try{
               
               var token = await _perService.GetPermission(idvideo,userUid);
               var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.PathBase}";
            
               return Ok(baseUrl+"/api/Video/"+token);

            }catch(NotFoundException e){
                return NotFound(e.Message);
            }catch(Exception e){
                return BadRequest(e.Message);
            }
            
      

            
        }

        
        // POST: api/Permission
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<string>> PostPermission(PermissionDTO permissiondto)
        {
            var userUid = User.FindFirst("user_id")?.Value;
            
            if(userUid == null){
              return NotFound("User not found");
            }

            try{
               var result = await _perService.CreatePermission(permissiondto,userUid);
               var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.PathBase}";
            
               return Ok(baseUrl+"/api/Video/"+result);
            }catch(NotFoundException e){

                return NotFound(e.Message);

            }catch(Exception e){
                return BadRequest(e.Message);
            }
        }
        

        // DELETE: api/Permission/5
        [Authorize]
        [HttpDelete("{idvideo}")]
        public async Task<IActionResult> DeletePermission(int idvideo)
        {
            var userUid = User.FindFirst("user_id")?.Value;

            if(userUid == null){
               return NotFound("User not found");
            }

            try{
                bool result = await _perService.DelPermissions(idvideo,userUid);
                
                if (result == false)
                {
                  return NotFound("Not found");
                }

                return NoContent();

            }catch(Exception e){
                return BadRequest(e.Message);
            }
        }
    
    }
}
