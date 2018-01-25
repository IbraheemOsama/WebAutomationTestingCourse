using System.Net;
using System.Threading.Tasks;
using Tax.Tests.Common;
using Xunit;

namespace Tax.Tests.IntegrationTests.Controllers
{
    public class HomeControllerTests : ServerFixture
    {
        [Fact]
        public async Task PassingTest()
        {
            var response = await Client.GetAsync("/");
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.False(string.IsNullOrEmpty(responseString));
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
