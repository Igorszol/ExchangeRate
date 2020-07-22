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

namespace ExchangeRate.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient client = new HttpClient();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            ExchangeRates model = new ExchangeRates();
            //fetch data
            await GetRates(model,Currency.usd);
            await GetRates(model,Currency.eur);
            return View(model);
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


        public async Task GetRates(ExchangeRates exchangeRates, Currency code)
        {
            try
            {
                int howMany = 10;
                HttpResponseMessage response = await client.GetAsync("http://api.nbp.pl/api/exchangerates/rates/a/" + code.ToString() + "/last/"+howMany.ToString()+"/?format=json");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                List<Rate> rateList = (JObject.Parse(responseBody)["rates"]).ToObject<List<Rate>>();
                switch (code)
                {
                    case Currency.usd:
                        exchangeRates.usdRates = rateList;
                        exchangeRates.usdDiff = Math.Round((exchangeRates.usdRates[howMany-1].mid - exchangeRates.usdRates[howMany - 2].mid) / exchangeRates.usdRates[howMany - 2].mid*100,3);
                        break;
                    case Currency.eur:
                        exchangeRates.eurRates = rateList;
                        exchangeRates.eurDiff = Math.Round((exchangeRates.eurRates[howMany - 1].mid - exchangeRates.eurRates[howMany - 2].mid) / exchangeRates.eurRates[howMany - 2].mid*100,3);
                        break;
                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }

        }



    }
    public enum Currency {eur,usd};
}
