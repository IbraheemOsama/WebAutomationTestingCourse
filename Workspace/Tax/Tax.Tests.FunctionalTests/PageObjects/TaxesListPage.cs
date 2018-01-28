using OpenQA.Selenium;

namespace Tax.Tests.FunctionalTests.PageObjects
{
    public class TaxesListPage : BasePage
    {
        public TaxesListPage(IWebDriver driver) : base(driver)
        {
        }

        public AddTaxPage ClickAddNewTax()
        {
            Driver.FindElement(By.ClassName("createNewUserAnchorSelector")).Click();
            return new AddTaxPage(Driver);
        }
    }
}
