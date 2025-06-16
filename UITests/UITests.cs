using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using UITests;
using UITests.PageObject;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using Allure.Commons;
using Newtonsoft.Json.Linq;


namespace TestAutomationFramework
{
    [AllureNUnit]
    //[AllureSuite("SmokeTest")]
    [TestFixture(Category = "SmokeTest")]
    public class SmokeTests
    {
        IWebDriver driver;
        HomePage homePage;
        TestMethods methods;
        WelcomeDialogPage welcomePage;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
           AllureLifecycle.Instance.CleanupResultDirectory();
        }

        [SetUp]
        public void Setup()
        {
            IWebDriver _driver = new ChromeDriver();
            this.driver = _driver;
            homePage = new HomePage(driver);
            methods = new TestMethods(driver);
            welcomePage = new WelcomeDialogPage(driver);
            driver.Navigate().GoToUrl(GeneralConfig.baseUrl);
            driver.Manage().Window.Maximize();
        }

        [Test]
        [AllureTms("1")]
        public void CheckWelcomePageExistsAndVisibleAfterEnterTheSite()
        {
            welcomePage.CheckElementExist(By.CssSelector(welcomePage.welcomeDialogClassNameLocator));
        }

        [Test, TestCaseSource(typeof(TestDataLoader), nameof(TestDataLoader.LoadTestData), new object[] { "TestData/RegisterUser.json" })]
        [AllureTms("2")]
        public void RegisterUser(JToken testData)
        {
            welcomePage.ClickConsentBtn();
            homePage.CheckHomePageDisplayed();
            methods.CheckSignUpUser(testData);
            methods.CheckDeleteCurrentAccount(testData);
        }

        [Test, TestCaseSource(typeof(TestDataLoader), nameof(TestDataLoader.LoadTestData), new object[] { "TestData/LoginUser.json" })]
        [AllureTms("3")]
        public void LoginUser(JToken testData)
        {
            welcomePage.ClickConsentBtn();
            homePage.CheckHomePageDisplayed();
            methods.CheckLogInUser(testData);
        }

        [Test, TestCaseSource(typeof(TestDataLoader), nameof(TestDataLoader.LoadTestData), new object[] { "TestData/NotValidLoginUser.json" })]
        [AllureTms("4")]
        public void TryLoginUserWithNotValidEmailAndPassword(JToken testData)
        {
            welcomePage.ClickConsentBtn();
            homePage.CheckHomePageDisplayed();
            methods.CheckLogInUser(testData, isPositiveTest: false);
        }

        [Test, TestCaseSource(typeof(TestDataLoader), nameof(TestDataLoader.LoadTestData), new object[] { "TestData/LoginUser.json" })]
        [AllureTms("5")]
        public void LogoutUser(JToken testData)
        {
            welcomePage.ClickConsentBtn();
            homePage.CheckHomePageDisplayed();
            methods.CheckLogInUser(testData);
            methods.CheckLogoutUser();
        }

        [Test, TestCaseSource(typeof(TestDataLoader), nameof(TestDataLoader.LoadTestData), new object[] { "TestData/RegisterUserWithExistingEmail.json" })]
        [AllureTms("6")]
        public void TryRegisterUserWithExistingEmail(JToken testData)
        {
            welcomePage.ClickConsentBtn();
            homePage.CheckHomePageDisplayed();
            methods.CheckSignUpUser(testData, isEmailExists: true);
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
        }
    }
}
