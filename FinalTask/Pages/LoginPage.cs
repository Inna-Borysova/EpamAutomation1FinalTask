using log4net;
using OpenQA.Selenium;

namespace FinalTask.Pages
{
    public class LoginPage : BasePage
    {
        private readonly By _userNameInput = By.XPath("//input[@id='user-name']");

        private readonly By _passwordInput = By.XPath("//input[@id='password']");

        private readonly By _loginButton = By.XPath("//input[@id='login-button']");

        private readonly By _errorMessage = By.XPath("//div[contains(@class, 'error-message-container')]/h3[@data-test='error']");

        public LoginPage(IWebDriver driver, IWaitStrategy waitStrategy, ILog logger) : base(driver, waitStrategy, logger)
        {
        }

        public void TypeUserName(string userName)
        {
            _logger.Debug($"Typing username {userName}");
            TypeText(_userNameInput, userName);
        }

        public void TypePassword(string password)
        {
            _logger.Debug($"Typing password {password}");
            TypeText(_passwordInput, password);
        }

        public void ClearUserName()
        {
            _logger.Debug($"Clearing username");
            ClearTextInput(_userNameInput);
        }

        public void ClearPassword()
        {
            _logger.Debug($"Clearing password");
            ClearTextInput(_passwordInput);
        }

        public void ClickLoginButton()
        {
            _logger.Debug($"Clicking login");
            Click(_loginButton);
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
