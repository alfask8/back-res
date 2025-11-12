using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Reservas.API.Data;
using Reservas.API.Models;

namespace Reservas.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EstadosReservaController : ControllerBase
    {
        private readonly ReservasContext _context;
        public EstadosReservaController(ReservasContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EstadoReserva>>> GetAll(CancellationToken ct)
            => Ok(await _context.EstadosReserva.AsNoTracking().OrderBy(e => e.IdEstado).ToListAsync(ct)); // Nombre p.ej. "Pendiente", "Confirmada", "Cancelada". :contentReference[oaicite:6]{index=6}

        [HttpGet("{id:int}")]
        public async Task<ActionResult<EstadoReserva>> GetById(int id, CancellationToken ct)
        {
            var entity = await _context.EstadosReserva.AsNoTracking().FirstOrDefaultAsync(e => e.IdEstado == id, ct);
            return entity is null ? NotFound() : Ok(entity);
        }

        [HttpPost]
        public async Task<ActionResult<EstadoReserva>> Create(EstadoReserva dto, CancellationToken ct)
        {
            _context.EstadosReserva.Add(dto);
            await _context.SaveChangesAsync(ct);
            return CreatedAtAction(nameof(GetById), new { id = dto.IdEstado }, dto);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, EstadoReserva dto, CancellationToken ct)
        {
            if (id != dto.IdEstado) return BadRequest();
            _context.Entry(dto).State = EntityState.Modified;
            await _context.SaveChangesAsync(ct);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id, CancellationToken ct)
        {
            var entity = await _context.EstadosReserva.FindAsync(new object?[] { id }, ct);
            if (entity is null) return NotFound();
            _context.EstadosReserva.Remove(entity);
            await _context.SaveChangesAsync(ct);
            return NoContent();
        }
    }
}
