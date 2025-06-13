using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using UITests.PageObject;

namespace UITests
{
    public class TestMethods : BasePage
    {
        public TestMethods(IWebDriver driver) : base(driver) { }

        public string accountCreatedTextXPathLocator => "//h2[@data-qa='account-created']/b";
        public string continueBtnXPathLocator => "//a[@data-qa='continue-button']";
        public string accountDeletedTextXPathLocator => "//h2[@data-qa='account-deleted']/b";

        public IWebElement continueButton => driver.FindElement(By.XPath(continueBtnXPathLocator));

        public void CheckSignUpUser(JToken testData)
        {
            //get test data
            string name = testData["name"]?.ToString();
            string email = testData["email"]?.ToString();
            string password = testData["password"]?.ToString();
            bool isMale = Convert.ToBoolean(testData["isMale"]);
            DateTime dateOfBirth = Convert.ToDateTime(testData["dateOfBirth"]);
            string lastName = testData["lastName"]?.ToString();
            string company = testData["company"]?.ToString();
            string address = testData["address"]?.ToString();
            string address2 = testData["address2"]?.ToString();
            string country = testData["country"]?.ToString();
            string state = testData["state"]?.ToString();
            string city = testData["city"]?.ToString();
            string zipcode = testData["zipcode"]?.ToString();
            string mobileNumber = testData["mobileNumber"]?.ToString();
            string newUserSignUpText = testData["newUserSignUpText"]?.ToString();
            string enterAccountInfoText = testData["enterAccountInfoText"]?.ToString();
            string accountCreatedText = testData["accountCreatedText"]?.ToString();
            
            HomePage homePage = new HomePage(driver);
            homePage.signUp_logInTab.Click();

            LoginPage loginPage = new LoginPage(driver);

            //check text msg
            CheckElementExist(By.XPath(loginPage.newUserSignUpTextXPathLocator));
            CheckElementText(By.XPath(loginPage.newUserSignUpTextXPathLocator), newUserSignUpText);

            loginPage.signUpNameField.SendKeys(name);
            loginPage.signUpEmailField.SendKeys(email);
            loginPage.submitButton.Click();

            SignupPage signupPage = new SignupPage(driver);

            //check text msg
            CheckElementExist(By.XPath(signupPage.enterAccountInfoTextXPathLocator));
            CheckElementText(By.XPath(signupPage.enterAccountInfoTextXPathLocator), enterAccountInfoText);

            signupPage.SignUpUser(name, password, isMale, dateOfBirth, lastName, company, address, address2, country, state, city, zipcode, mobileNumber);
           
            //check message on primary page
            CheckElementExist(By.XPath(accountCreatedTextXPathLocator));
            CheckElementText(By.XPath(accountCreatedTextXPathLocator),  accountCreatedText);
            continueButton.Click();

            //16.Verify that 'Logged in as username' is visible
            CheckElementExist(By.XPath(homePage.loggedInUserTabXPathLocator.Replace("user", name)));
        }

        public void CheckDeleteCurrentAccount(JToken testData)
        {
            //inputs
            string accountDeletedText = testData["accountDeletedText"]?.ToString();

            HomePage homePage = new HomePage(driver);
            IWebElement deleteAccountTab = driver.FindElement(By.XPath(homePage.deleteAccountTabXPathLocator));
            deleteAccountTab.Click();

            //check message on primary page
            CheckElementExist(By.XPath(accountDeletedTextXPathLocator));
            CheckElementText(By.XPath(accountDeletedTextXPathLocator), accountDeletedText);

            continueButton.Click();
        }

    }
}
