using OpenQA.Selenium;

namespace FinalTask
{
    public interface IWaitStrategy
    {
        void WaitForElement(IWebDriver driver, By locator);
    }
}
