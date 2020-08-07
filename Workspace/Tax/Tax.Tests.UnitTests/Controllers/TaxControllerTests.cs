using System;
using System.Security.Claims;
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

            Assert.True(false);
            Assert.True(taxController.ModelState.IsValid);
            Assert.IsType<ViewResult>(result);
            Assert.IsNotType<LocalRedirectResult>(result);
        }

        [Fact]
        public async Task GetTax_ValidData_ReturnsViewResults()
        {
            // TODO try to stub all dependencies inside GetTax Action inside Tax Controller :)
            var userManagerStub = Substitute.For<UserManager<ApplicationUser>>(Substitute.For<IUserStore<ApplicationUser>>(), null, null, null, null, null, null, null, null);
            var logger = Substitute.For<ILogger<TaxController>>();
            var userTaxRepo = Substitute.For<IUserTaxRepository>();
            var taxService = Substitute.For<ITaxService>();
            var taxController = new TaxController(userManagerStub, logger, userTaxRepo, taxService);
            var claimsPrincipal = Substitute.For<ClaimsPrincipal>();
            ApplicationUser user = new ApplicationUser
            {
                UserName = "test@test.it",
                Id = Guid.NewGuid().ToString(),
                Email = "test@test.it"
            };
            _ = userManagerStub.GetUserAsync(claimsPrincipal).Returns(user);
            const int year = 2000;
            var userTax = new UserTax
            {
                CharityPaidAmount = 10,
                NumberOfChildren = 0,
                TaxDueAmount = 10,
                TotalIncome = 3000,
                Year = 2000
            };
            _ = userTaxRepo.GetUserTax(user.Id, year).Returns(userTax);

            //var taxViewResult = new TaxViewModel
            //{
            //    CharityPaidAmount = userTax.CharityPaidAmount,
            //    NumberOfChildren = userTax.NumberOfChildren,
            //    TotalIncome = userTax.TotalIncome,
            //    Year = userTax.Year,
            //    TaxDueAmount = userTax.TaxDueAmount
            //};

            //I think we don't need to use taxViewResult and i want to know diff btn .Received() and .Returns()
            var result = await taxController.GetTax(year);
            Assert.IsType<ViewResult>(result);
        }
    }
}
