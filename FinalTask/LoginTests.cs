using FinalTask.Pages;
using FluentAssertions;
using log4net;
using log4net.Config;
using OpenQA.Selenium;

[assembly: Parallelize(Workers = 4, Scope = ExecutionScope.MethodLevel)]
namespace FinalTask
{
    [TestClass]
    public class LoginTests
    {
        private const string Url = "https://www.saucedemo.com/";

        private static readonly ILog Logger = LogManager.GetLogger(typeof(LoginTests));

        private IWebDriver? _driver;
        private IWaitStrategy? _waitStrategy;

        [AssemblyInitialize]
        public static void AssemblyInitialize(TestContext _)
        {
            var config = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "App.config");
            XmlConfigurator.Configure(config);
            Logger.Info("Logger configured");
        }

        [TestInitialize]
        public void TestInitialize()
        {
            _waitStrategy = new ExplicitWaitStrategy(TimeSpan.FromSeconds(1));
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
                LoginPage loginPage = new LoginPage(_driver, _waitStrategy!, Logger);
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
                TakeScreenshot(nameof(Login_EmptyCredentials_UserNameErrorMessageDisplayed));
                throw;
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
                LoginPage loginPage = new LoginPage(_driver, _waitStrategy!, Logger);
                loginPage.TypeUserName("bob");
                loginPage.TypePassword("password");
                loginPage.ClearPassword();
                loginPage.ClickLoginButton();
                loginPage.IsPasswordErrorMessageDisplayed().Should().BeTrue();
            }
            catch (Exception ex)
            {
                Logger.Error($"{nameof(Login_EmptyPassword_PasswordErrorMessageDisplayed)} {browser} error", ex);
                TakeScreenshot(nameof(Login_EmptyPassword_PasswordErrorMessageDisplayed));
                throw;
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
                LoginPage loginPage = new LoginPage(_driver, _waitStrategy!, Logger);
                loginPage.TypeUserName(userName);
                loginPage.TypePassword(password);
                loginPage.ClickLoginButton();
                InventoryPage inventoryPage = new InventoryPage(_driver, _waitStrategy!, Logger);
                inventoryPage.IsHeaderTitleDisplayed().Should().BeTrue();
            }
            catch (Exception ex)
            {
                Logger.Error($"{nameof(Login_CorrectCredentials_InventoryHeaderTitleDisplayed)} {browser} error", ex);
                TakeScreenshot(nameof(Login_CorrectCredentials_InventoryHeaderTitleDisplayed));
                throw;
            }
            finally
            {
                Logger.Info($"{nameof(Login_CorrectCredentials_InventoryHeaderTitleDisplayed)} {browser} end");
            }
        }

        private static IWebDriver CreateDriver(string browser)
        {
            IWebDriver driver =
                new WebDriverBuilder()
                    .WithBrowser(browser)
                    .WithImplicitWait(TimeSpan.FromSeconds(10))
                    .WithMaximize(true)
                    .Build();

            return driver;
        }

        private void TakeScreenshot(string testName)
        {
            try
            {
                if (_driver == null) return;

                var screenshot = ((ITakesScreenshot)_driver).GetScreenshot();

                var fileName = $"{testName}_{DateTime.Now:yyyyMMdd_HHmmss}.png";

                screenshot.SaveAsFile(fileName);

                Logger.Info($"Screenshot saved: {fileName}");
            }
            catch (Exception ex)
            {
                Logger.Error($"Failed to take screenshot", ex);
            }
        }
    }
}
