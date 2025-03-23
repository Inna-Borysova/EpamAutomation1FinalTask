using FinalTask.Pages;
using FluentAssertions;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;

namespace FinalTask
{
    [TestClass]
    public class LoginTests
    {
        private const string Url = "https://www.saucedemo.com/";

        private IWebDriver? _driver;

        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            string browser = GetBrowserFromRunSettings();
            _driver = CreateDriver(browser);
            var options = _driver.Manage();
            options.Window.Maximize();
            options.Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _driver?.Quit();
        }

        [TestMethod]
        public void Login_EmptyCredentials_UserNameErrorMessageDisplayed()
        {
            if (_driver == null)
            {
                Assert.Fail("Driver is null");
            }

            _driver.Navigate().GoToUrl(Url);
            LoginPage loginPage = new LoginPage(_driver);
            loginPage.TypeUserName("bob");
            loginPage.TypePassword("asdfghj");
            loginPage.ClearUserName();
            loginPage.ClearPassword();
            loginPage.ClickLoginButton();
            bool result = loginPage.IsUserNameErrorMessageDisplayed();
            result.Should().BeTrue();
        }

        [TestMethod]
        public void Login_EmptyPassword_PasswordErrorMessageDisplayed()
        {
            if (_driver == null)
            {
                Assert.Fail("Driver is null");
            }

            _driver.Navigate().GoToUrl(Url);
            LoginPage loginPage = new LoginPage(_driver);
            loginPage.TypeUserName("bob");
            loginPage.TypePassword("password");
            loginPage.ClearPassword();
            loginPage.ClickLoginButton();
            bool result = loginPage.IsPasswordErrorMessageDisplayed();
            result.Should().BeTrue();
        }

        [TestMethod]
        [DataRow("standard_user", "secret_sauce")]
        [DataRow("problem_user", "secret_sauce")]
        [DataRow("performance_glitch_user", "secret_sauce")]
        [DataRow("error_user", "secret_sauce")]
        [DataRow("visual_user", "secret_sauce")]
        public void Login_CorrectCredentials_InventoryHeaderTitleDisplayed(string userName, string password)
        {
            if (_driver == null)
            {
                Assert.Fail("Driver is null");
            }

            _driver.Navigate().GoToUrl(Url);
            LoginPage loginPage = new LoginPage(_driver);
            loginPage.TypeUserName(userName);
            loginPage.TypePassword(password);
            loginPage.ClickLoginButton();
            InventoryPage inventoryPage = new InventoryPage(_driver);
            bool result = inventoryPage.IsHeaderTitleDisplayed();
            result.Should().BeTrue();
        }

        private string GetBrowserFromRunSettings()
        {
            string? browser = TestContext.Properties["Browser"]?.ToString();
            return browser ?? "chrome";
        }

        private static IWebDriver CreateDriver(string browser)
        {
            switch (browser.ToLower())
            {
                case "chrome":
                    return new ChromeDriver();
                case "firefox":
                    return new FirefoxDriver();
                case "edge":
                    return new EdgeDriver();
                default:
                    throw new NotSupportedException($"Browser '{browser}' not supported.");
            }
        }
    }
}
