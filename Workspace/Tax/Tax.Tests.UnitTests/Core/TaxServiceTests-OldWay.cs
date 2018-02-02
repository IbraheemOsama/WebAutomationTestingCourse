using Tax.Core;
using Xunit;

namespace Tax.Tests.UnitTests.Core
{
    //[UnitOfWorkName]_[ScenarioUnderTest]_[ExpectedBehavior]
    public class TaxServiceOldWayTests
    {
        [Fact]
        public void CalculateCorrectly_ValidData_Success()
        {
            // Arrange
            const int year = 2000;
            var stub = new Stub();

            // Act
            var uut = new TaxService(stub);
            var result = uut.CalculateTax(year, 2, 10, 1000);

            //Assert
            Assert.Equal(9900, result);
        }

        class Stub : IYearService
        {
            public decimal GetYearPercentage(int year)
            {
                if (year == 2000)
                {
                    return 10;
                }
                
                return 5;
            }
        }

        [Fact]
        public void YearServiceGetCalled_ValidateCall_MethodCalledSuccessfully()
        {
            // Arrange
            const int year = 2000;
            var uut = new Mock();

            // Act
            var taxService = new TaxService(uut);
            taxService.CalculateTax(year, 3, 10, 1000);

            //Assert
            Assert.True(uut.IsCalled);
        }

        class Mock : IYearService
        {
            public bool IsCalled = false;

            public decimal GetYearPercentage(int year)
            {
                IsCalled = true;

                // Here I really don't care about the value 
                // I just care about the IsCalled
                return default(decimal);
            }
        }
    }
}
