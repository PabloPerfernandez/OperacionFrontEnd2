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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Equipo>>> GetEquipos()
        {
            try
            {
                var equipos = await _context.Equipos.ToListAsync();

                if (equipos == null || equipos.Count == 0)
                {
                    return NoContent();  // Puedes devolver un 204 si no hay equipos
                }

                return Ok(equipos);
            }
            catch (Exception ex)
            {
                // Registrar el error
                Console.WriteLine($"Error al obtener los equipos: {ex.Message}");
                return StatusCode(500, new { message = "Error al obtener los equipos." });
            }
        }

        [HttpGet("{equipoCodigo}")]
        public async Task<ActionResult<Equipo>> GetEquipo(int equipoCodigo)
        {
            var equipo = await _context.Equipos.FindAsync(equipoCodigo);

            if (equipo == null)
            {
                return NotFound(new { message = $"El equipo con código {equipoCodigo} no fue encontrado." });
            }

            return equipo;
        }

        [HttpPost("crear")]
        public async Task<ActionResult<Equipo>> CrearEquipo([FromBody]CrearEquipoDTO equipoDTO)
        {
            var equipo = new Equipo
            {
                equipoTipo = equipoDTO.equipoTipo,
                equipoDescripcion = equipoDTO.equipoDescripcion,
                EstadoEquipo = equipoDTO.EstadoEquipo,
                MisionID = equipoDTO.MisionID,
            };
            
            if (EquipoExists(equipo.equipoCodigo))
            {
                return Conflict(new { message = $"El equipo con código {equipo.equipoCodigo} ya existe." });
            }

            _context.Equipos.Add(equipo);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEquipo), new { equipoCodigo = equipo.equipoCodigo }, equipo);
        }

        [HttpPut("{equipoCodigo}")]
        public async Task<IActionResult> PutEquipo(int equipoCodigo, Equipo equipo)
        {
            if (equipoCodigo != equipo.equipoCodigo)
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
                if (!EquipoExists(equipoCodigo))
                {
                    return NotFound(new { message = $"El equipo con código {equipoCodigo} no existe." });
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteEquipo(int equipoCodigo)
        {
            var equipo = await _context.Equipos.FindAsync(equipoCodigo);
            if (equipo == null)
            {
                return NotFound(new { message = $"El equipo con código {equipoCodigo} no fue encontrado." });
            }

            _context.Equipos.Remove(equipo);
            await _context.SaveChangesAsync();

            return NoContent();
        }




        private bool EquipoExists(int equipoCodigo)
        {
            return _context.Equipos.Any(e => e.equipoCodigo == equipoCodigo);
        }

    }
}