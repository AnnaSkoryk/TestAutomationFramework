using OpenQA.Selenium;

namespace UITests.PageObject
{
    public class LoginPage : BasePage
    {
        public LoginPage(IWebDriver driver) : base(driver) { }

        public string newUserSignUpTextXPathLocator => "//div[@class='signup-form']/h2";
        public string signUpNameXPathLocator => "//input[@type='text']";
        public string signUpEmailXPathLocator => "//input[@data-qa='signup-email']";
        public string btnSubmitXPathLocator => "//button[@data-qa='signup-button']";
        
        public IWebElement submitButton => driver.FindElement(By.XPath(btnSubmitXPathLocator));
        public IWebElement signUpNameField => driver.FindElement(By.XPath(signUpNameXPathLocator));
        public IWebElement signUpEmailField => driver.FindElement(By.XPath(signUpEmailXPathLocator));
    }
}
