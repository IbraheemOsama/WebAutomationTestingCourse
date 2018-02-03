using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TechTalk.SpecFlow;

namespace Tax.Tests.AcceptanceTests
{
    [Binding]
    // تجهيزات
    public class TestFixture
    {
        internal static IWebDriver Driver;
        // Website Url
        private const string BaseAddress = "http://localhost:54860/";

        [BeforeScenario]
        public static void SetUp()
        {
            Driver = new ChromeDriver()
            {
                Url = BaseAddress
            };
        }

        [AfterScenario]
        public static void TearDown()
        {
            Driver.Dispose();
        }
    }
}
