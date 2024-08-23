using metallist.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using X.PagedList.Extensions;

namespace metallist.Controllers
{
    public class HomeController : Controller
    {
         ApplicationDbContext db;
        public HomeController(ApplicationDbContext context) 
        { 
            db = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ProductsAsync(int? page)
        {
            int pageSize = 8; // количество товаров на одной странице
            int pageNumber = (page ?? 1); // если страница не указана, открываем первую

            var products = db.Products.OrderBy(p => p.Name).ToPagedList(pageNumber, pageSize);

            return View(products);
        }
        // Метод для отображения деталей товара
        public IActionResult Details(int id)
        {
            var product = db.Products.FirstOrDefault(p => p.id == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
        public IActionResult Services()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult GetIcon(int id)
        {
            var mediaFile = db.MediaFiles.SingleOrDefault(m => m.id == id);
            if (mediaFile == null || mediaFile.File == null)
            {
                return NotFound();
            }

            return File(mediaFile.File, "image/png");
        }
        public IActionResult GetInfo(int id)
        {
            var info = db.Info.SingleOrDefault(p => p.id == id);
            if (info == null)
            {
                return NotFound();
            }
            return Content(info.Information, "text/plain");
        }
        [HttpGet]
        public async Task<IActionResult> Search(string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                return View("SearchResults", new List<Products>());
            }

            var results = await db.Products
                .Where(p => p.Name.Contains(query) || p.Vendor.Contains(query))
                .ToListAsync();

            return View("SearchResults", results);
        }
    }
}
