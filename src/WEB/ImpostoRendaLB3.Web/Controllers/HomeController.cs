using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ImpostoRendaLB3.Web.Models;
using ImpostoRendaLB3.Web.Interfaces;

namespace ImpostoRendaLB3.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IImpostoService impostoService;
        public HomeController(IImpostoService impostoService)
        {
            this.impostoService = impostoService;
        }
        public IActionResult Index()
        {
            return View(new ImpostoRequest());
        }
        [HttpPost]
        public async Task<IActionResult> Index(ImpostoRequest impostoRequest)
        {

            var result = await impostoService.CalcImpostoAsync(impostoRequest);
            ViewBag.Result = result;
            return View(impostoRequest);
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
