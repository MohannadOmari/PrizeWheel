using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using UmniahPrizeWheel.Models;
using UmniahPrizeWheel.Repository.Interface;

namespace UmniahPrizeWheel.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IInventoryRepository _inventoryRepo;

        public HomeController(ILogger<HomeController> logger, IInventoryRepository inventoryRepo)
        {
            _logger = logger;
            _inventoryRepo = inventoryRepo;
        }

        public async Task<IActionResult> Index()
        {
            List<string> items = await _inventoryRepo.GetItemNames();
            return View(items);
        }

        #region Prize Won Calculater
        [HttpGet("Home/PrizeWon")]
        public async Task<IActionResult> PrizeWon()
        {
            List<InventoryMatrix> items = new List<InventoryMatrix>();
            List<Inventory> inventory = await _inventoryRepo.GetAll();

            foreach(var item in inventory)
            {
                items.Add(new InventoryMatrix(
                    item.Id,
                    item.Item,
                    item.Quantity
                    ));
            }

            items[0].used = await _inventoryRepo.GetUsed(items[0].id);
            items[0].available = items[0].quantity - items[0].used;
            items[0].from = 0;
            items[0].to = items[0].from + items[0].available;

            for (int i = 1; i < items.Count; i++)
            {
                items[i].used = await _inventoryRepo.GetUsed(items[i].id);
                items[i].available = items[i].quantity - items[i].used;
                items[i].from = items[i-1].to + 1;
                items[i].to = items[i].from + items[i].available;
            }
            
            Random random = new Random();
            int count = random.Next(items[0].from, items[items.Count - 1].to);
            int result = 1;

            foreach (var item in items)
            {
                if (count > item.from && count <= item.to)
                {
                    result = item.id;
                }
            }

            await _inventoryRepo.AddTransaction(result);

            return Json(result);
        }
        #endregion

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}