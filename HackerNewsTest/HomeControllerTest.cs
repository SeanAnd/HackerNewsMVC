using NUnit.Framework;
using HackerNewsMVC;
using HackerNewsMVC.Controllers;
using Microsoft.AspNetCore.Mvc.Core;

namespace HackerNewsTester
{
    [TestFixture]
    public class HomeController_Test
    {
        [Test]
        public void IndexTest()
        {
            HomeController hc = new HomeController();
            var result = hc.Index();

            Assert.IsNotNull(result);
        }

        [Test]
        public void DetailsTest()
        {
            HomeController hc = new HomeController();
            var result = hc.Details(1);

            Assert.IsNotNull(result);
        }
    }
}
