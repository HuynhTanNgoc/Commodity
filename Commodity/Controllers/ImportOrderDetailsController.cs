using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Commodity.Data;
using Commodity.Models;

namespace Commodity.Controllers
{
    public class ImportOrderDetailsController : Controller
    {
        private readonly CommodityContext _context;

        public ImportOrderDetailsController(CommodityContext context)
        {
            _context = context;
        }

        // GET: ImportOrderDetails
        public async Task<IActionResult> Index()
        {
            var commodityContext = _context.ImportOrderDetail.Include(i => i.ImportOrder).Include(i => i.Product);
            return View(await commodityContext.ToListAsync());
        }

        // GET: ImportOrderDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var importOrderDetail = await _context.ImportOrderDetail
                .Include(i => i.ImportOrder)
                .Include(i => i.Product)
                .FirstOrDefaultAsync(m => m.ImportOrderDetailId == id);
            if (importOrderDetail == null)
            {
                return NotFound();
            }

            return View(importOrderDetail);
        }

        // GET: ImportOrderDetails/Create
        public IActionResult Create()
        {
            ViewData["ImportOrderId"] = new SelectList(_context.ImportOrder, "ImportOrderID", "ImportOrderID");
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "ProductId");
            return View();
        }

        // POST: ImportOrderDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ImportOrderDetailId,ImportOrderId,ProductId,Quantity,Total")] ImportOrderDetail importOrderDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(importOrderDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ImportOrderId"] = new SelectList(_context.ImportOrder, "ImportOrderID", "ImportOrderID", importOrderDetail.ImportOrderId);
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "ProductId", importOrderDetail.ProductId);
            return View(importOrderDetail);
        }

        // GET: ImportOrderDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var importOrderDetail = await _context.ImportOrderDetail.FindAsync(id);
            if (importOrderDetail == null)
            {
                return NotFound();
            }
            ViewData["ImportOrderId"] = new SelectList(_context.ImportOrder, "ImportOrderID", "ImportOrderID", importOrderDetail.ImportOrderId);
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "ProductId", importOrderDetail.ProductId);
            return View(importOrderDetail);
        }

        // POST: ImportOrderDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ImportOrderDetailId,ImportOrderId,ProductId,Quantity,Total")] ImportOrderDetail importOrderDetail)
        {
            if (id != importOrderDetail.ImportOrderDetailId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(importOrderDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ImportOrderDetailExists(importOrderDetail.ImportOrderDetailId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ImportOrderId"] = new SelectList(_context.ImportOrder, "ImportOrderID", "ImportOrderID", importOrderDetail.ImportOrderId);
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "ProductId", importOrderDetail.ProductId);
            return View(importOrderDetail);
        }

        // GET: ImportOrderDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var importOrderDetail = await _context.ImportOrderDetail
                .Include(i => i.ImportOrder)
                .Include(i => i.Product)
                .FirstOrDefaultAsync(m => m.ImportOrderDetailId == id);
            if (importOrderDetail == null)
            {
                return NotFound();
            }

            return View(importOrderDetail);
        }

        // POST: ImportOrderDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var importOrderDetail = await _context.ImportOrderDetail.FindAsync(id);
            if (importOrderDetail != null)
            {
                _context.ImportOrderDetail.Remove(importOrderDetail);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ImportOrderDetailExists(int id)
        {
            return _context.ImportOrderDetail.Any(e => e.ImportOrderDetailId == id);
        }
    }
}
