using KazahStore.Data;
using KazahStore.Data.Enum;
using KazahStore.Interfaces;
using KazahStore.Models;
using KazahStore.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Eventing.Reader;
using System.Security.AccessControl;

namespace KazahStore.Controllers
{
    public class StoreController : Controller
    {
        private readonly IStoreRepository _storeRepository;
        private readonly IPhotoService _photoService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public StoreController(IStoreRepository storeRepository, IPhotoService photoService,
            IHttpContextAccessor httpContextAccessor)
        {
            _storeRepository = storeRepository;
            _photoService = photoService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        [Route("MyServices")]
        public async Task<IActionResult> Index(int category = -1, int page = 1, int pageSize = 6)
        {
            if (page < 1 || pageSize < 1)
            {
                return NotFound();
            }

            // if category is -1 (All) dont filter else filter by selected category
            var stores = category switch
            {
                -1 => await _storeRepository.GetSliceAsync((page - 1) * pageSize, pageSize),
                _ => await _storeRepository.GetItemsByCategoryAndSliceAsync((StoreCategory)category, (page - 1) * pageSize, pageSize),
            };

            var count = category switch
            {
                -1 => await _storeRepository.GetCountAsync(),
                _ => await _storeRepository.GetCountByCategoryAsync((StoreCategory)category),
            };

            var storeViewModel = new IndexStoreViewModel
            {
                Stores = stores,
                Page = page,
                PageSize = pageSize,
                TotalStores = count,
                TotalPages = (int)Math.Ceiling(count / (double)pageSize),
                Category = category,
            };

            return View(storeViewModel);
        }

        [HttpGet]
        [Route("Store/{id}")]
        public async Task<IActionResult> DetailStore(int id, string runningStore)
        {
            var store = await _storeRepository.GetByIdAsync(id);

            return store == null ? NotFound() : View(store);
        }

        public async Task<IActionResult> Detail(int id)
        {
            Store store = await _storeRepository.GetByIdAsync(id);
            return View(store);
        }

        [HttpGet]
        [Route("MyServices/Create")]
        public IActionResult Create()
        {
            var curUserId = HttpContext.User.GetUserId();
            var createStoreViewModel = new CreateStoreViewModel { AppUserId = curUserId };
            return View(createStoreViewModel);
        }

        [HttpPost]
        [Route("MyServices/Create")]
        public async Task<IActionResult> Create(CreateStoreViewModel storeVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _photoService.AddPhotoAsync(storeVM.Image);

                var store = new Store
                {
                    Title = storeVM.Title,
                    Description = storeVM.Description,
                    Image = result.Url.ToString(),
                    AppUserId = storeVM.AppUserId
                };
                _storeRepository.Add(store);
                return RedirectToAction("Index");
            }
            else 
            {
                ModelState.AddModelError("", "Photo upload failed");
            }

            return View(storeVM);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var store = await _storeRepository.GetByIdAsync(id);
            if(store == null) return View("Error");
            var storeVM = new EditStoreViewModel
            {
                Title = store.Title,
                Description = store.Description,
                URL = store.Image,
                StoreCategory = store.StoreCategory,
            };
            return View(storeVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditStoreViewModel storeVM)
        {
            if (!ModelState.IsValid) 
            {
                ModelState.AddModelError("", "Failed to edit store");
                return View("Edit", storeVM);
            }

            var userStore = await _storeRepository.GetByIdAsyncNoTracking(id);
            if(userStore != null)
            {
                try
                {
                    await _photoService.DeletePhotoAsync(userStore.Image);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Could not delete photo");
                    return View(storeVM);
                }
                var photoResult = await _photoService.AddPhotoAsync(storeVM.Image);

                var store = new Store
                {
                    Id = id,
                    Title = storeVM.Title,
                    Description = storeVM.Description,
                    Image = photoResult.Url.ToString(),
                    StoreCategory = storeVM.StoreCategory
                };

                _storeRepository.Update(store);

                return RedirectToAction("Index");
            }
            else
            {
                return View(storeVM);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var storeDetails = await _storeRepository.GetByIdAsync(id);
            if (storeDetails == null) return View("Error");
            return View(storeDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteStore(int id)
        {
            var storeDetails = await _storeRepository.GetByIdAsync(id);
            if (storeDetails == null) return View("Error");

            _storeRepository.Delete(storeDetails);
            return RedirectToAction("Index");
        }
    }
}
