using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Reservas.API.Data;
using Reservas.API.Models;

namespace Reservas.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HistorialReservasController : ControllerBase
    {
        private readonly ReservasContext _context;
        public HistorialReservasController(ReservasContext context) => _context = context;

        // GET: api/historialreservas?reservaId=10&estadoId=2
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HistorialReserva>>> GetAll([FromQuery] int? reservaId, [FromQuery] int? estadoId, CancellationToken ct)
        {
            var q = _context.HistorialReservas.AsNoTracking().AsQueryable();
            if (reservaId is not null) q = q.Where(h => h.IdReserva == reservaId); // Campo en modelo. :contentReference[oaicite:7]{index=7}
            if (estadoId is not null) q = q.Where(h => h.IdEstado == estadoId);   // Campo en modelo. :contentReference[oaicite:8]{index=8}
            q = q.OrderByDescending(h => h.FechaCambio);                           // FechaCambio con UTC por defecto. :contentReference[oaicite:9]{index=9}
            return Ok(await q.ToListAsync(ct));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<HistorialReserva>> GetById(int id, CancellationToken ct)
        {
            var entity = await _context.HistorialReservas.AsNoTracking().FirstOrDefaultAsync(h => h.IdHistorial == id, ct);
            return entity is null ? NotFound() : Ok(entity);
        }

        [HttpPost]
        public async Task<ActionResult<HistorialReserva>> Create(HistorialReserva dto, CancellationToken ct)
        {
            // server-side default de FechaCambio ya lo aporta el modelo. :contentReference[oaicite:10]{index=10}
            _context.HistorialReservas.Add(dto);
            await _context.SaveChangesAsync(ct);
            return CreatedAtAction(nameof(GetById), new { id = dto.IdHistorial }, dto);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, HistorialReserva dto, CancellationToken ct)
        {
            if (id != dto.IdHistorial) return BadRequest();
            _context.Entry(dto).State = EntityState.Modified;
            await _context.SaveChangesAsync(ct);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id, CancellationToken ct)
        {
            var entity = await _context.HistorialReservas.FindAsync(new object?[] { id }, ct);
            if (entity is null) return NotFound();
            _context.HistorialReservas.Remove(entity);
            await _context.SaveChangesAsync(ct);
            return NoContent();
        }
    }
}
