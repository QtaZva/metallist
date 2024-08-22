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
        public async Task<IActionResult> ViewProducts()
        {
            var products = await db.Products.ToListAsync();
            return View(products);
        }
        // Метод для отображения страницы редактирования товара
        public async Task<IActionResult> EditProduct(int id)
        {
            var product = await db.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            var categories = await db.Categories
                .Select(c => new SelectListItem
                {
                    Value = c.id.ToString(),
                    Text = c.Name
                })
                .ToListAsync();

            var viewModel = new EditProductViewModel
            {
                Product = product,
                Categories = categories
            };

            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> EditProduct(EditProductViewModel viewModel)
        {
            var product = await db.Products.FindAsync(viewModel.Product.id);
            if (product == null)
            {
                return NotFound();
            }

            product.Name = viewModel.Product.Name;
            product.Desc = viewModel.Product.Desc;
            product.Vendor = viewModel.Product.Vendor;
            product.Size = viewModel.Product.Size;
            product.Cost = viewModel.Product.Cost;
            product.Category_id = viewModel.Product.Category_id;

            if (viewModel.Img != null)
            {
                // Обработка загрузки файла, если он был предоставлен
                using var memoryStream = new MemoryStream();
                await viewModel.Img.CopyToAsync(memoryStream);
                product.Img = memoryStream.ToArray();
            }

            db.Products.Update(product);
            await db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await db.Products.FindAsync(id);
            if (product != null)
            {
                db.Products.Remove(product);
                await db.SaveChangesAsync();
            }
            return RedirectToAction(nameof(ViewProducts));
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
        public async Task<IActionResult> ViewCategories()
        {
            var categories = await db.Categories.ToListAsync();
            return View(categories);
        }
        public async Task<IActionResult> EditCategory(int id)
        {
            var category = await db.Categories.FindAsync(id);
            return View(category);
        }
        [HttpPost]
        public async Task<IActionResult> EditCategory(Categories categoryModel)
        {
            var category = await db.Categories.FindAsync(categoryModel.id);
            if(category == null)
            {
                return NotFound();
            }
            category.Name = categoryModel.Name;
            db.Categories.Update(category);
            db.SaveChanges();
            return RedirectToAction(nameof(ViewCategories));
        }
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await db.Categories.FindAsync(id);
            if(category != null)
            {
                db.Categories.Remove(category);
                await db.SaveChangesAsync();
            }
            return RedirectToAction(nameof(ViewCategories));
        }
    }
}
