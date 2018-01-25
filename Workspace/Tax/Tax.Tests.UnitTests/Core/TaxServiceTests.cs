using NSubstitute;
using Tax.Core;
using Xunit;

namespace Tax.Tests.UnitTests.Core
{
    //[UnitOfWorkName]_[ScenarioUnderTest]_[ExpectedBehavior]
    public class TaxServiceTests
    {
        private readonly IYearService _stub;
        public TaxServiceTests()
        {
            _stub = Substitute.For<IYearService>();
        }

        [Fact]
        public void IsValidInput_BadData_throwException()
        {
            // Arrange
            const int year = 0;
            _stub.GetYearPercentage(year).Returns(10);

            // Act
            var uut = new TaxService(_stub);

            var result = Assert.Throws<BusinessRuleException>(() => uut.CalculateTax(year, 2, 10, 1000));

            //Assert
            Assert.Equal(Constants.YearZeroMessage, result.Message);
        }

        [Fact]
        public void CalculateCorrectly_ValidData_Success()
        {
            // Arrange
            const int year = 2000;
            _stub.GetYearPercentage(year).Returns(10);

            // Act
            var uut = new TaxService(_stub);
            var result = uut.CalculateTax(year, 2, 10, 1000);

            //Assert
            Assert.Equal(9900, result);
        }

        [Fact]
        public void CalculateCorrectly_Year2000NumChild3_Success()
        {
            // Arrange
            const int year = 2000;
            _stub.GetYearPercentage(year).Returns(10);

            // Act
            var uut = new TaxService(_stub);
            var result = uut.CalculateTax(year, 3, 10, 1000);

            //Assert
            Assert.Equal(6600, result);
        }

        [Fact]
        public void YearServiceGetCalled_ValidateCall_MethodCalledSuccessfully()
        {
            // Arrange
            const int year = 2000;
            var mock = Substitute.For<IYearService>();

            // Act
            var uut = new TaxService(mock);
            uut.CalculateTax(year, 3, 10, 1000);

            //Assert
            mock.Received().GetYearPercentage(year);
        }
    }
}
