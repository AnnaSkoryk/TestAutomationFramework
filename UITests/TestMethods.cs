using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using System.Xml.Linq;
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

        public void CheckSignUpUser(JToken testData, bool isEmailExists = false)
        {
            //get test data
            HomePage homePage = new HomePage(driver);
            homePage.signUp_logInTab.Click();

            //check text msg
            LoginPage loginPage = new LoginPage(driver);
            string newUserSignUpText = testData["newUserSignUpText"]?.ToString();
            CheckElementExist(By.XPath(loginPage.newUserSignUpTextXPathLocator));
            CheckElementText(By.XPath(loginPage.newUserSignUpTextXPathLocator), newUserSignUpText);

            string name = testData["name"]?.ToString();
            string email = testData["email"]?.ToString();
            loginPage.signUpNameField.SendKeys(name);
            loginPage.signUpEmailField.SendKeys(email);
            loginPage.signupButton.Click();

            if (isEmailExists) 
            {
                string emailExistsErrorMessage = testData["emailExistsErrorMessage"]?.ToString();
                CheckElementExist(By.XPath(loginPage.emailExistsErrorMessageLocator));
                CheckElementText(By.XPath(loginPage.emailExistsErrorMessageLocator), emailExistsErrorMessage);
            }
            else
            {
                SignUpUser(name, testData);

                //16.Verify that 'Logged in as username' is visible
                CheckElementExist(By.XPath(homePage.loggedInUserTabXPathLocator.Replace("user", name)));
            }
        }

        public void SignUpUser(string name, JToken testData)
        {
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
            string enterAccountInfoText = testData["enterAccountInfoText"]?.ToString();
            string accountCreatedText = testData["accountCreatedText"]?.ToString();

            SignupPage signupPage = new SignupPage(driver);

            //check text msg
            CheckElementExist(By.XPath(signupPage.enterAccountInfoTextXPathLocator));
            CheckElementText(By.XPath(signupPage.enterAccountInfoTextXPathLocator), enterAccountInfoText);

            signupPage.FillDataOnSignupPage(name, password, isMale, dateOfBirth, lastName, company, address, address2, country, state, city, zipcode, mobileNumber);

            //check message on primary page
            CheckElementExist(By.XPath(accountCreatedTextXPathLocator));
            CheckElementText(By.XPath(accountCreatedTextXPathLocator), accountCreatedText);
            continueButton.Click();
        }

        public void CheckLogInUser(JToken testData, bool isPositiveTest = true)
        {
            //get test data
            string loginToAccountText = testData["loginToAccountText"]?.ToString();
            string email = testData["email"]?.ToString();
            string password = testData["password"]?.ToString();
            string loginErrorMessage = testData["loginErrorText"]?.ToString();

            HomePage homePage = new HomePage(driver);
            homePage.signUp_logInTab.Click();

            //check text msg
            LoginPage loginPage = new LoginPage(driver);
            CheckElementExist(By.XPath(loginPage.loginToAccountTextXPathLocator));
            CheckElementText(By.XPath(loginPage.loginToAccountTextXPathLocator), loginToAccountText);

            loginPage.loginEmailField.SendKeys(email);
            loginPage.loginPasswordField.SendKeys(password);
            loginPage.loginButton.Click();

            //16.Verify that 'Logged in as username' is visible
            if (isPositiveTest)
            {
                string name = testData["name"]?.ToString();
                CheckElementExist(By.XPath(homePage.loggedInUserTabXPathLocator.Replace("user", name)));
            }
            else
            {
                CheckElementExist(By.XPath(loginPage.loginErrorTextXPathLocator));
                CheckElementText(By.XPath(loginPage.loginErrorTextXPathLocator), loginErrorMessage);
            }
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

        public void CheckLogoutUser()
        {
            HomePage homePage = new HomePage(driver);
            IWebElement logoutTab = driver.FindElement(By.XPath(homePage.logoutTabXPathLocator));
            logoutTab.Click();

            //check login page displayed
            LoginPage loginPage = new LoginPage(driver);
            loginPage.CheckLoginPageDisplayed();
        }
    }
}
