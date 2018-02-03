using OpenQA.Selenium;

namespace Tax.Tests.AcceptanceTests.PageObjects
{
    public class AddTaxPage : BasePage
    {
        private const string YearName = "Year";
        private const string TotalIncomeName = "TotalIncome";
        private const string CharityPaidAmountName = "CharityPaidAmount";
        private const string NumberOfChildrenName = "NumberOfChildren";
        
        public string YearTextBox
        {
            get
            {
                return Driver.FindElement(By.Name(YearName)).Text;
            }
            set
            {
                var element = Driver.FindElement(By.Name(YearName));
                element.Clear();
                element.SendKeys(value);
            }
        }

        public string TotalIncomeTextBox
        {
            get
            {
                return Driver.FindElement(By.Name(TotalIncomeName)).Text;
            }
            set
            {
                var element = Driver.FindElement(By.Name(TotalIncomeName));
                element.Clear();
                element.SendKeys(value);
            }
        }

        public string CharityPaidAmountTextBox
        {
            get
            {
                return Driver.FindElement(By.Name(CharityPaidAmountName)).Text;
            }
            set
            {
                var element = Driver.FindElement(By.Name(CharityPaidAmountName));
                element.Clear();
                element.SendKeys(value);
            }
        }

        public string NumberOfChildrenTextBox
        {
            get
            {
                return Driver.FindElement(By.Name(NumberOfChildrenName)).Text;
            }
            set
            {
                var element = Driver.FindElement(By.Name(NumberOfChildrenName));
                element.Clear();
                element.SendKeys(value);
            }
        }

        public void SubmitTax()
        {
            ClickSubmitButton();;
        }
    }
}
