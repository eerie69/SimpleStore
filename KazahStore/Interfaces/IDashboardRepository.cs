using KazahStore.Models;

namespace KazahStore.Interfaces
{
    public interface IDashboardRepository
    {
        Task<List<Store>> GetAllUserStores();

        Task<List<Trade>> GetAllUserTrades();

        Task<AppUser> GetUserById(string id);

        Task<AppUser> GetByIdNoTracking(string id);

        bool Update(AppUser user);

        bool Save();
    }
}
