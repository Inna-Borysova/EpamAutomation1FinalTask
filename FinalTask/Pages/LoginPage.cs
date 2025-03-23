using OpenQA.Selenium;

namespace FinalTask.Pages
{
    public class LoginPage : BasePage
    {
        private readonly By _userNameInput = By.Id("user-name");

        private readonly By _passwordInput = By.Id("password");

        private readonly By _loginButton = By.Id("login-button");

        private readonly By _errorMessage = By.XPath("//div[contains(@class, 'error-message-container')]/h3[@data-test='error']");

        public LoginPage(IWebDriver driver) : base(driver)
        {
        }

        public void TypeUserName(string userName)
        {
            _driver.FindElement(_userNameInput).SendKeys(userName);
        }

        public void TypePassword(string password)
        {
            _driver.FindElement(_passwordInput).SendKeys(password);
        }

        public void ClearUserName()
        {
            ClearTextInput(_userNameInput);
        }

        public void ClearPassword()
        {
            ClearTextInput(_passwordInput);
        }

        public void ClickLoginButton()
        {
            _driver.FindElement(_loginButton).Click();
        }

        public bool IsUserNameErrorMessageDisplayed()
        {
            return IsErrorMessageDisplayed("Username is required");
        }

        public bool IsPasswordErrorMessageDisplayed()
        {
            return IsErrorMessageDisplayed("Password is required");
        }

        private bool IsErrorMessageDisplayed(string text)
        {
            return IsElementDisplayedAndContainsText(_errorMessage, text);
        }
    }
}
