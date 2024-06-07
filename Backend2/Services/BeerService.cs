using Backend2.DTOs;
using Backend2.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend2.Services
{
    public class BeerService : IBeerService
    {

        private StoreContext _context;

        public BeerService(StoreContext storeContext)
        {
            _context = storeContext;
        }

        public async Task<BeerDto> Add(BeerInsertDto beerInsertDto)
        {
            // SAVE IN A OBJECT BEER
            var beer = new Beer()
            {
                Name = beerInsertDto.Name,
                BrandID = beerInsertDto.BrandID,
                Alcohol = beerInsertDto.Alcohol
            };

            // ADDING TO DB
            await _context.Beer.AddAsync(beer); // agrega los datos faltantes a beer y se puede usar lineas abajo
            await _context.SaveChangesAsync(); // para que se representen los datos en la db

            // USING DTO FOR RETURN DATA
            var beerDto = new BeerDto()
            {
                Id = beer.BeerID,
                Name = beer.Name,
                BrandID = beer.BrandID,
                Alcohol = beer.Alcohol,
            };

            return beerDto;
        }

        public async Task<BeerDto> Delete(int id)
        {
            var beer = await _context.Beer.FindAsync(id);

            if (beer == null) {
                return null;
            }

            var beerDto = new BeerDto()
            {
                Id = beer.BeerID,
                Name = beer.Name,
                BrandID = beer.BrandID,
                Alcohol = beer.Alcohol,
            };

            _context.Beer.Remove(beer);
            await _context.SaveChangesAsync();

            return beerDto;

        }

        public async Task<IEnumerable<BeerDto>> Get() => await _context.Beer.Select(b => new BeerDto
        {
            Id = b.BeerID,
            Name = b.Name,
            Alcohol = b.Alcohol,
            BrandID = b.BrandID,
        }).ToListAsync();


        public async Task<BeerDto> Update(int id, BeerUpdateDto beerUpdateDto)
        {
            var beer = await _context.Beer.FindAsync(id);

            if (beer == null)
            {
                return null;
            }

            beer.Name = beerUpdateDto.Name;
            beer.Alcohol = beerUpdateDto.Alcohol;
            beer.BrandID = beerUpdateDto.BrandID;
            await _context.SaveChangesAsync();

            var beerDto = new BeerDto()
            {
                Id = beer.BeerID,
                Name = beer.Name,
                BrandID = beer.BrandID,
                Alcohol = beer.Alcohol,
            };

            return beerDto;

        }

        public async Task<BeerDto> GetById(int id)
        {
            var beer = await _context.Beer.FindAsync(id);
            if (beer != null)
            {
                var beerDto = new BeerDto
                {
                    Id = beer.BeerID,
                    Name = beer.Name,
                    Alcohol = beer.Alcohol,
                    BrandID = beer.BrandID,
                };
                return beerDto;
            }
            return null;
        }

    }
}
