using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using NMS.Models;
using NMS.Services;
using NuGet.Packaging;

namespace NMS.Controllers
{
    public class NewsArticlesController : Controller
    {
        private readonly NmsContext _context;
        private readonly IHubContext<SignalRService> _signalRHub;

        public NewsArticlesController(NmsContext context, IHubContext<SignalRService> hubContext)
        {
            _context = context;
            _signalRHub = hubContext;
        }

        // GET: NewsArticles
        [HttpGet]
        public async Task<IActionResult> Index(int? categoryId, int? tag, int? createdById)
        {
            var nmsContext = _context.NewsArticles
                .Include(n => n.Category)
                .Include(n => n.CreatedBy)
                .Include(n => n.UpdateBy)
                .Include(n => n.Tags)
                .Where(n => n.NewsStatus == true);

            if (categoryId.HasValue)
            {
                nmsContext = nmsContext.Where(n => n.Category.CategoryId == categoryId.Value);
                ViewBag.SelectedCategoryId = categoryId;
            }

            if(tag.HasValue)
            {
                nmsContext = nmsContext.Where(n => n.Tags.Any(t => t.TagId == tag.Value));
                ViewBag.SelectedTagId = tag;
            }

            if (createdById.HasValue)
            {
                nmsContext = nmsContext.Where(n => n.CreatedBy.AccountId == createdById.Value);
                ViewBag.SelectedCreatedById = createdById;
            }

            var newsArticles = await nmsContext
                .Select(n => new NewsArticle
                {
                    NewsArticleId = n.NewsArticleId,
                    CreateDate = n.CreateDate,
                    Headline = n.Headline,
                    NewsContent = n.NewsContent,
                    NewsTitle = n.NewsTitle,
                    Category = n.Category,
                    CreatedBy = n.CreatedBy,
                    Tags = n.Tags,
                    NewsSource = n.NewsSource,
                }).ToListAsync();

            var tags = await _context.Tags
                .Select(t => new Tag
                {
                    TagId = t.TagId,
                    TagName = t.TagName
                }).ToListAsync();

            var categories = await _context.Categories
                .Where(c => c.IsActive == true)
                .Select(c => new Category
                {
                    CategoryName = c.CategoryName,
                    CategoryId = c.CategoryId,
                }).ToListAsync();

            var staffList = await _context.SystemAccounts
                .Where(s => s.AccountRole == 1)
                .Select(s => new SystemAccount
                {
                    AccountId = s.AccountId,
                    AccountName = s.AccountName
                }).ToListAsync();

            ViewBag.NewsArticles = newsArticles;
            ViewBag.Category = categories;
            ViewBag.Tags = tags;
            ViewBag.StaffList = staffList;

            return View(newsArticles);
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
                .Include(t => t.Tags)
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
            var user = HttpContext.Session.GetString("user");
            var category = _context.Categories
                .Where(c => c.IsActive == true)
                .ToList();

            var tags = _context.Tags.ToList();

            ViewData["CategoryId"] = new SelectList(category, "CategoryId", "CategoryName");
            ViewData["UpdateById"] = new SelectList(_context.SystemAccounts, "AccountId", "AccountName");
            ViewBag.Tags = tags;
            return View();
        }

        // POST: NewsArticles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NewsArticleId,NewsTitle,Headline,CreateDate,NewsContent,NewsSource,CategoryId,NewsStatus,ModifyDate")] NewsArticle newsArticle, List<int> SelectedTagIds)
        {
            if (ModelState.IsValid)
            {

                var userId = HttpContext.Session.GetString("userId");

                if (string.IsNullOrEmpty(userId))
                {
                    return RedirectToAction("Login", "Home");
                }

                newsArticle.CreatedById = int.Parse(userId);
                newsArticle.CreateDate = DateTime.Now;

                if (SelectedTagIds != null && SelectedTagIds.Any())
                {
                    newsArticle.Tags = _context.Tags
                        .Where(t => SelectedTagIds.Contains(t.TagId))
                        .ToList();
                }

                _context.Add(newsArticle);
                await _context.SaveChangesAsync();
                await _signalRHub.Clients.All.SendAsync("LoadNewsArticle");
                return RedirectToAction(nameof(Index));
            }

            

            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", newsArticle.CategoryId);
            ViewData["CreatedById"] = new SelectList(_context.SystemAccounts, "AccountId", "AccountName", newsArticle.CreatedById);
            ViewData["UpdateById"] = new SelectList(_context.SystemAccounts, "AccountId", "AccountName", newsArticle.UpdateById);
            return View(newsArticle);

        }

        // GET: NewsArticles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newsArticle = await _context.NewsArticles
                .Include(t => t.Tags)
                .FirstOrDefaultAsync(n => n.NewsArticleId == id);

            var allTags = await _context.Tags.ToListAsync();
            var selectedTagIds = newsArticle.Tags.Select(t => t.TagId).ToList();

            ViewBag.Tags = allTags.Select(tag => new
            {
                tag.TagId,
                tag.TagName,
                IsChecked = selectedTagIds.Contains(tag.TagId)
            }).ToList();

            var authorInfo = (from article in _context.NewsArticles
                              join account in _context.SystemAccounts
                              on article.CreatedById equals account.AccountId
                              where article.NewsArticleId == id 
                              select new
                              {
                                  AuthorName = account.AccountName,
                                  AuthorId = account.AccountId
                              }).FirstOrDefault();

            if (authorInfo != null)
            {
                ViewBag.AuthorName = authorInfo.AuthorName; // Display author name in the view
                ViewBag.CreatedById = authorInfo.AuthorId;  // Set author ID for form submission
            }


            if (newsArticle == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", newsArticle.CategoryId);
            ViewBag.AuthorUpdate = HttpContext.Session.GetString("user");

            return View(newsArticle);
        }

        // POST: NewsArticles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
    int id,
    [Bind("NewsArticleId,NewsTitle,Headline,CreateDate,NewsContent,NewsSource,CategoryId,NewsStatus,CreatedById")] NewsArticle newsArticle,
    List<int> SelectedTagIds)
        {
            if (id != newsArticle.NewsArticleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingNews = await _context.NewsArticles
                        .Include(n => n.Tags)
                        .FirstOrDefaultAsync(n => n.NewsArticleId == id);

                    if (existingNews == null)
                    {
                        return NotFound();
                    }

                    
                    existingNews.NewsTitle = newsArticle.NewsTitle;
                    existingNews.Headline = newsArticle.Headline;
                    existingNews.CreateDate = newsArticle.CreateDate;
                    existingNews.NewsContent = newsArticle.NewsContent;
                    existingNews.NewsSource = newsArticle.NewsSource;
                    existingNews.CategoryId = newsArticle.CategoryId;
                    existingNews.NewsStatus = newsArticle.NewsStatus;
                    existingNews.ModifyDate = DateTime.Now;
                    existingNews.UpdateById = int.Parse(HttpContext.Session.GetString("userId"));

                    existingNews.Tags.Clear();

                    var newTags = await _context.Tags
                        .Where(t => SelectedTagIds.Contains(t.TagId))
                        .ToListAsync();

                    existingNews.Tags.AddRange(newTags);

                    await _context.SaveChangesAsync();

                    await _signalRHub.Clients.All.SendAsync("LoadNewsArticle");

                    return RedirectToAction(nameof(Index));
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
            }

            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", newsArticle.CategoryId);
            ViewData["CreatedById"] = new SelectList(_context.SystemAccounts, "AccountId", "AccountName", newsArticle.CreatedById);
            ViewData["UpdateById"] = new SelectList(_context.SystemAccounts, "AccountId", "AccountName", newsArticle.UpdateById);

            return View(newsArticle);
        }


        private void UpdateNewsTags(NewsArticle newsArticle, List<int> listTag)
        {

            foreach (var newTag in listTag)
            {
                var tag = _context.Tags.FirstOrDefault(t => t.TagId == newTag);
                if (tag != null)
                {
                    newsArticle.Tags.Add(tag);
                }
            }
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
            var newsArticle = await _context.NewsArticles
                .Include(n => n.Tags) 
                .FirstOrDefaultAsync(n => n.NewsArticleId == id);

            if (newsArticle != null)
            {
                newsArticle.Tags.Clear();
                _context.NewsArticles.Remove(newsArticle);

                await _context.SaveChangesAsync();
                await _signalRHub.Clients.All.SendAsync("LoadNewsArticle");
            }

            return RedirectToAction(nameof(Index));
        }


        private bool NewsArticleExists(int id)
        {
            return _context.NewsArticles.Any(e => e.NewsArticleId == id);
        }
    }
}
