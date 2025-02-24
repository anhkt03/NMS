using Microsoft.AspNetCore.Mvc;
using NMS.Models;
using System.Diagnostics;

namespace NMS.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly NmsContext _context;

        private readonly IConfiguration _configuration;
        

        public HomeController(ILogger<HomeController> logger, NmsContext context, IConfiguration configuration)
        {
            _logger = logger;
            _context = context;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(SystemAccount account)
        {
            string defaultEmail = _configuration["DefaultAccount: Email"];
            string defaultPass = _configuration["DefaultAccount: Password"];

            if (account.AccountEmail == defaultEmail && account.AccountPassword == defaultPass)
            {
                HttpContext.Session.SetString("role", account.AccountRole.ToString());
                return Index();
            }
            else
            {
                var user = _context.SystemAccounts.FirstOrDefault(u => u.AccountEmail == account.AccountEmail && u.AccountPassword == account.AccountPassword);
                if (user != null)
                {
                    HttpContext.Session.SetString("role", user.AccountRole.ToString());
                    HttpContext.Session.SetString("user", user.AccountName.ToString());

                    // staff
                    if (user.AccountRole == 1)
                    {
                        return RedirectToAction("Index", "NewsArticles");
                    }

                    // lecturer
                    else if(user.AccountRole == 2)
                    {
                        return RedirectToAction("Index", "NewsArticles");
                    }
                }
            }

            return View();

            
        }
    }
}
