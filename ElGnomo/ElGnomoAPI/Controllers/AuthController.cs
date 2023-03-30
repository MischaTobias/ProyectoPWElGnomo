using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ElGnomoAPI.GnomoDbContext;
using ElGnomoAPI.Models;

namespace ElGnomoAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{

    [HttpPost("register")]
    public async Task<bool> Register(User user)
    {
        return true;
    }

    [HttpPost("login")]
    public async Task<bool> Login(User user)
    {
        //if (_context.Users == null)
        //{
        //    return Problem("Entity set 'ElgnomoContext.Users'  is null.");
        //}
        //_context.Users.Add(user);
        //await _context.SaveChangesAsync();

        //return CreatedAtAction("GetUser", new { id = user.Id }, user);
        return true;
    }
}
