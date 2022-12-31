using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SS33Tiers.Api.Data;
using SS33Tiers.Api.Models;

namespace SS33Tiers.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PositionController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PositionController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Position
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Position>>> GetPosition()
        {
            return await _context.Position.ToListAsync();
        }

        // GET: api/Position/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Position>> GetPosition(Guid id)
        {
            var position = await _context.Position.FindAsync(id);

            if (position == null)
            {
                return NotFound();
            }

            return position;
        }
        // GET: api/Position/search/name
        [HttpGet("search/{name}")]
        public async Task<ActionResult<List<Position>>> Search(string name)
        {
            return await _context.Position.Where(o => o.PositionName.Contains(name)).ToListAsync();
        }
        // PUT: api/Position/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPosition(Guid id, Position position)
        {
            if (id != position.PositionId)
            {
                return BadRequest();
            }

            _context.Entry(position).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PositionExists(id))
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

        // POST: api/Position
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Position>> PostPosition(Position position)
        {
            if (ModelState.IsValid)
            {
                if(await IsExist(position.PositionName))
                {
                    return BadRequest("Position already exist");
                }
                _context.Position.Add(position);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetPosition", new { id = position.PositionId }, position);
            }
            return BadRequest(ModelState);
        }
        private async Task<bool> IsExist(string name)
            => await _context.Position.AnyAsync(x => x.PositionName.Equals(name));
        // DELETE: api/Position/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePosition(Guid id)
        {
            var position = await _context.Position.FindAsync(id);
            if (position == null)
            {
                return NotFound();
            }

            _context.Position.Remove(position);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PositionExists(Guid id)
        {
            return _context.Position.Any(e => e.PositionId == id);
        }
    }
}
