using OpenQA.Selenium;

namespace Tax.Tests.AcceptanceTests.PageObjects
{
    public class TaxesListPage : BasePage
    {
        public AddTaxPage ClickAddNewTax()
        {
            Driver.FindElement(By.ClassName("createNewUserAnchorSelector")).Click();
            return new AddTaxPage();
        }
    }
}
