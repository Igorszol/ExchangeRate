using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ExchangeRate.Models;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using ExchangeRate.Services;

namespace ExchangeRate.Controllers
{
    public class HomeController : Controller
    {
        private FetchDataService _fetchDataService;

        public HomeController()
        {
            _fetchDataService = new FetchDataService();
        }

        public async Task<IActionResult> Index()
        {
            ExchangeRates model = new ExchangeRates();
            //fetch data
            await _fetchDataService.GetRates(model, Currency.usd);
            await _fetchDataService.GetRates(model, Currency.eur);
            if (model.eurRates == null || model.usdRates == null) return View("Error");
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }
    }
}
