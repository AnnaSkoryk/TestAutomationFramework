using OpenQA.Selenium;

namespace UITests.PageObject
{
    public class WelcomeDialogPage : BasePage
    {
        public WelcomeDialogPage(IWebDriver driver) : base(driver) { }

        public string welcomeDialogClassNameLocator => ".fc-dialog-container";
        public string consentButtonClassNameLocator => "button.fc-button.fc-cta-consent.fc-primary-button";
        
        public IWebElement welcomeDialog => driver.FindElement(By.CssSelector(welcomeDialogClassNameLocator));
        public IWebElement consentButton => driver.FindElement(By.CssSelector(consentButtonClassNameLocator));

        public void ClickConsentBtn()
        {
            if (IsElementExist(By.CssSelector(consentButtonClassNameLocator)))
                consentButton.Click();
            else
                Assert.Fail("consent button was not found on page");
        }
    }
}
