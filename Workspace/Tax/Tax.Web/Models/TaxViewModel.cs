using System.ComponentModel.DataAnnotations;

namespace Tax.Web.Models
{
    public class TaxViewModel
    {
        [Required]
        [Display(Name = "Tax Year")]
        public int Year { get; set; }

        [Required]
        [Display(Name = "Total Income")]
        public decimal TotalIncome { get; set; }

        [Required]
        [Display(Name = "Charity Paid Amount")]
        public decimal CharityPaidAmount { get; set; }

        [Required]
        [Display(Name = "Number Of Children")]
        public short NumberOfChildren { get; set; }

        [Display(Name = "Tax Due Amount (What you should pay!)")]
        public decimal TaxDueAmount { get; set; }
    }
}
