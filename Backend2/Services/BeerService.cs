using Backend2.DTOs;

namespace Backend2.Services
{
    public class BeerService : IBeerService
    {
        Task<BeerDto> IBeerService.Add(BeerInsertDto beerInsertDto)
        {
            throw new NotImplementedException();
        }

        Task<BeerDto> IBeerService.Delete(int id)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<BeerDto>> IBeerService.Get()
        {
            throw new NotImplementedException();
        }

        Task<BeerDto> IBeerService.Get(int id)
        {
            throw new NotImplementedException();
        }

        Task<BeerDto> IBeerService.Update(int id, BeerUpdateDto beerUpdateDto)
        {
            throw new NotImplementedException();
        }
    }
}
