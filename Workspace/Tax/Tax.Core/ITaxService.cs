namespace Tax.Core
{
    public interface ITaxService
    {
        decimal CalculateTax(int year, short numberOfChildren, decimal charityPaidAmount, decimal totalIncome);
    }
}