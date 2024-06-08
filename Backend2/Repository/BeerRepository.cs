using Backend2.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend2.Repository
{
    public class BeerRepository : IRepository<Beer>
    {

        private StoreContext _context;

        public BeerRepository(StoreContext context)
        {
            _context = context;
        }

        public Task Add(Beer entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Beer entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Beer>> Get() => await _context.Beer.ToListAsync();

        public async Task<Beer> GetById(int id) => await _context.Beer.FindAsync(id);

        public Task Save()
        {
            throw new NotImplementedException();
        }

        public void Update(Beer entity)
        {
            throw new NotImplementedException();
        }
    }
}
