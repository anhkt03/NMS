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
    public class NewsArticlesController : Controller
    {
        private readonly NmsContext _context;

        public NewsArticlesController(NmsContext context)
        {
            _context = context;
        }

        // GET: NewsArticles
        public async Task<IActionResult> Index()
        {
            var nmsContext = _context.NewsArticles.Include(n => n.Category).Include(n => n.CreatedBy).Include(n => n.UpdateBy);
            return View(await nmsContext.ToListAsync());
        }

        // GET: NewsArticles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newsArticle = await _context.NewsArticles
                .Include(n => n.Category)
                .Include(n => n.CreatedBy)
                .Include(n => n.UpdateBy)
                .FirstOrDefaultAsync(m => m.NewsArticleId == id);
            if (newsArticle == null)
            {
                return NotFound();
            }

            return View(newsArticle);
        }

        // GET: NewsArticles/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId");
            ViewData["CreatedById"] = new SelectList(_context.SystemAccounts, "AccountId", "AccountId");
            ViewData["UpdateById"] = new SelectList(_context.SystemAccounts, "AccountId", "AccountId");
            return View();
        }

        // POST: NewsArticles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NewsArticleId,NewsTitle,Headline,CreateDate,NewsContent,NewsSource,CategoryId,NewsStatus,CreatedById,UpdateById,ModifyDate")] NewsArticle newsArticle)
        {
            if (ModelState.IsValid)
            {
                _context.Add(newsArticle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", newsArticle.CategoryId);
            ViewData["CreatedById"] = new SelectList(_context.SystemAccounts, "AccountId", "AccountId", newsArticle.CreatedById);
            ViewData["UpdateById"] = new SelectList(_context.SystemAccounts, "AccountId", "AccountId", newsArticle.UpdateById);
            return View(newsArticle);
        }

        // GET: NewsArticles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newsArticle = await _context.NewsArticles.FindAsync(id);
            if (newsArticle == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", newsArticle.CategoryId);
            ViewData["CreatedById"] = new SelectList(_context.SystemAccounts, "AccountId", "AccountId", newsArticle.CreatedById);
            ViewData["UpdateById"] = new SelectList(_context.SystemAccounts, "AccountId", "AccountId", newsArticle.UpdateById);
            return View(newsArticle);
        }

        // POST: NewsArticles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NewsArticleId,NewsTitle,Headline,CreateDate,NewsContent,NewsSource,CategoryId,NewsStatus,CreatedById,UpdateById,ModifyDate")] NewsArticle newsArticle)
        {
            if (id != newsArticle.NewsArticleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(newsArticle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NewsArticleExists(newsArticle.NewsArticleId))
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", newsArticle.CategoryId);
            ViewData["CreatedById"] = new SelectList(_context.SystemAccounts, "AccountId", "AccountId", newsArticle.CreatedById);
            ViewData["UpdateById"] = new SelectList(_context.SystemAccounts, "AccountId", "AccountId", newsArticle.UpdateById);
            return View(newsArticle);
        }

        // GET: NewsArticles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newsArticle = await _context.NewsArticles
                .Include(n => n.Category)
                .Include(n => n.CreatedBy)
                .Include(n => n.UpdateBy)
                .FirstOrDefaultAsync(m => m.NewsArticleId == id);
            if (newsArticle == null)
            {
                return NotFound();
            }

            return View(newsArticle);
        }

        // POST: NewsArticles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var newsArticle = await _context.NewsArticles.FindAsync(id);
            if (newsArticle != null)
            {
                _context.NewsArticles.Remove(newsArticle);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NewsArticleExists(int id)
        {
            return _context.NewsArticles.Any(e => e.NewsArticleId == id);
        }
    }
}
