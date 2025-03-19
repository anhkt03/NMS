using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NMS.Models;

namespace NMS.Controllers
{
    public class AdminController : Controller
    {

        private readonly NmsContext _context;

        public AdminController(NmsContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Home(DateTime? startDate, DateTime? endDate)
        {
            var nmsContext = _context.NewsArticles
                .Include(n => n.Category)
                .Include(n => n.CreatedBy)
                .Include(n => n.Tags)
                .Where(n => n.NewsStatus == true);

            // 🔍 Filter by Date Range (StartDate to EndDate)
            if (startDate.HasValue && endDate.HasValue)
            {
                nmsContext = nmsContext.Where(n => n.CreateDate >= startDate.Value && n.CreateDate <= endDate.Value);
                ViewBag.StartDate = startDate.Value.ToString("yyyy-MM-dd");
                ViewBag.EndDate = endDate.Value.ToString("yyyy-MM-dd");
            }

            // 🎯 Fetch News Articles in Descending Order by CreateDate
            var newsArticles = await nmsContext
                .OrderByDescending(n => n.CreateDate)
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

            ViewBag.NewsArticles = newsArticles;
            return View(newsArticles);
        }


        public async Task<IActionResult> ManageAccount()
        {
            return View(await _context.SystemAccounts.ToListAsync());
        }

        public async Task<IActionResult> ManageCategory()
        {
            return View(await _context.Categories.ToListAsync());
        }

        public async Task<IActionResult> ManageTag()
        {
            return View(await _context.Tags.ToListAsync());
        }
    }
}
