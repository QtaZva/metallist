﻿using metallist.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;

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
        public async Task<IActionResult> ViewMediaFiles()
        {
            var mediaFiles = await db.MediaFiles.ToListAsync();
            return View(mediaFiles);
        }
        public async Task<IActionResult> DeleteMediaFile(int id)
        {
            var media = await db.MediaFiles.FindAsync(id);
            if (media != null)
            {
                db.MediaFiles.Remove(media);
                await db.SaveChangesAsync();
            }
            return RedirectToAction(nameof(ViewMediaFiles));
        }
        public IActionResult AddMediaFile()
        {
            var pages = db.Info
            .Select(c => new SelectListItem
            {
                Value = c.id.ToString(),
                Text = c.Page
            })
            .ToList();

            var viewModel = new MediaFileViewModel
            {
                Pages = pages
            };

            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> CreateMediaFile(MediaFileViewModel model)
        {
            var newMediaFile = new MediaFiles
            {
                Name = model.Name,
                Page_id = model.page_id
            };
            if (model.File != null)
            {
                using (var ms = new MemoryStream())
                {
                    await model.File.CopyToAsync(ms);
                    newMediaFile.File = ms.ToArray();
                }
            }
            db.MediaFiles.Add(newMediaFile);
            db.SaveChanges();
            return RedirectToAction(nameof(ViewMediaFiles));
        }
        public async Task<IActionResult> EditMediaFile(int id)
        {
            var mediaFile = await db.MediaFiles.FindAsync(id);
            if (mediaFile == null)
            {
                return NotFound();
            }
            var pages = db.Info
                .Select(c => new SelectListItem
                {
                    Value = c.id.ToString(),
                    Text = c.Page
                })
                .ToList();
            var viewModel = new EditMediaFileViewModel
            {
                MediaFile = mediaFile,
                Pages = pages
            };
            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> EditMediaFile(EditMediaFileViewModel model)
        {
            var MediaFile = await db.MediaFiles.FindAsync(model.MediaFile.id);
            if (MediaFile == null)
            {
                return NotFound();
            }
            MediaFile.Name = model.MediaFile.Name;
            MediaFile.Page_id = model.MediaFile.Page_id;
            if (model.File != null)
            {
                using (var ms = new MemoryStream())
                {
                    await model.File.CopyToAsync(ms);
                    MediaFile.File = ms.ToArray();
                }
            }
            db.MediaFiles.Update(MediaFile);
            db.SaveChanges();
            return RedirectToAction(nameof(ViewMediaFiles));
        }
        public async Task<IActionResult> ViewInformation()
        {
            var info = await db.Info.ToListAsync();
            return View(info);
        }
        public async Task<IActionResult> EditInformation(int id)
        {
            var info = await db.Info.FindAsync(id);
            if (info == null)
            {
                return NotFound();
            }
            return View(info);
        }
        [HttpPost]
        public async Task<IActionResult> EditInformation(Info info)
        {
            var editedInfo = await db.Info.FindAsync(info.id);
            if (editedInfo == null)
            {
                return NotFound();
            }
            editedInfo.Information = info.Information;
            db.Info.Update(editedInfo);
            db.SaveChanges();
            return RedirectToAction(nameof(ViewInformation));
        }
    }
}
