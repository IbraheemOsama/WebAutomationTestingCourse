using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Tax.Tests.Common;
using Microsoft.Extensions.DependencyInjection;
using Tax.Data;
using Tax.Web.Models.AccountViewModels;
using Xunit;

namespace Tax.Tests.IntegrationTests.Controllers
{
    //[UnitOfWorkName]_[ScenarioUnderTest]_[ExpectedBehavior]
    public class AccountControllerTests : ServerFixture
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private const string Email = "ibraheem.osama@gmail.com";
        private const string Password = "P@ssw0rd";

        public AccountControllerTests()
        {
            _userManager = ServiceProvider.GetService<UserManager<ApplicationUser>>();
        }

        [Fact]
        public async Task Registration_CreateNewUser_UserCreated()
        {
            // Arrange
            var formData = new RegisterViewModel
            {
                Email = Email,
                Password = Password,
                ConfirmPassword = Password
            }.ToDictionary();

            // Act
            HttpResponseMessage response = await PostAsync("/Account/Register", formData);
            var user = GetCurrentUser();

            //Assert
            Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
            Assert.NotNull(user);
            Assert.Equal(Email, user.UserName);
        }

        [Fact]
        public async Task Login_RegisteredUser_AbleToLogin()
        {
            // Arrange
            var registerData = new RegisterViewModel
            {
                Email = Email,
                Password = Password,
                ConfirmPassword = Password
            }.ToDictionary();

            var loginData = new LoginViewModel
            {
                Email = Email,
                Password = Password
            }.ToDictionary();

            // Act
            await PostAsync("/Account/Register", registerData);
            var response = await PostAsync("/Account/Login", loginData);

            //Assert
            Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
        }

        private ApplicationUser GetCurrentUser()
        {
            return _userManager.Users.FirstOrDefault(x => x.Email == Email);
        }

        public override void Dispose()
        {
            _userManager.DeleteAsync(GetCurrentUser()).Wait();
            base.Dispose();
        }
    }
}
