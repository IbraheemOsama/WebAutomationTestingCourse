using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Tax.Tests.Common;
using Microsoft.Extensions.DependencyInjection;
using Tax.Data;
using Tax.Repository;
using Tax.Web.Models;
using Xunit;

namespace Tax.Tests.IntegrationTests.Controllers
{
    //[UnitOfWorkName]_[ScenarioUnderTest]_[ExpectedBehavior]
    public class TaxControllerTests : ServerFixture
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserTaxRepository _userTaxRepository;
        private const string Email = "ibraheem.osama@gmail.com";
        private const string Password = "P@ssw0rd";

        public TaxControllerTests()
        {
            _userManager = ServiceProvider.GetService<UserManager<ApplicationUser>>();
            _userTaxRepository = ServiceProvider.GetService<IUserTaxRepository>();
        }

        [Fact]
        public async Task Registration_CreateNewUser_UserCreated()
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

            await _userManager.CreateAsync(new ApplicationUser
            {
                Email = Email,
                UserName = Email
            }, Password);

            var response = await PostAsync("/Tax/AddTax", taxViewModelData, Email, Password);

            var result = await response.Content.ReadAsStringAsync();

            var user = GetCurrentUser();
            var taxResult = await _userTaxRepository.GetUserTax(user.Id, taxViewModel.Year);

            //Assert
            Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
            Assert.NotNull(taxResult);

            // making sure the DB is retreving correctly
            Assert.Equal(taxViewModel.TotalIncome, taxResult.TotalIncome);
            Assert.Equal(taxViewModel.CharityPaidAmount, taxResult.CharityPaidAmount);
            Assert.Equal(taxViewModel.NumberOfChildren, taxResult.NumberOfChildren);
            Assert.Equal(taxViewModel.Year, taxResult.Year);

            // making sure calcuation with db integration is working correctly
            Assert.Equal((decimal)149.85, taxResult.TaxDueAmount);
        }

        private ApplicationUser GetCurrentUser()
        {
            return _userManager.Users.FirstOrDefault(x => x.Email == Email);
        }

        public override void Dispose()
        {
            var context = ServiceProvider.GetService<TaxDbContext>();

            // should be replaced with truncate script for performance.
            context.UserTaxes.RemoveRange(context.UserTaxes.ToArray());

            _userManager.DeleteAsync(GetCurrentUser()).Wait();
            base.Dispose();
        }
    }
}
