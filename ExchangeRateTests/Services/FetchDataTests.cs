using Microsoft.VisualStudio.TestTools.UnitTesting;
using ExchangeRate.Services;
using ExchangeRate.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using System.Net.Http;

namespace ExchangeRateTests.Services
{
    [TestClass]
    public class FetchDataTests
    {
        FetchDataService _fetchData = new FetchDataService();

        [TestMethod]
        public async Task GetRates_HowManyRates_Correct()
        {
            //Given
            ExchangeRates rates = new ExchangeRates();
            Currency currency = Currency.eur;

            //When
             await _fetchData.GetRates(rates, currency);

            //Then
            Assert.AreEqual(rates.eurRates.Count, 10);
        }
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public async Task GetRates_NullArgument_ThrowException()
        {
            //Given
            Currency currency = Currency.usd;

            //When
            await _fetchData.GetRates(null, currency);

            //Then
            //Exception was thrown
        }
        [TestMethod]
        public async Task GetRates_WrongCurrency_Correct()
        {
            //Given
            ExchangeRates rates = new ExchangeRates();
            Currency currency = (Currency) 5;

            //When
            await _fetchData.GetRates(rates, currency);

            //Then
            Assert.IsNull(rates.usdRates);
        }
    }
}
