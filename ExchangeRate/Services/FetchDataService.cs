using ExchangeRate.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace ExchangeRate.Services
{
    public class FetchDataService
    {
        private readonly HttpClient client = new HttpClient();
        public async Task GetRates(ExchangeRates exchangeRates, Currency code)
        {
            try
            {
                int howMany = 10;
                HttpResponseMessage response = await client.GetAsync("http://api.nbp.pl/api/exchangerates/rates/a/" + code.ToString() + "/last/" + howMany.ToString() + "/?format=json");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    List<Rate> rateList = (JObject.Parse(responseBody)["rates"]).ToObject<List<Rate>>();
                    switch (code)
                    {
                        case Currency.usd:
                            exchangeRates.usdRates = rateList;
                            exchangeRates.usdDiff = Math.Round((exchangeRates.usdRates[howMany - 1].mid - exchangeRates.usdRates[howMany - 2].mid) / exchangeRates.usdRates[howMany - 2].mid * 100, 3);
                            break;
                        case Currency.eur:
                            exchangeRates.eurRates = rateList;
                            exchangeRates.eurDiff = Math.Round((exchangeRates.eurRates[howMany - 1].mid - exchangeRates.eurRates[howMany - 2].mid) / exchangeRates.eurRates[howMany - 2].mid * 100, 3);
                            break;
                    }
                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }

        }

    }

    public enum Currency { eur, usd };
}
