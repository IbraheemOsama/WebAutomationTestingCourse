using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tax.Tests.Common;
using Xunit;

namespace Tax.Tests.FunctionalTests
{
    public class HelloWorldTests : ServerFixture
    {
        [Fact]
        public async Task PassingTest()
        {
            //await Task.Delay(TimeSpan.FromSeconds(10));

            var response = await Client.GetAsync("/");
            var responseString = await response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode();
        }
    }
}
