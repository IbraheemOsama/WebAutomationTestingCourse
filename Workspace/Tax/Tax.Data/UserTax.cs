namespace Tax.Data
{
    public class UserTax
    {
        public int Year { get; set; }
        public string UserId { get; set; }

        public decimal TotalIncome { get; set; }
        public decimal CharityPaidAmount { get; set; }
        public decimal TaxDueAmount { get; set; }
        public short NumberOfChildren { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
