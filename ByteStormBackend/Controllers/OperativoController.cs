using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ByteStormBackend.Models;
using ByteStormBackend.Data;

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

        [HttpPost]
        public async Task<ActionResult<Operativo>> PostOperativo(Operativo operativo)
        {
            _context.Operativos.Add(operativo);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetOperativo), new { id = operativo.ID }, operativo);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutOperativo(int id, Operativo operativo)
        {
            if (id != operativo.ID)
            {
                return BadRequest();
            }

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

        [HttpDelete("{id}")]
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