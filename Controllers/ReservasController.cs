using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Reservas.Api.Data;
using Reservas.Api.Models;

namespace Reservas.Api.Controllers
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
		public async Task<ActionResult<Reserva>> GetReserva(string id)
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
			return CreatedAtAction(nameof(GetReserva), new { id = reserva.Id }, reserva);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> PutReserva(string id, Reserva reserva)
		{
			if (id != reserva.Id) return BadRequest();

			_context.Entry(reserva).State = EntityState.Modified;
			await _context.SaveChangesAsync();
			return NoContent();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteReserva(string id)
		{
			var reserva = await _context.Reservas.FindAsync(id);
			if (reserva == null) return NotFound();

			_context.Reservas.Remove(reserva);
			await _context.SaveChangesAsync();
			return NoContent();
		}
	}
}