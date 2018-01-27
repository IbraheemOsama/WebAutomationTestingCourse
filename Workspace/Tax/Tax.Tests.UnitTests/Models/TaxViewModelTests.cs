using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Tax.Web.Models;
using Xunit;

namespace Tax.Tests.UnitTests.Models
{
    public class TaxViewModelTests
    {
        [Fact]
        public void AddTax_InvalidInput_ThrowValidation()
        {
            // Arrange
            var model = new TaxViewModel();
            var context = new ValidationContext(model, null, null);
            var result = new List<ValidationResult>();

            // Act
            var valid = Validator.TryValidateObject(model, context, result, true);

            // Assert
            Assert.False(valid);
            Assert.Equal(4, result.Count);
            Assert.Contains(result, i => i.ErrorMessage == "The Tax Year field is required.");
            Assert.Contains(result, i => i.ErrorMessage == "The Total Income field is required.");
            Assert.Contains(result, i => i.ErrorMessage == "The Charity Paid Amount field is required.");
            Assert.Contains(result, i => i.ErrorMessage == "The Number Of Children field is required.");
        }

        [Fact]
        public void AddTax_InvalidYearInput_ThrowValidation()
        {
            // Arrange
            var model = new TaxViewModel
            {
                Year = 0,
                CharityPaidAmount = 1,
                NumberOfChildren = 1,
                TotalIncome = 1
            };

            var context = new ValidationContext(model, null, null);
            var result = new List<ValidationResult>();

            // Act
            var valid = Validator.TryValidateObject(model, context, result, true);

            // Assert
            Assert.False(valid);
            Assert.Single(result);
            Assert.Contains(result, i => i.ErrorMessage == "The field Tax Year must be between 2000 and 2020.");
        }
    }
}
