using Tax.Core;
using Xunit;

namespace Tax.Tests.UnitTests.Core
{
    //[UnitOfWorkName]_[ScenarioUnderTest]_[ExpectedBehavior]
    public class YearServiceTests
    {
        [Fact]
        public void GetYearPercentage_SendValidData_ReturnsResult()
        {
            // Arrange
            var yearService = new YearService();

            // Act
            var result = yearService.GetYearPercentage(2000);

            //Assert
            Assert.Equal((decimal)0.015, result);
        }

        [Theory]
        [InlineData(0.015, 2000)]
        [InlineData(0.025, 2016)]
        [InlineData(0.035, 2017)]
        [InlineData(0.045, 2018)]
        public void GetYearPercentage_SendValidManyData_ReturnsResult(decimal actual, int year)
        {
            // Arrange
            var yearService = new YearService();

            // Act
            var result = yearService.GetYearPercentage(year);

            //Assert
            Assert.Equal(actual, result);
        }
    }
}
