using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ByteStormBackend.Models;
using ByteStormBackend.Data;

namespace ByteStormBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MisionController : ControllerBase
    {
        private readonly ByteStormContext _context;

        public MisionController(ByteStormContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Mision>>> GetMisiones()
        {
            return await _context.Misiones.ToListAsync();
        }

        [HttpGet("{codigo}")]
        public async Task<ActionResult<Mision>> GetMision(int codigo)
        {
            var mision = await _context.Misiones.FindAsync(codigo);

            if (mision == null)
            {
                return NotFound();
            }

            return mision;
        }

        [HttpPost("crear")]
        public async Task<ActionResult<Mision>> CrearMision(Mision mision)
        {
            _context.Misiones.Add(mision);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMision), new { codigo = mision.codigo }, mision);
        }

        [HttpPut("{codigo}")]
        public async Task<IActionResult> PutMision(int codigo, Mision mision)
        {
            if (codigo != mision.codigo)
            {
                return BadRequest();
            }

            _context.Entry(mision).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MisionExists(codigo))
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
        public async Task<IActionResult> DeleteMision(int codigo)
        {
            var mision = await _context.Misiones.FindAsync(codigo);
            if (mision == null)
            {
                return NotFound();
            }

            _context.Misiones.Remove(mision);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MisionExists(int codigo)
        {
            return _context.Misiones.Any(e => e.codigo == codigo);
        }
    }
}