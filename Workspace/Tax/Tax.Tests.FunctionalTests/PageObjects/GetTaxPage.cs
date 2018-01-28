using OpenQA.Selenium;
using Tax.Tests.FunctionalTests.Common;

namespace Tax.Tests.FunctionalTests.PageObjects
{
    public class GetTaxPage : BasePage
    {
        public const string DisplayValueXPath = "//div[@class='form-group'][{0}]//span";
        public GetTaxPage(IWebDriver driver) : base(driver)
        {
        }

        public string YearDisplay => Driver.FindElementWithTimeout(By.XPath(string.Format(DisplayValueXPath, 1))).Text;

        public string TotalIncomeDisplay => Driver.FindElementWithTimeout(By.XPath(string.Format(DisplayValueXPath, 2))).Text;

        public string CharityPaidAmountDisplay => Driver.FindElementWithTimeout(By.XPath(string.Format(DisplayValueXPath, 3))).Text;

        public string NumberOfChildrenDisplay => Driver.FindElementWithTimeout(By.XPath(string.Format(DisplayValueXPath, 4))).Text;

        public string TaxDueAmountDisplay => Driver.FindElementWithTimeout(By.XPath(string.Format(DisplayValueXPath, 5))).Text;
    }
}
