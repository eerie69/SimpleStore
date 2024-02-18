using KazahStore.Data;
using KazahStore.Interfaces;
using KazahStore.Models;
using Microsoft.EntityFrameworkCore;

namespace KazahStore.Repository
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DashboardRepository(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor) 
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<Store>> GetAllUserStores()
        {
            var curUser = _httpContextAccessor.HttpContext?.User.GetUserId();
            var userStores = _context.Items.Where(r => r.AppUser.Id == curUser);
            return userStores.ToList();
        }

        public async Task<List<Trade>> GetAllUserTrades()
        {
            var curUser = _httpContextAccessor.HttpContext?.User.GetUserId();
            var userTrades = _context.Trades.Where(r => r.AppUser.Id == curUser);
            return userTrades.ToList();
        }

        public async Task<AppUser> GetUserById(string id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<AppUser> GetByIdNoTracking(string id)
        {
            return await _context.Users.Where(u => u.Id == id).AsNoTracking().FirstOrDefaultAsync();
        }

        public bool Update(AppUser user)
        {
            _context.Users.Update(user);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
