using System.Linq;
using OpenQA.Selenium;
using Tax.Tests.FunctionalTests.PageObjects;
using Xunit;

namespace Tax.Tests.FunctionalTests.Controllers
{
    //[UnitOfWorkName]_[ScenarioUnderTest]_[ExpectedBehavior]
    public class HomeControllerTests : ServerFixture
    {
        [Fact]
        public void HomePage_LandingPage_DisplayTabsAndText()
        {
            // Arrange
            var parentNavBar = Driver.FindElement(By.ClassName("navbar-nav"));
            var tabs = parentNavBar.FindElements(By.XPath("li//a"));

            // Assert
            Assert.Equal(2, tabs.Count);
            Assert.Equal("Home", tabs.First().Text);
            Assert.Equal("My Taxes", tabs.ElementAt(1).Text);
        }

        [Fact]
        public void HomePageObject_LandingPage_DisplayTabsAndText()
        {
            // Arrange
            var homePage = new HomePage(Driver);

            // Assert
            Assert.Equal(2, homePage.TabsCount);
            Assert.Equal("Home", homePage.HomeTabText);
            Assert.Equal("My Taxes", homePage.MyTaxesText);
        }
    }
}
