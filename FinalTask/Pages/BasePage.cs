using log4net;
using OpenQA.Selenium;

namespace FinalTask.Pages
{
    public abstract class BasePage
    {
        protected readonly IWebDriver _driver;
        protected readonly IWaitStrategy _waitStrategy;
        protected readonly ILog _logger;

        public BasePage(IWebDriver driver, IWaitStrategy waitStrategy, ILog logger)
        {
            _driver = driver;
            _waitStrategy = waitStrategy;
            _logger = logger;
        }

        protected void ClearTextInput(By by)
        {
            _logger.Debug($"Clearing input {by}");
            _waitStrategy.WaitForElement(_driver, by);
            IWebElement element = _driver.FindElement(by);
            element.SendKeys($"{Keys.Control}a");
            element.SendKeys(Keys.Backspace);
        }

        protected void TypeText(By by, string text)
        {
            _logger.Debug($"Typing text {by}");
            _waitStrategy.WaitForElement(_driver, by);
            IWebElement element = _driver.FindElement(by);
            element.SendKeys(text);
        }

        protected void Click(By by)
        {
            _logger.Debug($"Clicking {by}");
            _waitStrategy.WaitForElement(_driver, by);
            _driver.FindElement(by).Click();
        }

        protected bool IsElementDisplayedAndContainsText(By by, string text)
        {
            _waitStrategy.WaitForElement(_driver, by);

            IWebElement element = _driver.FindElement(by);

            return element.Text.Contains(text, StringComparison.InvariantCultureIgnoreCase);
        }

        protected bool IsElementDisplayedAndWithText(By by, string text)
        {
            _waitStrategy.WaitForElement(_driver, by);

            IWebElement element = _driver.FindElement(by);

            return element.Text.Equals(text, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
