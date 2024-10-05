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
    public class AccountsController : Controller
    {
        private readonly CommodityContext _context;

        public AccountsController(CommodityContext context)
        {
            _context = context;
        }

        // GET: Accounts
        public async Task<IActionResult> Index()
        {
            return View(await _context.Account.ToListAsync());
        }

        // GET: Accounts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _context.Account.FirstOrDefaultAsync(m => m.AccountID == id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        // GET: Accounts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Accounts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AccountID,FullName,Gender,Email,Password,ConfirmPassword,Phone,Birth,Address")] Account account)
        {
            if (ModelState.IsValid)
            {
                if (_context.Account.Any(a => a.Email == account.Email))
                {
                    ModelState.AddModelError(string.Empty, "Email already exists.");
                    return View(account);
                }
                else if (_context.Account.Any(a => a.Phone == account.Phone))
                {
                    ModelState.AddModelError(string.Empty, "Phone already exists.");
                    return View(account);
                }
                _context.Add(account);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(SignIn));
            }
            return View(account);
        }
        // GET: Accounts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _context.Account.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }

            TempData["Password"] = account.Password;
            return View(account);
        }


        // POST: Accounts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AccountID,FullName,Gender,Email,Password,ConfirmPassword,Phone,Birth,Address,LastLogin,CreateDate")] Account account)
        {
            if (id != account.AccountID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(account);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccountExists(account.AccountID))
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
            return View(account);
        }

        // GET: Accounts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _context.Account
                .FirstOrDefaultAsync(m => m.AccountID == id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        // POST: Accounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var account = await _context.Account.FindAsync(id);
            if (account != null)
            {
                _context.Account.Remove(account);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AccountExists(int id)
        {
            return _context.Account.Any(e => e.AccountID == id);
        }

        // GET: Accounts/SignIn
        public IActionResult SignIn()
        {
            return View();
        }

        // POST: Accounts/SignIn
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn(Account model)
        {
            var user = await _context.Account.FirstOrDefaultAsync(u => u.Email == model.Email && u.Password == model.Password);
           
            if (user != null)
            {
                user.LastLogin = DateTime.Now;
                _context.Update(user);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid email or password");
                return View(model);
            }
        }
    }
}
