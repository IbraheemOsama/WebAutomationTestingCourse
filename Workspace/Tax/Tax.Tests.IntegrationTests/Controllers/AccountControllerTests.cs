using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Tax.Tests.Common;
using Microsoft.Extensions.DependencyInjection;
using Tax.Data;
using Xunit;

namespace Tax.Tests.IntegrationTests.Controllers
{
    //[UnitOfWorkName]_[ScenarioUnderTest]_[ExpectedBehavior]
    public class AccountControllerTests : ServerFixture
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public AccountControllerTests()
        {
            _userManager = ServiceProvider.GetService<UserManager<ApplicationUser>>();
        }

        [Fact]
        public async Task Registration_CreateNewUser_UserCreated()
        {
            // Arrange
            const string email = "ibraheem.osama@gmail.com";
            const string password = "P@ssw0rd";
            var formData = new Dictionary<string, string>
                  {
                    {"Email", email},
                    {"Password", password},
                    {"ConfirmPassword", password}
                  };

            // Act
            HttpResponseMessage response = await PostAsync("/Account/Register", formData);
            var user = _userManager.Users.FirstOrDefault(x => x.Email == email);

            //Assert
            Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
            Assert.NotNull(user);
            Assert.Equal(email, user.UserName);
        }
    }
}
