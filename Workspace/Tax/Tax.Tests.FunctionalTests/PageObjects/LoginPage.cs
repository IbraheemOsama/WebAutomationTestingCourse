using OpenQA.Selenium;

namespace Tax.Tests.FunctionalTests.PageObjects
{
    public class LoginPage : BasePage
    {
        protected const string EmailName = "Email";
        protected const string PasswordName = "Password";

        public LoginPage(IWebDriver driver) : base(driver)
        {
        }

        public string EmailTextBox
        {
            get
            {
                return Driver.FindElement(By.Name(EmailName)).Text;
            }
            set
            {
                var element = Driver.FindElement(By.Name(EmailName));
                element.Clear();
                element.SendKeys(value);
            }
        }

        public string PasswordTextBox
        {
            get
            {
                return Driver.FindElement(By.Name(PasswordName)).Text;
            }
            set
            {
                var element = Driver.FindElement(By.Name(PasswordName));
                element.Clear();
                element.SendKeys(value);
            }
        }

        public HomePage ClickLogin()
        {
            ClickSubmitButton();
            return new HomePage(Driver);
        }
    }
}
