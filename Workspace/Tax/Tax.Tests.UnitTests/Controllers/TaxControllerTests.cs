using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Tax.Core;
using Tax.Data;
using Tax.Repository;
using Tax.Web.Controllers;
using Tax.Web.Models;
using Xunit;

namespace Tax.Tests.UnitTests.Controllers
{
    public class TaxControllerTests
    {
        [Fact]
        public async Task AddTax_InvalidInput_ReturnsToSameActionWithValidation()
        {
            // why all these nulls !!
            var userManagerStub = Substitute.For<UserManager<ApplicationUser>>(Substitute.For<IUserStore<ApplicationUser>>(), null, null, null, null, null, null, null, null);

            var logger = Substitute.For<ILogger<TaxController>>();
            var userTaxRepo = Substitute.For<IUserTaxRepository>();
            var taxService = Substitute.For<ITaxService>();
            var taxController = new TaxController(userManagerStub, logger, userTaxRepo, taxService);
            taxController.ModelState.AddModelError("error", "error");
            var result = await taxController.AddTax(new TaxViewModel());
            Assert.False(taxController.ModelState.IsValid);
            Assert.IsType<ViewResult>(result);
            Assert.IsNotType<LocalRedirectResult>(result);
        }

        [Fact(Skip = "To be done by you, remove Skip property in the Fact attribute to be able to run this method and see it in text explorer")]
        public async Task GetTax_ValidData_ReturnsViewResults()
        {
            // TODO try to stub all dependencies inside GetTax Action inside Tax Controller :)
        }
    }
}
