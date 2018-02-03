using OpenQA.Selenium;

namespace Tax.Tests.AcceptanceTests.PageObjects
{
    public class RegisterPage : LoginPage
    {
        protected const string ConfirmPasswordName = "ConfirmPassword";

        public string ConfirmPasswordTextBox
        {
            get
            {
                return Driver.FindElement(By.Name(ConfirmPasswordName)).Text;
            }
            set
            {
                var element = Driver.FindElement(By.Name(ConfirmPasswordName));
                element.Clear();
                element.SendKeys(value);
            }
        }
    }
}
