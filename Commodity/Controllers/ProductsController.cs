using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Commodity.Data;
using Commodity.Models;
using static System.Reflection.Metadata.BlobBuilder;

namespace Commodity.Controllers
{
    public class ProductsController : Controller
    {
        private readonly CommodityContext _context;

        public ProductsController(CommodityContext context)
        {
            _context = context;
        }

        // GET: Products

        public async Task<IActionResult> Index(string searchString, string productCategory)
        {
            // Retrieve all product categories
            var productCategories = await _context.ProductCategory.Select(pc => new SelectListItem { Value = pc.ProductCategoryId.ToString(), Text = pc.ProductCategoryName }).ToListAsync();

            // Query products
            var products = _context.Product.Include(p => p.ProductCategory).AsQueryable();

            // Apply filters
            if (!string.IsNullOrEmpty(searchString))
            {
                products = products.Where(p => p.Name.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(productCategory))
            {
                int categoryId = int.Parse(productCategory);
                products = products.Where(p => p.ProductCategoryId == categoryId);
            }

            // Populate view model
            var viewModel = new ProductIndexViewModel
            {
                SearchString = searchString,
                ProductCategory = productCategory,
                Products = await products.ToListAsync(),
                ProductCategories = new SelectList(productCategories, "Value", "Text", productCategory)
            };
            var suppliers = _context.Supplier.ToList();
            ViewData["SupplierID"] = new SelectList(suppliers, "SupplierID", "Name");
            return View(viewModel);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            var productCategories = _context.ProductCategory.ToList();
            ViewData["ProductCategoryId"] = new SelectList(productCategories, "ProductCategoryId", "ProductCategoryName");

            var suppliers = _context.Supplier.ToList();
            ViewData["SupplierID"] = new SelectList(suppliers, "SupplierID", "Name");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,Name,ProductCategoryId,SupplierID,Price")] Product product)
        {
            try
            {
                // Kiểm tra xem tên sản phẩm đã tồn tại chưa
                if (_context.Product.Any(p => p.Name == product.Name))
                {
                    ModelState.AddModelError("Name", "Product already exists.");
                }
                else
                {
                    _context.Add(product);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error occurred while saving data: " + ex.Message);
            }

            // Load lại danh sách category và supplier để hiển thị trong dropdownlist
            var productCategories = _context.ProductCategory.ToList();
            ViewData["ProductCategoryId"] = new SelectList(productCategories, "ProductCategoryId", "ProductCategoryName", product.ProductCategoryId);

            var suppliers = _context.Supplier.ToList();
            ViewData["SupplierID"] = new SelectList(suppliers, "SupplierID", "Name", product.SupplierID);

            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["ProductCategoryId"] = new SelectList(_context.Set<ProductCategory>(), "ProductCategoryId", "ProductCategoryName", product.ProductCategoryId);
            var suppliers = _context.Supplier.ToList();
            ViewData["SupplierID"] = new SelectList(suppliers, "SupplierID", "Name");
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,Name,ProductCategoryId,SupplierID,Price")] Product product)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }
            try
            {
                _context.Update(product);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(product.ProductId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
            ViewData["ProductCategoryId"] = new SelectList(_context.Set<ProductCategory>(), "ProductCategoryId", "ProductCategoryName", product.ProductCategoryId);
            var suppliers = _context.Supplier.ToList();
            ViewData["SupplierID"] = new SelectList(suppliers, "SupplierID", "Name");
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .Include(p => p.ProductCategory)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Product.FindAsync(id);
            if (product != null)
            {
                _context.Product.Remove(product);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.ProductId == id);
        }
    }
}
