using System.Net;
using System.Threading.Tasks;
using Tax.Tests.Common;
using Tax.Data;
using Tax.Web.Models;
using Xunit;

namespace Tax.Tests.IntegrationTests.Controllers
{
    //[UnitOfWorkName]_[ScenarioUnderTest]_[ExpectedBehavior]
    public class TaxControllerTests : ServerFixture
    {
        private const string AddTaxUrl = "/Tax/AddTax";

        [Fact]
        public async Task AddTax_SubmitTax_DataCreated()
        {
            // Arrange
            var taxViewModel = new TaxViewModel
            {
                CharityPaidAmount = 10,
                NumberOfChildren = 1,
                TotalIncome = 10000,
                Year = 2000
            };
            var taxViewModelData = taxViewModel.ToDictionary();

            // Act

            await UserManager.CreateAsync(new ApplicationUser
            {
                Email = Email,
                UserName = Email
            }, Password);

            var response = await PostAsync(AddTaxUrl, taxViewModelData, Email, Password);

            var taxResult = await UserTaxRepository.GetUserTax(User.Id, taxViewModel.Year.Value);

            //Assert
            Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
            Assert.NotNull(taxResult);

            // making sure the DB is retreving correctly
            Assert.Equal(taxViewModel.TotalIncome.Value, taxResult.TotalIncome);
            Assert.Equal(taxViewModel.CharityPaidAmount.Value, taxResult.CharityPaidAmount);
            Assert.Equal(taxViewModel.NumberOfChildren.Value, taxResult.NumberOfChildren);
            Assert.Equal(taxViewModel.Year.Value, taxResult.Year);

            // making sure calcuation with db integration is working correctly
            Assert.Equal((decimal)149.85, taxResult.TaxDueAmount);
        }

        // Jump to unit tests for controller tests
        [Fact]
        public async Task AddTax_SubmitInvalidYear_ThrowValidation()
        {
            // Arrange
            var taxViewModel = new TaxViewModel
            {
                CharityPaidAmount = 10,
                NumberOfChildren = 1,
                TotalIncome = 10000,
                Year = 0
            };
            var taxViewModelData = taxViewModel.ToDictionary();

            // Act
            await UserManager.CreateAsync(new ApplicationUser
            {
                Email = Email,
                UserName = Email
            }, Password);

            var response = await PostAsync(AddTaxUrl, taxViewModelData, Email, Password);
            var responseContent = await response.Content.ReadAsStringAsync();
            var taxResult = await UserTaxRepository.GetUserTax(User.Id, taxViewModel.Year.Value);

            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            // checking no data is inserted
            Assert.Null(taxResult);
            Assert.Contains("The field Tax Year must be between 2000 and 2020.", responseContent);
        }

        [Fact(Skip = "To be done by you, remove Skip property in the Fact attribute to be able to run this method and see it in text explorer")]
        public async Task GetTax_WhenInsertValidData_DataRetrieved()
        {
            // TODO as Home work ;)
        }
    }
}
