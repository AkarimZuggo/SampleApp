using WebApp.Controllers.Base;
using WebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Common.Logging;

namespace WebApp.Controllers
{
    [Authorize]
    public class HomeController : AppBaseWebController
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var userId = UserId;
            //var name = Name;
            var emailAddress = EmailAddress;
            var role = Role;
            return View();
        }
        public async Task<IActionResult> GetException()
        {
            try
            {
                throw new Exception("Log Test From WebApp");
            }
            catch (Exception ex)
            {
                AppLogging.LogException(ex.Message);
            }
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
    }
}
