using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Reservas.API.Data;
using Reservas.API.Models;

namespace Reservas.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RolesController : ControllerBase
    {
        private readonly ReservasContext _context;
        public RolesController(ReservasContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rol>>> GetAll(CancellationToken ct)
            => Ok(await _context.Roles.AsNoTracking().OrderBy(r => r.IdRol).ToListAsync(ct)); // IdRol y Nombre. :contentReference[oaicite:11]{index=11}

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Rol>> GetById(int id, CancellationToken ct)
        {
            var entity = await _context.Roles.AsNoTracking().FirstOrDefaultAsync(r => r.IdRol == id, ct);
            return entity is null ? NotFound() : Ok(entity);
        }

        [HttpPost]
        public async Task<ActionResult<Rol>> Create(Rol dto, CancellationToken ct)
        {
            _context.Roles.Add(dto);
            await _context.SaveChangesAsync(ct);
            return CreatedAtAction(nameof(GetById), new { id = dto.IdRol }, dto);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, Rol dto, CancellationToken ct)
        {
            if (id != dto.IdRol) return BadRequest();
            _context.Entry(dto).State = EntityState.Modified;
            await _context.SaveChangesAsync(ct);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id, CancellationToken ct)
        {
            var entity = await _context.Roles.FindAsync(new object?[] { id }, ct);
            if (entity is null) return NotFound();
            _context.Roles.Remove(entity);
            await _context.SaveChangesAsync(ct);
            return NoContent();
        }
    }
}
