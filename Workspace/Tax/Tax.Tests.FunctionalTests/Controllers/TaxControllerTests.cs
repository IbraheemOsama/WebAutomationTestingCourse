using System.Threading.Tasks;
using Tax.Data;
using Tax.Tests.FunctionalTests.Common;
using Xunit;

namespace Tax.Tests.FunctionalTests.Controllers
{
    public class TaxControllerTests : ServerFixture
    {
        [Fact]
        public async Task AddTaxGetTax_SubmitValidData_DataGetsDisplayedBack()
        {
            //Arrange
            const string year = "2000";
            const string totalIncome = "10000";
            const string charityPaidAmount = "10";
            const string numberOfChildren = "1";

            await UserManager.CreateAsync(new ApplicationUser
            {
                Email = Email,
                UserName = Email
            }, Password);

            // Act
            var loginPage = Driver.NavigateToLoginPage();
            loginPage.EmailTextBox = Email;
            loginPage.PasswordTextBox = Password;
            loginPage.ClickLogin();

            var taxesListPage = Driver.NavigateToTaxPage();
            var addTaxPage = taxesListPage.ClickAddNewTax();
            addTaxPage.YearTextBox = year;
            addTaxPage.TotalIncomeTextBox = totalIncome;
            addTaxPage.CharityPaidAmountTextBox = charityPaidAmount;
            addTaxPage.NumberOfChildrenTextBox = numberOfChildren;

            var getTaxPage = addTaxPage.SubmitTax();

            Assert.Equal(year, getTaxPage.YearDisplay);
            Assert.Equal(totalIncome, getTaxPage.TotalIncomeDisplay);
            Assert.Equal(charityPaidAmount, getTaxPage.CharityPaidAmountDisplay);
            Assert.Equal(numberOfChildren, getTaxPage.NumberOfChildrenDisplay);
            Assert.Equal("149.85", getTaxPage.TaxDueAmountDisplay);
        }
    }
}
