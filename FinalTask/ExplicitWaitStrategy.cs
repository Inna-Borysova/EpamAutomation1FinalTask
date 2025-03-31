using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace FinalTask
{
    public class ExplicitWaitStrategy : IWaitStrategy
    {
        private readonly TimeSpan _timeout;

        public ExplicitWaitStrategy(TimeSpan timeout)
        {
            _timeout = timeout;
        }

        public void WaitForElement(IWebDriver driver, By locator)
        {
            var wait = new WebDriverWait(driver, _timeout);

            wait.Until(x => x.FindElement(locator).Displayed);
        }
    }
}
