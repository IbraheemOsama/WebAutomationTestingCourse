using System;

namespace Tax.Core
{
    public class TaxService : ITaxService
    {
        private readonly IYearService _yearService;
        public TaxService(IYearService yearService)
        {
            _yearService = yearService;
        }
        public decimal CalculateTax(int year, short numberOfChildren, decimal charityPaidAmount, decimal totalIncome)
        {
            if (year == 0)
            {
                throw new BusinessRuleException(Constants.YearZeroMessage);
            }

            var taxAmount = _yearService.GetYearPercentage(year) * (totalIncome - charityPaidAmount);

            if (numberOfChildren > 2)
            {
                return (taxAmount / numberOfChildren * 2);
            }
            return taxAmount;
        }
    }
}
