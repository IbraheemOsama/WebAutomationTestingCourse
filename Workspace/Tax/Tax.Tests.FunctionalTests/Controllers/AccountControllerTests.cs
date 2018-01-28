using System.Threading.Tasks;
using Tax.Data;
using Tax.Tests.FunctionalTests.Common;
using Xunit;

namespace Tax.Tests.FunctionalTests.Controllers
{
    public class AccountControllerTests : ServerFixture
    {
        [Fact]
        public async Task Login_SubmitValidData_UserLoggedIn()
        {
            //Arrange
            // Don't create users using UI unless it's part of your scenario. for performance
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

            var homePage = Driver.GetHomePage();
            var result = homePage.CurrentLoggedInUserText;

             Assert.Equal($"Hello {Email}!", result);
        }

        [Fact(Skip = "To be done by you, remove Skip property in the Fact attribute to be able to run this method and see it in text explorer")]
        public async Task Register_SubmitValidData_UserCreated()
        {
            // TODO by you :)
        }

        public override void Dispose()
        {
            // we can use different approach which is creating random different users then on all tests tearDown we delete the database or leave the framework to remove it.
            UserManager.DeleteAsync(User).Wait();
            base.Dispose();
        }
    }
}
