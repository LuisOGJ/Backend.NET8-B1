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

        public async Task Add(Beer beer) => await _context.Beer.AddAsync(beer);

        public void Delete(Beer beer) => _context.Beer.Remove(beer);

        public async Task<IEnumerable<Beer>> Get() => await _context.Beer.ToListAsync();

        public async Task<Beer> GetById(int id) => await _context.Beer.FindAsync(id);

        public async Task Save() => await _context.SaveChangesAsync();

        public void Update(Beer beer) {
            _context.Beer.Attach(beer);
            _context.Beer.Entry(beer).State = EntityState.Modified;
        }
    }
}
