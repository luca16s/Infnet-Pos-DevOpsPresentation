using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using DeadFishStudio.InfnetDevOps.ApiConnectionFactory;
using Microsoft.AspNetCore.Mvc;
using DeadFishStudio.InfnetDevOps.Presentation.Models;

namespace DeadFishStudio.InfnetDevOps.Presentation.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient("B");

            var result = await client.GetStringAsync("api/Products/");

            return Ok(result);
            //return View();
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
