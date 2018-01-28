using System;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Tax.Tests.FunctionalTests.PageObjects;

namespace Tax.Tests.FunctionalTests.Common
{
    public static class DriverExt
    {
        public static HomePage NavigateToHomePage(this IWebDriver driver)
        {
            driver.FindElement(By.XPath($"{BasePage.LeftTabXPath}[1]//a")).Click();
            return new HomePage(driver);
        }

        public static TaxesListPage NavigateToTaxPage(this IWebDriver driver)
        {
            driver.FindElement(By.XPath($"{BasePage.LeftTabXPath}[2]//a")).Click();
            return new TaxesListPage(driver);
        }

        public static RegisterPage NavigateToRegisterPage(this IWebDriver driver)
        {
            driver.FindElement(By.XPath($"{BasePage.RightTabXPath}[1]//a")).Click();
            return new RegisterPage(driver);
        }

        public static LoginPage NavigateToLoginPage(this IWebDriver driver)
        {
            driver.FindElement(By.XPath($"{BasePage.RightTabXPath}[2]//a")).Click();
            return new LoginPage(driver);
        }

        public static HomePage GetHomePage(this IWebDriver driver)
        {
            return new HomePage(driver);
        }

        public static IWebElement FindElementWithTimeout(this IWebDriver driver, By by, int timeoutInSeconds = 3)
        {
            if (timeoutInSeconds > 0)
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
                return wait.Until(drv => drv.FindElement(by));
            }
            return driver.FindElement(by);
        }
    }
}
