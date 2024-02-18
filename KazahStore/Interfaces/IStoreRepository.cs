using KazahStore.Data.Enum;
using KazahStore.Models;

namespace KazahStore.Interfaces
{
    public interface IStoreRepository
    {
        Task<IEnumerable<Store>> GetAll();

        Task<IEnumerable<Store>> GetSliceAsync(int offset, int size);

        Task<IEnumerable<Store>> GetItemsByCategoryAndSliceAsync(StoreCategory category, int offset, int size);

        Task<Store> GetByIdAsync(int id);

        Task<Store> GetByIdAsyncNoTracking(int id);

        Task<int> GetCountAsync();

        Task<int> GetCountByCategoryAsync(StoreCategory category);

        bool Add(Store store);

        bool Update(Store store);

        bool Delete(Store store);

        bool Save();
    }
}
