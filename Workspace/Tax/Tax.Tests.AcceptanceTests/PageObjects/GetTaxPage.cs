using OpenQA.Selenium;

namespace Tax.Tests.AcceptanceTests.PageObjects
{
    public class GetTaxPage : BasePage
    {
        public const string DisplayValueXPath = "//div[@class='form-group'][{0}]//span";
      
        public string YearDisplay => Driver.FindElement(By.XPath(string.Format(DisplayValueXPath, 1))).Text;

        public string TotalIncomeDisplay => Driver.FindElement(By.XPath(string.Format(DisplayValueXPath, 2))).Text;

        public string CharityPaidAmountDisplay => Driver.FindElement(By.XPath(string.Format(DisplayValueXPath, 3))).Text;

        public string NumberOfChildrenDisplay => Driver.FindElement(By.XPath(string.Format(DisplayValueXPath, 4))).Text;

        public string TaxDueAmountDisplay => Driver.FindElement(By.XPath(string.Format(DisplayValueXPath, 5))).Text;
    }
}
