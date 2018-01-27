using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Tax.Tests.Common;
using Tax.Web.Models.AccountViewModels;
using Xunit;

namespace Tax.Tests.IntegrationTests.Controllers
{
    //[UnitOfWorkName]_[ScenarioUnderTest]_[ExpectedBehavior]
    public class AccountControllerTests : ServerFixture
    {
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

            //Assert
            Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
            Assert.NotNull(User);
            Assert.Equal(Email, User.UserName);
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

        public override void Dispose()
        {
            UserManager.DeleteAsync(User).Wait();
            base.Dispose();
        }
    }
}
