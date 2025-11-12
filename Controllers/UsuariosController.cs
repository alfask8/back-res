using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Reservas.API.Data;
using Reservas.API.Models;

namespace Reservas.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly ReservasContext _context;
        public UsuariosController(ReservasContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
            => await _context.Usuarios.ToListAsync();

        [HttpGet("{dni}")]
        public async Task<ActionResult<Usuario>> GetUsuario(string dni)
        {
            var usuario = await _context.Usuarios.FindAsync(dni);
            if (usuario == null) return NotFound();
            return usuario;
        }

        [HttpPost]
        public async Task<ActionResult<Usuario>> PostUsuario(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetUsuario), new { dni = usuario.Dni }, usuario);
        }

        [HttpPut("{dni}")]
        public async Task<IActionResult> PutUsuario(string dni, Usuario usuario)
        {
            if (dni != usuario.Dni) return BadRequest();

            _context.Entry(usuario).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{dni}")]
        public async Task<IActionResult> DeleteUsuario(string dni)
        {
            var usuario = await _context.Usuarios.FindAsync(dni);
            if (usuario == null) return NotFound();

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
