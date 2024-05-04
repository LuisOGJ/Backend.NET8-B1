using Backend2.DTOs;
using Backend2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeerController : ControllerBase
    {
        public StoreContext _context;

        public BeerController(StoreContext context)
        {
            _context = context; 
        }

        [HttpGet]
        public async Task<IEnumerable<BeerDto>> Get() => await _context.Beer.Select(b => new BeerDto 
            { 
                Id = b.BeerID,
                Name = b.Name,
                Alcohol = b.Alcohol,
                BrandID = b.BrandID,
            }).ToListAsync();


        [HttpGet("{id}")]
        public async Task<ActionResult<BeerDto>> GetById(int id) {
            var beer = await _context.Beer.FindAsync(id);
            if (beer == null) { 
                return NotFound();
            }
            var beerDto = new BeerDto
            {
                Id = beer.BeerID,
                Name = beer.Name,
                Alcohol = beer.Alcohol,
                BrandID = beer.BrandID,
            };
            return Ok(beerDto);
        }
    }
}
