using NUnit.Framework.Legacy;
using OpenQA.Selenium;

namespace UITests.PageObject
{
    public class LoginPage : BasePage
    {
        public LoginPage(IWebDriver driver) : base(driver) { }

        public string newUserSignUpTextXPathLocator => "//div[@class='signup-form']/h2";
        public string signUpNameXPathLocator => "//input[@type='text']";
        public string signUpEmailXPathLocator => "//input[@data-qa='signup-email']";
        public string btnSignupXPathLocator => "//button[@data-qa='signup-button']";
        public string loginToAccountTextXPathLocator => "//div[@class='login-form']/h2";
        public string loginEmailFieldXPathLocator => "//input[@data-qa='login-email']";
        public string loginPasswordFieldXPathLocator => "//input[@type='password']";
        public string loginButtonXPathLocator => "//button[@data-qa='login-button']";
        public string loginErrorTextXPathLocator => "//form[@action='/login']//p";
        public string loginFormCSSLocator => ".login-form";
        public string signupFormCSSLocator => ".signup-form";
        public string emailExistsErrorMessageLocator => "//form[@action='/signup']//p";
        

        public IWebElement signupButton => driver.FindElement(By.XPath(btnSignupXPathLocator));
        public IWebElement signUpNameField => driver.FindElement(By.XPath(signUpNameXPathLocator));
        public IWebElement signUpEmailField => driver.FindElement(By.XPath(signUpEmailXPathLocator));
        public IWebElement loginButton => driver.FindElement(By.XPath(loginButtonXPathLocator));
        public IWebElement loginEmailField => driver.FindElement(By.XPath(loginEmailFieldXPathLocator));
        public IWebElement loginPasswordField => driver.FindElement(By.XPath(loginPasswordFieldXPathLocator));

        public void CheckLoginPageDisplayed() 
        {
            CheckElementExist(By.CssSelector(loginFormCSSLocator));
            CheckElementExist(By.CssSelector(signupFormCSSLocator));
        }
    }
}
