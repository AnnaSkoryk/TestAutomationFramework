using OpenQA.Selenium;

namespace UITests.PageObject
{
    public class ContactUsPage : BasePage
    {
        public ContactUsPage(IWebDriver driver) : base(driver) { }

        public string getInTouchTextXPathLocator => "//div[@class='contact-form']/h2";
        public string nameFieldXPathLocator => "//input[@data-qa='name']";
        public string emailFieldXPathLocator => "//input[@data-qa='email']";
        public string subjectFieldXPathLocator => "//input[@data-qa='subject']";
        public string messageFieldIdLocator => "message";
        public string upoadFileFieldXPathLocator => "//input[@name='upload_file']";
        public string submitButtonXPathLocator => "//input[@data-qa='submit-button']";
        public string successMessageTextCSSLocator => ".status.alert.alert-success";
        public string homeBtnCSSLocator => ".btn.btn-success";

        public IWebElement nameField => driver.FindElement(By.XPath(nameFieldXPathLocator));
        public IWebElement emailField => driver.FindElement(By.XPath(emailFieldXPathLocator));
        public IWebElement subjectField => driver.FindElement(By.XPath(subjectFieldXPathLocator));
        public IWebElement messageField => driver.FindElement(By.Id(messageFieldIdLocator));
        public IWebElement upoadFileField => driver.FindElement(By.XPath(upoadFileFieldXPathLocator));
        public IWebElement submitButton => driver.FindElement(By.XPath(submitButtonXPathLocator));
        public IWebElement homeButton => driver.FindElement(By.CssSelector(homeBtnCSSLocator));
    }
}
