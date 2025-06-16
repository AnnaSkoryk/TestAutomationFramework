using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using UITests;
using UITests.PageObject;
//using NUnit.Allure.Attributes;
//using NUnit.Allure.Core;
//using Allure.Commons;
using Newtonsoft.Json.Linq;


namespace TestAutomationFramework
{
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
           // AllureLifecycle.Instance.CleanupResultDirectory();
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

        //0
        [Test]
        public void CheckWelcomePageExistsAndVisibleAfterEnterTheSite()
        {
            welcomePage.CheckElementExist(By.CssSelector(welcomePage.welcomeDialogClassNameLocator));
        }

        //1
        [Test, TestCaseSource(typeof(TestDataLoader), nameof(TestDataLoader.LoadTestData), new object[] { "TestData/RegisterUser.json" })]
        public void RegisterUser(JToken testData)
        {
            welcomePage.ClickConsentBtn();
            homePage.CheckHomePageDisplayed();
            methods.CheckSignUpUser(testData);
            methods.CheckDeleteCurrentAccount(testData);
        }

        //2
        [Test, TestCaseSource(typeof(TestDataLoader), nameof(TestDataLoader.LoadTestData), new object[] { "TestData/LoginUser.json" })]
        public void LoginUser(JToken testData)
        {
            welcomePage.ClickConsentBtn();
            homePage.CheckHomePageDisplayed();
            methods.CheckLogInUser(testData);
        }

        //3
        [Test, TestCaseSource(typeof(TestDataLoader), nameof(TestDataLoader.LoadTestData), new object[] { "TestData/NotValidLoginUser.json" })]
        public void TryLoginUserWithNotValidEmailAndPassword(JToken testData)
        {
            welcomePage.ClickConsentBtn();
            homePage.CheckHomePageDisplayed();
            methods.CheckLogInUser(testData, isPositiveTest: false);
        }

        //4
        [Test, TestCaseSource(typeof(TestDataLoader), nameof(TestDataLoader.LoadTestData), new object[] { "TestData/LoginUser.json" })]
        public void LogoutUser(JToken testData)
        {
            welcomePage.ClickConsentBtn();
            homePage.CheckHomePageDisplayed();
            methods.CheckLogInUser(testData);
            methods.CheckLogoutUser();
        }

        [TearDown]
        public void TearDown()
        {
            //driver.Quit();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
        }
    }
}
