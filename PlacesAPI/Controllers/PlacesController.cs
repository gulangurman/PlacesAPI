using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlacesAPI.Models;

namespace PlacesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlacesController : ControllerBase
    {
        private readonly PlaceContext _context;

        public PlacesController(PlaceContext context)
        {
            _context = context;
        }

        // GET: api/Places
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlaceDTO>>> GetPlaces()
        {            
            return await _context.Places.Select(x=>PlaceToDTO(x)).ToListAsync();
        }

        // GET: api/Places/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PlaceDTO>> GetPlace(long id)
        {
            var place = await _context.Places.FindAsync(id);

            if (place == null)
            {
                return NotFound();
            }

            return PlaceToDTO(place);
        }

        // PUT: api/Places/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePlace(long id, PlaceDTO placeDTO)
        {
            if (id != placeDTO.Id)
            {
                return BadRequest();
            }
            var place = await _context.Places.FindAsync(id);
            if (place == null)
            {
                return NotFound();
            }
            place.Name = placeDTO.Name;
            place.IsOpen = placeDTO.IsOpen;
            place.Tags = placeDTO.Tags;           

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!PlaceExists(id))
            {
                return NotFound();               
            }

            return NoContent();
        }

        // POST: api/Places
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PlaceDTO>> CreatePlace(PlaceDTO placeDTO)
        {            
            var place = new Place
            {
                IsOpen = placeDTO.IsOpen,
                Name = placeDTO.Name,
                Tags = placeDTO.Tags
            };
            _context.Places.Add(place);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPlace), new { id = place.Id }, PlaceToDTO(place));
        }

        // DELETE: api/Places/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlace(long id)
        {
            var place = await _context.Places.FindAsync(id);
            if (place == null)
            {
                return NotFound();
            }

            _context.Places.Remove(place);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PlaceExists(long id)
        {
            return _context.Places.Any(e => e.Id == id);
        }
        private static PlaceDTO PlaceToDTO(Place place) =>
            new PlaceDTO
            {
                Id = place.Id,
                Name = place.Name,
                IsOpen = place.IsOpen,
                Tags = place.Tags
            };
    }
}
