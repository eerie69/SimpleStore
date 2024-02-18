using KazahStore.Data.Enum;

namespace KazahStore.ViewModels
{
    public class CreateStoreViewModel
    {
        public int Id { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public IFormFile Image { get; set; }

        public StoreCategory StoreCategory { get; set; }

        public string? AppUserId { get; set; }
    }
}
