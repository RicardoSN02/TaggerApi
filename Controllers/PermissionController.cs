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
              throw new NotFoundException("User not found");
            }

            var token = await _perService.GetPermission(idvideo,userUid);
            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.PathBase}";
        
            return Ok(baseUrl+"/api/Video/"+token);

            
        }

        
        // POST: api/Permission
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<string>> PostPermission(PermissionDTO permissiondto)
        {
            var userUid = User.FindFirst("user_id")?.Value;
            
            if(userUid == null){
               throw new NotFoundException("User not found");
            }

            var result = await _perService.CreatePermission(permissiondto,userUid);
            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.PathBase}";
            
            return Ok(baseUrl+"/api/Video/"+result);

        }
        

        // DELETE: api/Permission/5
        [Authorize]
        [HttpDelete("{idvideo}")]
        public async Task<IActionResult> DeletePermission(int idvideo)
        {
            var userUid = User.FindFirst("user_id")?.Value;

            if(userUid == null){
                throw new NotFoundException("User not found");
            }

            bool result = await _perService.DelPermissions(idvideo,userUid);

            return Ok("Permission deleted");
  
        }
    
    }
}
