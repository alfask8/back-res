using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Reservas.API.Data;
using Reservas.API.Models;

namespace Reservas.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class ReservasController : ControllerBase
	{
		private readonly ReservasContext _context;
		public ReservasController(ReservasContext context) => _context = context;

		[HttpGet]
		public async Task<ActionResult<IEnumerable<Reserva>>> GetReservas()
			=> await _context.Reservas.ToListAsync();

		[HttpGet("{id}")]
		public async Task<ActionResult<Reserva>> GetReserva(int id)
		{
			var reserva = await _context.Reservas.FindAsync(id);
			if (reserva == null) return NotFound();
			return reserva;
		}

		[HttpPost]
		public async Task<ActionResult<Reserva>> PostReserva(Reserva reserva)
		{
			_context.Reservas.Add(reserva);
			await _context.SaveChangesAsync();
			return CreatedAtAction(nameof(GetReserva), new { id = reserva.IdReserva }, reserva);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> PutReserva(int id, Reserva reserva)
		{
			if (id != reserva.IdReserva) return BadRequest();

			_context.Entry(reserva).State = EntityState.Modified;
			await _context.SaveChangesAsync();
			return NoContent();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteReserva(int id)
		{
			var reserva = await _context.Reservas.FindAsync(id);
			if (reserva == null) return NotFound();

			_context.Reservas.Remove(reserva);
			await _context.SaveChangesAsync();
			return NoContent();
		}
	}
}