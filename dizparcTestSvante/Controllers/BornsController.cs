using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dizparcTestSvante;

namespace dizparcTestSvante.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BornsController : ControllerBase
    {
        private readonly CSVImportContext _context;

        public BornsController(CSVImportContext context)
        {
            _context = context;
        }

        // GET: api/Borns
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Born>>> GetBorns()
        {
            return await _context.Borns.ToListAsync();
        }

        // GET: api/Borns/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Born>> GetBorn(int id)
        {
            var born = await _context.Borns.FindAsync(id);

            if (born == null)
            {
                return NotFound();
            }

            return born;
        }

        // PUT: api/Borns/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBorn(int id, Born born)
        {
            if (id != born.Id)
            {
                return BadRequest();
            }

            _context.Entry(born).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BornExists(id))
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

        // POST: api/Borns
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Born>> PostBorn(Born born)
        {
            _context.Borns.Add(born);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBorn", new { id = born.Id }, born);
        }

        // DELETE: api/Borns/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBorn(int id)
        {
            var born = await _context.Borns.FindAsync(id);
            if (born == null)
            {
                return NotFound();
            }

            _context.Borns.Remove(born);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BornExists(int id)
        {
            return _context.Borns.Any(e => e.Id == id);
        }
    }
}
