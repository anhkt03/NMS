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
    public class CategoriesController : Controller
    {
        private readonly NmsContext _context;

        public CategoriesController(NmsContext context)
        {
            _context = context;
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {
            var nmsContext = _context.Categories.Include(c => c.ParentCategory);
            return View(await nmsContext.ToListAsync());
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .Include(c => c.ParentCategory)
                .FirstOrDefaultAsync(m => m.CategoryId == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            ViewData["ParentCategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName");
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryId,CategoryName,CategoryDescription,ParentCategoryId,IsActive")] Category category)
        {
            var roleId = HttpContext.Session.GetString("role");
            _context.Add(category);
            await _context.SaveChangesAsync();
            ViewData["ParentCategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", category.ParentCategoryId);
            
            if(roleId == "1")
            {
                return RedirectToAction("Index", "Categories");
            }
            else
            {
                return RedirectToAction("ManageCategory", "Admin");
            }
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            ViewData["ParentCategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", category.ParentCategoryId);
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CategoryId,CategoryName,CategoryDescription,ParentCategoryId,IsActive")] Category category)
        {
            var roleId = HttpContext.Session.GetString("role");

            if (id != category.CategoryId)
            {
                return NotFound();
            }

            try
            {
                _context.Update(category);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(category.CategoryId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            ViewData["ParentCategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", category.ParentCategoryId);
            if (roleId == "1")
            {
                return RedirectToAction("Index", "Categories");
            }
            else
            {
                return RedirectToAction("ManageCategory", "Admin");
            }
        }

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .Include(c => c.ParentCategory)
                .FirstOrDefaultAsync(m => m.CategoryId == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            var roleId = HttpContext.Session.GetString("role");

            if (category == null)
            {
                TempData["ErrorMessage"] = "Category not found!";
                return RedirectToAction("Index", "Categories");
            }

            // Check if the category is in use (e.g., by News Articles)
            bool isCategoryInUse = await _context.NewsArticles
                .AnyAsync(n => n.CategoryId == id);

            if (isCategoryInUse)
            {
                TempData["ErrorMessage"] = "Cannot delete this category. It is currently in use!";
                return RedirectToAction("Delete", "Categories");
            }

            // Safe to delete
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Category deleted successfully!";
            if(roleId == "1")
            {
                return RedirectToAction("Index", "Categories");
            }
            else
            {
                return RedirectToAction("ManageCategory", "Admin");
            }
        }

        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.CategoryId == id);
        }
    }
}
