using KazahStore.Data.Enum;

namespace KazahStore.ViewModels
{
    public class EditStoreViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public IFormFile Image { get; set; }

        public string? URL { get; set; }

        public StoreCategory StoreCategory { get; set; }
    }
}
