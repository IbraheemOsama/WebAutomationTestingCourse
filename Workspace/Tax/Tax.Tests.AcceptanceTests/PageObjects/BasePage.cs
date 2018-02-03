using OpenQA.Selenium;

namespace Tax.Tests.AcceptanceTests.PageObjects
{
    public abstract class BasePage
    {
        protected IWebDriver Driver => TestFixture.Driver;
        public const string LeftTabXPath = "//ul[@class='nav navbar-nav']//li";
        public const string RightTabXPath = "//ul[@class='nav navbar-nav navbar-right']//li";
        private const string SubmitButton = "//button[@type='submit' and @class='btn btn-default']";

        public string HomeTabText => Driver.FindElement(By.XPath($"{LeftTabXPath}[1]//a")).Text;

        public string MyTaxesText => Driver.FindElement(By.XPath($"{LeftTabXPath}[2]//a")).Text;

        public string CurrentLoggedInUserText => Driver.FindElement(By.XPath($"{RightTabXPath}[1]//a")).Text;

        public int TabsCount => Driver.FindElements(By.XPath(LeftTabXPath)).Count;

        public void ClickSubmitButton()
        {
            Driver.FindElement(By.XPath(SubmitButton)).Click();
        }

        public void NavigateToHomePage()
        {
            Driver.FindElement(By.XPath($"{LeftTabXPath}[1]//a")).Click();
        }

        public void NavigateToTaxPage()
        {
            Driver.FindElement(By.XPath($"{LeftTabXPath}[2]//a")).Click();
        }

        public void NavigateToRegisterPage()
        {
            Driver.FindElement(By.XPath($"{RightTabXPath}[1]//a")).Click();
        }

        public void NavigateToLoginPage()
        {
            Driver.FindElement(By.XPath($"{RightTabXPath}[2]//a")).Click();
        }
    }
}
