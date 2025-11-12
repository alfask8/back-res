using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Reservas.API.Data;
using Reservas.API.Models;

namespace Reservas.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificacionesController : ControllerBase
    {
        private readonly ReservasContext _context;
        public NotificacionesController(ReservasContext context) => _context = context;

        // GET: api/notificaciones?dni=12345678A&idReserva=10&soloNoLeidas=true
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Notificacion>>> GetAll([FromQuery] string? dni, [FromQuery] int? idReserva, [FromQuery] bool? soloNoLeidas, CancellationToken ct)
        {
            var q = _context.Notificaciones.AsNoTracking().AsQueryable();
            if (!string.IsNullOrWhiteSpace(dni)) q = q.Where(n => n.DniUsuario == dni); // DNI del usuario receptor. :contentReference[oaicite:12]{index=12}
            if (idReserva is not null) q = q.Where(n => n.IdReserva == idReserva); // FK a Reserva. :contentReference[oaicite:13]{index=13}
            if (soloNoLeidas is true) q = q.Where(n => !n.Leida); // Flag Leida. :contentReference[oaicite:14]{index=14}
            q = q.OrderByDescending(n => n.FechaEnvio); // FechaEnvio con UTC por defecto. :contentReference[oaicite:15]{index=15}
            return Ok(await q.ToListAsync(ct));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Notificacion>> GetById(int id, CancellationToken ct)
        {
            var entity = await _context.Notificaciones.AsNoTracking().FirstOrDefaultAsync(n => n.IdNotificacion == id, ct);
            return entity is null ? NotFound() : Ok(entity);
        }

        [HttpPost]
        public async Task<ActionResult<Notificacion>> Create(Notificacion dto, CancellationToken ct)
        {
            // El modelo ya valida formato de DNI (RegularExpression) y longitudes. :contentReference[oaicite:16]{index=16}
            _context.Notificaciones.Add(dto);
            await _context.SaveChangesAsync(ct);
            return CreatedAtAction(nameof(GetById), new { id = dto.IdNotificacion }, dto);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, Notificacion dto, CancellationToken ct)
        {
            if (id != dto.IdNotificacion) return BadRequest();
            _context.Entry(dto).State = EntityState.Modified;
            await _context.SaveChangesAsync(ct);
            return NoContent();
        }

        // PATCH: api/notificaciones/5/marcar-leida
        [HttpPatch("{id:int}/marcar-leida")]
        public async Task<IActionResult> MarcarLeida(int id, CancellationToken ct)
        {
            var entity = await _context.Notificaciones.FirstOrDefaultAsync(n => n.IdNotificacion == id, ct);
            if (entity is null) return NotFound();
            if (!entity.Leida) entity.Leida = true;
            await _context.SaveChangesAsync(ct);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id, CancellationToken ct)
        {
            var entity = await _context.Notificaciones.FindAsync(new object?[] { id }, ct);
            if (entity is null) return NotFound();
            _context.Notificaciones.Remove(entity);
            await _context.SaveChangesAsync(ct);
            return NoContent();
        }
    }
}
