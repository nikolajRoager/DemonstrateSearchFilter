using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DemonstrateSearchFilter.Models;
using DemonstrateSearchFilter.Services;

namespace DemonstrateSearchFilter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoxesController : ControllerBase
    {
        private readonly BoxContext _context;
        private readonly IBoxService _boxService;

        public BoxesController(BoxContext context,IBoxService boxService)
        {
            _context = context;
            _boxService = boxService;
        }

        // GET: api/Boxes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Box>>> GetBoxes()
        {
            return await _context.Boxes.ToListAsync();
        }

        //Search boxes
        // GET: api/Boxes/getBoxes?BoxQuery[0].key=Color&BoxQuery[0].value=Red&...
        [HttpGet("Search")]
        public async Task<ActionResult<IEnumerable<Box>>> GetBoxesQuery([FromQuery] Dictionary<string,string> query)
        {
            Console.WriteLine("Received Query");
            foreach (var boxQuery in query)
            {
                Console.WriteLine($"{boxQuery.Key} : {boxQuery.Value}");
            }
            
            var boxes = await  _boxService.GetBoxesByKeyValuesAsync(query);

            if (boxes == null)
                return NotFound();
            else
                return Ok(boxes);
        }

        // GET: api/Boxes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Box>> GetBox(long id)
        {
            var box = await _context.Boxes.FindAsync(id);

            if (box == null)
            {
                return NotFound();
            }

            return box;
        }

        // PUT: api/Boxes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBox(long id, Box box)
        {
            if (id != box.Id)
            {
                return BadRequest();
            }

            _context.Entry(box).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BoxExists(id))
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

        // POST: api/Boxes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Box>> PostBox(Box box)
        {
            _context.Boxes.Add(box);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBox), new { id = box.Id }, box);
        }

        // DELETE: api/Boxes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBox(long id)
        {
            var box = await _context.Boxes.FindAsync(id);
            if (box == null)
            {
                return NotFound();
            }

            _context.Boxes.Remove(box);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BoxExists(long id)
        {
            return _context.Boxes.Any(e => e.Id == id);
        }
    }
}
