using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ElGnomoAPI.GnomoDbContext;
using ElGnomoAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace ElGnomoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class RoleUsersController : ControllerBase
    {
        private readonly ElgnomoContext _context;

        public RoleUsersController(ElgnomoContext context)
        {
            _context = context;
        }

        // GET: api/RoleUsers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoleUser>>> GetRoleUsers()
        {
          if (_context.RoleUsers == null)
          {
              return NotFound();
          }
            return await _context.RoleUsers.ToListAsync();
        }

        // GET: api/RoleUsers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RoleUser>> GetRoleUser(int id)
        {
          if (_context.RoleUsers == null)
          {
              return NotFound();
          }
            var roleUser = await _context.RoleUsers.FindAsync(id);

            if (roleUser == null)
            {
                return NotFound();
            }

            return roleUser;
        }

        // PUT: api/RoleUsers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoleUser(int id, RoleUser roleUser)
        {
            if (id != roleUser.Id)
            {
                return BadRequest();
            }

            _context.Entry(roleUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoleUserExists(id))
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

        // POST: api/RoleUsers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RoleUser>> PostRoleUser(RoleUser roleUser)
        {
          if (_context.RoleUsers == null)
          {
              return Problem("Entity set 'ElgnomoContext.RoleUsers'  is null.");
          }
            _context.RoleUsers.Add(roleUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRoleUser", new { id = roleUser.Id }, roleUser);
        }

        // DELETE: api/RoleUsers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoleUser(int id)
        {
            if (_context.RoleUsers == null)
            {
                return NotFound();
            }
            var roleUser = await _context.RoleUsers.FindAsync(id);
            if (roleUser == null)
            {
                return NotFound();
            }

            _context.RoleUsers.Remove(roleUser);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RoleUserExists(int id)
        {
            return (_context.RoleUsers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
