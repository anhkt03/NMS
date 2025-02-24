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

        public IActionResult Home()
        {
            return View();
        }

        public async Task<IActionResult> ManageAccount()
        {
            return View(await _context.SystemAccounts.ToListAsync());
        }

        public async Task<IActionResult> ManageCategory()
        {
            return View(await _context.Categories.ToListAsync());
        }
    }
}
