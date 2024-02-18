using KazahStore.Models;

namespace KazahStore.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Store>? Stores { get; set; }

        public HomeUserCreateViewModel Register { get; set; } = new HomeUserCreateViewModel();
    }
}
