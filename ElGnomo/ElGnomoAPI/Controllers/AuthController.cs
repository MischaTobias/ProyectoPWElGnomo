using Microsoft.AspNetCore.Mvc;
using ElGnomoAPI.GnomoDbContext;
using Microsoft.EntityFrameworkCore;
using ElGnomoModels.ViewModels;
using ElGnomoAPI.Utils;
using ElGnomoAPI.Models;

namespace ElGnomoAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly ElgnomoContext _context;
    private readonly IConfiguration _configuration;

    public AuthController(ElgnomoContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    [HttpPost("register")]
    public async Task<TokenView> Register(UserView user)
    {
        var exists = _context.Users.Where(u => u.Email == user.Email).Any();
        if (exists) return new TokenView();

        User newUser = new()
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            PasswordHash = Cypher.CypherText(user.Password!)
        };

        _context.Users.Add(newUser);
        await _context.SaveChangesAsync();
        return new TokenView() { Token = CustomJWT.GetToken(newUser.Email, _configuration) };
    }

    [HttpPost("login")]
    public async Task<TokenView> Login(UserView user)
    {
        var userInfo = await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Email && u.PasswordHash == Cypher.CypherText(user.Password!));
        if (userInfo == null) return new TokenView();
        return new TokenView() { Token = CustomJWT.GetToken(userInfo.Email, _configuration) };
    }
}
