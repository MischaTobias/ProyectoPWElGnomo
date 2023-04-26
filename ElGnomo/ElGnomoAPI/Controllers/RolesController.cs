﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ElGnomoAPI.GnomoDbContext;
using ElGnomoAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace ElGnomoAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class RolesController : ControllerBase
{
    private readonly ElgnomoContext _context;

    public RolesController(ElgnomoContext context)
    {
        _context = context;
    }

    // GET: api/Roles
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Role>>> GetRoles()
    {
      if (_context.Roles == null)
      {
          return NotFound();
      }
        return await _context.Roles.ToListAsync();
    }

    // GET: api/Roles/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Role>> GetRole(int id)
    {
      if (_context.Roles == null)
      {
          return NotFound();
      }
        var role = await _context.Roles.FindAsync(id);

        if (role == null)
        {
            return NotFound();
        }

        return role;
    }

    // PUT: api/Roles/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutRole(int id, Role role)
    {
        if (id != role.Id)
        {
            return BadRequest();
        }

        _context.Entry(role).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!RoleExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // POST: api/Roles
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Role>> PostRole(Role role)
    {
      if (_context.Roles == null)
      {
          return Problem("Entity set 'ElgnomoContext.Roles'  is null.");
      }
        _context.Roles.Add(role);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetRole", new { id = role.Id }, role);
    }

    // DELETE: api/Roles/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRole(int id)
    {
        if (_context.Roles == null)
        {
            return NotFound();
        }
        var role = await _context.Roles.FindAsync(id);
        if (role == null)
        {
            return NotFound();
        }

        _context.Roles.Remove(role);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool RoleExists(int id)
    {
        return (_context.Roles?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}
