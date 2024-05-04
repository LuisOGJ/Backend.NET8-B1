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

        [HttpPost]
        public async Task<ActionResult<BeerDto>> Add(BeerInsertDto beerInsertDto) {
            var beer = new Beer()
            {
                Name = beerInsertDto.Name,
                BrandID = beerInsertDto.BrandID,
                Alcohol = beerInsertDto.Alcohol
            };

            await _context.Beer.AddAsync(beer); // agrega los datos faltantes a beer y se puede usar lineas abajo
            await _context.SaveChangesAsync(); // para que se representen los datos en la db

            var beerDto = new BeerDto()
            {
                Id = beer.BeerID,
                Name = beer.Name,
                BrandID = beer.BrandID,
                Alcohol = beer.Alcohol,
            };

            return CreatedAtAction(nameof(GetById), new { id = beer.BeerID}, beerDto);
        }

    }
}
