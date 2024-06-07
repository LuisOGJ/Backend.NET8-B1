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

        public Task<BeerDto> Add(BeerInsertDto beerInsertDto)
        {
            throw new NotImplementedException();
        }

        public Task<BeerDto> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<BeerDto>> Get() => await _context.Beer.Select(b => new BeerDto
        {
            Id = b.BeerID,
            Name = b.Name,
            Alcohol = b.Alcohol,
            BrandID = b.BrandID,
        }).ToListAsync();


        public Task<BeerDto> Update(int id, BeerUpdateDto beerUpdateDto)
        {
            throw new NotImplementedException();
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
