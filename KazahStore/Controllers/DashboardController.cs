using CloudinaryDotNet.Actions;
using KazahStore.Interfaces;
using KazahStore.Models;
using KazahStore.Repository;
using KazahStore.Services;
using KazahStore.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace KazahStore.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IDashboardRepository _dashboardRepository;
        private readonly IPhotoService _photoService;

        public DashboardController(IDashboardRepository dashboardRepository, IPhotoService photoService) 
        {
            _dashboardRepository = dashboardRepository;
            _photoService = photoService;
        }

        public async Task<IActionResult> Index()
        {
            var userTrades = await _dashboardRepository.GetAllUserTrades();
            var userStores = await _dashboardRepository.GetAllUserStores();
            var dashboardViewModel = new DashboardViewModel()
            {
                Trades = userTrades,
                Stores = userStores
            };
            return View(dashboardViewModel);
        }

        
    }
}
