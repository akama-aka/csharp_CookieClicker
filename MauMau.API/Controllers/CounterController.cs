using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MauMau.API.Models;
/*
 *  (C) by Akama Aka
 *  LICENSE: ASPL 1.0 | https://licenses.akami-solutions.cc/
 *
 */
namespace MauMau.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CounterController : ControllerBase
    {
        private readonly CounterContext _context;

        public CounterController(CounterContext context)
        {
            _context = context;
        }

        // GET: api/Counter
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Counter>>> GetCounters()
        {
            return await _context.Counters.ToListAsync();
        }

        // GET: api/Counter/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Counter>> GetCounter(long id)
        {
            var counter = await _context.Counters.FindAsync(id);

            if (counter == null)
            {
                return NotFound();
            }

            return counter;
        }

        // PUT: api/Counter/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCounter(long id, Counter counter)
        {
            if (id != counter.Id)
            {
                return BadRequest();
            }

            _context.Entry(counter).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CounterExists(id))
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

        // POST: api/Counter
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Counter>> PostCounter(Counter counter)
        {
            _context.Counters.Add(counter);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCounter", new { id = counter.Id }, counter);
        }

        // DELETE: api/Counter/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCounter(long id)
        {
            var counter = await _context.Counters.FindAsync(id);
            if (counter == null)
            {
                return NotFound();
            }

            _context.Counters.Remove(counter);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CounterExists(long id)
        {
            return _context.Counters.Any(e => e.Id == id);
        }
    }
}
