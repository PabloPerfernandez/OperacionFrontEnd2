using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ByteStormBackend.Models;
using ByteStormBackend.Data;

namespace ByteStormBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipoController : ControllerBase
    {
        private readonly ByteStormContext _context;

        public EquipoController(ByteStormContext context)
        {
            _context = context;
        }

        // Obtener todos los equipos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Equipo>>> GetEquipos()
        {
            return await _context.Equipos.ToListAsync();
        }

        // Obtener un equipo por su código
        [HttpGet("{EquipoCodigo}")]
        public async Task<ActionResult<Equipo>> GetEquipo(int EquipoCodigo)
        {
            var equipo = await _context.Equipos.FindAsync(EquipoCodigo);

            if (equipo == null)
            {
                return NotFound(new { message = $"El equipo con código {EquipoCodigo} no fue encontrado." });
            }

            return equipo;
        }

        // Crear un nuevo equipo
        [HttpPost("crear")]
        public async Task<ActionResult<Equipo>> CrearEquipo(Equipo equipo)
        {
            if (EquipoExists(equipo.EquipoCodigo))
            {
                return Conflict(new { message = $"El equipo con código {equipo.EquipoCodigo} ya existe." });
            }

            _context.Equipos.Add(equipo);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEquipo), new { codigo = equipo.EquipoCodigo }, equipo);
        }

        // Actualizar un equipo existente
        [HttpPut("{EquipoCodigo}")]
        public async Task<IActionResult> PutEquipo(int EquipoCodigo, Equipo equipo)
        {
            if (EquipoCodigo != equipo.EquipoCodigo)
            {
                return BadRequest(new { message = "El código del equipo no coincide con el código proporcionado." });
            }

            _context.Entry(equipo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EquipoExists(EquipoCodigo))
                {
                    return NotFound(new { message = $"El equipo con código {EquipoCodigo} no existe." });
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // Eliminar un equipo por su código
        [HttpDelete("{codigo}")]
        public async Task<IActionResult> DeleteEquipo(int EquipoCodigo)
        {
            var equipo = await _context.Equipos.FindAsync(EquipoCodigo);
            if (equipo == null)
            {
                return NotFound(new { message = $"El equipo con código {EquipoCodigo} no fue encontrado." });
            }

            _context.Equipos.Remove(equipo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Verificar si un equipo existe por su código
        private bool EquipoExists(int codigo)
        {
            return _context.Equipos.Any(e => e.EquipoCodigo == codigo);
        }
    }
}