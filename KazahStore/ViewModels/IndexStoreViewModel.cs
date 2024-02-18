using KazahStore.Models;

namespace KazahStore.ViewModels
{
    public class IndexStoreViewModel
    {
        public IEnumerable<Store> Stores { get; set; }
        public int PageSize { get; set; }
        public int Page { get; set; }
        public int TotalPages { get; set; }
        public int TotalStores { get; set; }
        public int Category { get; set; }
        public bool HasPreviousPage => Page > 1;

        public bool HasNextPage => Page < TotalPages;
    }
}
