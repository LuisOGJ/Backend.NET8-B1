using Backend2.DTOs;

namespace Backend2.Services
{
    public interface ICommonService<T, TI, TU>
    {

        public List<string> Errors { get; }

        Task<IEnumerable<T>> Get();
        Task<T> GetById(int id);
        Task<T> Add(TI ti);
        Task<T> Update(int id, TU tu);
        Task<T> Delete(int id);
        bool Validate(TI dto);
        bool Validate(TU dto);
    }
}
