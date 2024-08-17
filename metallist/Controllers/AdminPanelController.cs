using metallist.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace metallist.Controllers
{
    [Authorize(Roles = "Admin")]
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
            var categories = db.Categories
            .Select(c => new SelectListItem
            {
                Value = c.id.ToString(),
                Text = c.Name
            })
            .ToList();

            var viewModel = new ProductViewModel
            {
                Categories = categories
            };

            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductViewModel model)
        {
            var newProduct = new Products
            {
                Name = model.Name,
                Desc = model.Desc,
                Vendor = model.Vendor,
                Size = model.Size,
                Cost = model.Cost,
                Category_id = model.Category_id
            };

            if (model.Img != null && model.Img.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    await model.Img.CopyToAsync(ms);
                    newProduct.Img = ms.ToArray();
                }
            }
            db.Products.Add(newProduct);
            db.SaveChanges();
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
