using KazahStore.Data;
using KazahStore.Interfaces;
using KazahStore.Models;
using KazahStore.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net;

namespace KazahStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IStoreRepository _storeRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IConfiguration _config;

        public HomeController(ILogger<HomeController> logger, IStoreRepository storeRepository,
            UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IConfiguration config)
        {
            _logger = logger;
            _storeRepository = storeRepository;
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config;
        }

        
        public async Task<ActionResult> Index()
        {
            var homeViewModel = new HomeViewModel();
            
            homeViewModel.Stores = await _storeRepository.GetAll();

            return View(homeViewModel);
        }

        public IActionResult Register()
        {
            var response = new HomeUserCreateViewModel();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Index(HomeViewModel homeVM)
        {
            var createVM = homeVM.Register;

            if (!ModelState.IsValid) return View(homeVM);

            var user = await _userManager.FindByEmailAsync(createVM.EmailAddress);
            if (user != null)
            {
                ModelState.AddModelError("Register.Email", "This email address is already in use");
                return View(homeVM);
            }

            var newUser = new AppUser
            {
                UserName = createVM.UserName,
                Email = createVM.EmailAddress,
            };

            var newUserResponse = await _userManager.CreateAsync(newUser, createVM.Password);

            if (newUserResponse.Succeeded)
            {
                await _signInManager.SignInAsync(newUser, isPersistent: false);
                await _userManager.AddToRoleAsync(newUser, UserRoles.User);
            }
            return RedirectToAction("Index", "Store");
        }

        public IActionResult Privacy()
        {
            return View();
        }

    }
}