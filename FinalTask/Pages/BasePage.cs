using OpenQA.Selenium;

namespace FinalTask.Pages
{
    public abstract class BasePage
    {
        protected readonly IWebDriver _driver;

        public BasePage(IWebDriver driver)
        {
            _driver = driver;
        }

        protected void ClearTextInput(By by)
        {
            IWebElement element = _driver.FindElement(by);
            element.SendKeys($"{Keys.Control}a");
            element.SendKeys(Keys.Backspace);
        }

        protected bool IsElementDisplayedAndContainsText(By by, string text)
        {
            IWebElement element = _driver.FindElement(by);

            if (!element.Displayed)
            {
                return false;
            }

            return element.Text.Contains(text, StringComparison.InvariantCultureIgnoreCase);
        }

        protected bool IsElementDisplayedAndWithText(By by, string text)
        {
            IWebElement element = _driver.FindElement(by);

            if (!element.Displayed)
            {
                return false;
            }

            return element.Text.Equals(text, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
