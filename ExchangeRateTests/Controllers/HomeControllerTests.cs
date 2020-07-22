using Microsoft.VisualStudio.TestTools.UnitTesting;
using ExchangeRate.Controllers;
using ExchangeRate.Models;
using Microsoft.AspNetCore.Mvc;

namespace ExchangeRateTests.Controllers
{
    [TestClass]
    public class HomeControllerTests
    { 
        [TestMethod]
        public void Index_CheckModel_Correct()
        {
            //Given
            var controller = new HomeController();

            //When
            var result =controller.Index().Result as ViewResult;

            //Then
            Assert.IsTrue(result.Model is ExchangeRates);
        }

    }
}
