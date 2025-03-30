using OpenQA.Selenium;

namespace FinalTask
{
    public interface IWebDriverFactory
    {
        IWebDriver CreateDriver(string browser);
    }
}
