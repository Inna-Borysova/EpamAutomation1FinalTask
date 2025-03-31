using OpenQA.Selenium;

namespace FinalTask
{
    public class WebDriverBuilder
    {
        private string _browser = "chrome";

        private bool _maximize = true;

        private TimeSpan _implicitWait = TimeSpan.FromSeconds(10);

        public WebDriverBuilder WithBrowser(string browser)
        {
            _browser = browser;

            return this;
        }

        public WebDriverBuilder WithMaximize(bool maximize)
        {
            _maximize = maximize; 
            
            return this;
        }

        public WebDriverBuilder WithImplicitWait(TimeSpan wait)
        {
            _implicitWait = wait;

            return this;
        }

        public IWebDriver Build()
        {
            IWebDriver driver = new WebDriverFactory().CreateDriver(_browser);

            driver.Manage().Timeouts().ImplicitWait = _implicitWait;

            if (_maximize)
            {
                driver.Manage().Window.Maximize();
            }

            return driver;
        }
    }
}
