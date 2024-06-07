using Backend2.DTOs;

namespace Backend2.Services
{
    public interface ICommonService<T, TI, TU>
    {

        Task<IEnumerable<T>> Get();
        Task<T> GetById(int id);
        Task<T> Add(TI beerInsertDto);
        Task<T> Update(int id, TU beerUpdateDto);
        Task<T> Delete(int id);
    }
}
