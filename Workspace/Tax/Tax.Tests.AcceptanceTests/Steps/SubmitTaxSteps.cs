using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tax.Tests.AcceptanceTests.PageObjects;
using TechTalk.SpecFlow;

namespace Tax.Tests.AcceptanceTests.Steps
{
    [Binding]
    public class SubmitTaxSteps
    {
        [Given(@"login with user")]
        public void GivenLoginWithUser(Table table)
        {
            var loginPage = new LoginPage();
            loginPage.NavigateToLoginPage();
            var email = table.Rows[0]["email"];
            var password = table.Rows[0]["password"];
            loginPage.EmailTextBox = email;
            loginPage.PasswordTextBox = password;

            loginPage.ClickSubmitButton();
        }

        [Given(@"I provide the following tax details")]
        public void GivenIProvideTheFollowingTaxDetails(Table table)
        {
            var taxListPage = new TaxesListPage();
            taxListPage.NavigateToTaxPage();
            var addTaxPage = taxListPage.ClickAddNewTax();

            addTaxPage.YearTextBox = table.Rows[0]["year"];
            addTaxPage.TotalIncomeTextBox = table.Rows[0]["totalIncome"];
            addTaxPage.CharityPaidAmountTextBox = table.Rows[0]["charityPaidAmount"];
            addTaxPage.NumberOfChildrenTextBox = table.Rows[0]["numberOfChildren"];
        }

        [When(@"submit the data")]
        public void WhenSubmitTheData()
        {
            var addTaxPage = new AddTaxPage();
            addTaxPage.SubmitTax();
        }

        [Then(@"Tax due amount should be")]
        public void ThenTaxDueAmountShouldBe(Table table)
        {
            var getTaxPage = new GetTaxPage();
            Assert.AreEqual(table.Rows[0]["taxDueAmount"], getTaxPage.TaxDueAmountDisplay);
        }
    }
}
