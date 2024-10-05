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
    public class ImportOrdersController : Controller
    {
        private readonly CommodityContext _context;

        public ImportOrdersController(CommodityContext context)
        {
            _context = context;
        }

        // GET: ImportOrders
        public async Task<IActionResult> Index()
        {
            return View(await _context.ImportOrder.ToListAsync());
        }

        // GET: ImportOrders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var importOrder = await _context.ImportOrder
                .FirstOrDefaultAsync(m => m.ImportOrderID == id);
            if (importOrder == null)
            {
                return NotFound();
            }

            return View(importOrder);
        }

        // GET: ImportOrders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ImportOrders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ImportOrderID,ImportOrderName,ImportDate")] ImportOrder importOrder)
        {
            if (ModelState.IsValid)
            {
                var existingOrder = await _context.ImportOrder.FirstOrDefaultAsync(o => o.ImportOrderName == importOrder.ImportOrderName);
                if (existingOrder != null)
                {
                    ModelState.AddModelError("ImportOrderName", "Order name already exists.");
                    return View(importOrder);
                }

                importOrder.ImportDate = DateTime.Now;
                _context.Add(importOrder);
                await _context.SaveChangesAsync();

                // Redirect to the Create action of ImportOrderDetailsController
                return RedirectToAction("Create", "ImportOrderDetails");
            }
            return View(importOrder);
        }
        // GET: ImportOrders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var importOrder = await _context.ImportOrder.FindAsync(id);
            if (importOrder == null)
            {
                return NotFound();
            }
            return View(importOrder);
        }

        // POST: ImportOrders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ImportOrderID,ImportOrderName,ImportDate")] ImportOrder importOrder)
        {
            if (id != importOrder.ImportOrderID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(importOrder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ImportOrderExists(importOrder.ImportOrderID))
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
            return View(importOrder);
        }

        // GET: ImportOrders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var importOrder = await _context.ImportOrder
                .FirstOrDefaultAsync(m => m.ImportOrderID == id);
            if (importOrder == null)
            {
                return NotFound();
            }

            return View(importOrder);
        }

        // POST: ImportOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var importOrder = await _context.ImportOrder.FindAsync(id);
            if (importOrder != null)
            {
                _context.ImportOrder.Remove(importOrder);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ImportOrderExists(int id)
        {
            return _context.ImportOrder.Any(e => e.ImportOrderID == id);
        }
    }
}
