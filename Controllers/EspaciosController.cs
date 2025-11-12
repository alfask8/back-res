using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Reservas.API.Data;
using Reservas.API.Models;

namespace Reservas.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EspaciosController : ControllerBase
    {
        private readonly ReservasContext _context;
        public EspaciosController(ReservasContext context) => _context = context;

        // GET: api/espacios?skip=0&take=50&soloDisponibles=true
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Espacio>>> GetAll([FromQuery] int skip = 0, [FromQuery] int take = 50, [FromQuery] bool? soloDisponibles = null, CancellationToken ct = default)
        {
            var q = _context.Espacios.AsNoTracking().AsQueryable();
            if (soloDisponibles is true) q = q.Where(e => e.Disponible); // Disponible por defecto en el modelo. :contentReference[oaicite:5]{index=5}
            var total = await q.CountAsync(ct);
            var data = await q.OrderBy(e => e.IdEspacio).Skip(skip).Take(take).ToListAsync(ct);
            Response.Headers["X-Total-Count"] = total.ToString();
            return Ok(data);
        }

        // GET: api/espacios/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Espacio>> GetById(int id, CancellationToken ct)
        {
            var entity = await _context.Espacios.AsNoTracking().FirstOrDefaultAsync(e => e.IdEspacio == id, ct);
            return entity is null ? NotFound() : Ok(entity);
        }

        // POST: api/espacios
        [HttpPost]
        public async Task<ActionResult<Espacio>> Create(Espacio dto, CancellationToken ct)
        {
            _context.Espacios.Add(dto);
            await _context.SaveChangesAsync(ct);
            return CreatedAtAction(nameof(GetById), new { id = dto.IdEspacio }, dto);
        }

        // PUT: api/espacios/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, Espacio dto, CancellationToken ct)
        {
            if (id != dto.IdEspacio) return BadRequest("El id de la ruta no coincide con el del cuerpo.");
            _context.Entry(dto).State = EntityState.Modified;
            await _context.SaveChangesAsync(ct);
            return NoContent();
        }

        // DELETE: api/espacios/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id, CancellationToken ct)
        {
            var entity = await _context.Espacios.FindAsync(new object?[] { id }, ct);
            if (entity is null) return NotFound();
            _context.Espacios.Remove(entity);
            await _context.SaveChangesAsync(ct);
            return NoContent();
        }
    }
}
