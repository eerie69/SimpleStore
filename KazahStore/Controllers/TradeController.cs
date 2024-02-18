using Microsoft.AspNetCore.Mvc;

namespace KazahStore.Controllers
{
    public class TradeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
