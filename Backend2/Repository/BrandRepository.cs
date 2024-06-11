using Backend2.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend2.Repository
{
    public class BrandRepository : IRepository<Brand>
    {

        private StoreContext _context;

        public BrandRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task Add(Brand brand) => await _context.Brand.AddAsync(brand);

        public void Delete(Brand brand) => _context.Brand.Remove(brand);

        public async Task<IEnumerable<Brand>> Get() => await _context.Brand.ToListAsync();

        public async Task<Brand> GetById(int id) => await _context.Brand.FindAsync(id);

        public async Task Save() => await _context.SaveChangesAsync();

        public void Update(Brand brand)
        {
            _context.Brand.Attach(brand);
            _context.Brand.Entry(brand).State = EntityState.Modified;
        }
    }
}
