using AutoMapper;
using Backend2.DTOs;
using Backend2.Models;
using Backend2.Repository;
using Microsoft.EntityFrameworkCore;

namespace Backend2.Services
{
    public class BeerService : ICommonService<BeerDto, BeerInsertDto, BeerUpdateDto>
    {

        private IRepository<Beer> _beerRepository;
        private IMapper _mapper; // utiliza el maper inyectado

        public BeerService(IRepository<Beer> beerRepository, IMapper mapper)
        {
            _beerRepository = beerRepository;
            _mapper = mapper;
        }

        public async Task<BeerDto> Add(BeerInsertDto beerInsertDto)
        {
            // SAVE IN A OBJECT BEER
            var beer = _mapper.Map<Beer>(beerInsertDto);

            // ADDING TO DB with repository
            await _beerRepository.Add(beer); // agrega los datos faltantes a beer y se puede usar lineas abajo
            await _beerRepository.Save(); // para que se representen los datos en la db

            // USING DTO FOR RETURN DATA
            var beerDto = _mapper.Map<BeerDto>(beer);

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
            return beers.Select(beer => _mapper.Map<BeerDto>(beer));

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

            var beerDto = _mapper.Map<BeerDto>(beer);

            return beerDto;

        }

        public async Task<BeerDto> GetById(int id)
        {
            var beer = await _beerRepository.GetById(id);
            if (beer != null)
            {
                var beerDto = _mapper.Map<BeerDto>(beer);
                return beerDto;
            }
            return null;
        }

    }
}
