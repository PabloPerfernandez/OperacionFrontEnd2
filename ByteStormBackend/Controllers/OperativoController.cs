using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ByteStormBackend.Models;
using ByteStormBackend.Data;
using ByteStormBackend.DTO;

namespace ByteStormBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperativoController : ControllerBase
    {
        private readonly ByteStormContext _context;

        public OperativoController(ByteStormContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Operativo>>> GetOperativos()
        {
            return await _context.Operativos.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Operativo>> GetOperativo(int id)
        {
            var operativo = await _context.Operativos.FindAsync(id);

            if (operativo == null)
            {
                return NotFound();
            }

            return operativo;
        }

        [HttpPost("crear")]
        public async Task<ActionResult<Operativo>> CrearOperativo([FromBody]CrearOperativoDTO operativoDTO)
        {
            var operativo = new Operativo
            {
                Nombre = operativoDTO.Nombre,
                Rol = operativoDTO.Rol,
                MisionAsignada = operativoDTO.MisionAsignada,
            };
            _context.Operativos.Add(operativo);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetOperativo), new { id = operativo.ID }, operativo);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutOperativo(int id, [FromBody]ActualizarOperativoDTO operativoDTO)
        {
            var operativo = await _context.Operativos.FindAsync(id);
            if (operativo == null)
            {
                return NotFound();
            }

            operativo.Nombre = operativoDTO.Nombre;
            operativo.Rol = operativoDTO.Rol;
            operativo.MisionAsignada = operativoDTO.MisionAsignada;

            _context.Entry(operativo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OperativoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteOperativo(int id)
        {
            var operativo = await _context.Operativos.FindAsync(id);
            if (operativo == null)
            {
                return NotFound();
            }

            _context.Operativos.Remove(operativo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OperativoExists(int id)
        {
            return _context.Operativos.Any(e => e.ID == id);
        }
    }
}