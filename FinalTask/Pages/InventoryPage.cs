using log4net;
using OpenQA.Selenium;

namespace FinalTask.Pages
{
    public class InventoryPage : BasePage
    {
        private const string HeaderTitleText = "Swag Labs";

        private readonly By _headerTitle = By.CssSelector("div#header_container div.app_logo");

        public InventoryPage(IWebDriver driver, IWaitStrategy waitStrategy, ILog logger) : base(driver, waitStrategy, logger)
        {
        }

        public bool IsHeaderTitleDisplayed()
        {
            return IsElementDisplayedAndWithText(_headerTitle, HeaderTitleText);
        }
    }
}
