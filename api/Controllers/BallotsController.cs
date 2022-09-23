using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using election.Data;
using shared;

namespace election.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BallotsController : ControllerBase
    {
        private readonly InstantRunoffContext _context;
        public BallotsController(InstantRunoffContext context)
        {
            _context = context;
        }
        //Get: api?Ballots
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ballot>>> GetBallots()
        {
            if (_context.Ballots == null)
            {
                return NotFound();
            }
            return await _context.Ballots.ToListAsync();
        }

        //Get:api/Ballots/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ballot>> GetBallot(int id)
        {
            if (_context.Ballots == null)
            {
                return NotFound();
            }
            var ballot = await _context.Ballots.FindAsync(id);
            if(ballot == null)
            {
                return NotFound();
            }

            return ballot;
        }

        //Put: api/Ballots/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBallot(int id, Ballot ballot)
        {
            if (id != ballot.Id)
            {
                return BadRequest();
            }
            _context.Entry(ballot).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if(!BallotExists(id))
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
        //Post: api/Ballots
        [HttpPost]
        public async Task<ActionResult<Ballot>> PostBallot(Ballot ballot)
        {
            if(_context.Ballots ==null)
            {
                return Problem("Entity set 'InstantRunoffContext.Ballots' is null.");
            }
            _context.Ballots.Add(ballot);
            await _context.SaveChangesAsync();
            
            return CreatedAtAction("GetBallot", new { id = ballot.Id }, ballot);
        }

        //DELETE: api/Ballots/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBallot(int id)
        {
            if (_context.Ballots == null)
            {
                return NotFound();
            }
            var ballot = await _context.Ballots.FindAsync(id);
            if (ballot == null)
            {
                return NotFound();
            }

            _context.Ballots.Remove(ballot);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        
        private bool BallotExists(int id)
        {
            return (_context.Ballots?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
