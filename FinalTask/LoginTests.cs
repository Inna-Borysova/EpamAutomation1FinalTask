using FinalTask.Pages;
using FluentAssertions;
using log4net;
using log4net.Config;
using OpenQA.Selenium;

[assembly: Parallelize(Workers = 2, Scope = ExecutionScope.MethodLevel)]
namespace FinalTask
{
    [TestClass]
    public class LoginTests
    {
        private const string Url = "https://www.saucedemo.com/";

        private static readonly ILog Logger = LogManager.GetLogger(typeof(LoginTests));
        private static IWebDriverFactory? _driverFactory;

        private IWebDriver? _driver;

        public TestContext TestContext { get; set; }

        [AssemblyInitialize]
        public static void AssemblyInitialize(TestContext context)
        {
            var config = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "App.config");
            XmlConfigurator.Configure(config);
            Logger.Info("Logger configured");
        }

        [ClassInitialize]
        public static void ClassInitialize(TestContext testContext)
        {
            _driverFactory = new WebDriverFactory();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            Logger.Info($"{nameof(TestCleanup)} start");

            try
            {
                if (_driver == null)
                {
                    Logger.Warn($"{nameof(TestCleanup)} Driver is null");
                }
                else
                {
                    _driver.Close();
                    _driver.Quit();
                    _driver.Dispose();
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"{nameof(TestCleanup)} error", ex);
            }
            finally
            {
                Logger.Info($"{nameof(TestCleanup)} end");
            }
        }

        [TestMethod]
        [DataRow("chrome")]
        [DataRow("firefox")]
        public void Login_EmptyCredentials_UserNameErrorMessageDisplayed(string browser)
        {
            Logger.Info($"{nameof(Login_EmptyCredentials_UserNameErrorMessageDisplayed)} {browser} start");

            try
            {
                _driver = CreateDriver(browser);
                _driver.Navigate().GoToUrl(Url);
                LoginPage loginPage = new LoginPage(_driver);
                loginPage.TypeUserName("bob");
                loginPage.TypePassword("asdfghj");
                loginPage.ClearUserName();
                loginPage.ClearPassword();
                loginPage.ClickLoginButton();
                loginPage.IsUserNameErrorMessageDisplayed().Should().BeTrue();
            }
            catch (Exception ex)
            {
                Logger.Error($"{nameof(Login_EmptyCredentials_UserNameErrorMessageDisplayed)} {browser} error", ex);
            }
            finally
            {
                Logger.Info($"{nameof(Login_EmptyCredentials_UserNameErrorMessageDisplayed)} {browser} end");
            }
        }

        [TestMethod]
        [DataRow("chrome")]
        [DataRow("firefox")]
        public void Login_EmptyPassword_PasswordErrorMessageDisplayed(string browser)
        {
            Logger.Info($"{nameof(Login_EmptyPassword_PasswordErrorMessageDisplayed)} {browser} start");

            try
            {
                _driver = CreateDriver(browser);
                _driver.Navigate().GoToUrl(Url);
                LoginPage loginPage = new LoginPage(_driver);
                loginPage.TypeUserName("bob");
                loginPage.TypePassword("password");
                loginPage.ClearPassword();
                loginPage.ClickLoginButton();
                loginPage.IsPasswordErrorMessageDisplayed().Should().BeTrue();
            }
            catch (Exception ex)
            {
                Logger.Error($"{nameof(Login_EmptyPassword_PasswordErrorMessageDisplayed)} {browser} error", ex);
            }
            finally
            {
                Logger.Info($"{nameof(Login_EmptyPassword_PasswordErrorMessageDisplayed)} {browser} end");
            }
        }

        [TestMethod]
        [DataRow("chrome", "standard_user", "secret_sauce")]
        [DataRow("firefox", "standard_user", "secret_sauce")]
        [DataRow("chrome", "problem_user", "secret_sauce")]
        [DataRow("firefox", "problem_user", "secret_sauce")]
        [DataRow("chrome", "performance_glitch_user", "secret_sauce")]
        [DataRow("firefox", "performance_glitch_user", "secret_sauce")]
        [DataRow("chrome", "error_user", "secret_sauce")]
        [DataRow("firefox", "error_user", "secret_sauce")]
        [DataRow("chrome", "visual_user", "secret_sauce")]
        [DataRow("firefox", "visual_user", "secret_sauce")]
        public void Login_CorrectCredentials_InventoryHeaderTitleDisplayed(string browser, string userName, string password)
        {
            Logger.Info($"{nameof(Login_CorrectCredentials_InventoryHeaderTitleDisplayed)} {browser} start");

            try
            {
                _driver = CreateDriver(browser);
                _driver.Navigate().GoToUrl(Url);
                LoginPage loginPage = new LoginPage(_driver);
                loginPage.TypeUserName(userName);
                loginPage.TypePassword(password);
                loginPage.ClickLoginButton();
                InventoryPage inventoryPage = new InventoryPage(_driver);
                inventoryPage.IsHeaderTitleDisplayed().Should().BeTrue();
            }
            catch (Exception ex)
            {
                Logger.Error($"{nameof(Login_CorrectCredentials_InventoryHeaderTitleDisplayed)} {browser} error", ex);
            }
            finally
            {
                Logger.Info($"{nameof(Login_CorrectCredentials_InventoryHeaderTitleDisplayed)} {browser} end");
            }
        }

        private IWebDriver CreateDriver(string browser)
        {
            IWebDriver driver = _driverFactory.CreateDriver(browser);

            var options = driver.Manage();
            options.Window.Maximize();
            options.Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            return driver;
        }
    }
}
