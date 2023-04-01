using Microsoft.AspNetCore.Mvc;
using ElGnomoAPI.Models;
using ElGnomoAPI.GnomoDbContext;
using Microsoft.EntityFrameworkCore;

namespace ElGnomoAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly ElgnomoContext _context;

    public AuthController(ElgnomoContext context)
    {
        _context = context;
    }

    [HttpPost("register")]
    public async Task<bool> Register(User user)
    {
        var exists = _context.Users.Where(u => u.Email == user.Email).Any();
        if (exists) return false;

        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return true;
    }

    [HttpPost("login")]
    public async Task<bool> Login(User user)
    {
        var userInfo = await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Email && u.PasswordHash == user.PasswordHash);
        if (userInfo == null) return false;
        return true;
    }
}
