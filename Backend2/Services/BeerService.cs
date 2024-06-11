using Backend2.DTOs;
using Backend2.Models;
using Backend2.Repository;
using Microsoft.EntityFrameworkCore;

namespace Backend2.Services
{
    public class BeerService : ICommonService<BeerDto, BeerInsertDto, BeerUpdateDto>
    {

        private StoreContext _context;
        private IRepository<Beer> _beerRepository;

        public BeerService(StoreContext storeContext, IRepository<Beer> beerRepository)
        {
            _context = storeContext;
            _beerRepository = beerRepository;
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

            // ADDING TO DB with repository
            await _beerRepository.Add(beer); // agrega los datos faltantes a beer y se puede usar lineas abajo
            await _beerRepository.Save(); // para que se representen los datos en la db

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
            var beer = await _beerRepository.GetById(id);

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

            _beerRepository.Delete(beer);
            await _beerRepository.Save();

            return beerDto;

        }

        public async Task<IEnumerable<BeerDto>> Get() {
            var beers = await _beerRepository.Get();
            return beers.Select(beer => new BeerDto() { 
                Id = beer.BeerID, 
                Name = beer.Name, 
                BrandID = beer.BrandID,
                Alcohol= beer.Alcohol,
            });

        }


        public async Task<BeerDto> Update(int id, BeerUpdateDto beerUpdateDto)
        {
            var beer = await _beerRepository.GetById(id);

            if (beer == null)
            {
                return null;
            }

            beer.Name = beerUpdateDto.Name;
            beer.Alcohol = beerUpdateDto.Alcohol;
            beer.BrandID = beerUpdateDto.BrandID;

            _beerRepository.Update(beer);
            await _beerRepository.Save();

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
            var beer = await _beerRepository.GetById(id);
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
