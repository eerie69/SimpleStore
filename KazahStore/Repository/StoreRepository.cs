using KazahStore.Data;
using KazahStore.Data.Enum;
using KazahStore.Interfaces;
using KazahStore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace KazahStore.Repository
{
    public class StoreRepository : IStoreRepository
    {
        private readonly ApplicationDbContext _context;

        public StoreRepository(ApplicationDbContext context) 
        {
            _context = context;
        }

        public bool Add(Store store)
        {
            _context.Add(store);
            return Save();
        }

        public bool Delete(Store store)
        {
            _context.Remove(store);
            return Save();
        }

        public async Task<IEnumerable<Store>> GetAll()
        {
            return await _context.Items.ToListAsync();
            
        }

        public async Task<IEnumerable<Store>> GetSliceAsync(int offset, int size)
        {
            return await _context.Items.Skip(offset).Take(size).ToListAsync();
        }

        public async Task<IEnumerable<Store>> GetItemsByCategoryAndSliceAsync(StoreCategory category, int offset, int size)
        {
            return await _context.Items
                .Where(c => c.StoreCategory == category)
                .Skip(offset)
                .Take(size)
                .ToListAsync();
        }

        public async Task<int> GetCountByCategoryAsync(StoreCategory category)
        {
            return await _context.Items.CountAsync(c => c.StoreCategory == category);
        }


        public async Task<Store> GetByIdAsync(int id)
        {
            return await _context.Items.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<Store> GetByIdAsyncNoTracking(int id)
        {
            return await _context.Items.AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Store store)
        {
            _context.Update(store);
            return Save();
        }

        public async Task<int> GetCountAsync()
        {
            return await _context.Items.CountAsync();
        }
    }
}
