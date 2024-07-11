using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ByteStormBackend.Data;
using ByteStormBackend.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        // GET: api/Mision
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Mision>>> GetMisiones()
        {
            return await _context.Misiones.ToListAsync();
        }

        // GET: api/Mision/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Mision>> GetMision(int id)
        {
            var mision = await _context.Misiones.FindAsync(id);

            if (mision == null)
            {
                return NotFound();
            }

            return mision;
        }

        // PUT: api/Mision/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMision(int id, Mision mision)
        {
            if (id != mision.ID)
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
                if (!MisionExists(id))
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

        // POST: api/Mision
        [HttpPost]
        public async Task<ActionResult<Mision>> PostMision(Mision mision)
        {
            _context.Misiones.Add(mision);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMision", new { id = mision.ID }, mision);
        }

        // DELETE: api/Mision/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMision(int id)
        {
            var mision = await _context.Misiones.FindAsync(id);
            if (mision == null)
            {
                return NotFound();
            }

            _context.Misiones.Remove(mision);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MisionExists(int id)
        {
            return _context.Misiones.Any(e => e.ID == id);
        }
    }
}