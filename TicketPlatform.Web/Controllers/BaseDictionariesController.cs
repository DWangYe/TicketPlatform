using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketPlatform.Web.Models;
using TicketPlatform.Web.Repository;

namespace TicketPlatform.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseDictionariesController : ControllerBase
    {
        private readonly TpContext _context;

        public BaseDictionariesController(TpContext context)
        {
            _context = context;
        }

        // GET: api/BaseDictionaries
        [HttpGet]
        public IEnumerable<BaseDictionary> GetBaseDictionaries()
        {
            return _context.BaseDictionaries;
        }

        // GET: api/BaseDictionaries/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBaseDictionary([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var baseDictionary = await _context.BaseDictionaries.FindAsync(id);

            if (baseDictionary == null)
            {
                return NotFound();
            }

            return Ok(baseDictionary);
        }

        // PUT: api/BaseDictionaries/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBaseDictionary([FromRoute] int id, [FromBody] BaseDictionary baseDictionary)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != baseDictionary.ID)
            {
                return BadRequest();
            }

            _context.Entry(baseDictionary).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BaseDictionaryExists(id))
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

        // POST: api/BaseDictionaries
        [HttpPost]
        public async Task<IActionResult> PostBaseDictionary([FromBody] BaseDictionary baseDictionary)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.BaseDictionaries.Add(baseDictionary);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBaseDictionary", new { id = baseDictionary.ID }, baseDictionary);
        }

        // DELETE: api/BaseDictionaries/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBaseDictionary([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var baseDictionary = await _context.BaseDictionaries.FindAsync(id);
            if (baseDictionary == null)
            {
                return NotFound();
            }

            _context.BaseDictionaries.Remove(baseDictionary);
            await _context.SaveChangesAsync();

            return Ok(baseDictionary);
        }

        private bool BaseDictionaryExists(int id)
        {
            return _context.BaseDictionaries.Any(e => e.ID == id);
        }
    }
}