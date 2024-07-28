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
        /*public IActionResult CreateProductPage()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct(Products product)
        {
            db.Products.Add(product);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }*/
    }
}
