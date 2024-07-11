using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ByteStormBackend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using ByteStormBackend.Data;

[Route("api/[controller]")]
[ApiController]
public class OperativoController : ControllerBase
{
    private readonly ByteStormContext _context;

    public OperativoController(ByteStormContext context)
    {
        _context = context;
    }

    // GET: api/Operativo
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Operativo>>> GetOperativos()
    {
        return await _context.Operativos.ToListAsync();
    }

    // GET: api/Operativo/5
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

    // POST: api/Operativo
    [HttpPost]
    public async Task<ActionResult<Operativo>> PostOperativo(Operativo operativo)
    {
        _context.Operativos.Add(operativo);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetOperativo), new { id = operativo.ID }, operativo);
    }

    // PUT: api/Operativo/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutOperativo(int id, Operativo operativo)
    {
        if (id != operativo.ID)
        {
            return BadRequest();
        }

        _context.Entry(operativo).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/Operativo/5
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
} 