using metallist.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace metallist.Controllers
{
    public class AdminPanelController : Controller
    {
        ApplicationDbContext db;
        public AdminPanelController(ApplicationDbContext context) 
        { 
            db = context;
        }
        public IActionResult Index()
        {
            return View(); 
        }
        public IActionResult AddProduct()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct(Products product)
        {
            db.Products.Add(product);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public IActionResult AddCategory()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateCategory(Categories category)
        {
            db.Categories.Add(category);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
