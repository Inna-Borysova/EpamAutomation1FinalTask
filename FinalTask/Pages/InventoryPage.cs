using OpenQA.Selenium;

namespace FinalTask.Pages
{
    public class InventoryPage : BasePage
    {
        private const string HeaderTitleText = "Swag Labs";

        private readonly By _headerTitle = By.XPath("//*[@id='header_container']/div[@class='primary_header' and @data-test='primary-header']//div[@class='header_label']//div[@class='app_logo']");

        public InventoryPage(IWebDriver driver) : base(driver)
        {
        }

        public bool IsHeaderTitleDisplayed()
        {
            return IsElementDisplayedAndWithText(_headerTitle, HeaderTitleText);
        }
    }
}
