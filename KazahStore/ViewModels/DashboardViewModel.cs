using KazahStore.Models;

namespace KazahStore.ViewModels
{
    public class DashboardViewModel
    {
        public List<Store> Stores { get; set; }

        public List<Trade> Trades { get; set; }
    }
}
