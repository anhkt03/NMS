using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NMS.Models;

namespace NMS.Controllers
{
    public class SystemAccountsController : Controller
    {
        private readonly NmsContext _context;

        public SystemAccountsController(NmsContext context)
        {
            _context = context;
        }

        // GET: SystemAccounts
        public async Task<IActionResult> Index()
        {
            return View(await _context.SystemAccounts.ToListAsync());
        }

        // GET: SystemAccounts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var systemAccount = await _context.SystemAccounts
                .FirstOrDefaultAsync(m => m.AccountId == id);
            if (systemAccount == null)
            {
                return NotFound();
            }

            return View(systemAccount);
        }

        // GET: SystemAccounts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SystemAccounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AccountId,AccountName,AccountEmail,AccountRole,AccountPassword")] SystemAccount systemAccount)
        {
            if (ModelState.IsValid)
            {
                _context.Add(systemAccount);
                await _context.SaveChangesAsync();
                return RedirectToAction("ManageAccount", "Admin");
            }
            return RedirectToAction("ManageAccount", "Admin");
        }

        // GET: SystemAccounts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var systemAccount = await _context.SystemAccounts.FindAsync(id);
            if (systemAccount == null)
            {
                return NotFound();
            }
            return View(systemAccount);
        }

        // POST: SystemAccounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AccountId,AccountName,AccountEmail,AccountRole,AccountPassword")] SystemAccount systemAccount)
        {
            if (id != systemAccount.AccountId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(systemAccount);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SystemAccountExists(systemAccount.AccountId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("ManageAccount", "Admin");
            }
            return RedirectToAction("ManageAccount", "Admin");
        }

        // GET: SystemAccounts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var systemAccount = await _context.SystemAccounts
                .FirstOrDefaultAsync(m => m.AccountId == id);
            if (systemAccount == null)
            {
                return NotFound();
            }

            return View(systemAccount);
        }

        // POST: SystemAccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var systemAccount = await _context.SystemAccounts.FindAsync(id);
            if (systemAccount != null)
            {
                _context.SystemAccounts.Remove(systemAccount);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("ManageAccount", "Admin");
        }

        private bool SystemAccountExists(int id)
        {
            return _context.SystemAccounts.Any(e => e.AccountId == id);
        }
    }
}
