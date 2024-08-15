using metallist.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

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

        public async Task<IActionResult> ProductsAsync()
        {
            return View(await db.Products.ToListAsync());
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
    }
}
