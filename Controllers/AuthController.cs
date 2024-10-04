using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaggerApi.Models;
using TaggerApi.DTOs;
using TaggerApi.Services.Authentication;
using FirebaseAdmin.Auth;

namespace TaggerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly PostgresContext _context;
        private readonly IAuthenticationService _auth;

        public AuthController(PostgresContext context, IAuthenticationService auth)
        {
            _context = context;
            _auth = auth;
        }

        /*
        [HttpGet("{uid}")]
        public async Task<ActionResult<UserRecord>> GetUser(string uid)
        {
           return await _auth.getUser(uid);
        }
        */
       
/*
        // GET: api/User/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUser(long id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return UserToDTO(user);
        }
*/


        // POST: api/User
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<string>> RegisterUser(UserRegisterDTO userDTO)
        {
           return await _auth.RegisterAsync(userDTO);;
        }

    }
}