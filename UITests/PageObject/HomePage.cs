using NUnit.Framework.Legacy;
using OpenQA.Selenium;

namespace UITests.PageObject
{
    public class HomePage : BasePage
    {
        public HomePage(IWebDriver driver) : base(driver) { }

        public string homeTabXPathLocator => "//a[contains(., ' Home')]";
        public string loggedInUserTabXPathLocator => "//a[contains(., ' Logged in as ')]//b[text()='user']";
        public string signUp_LogInTabXPathLocator => "//a[text()=' Signup / Login']";
        public string sliderIdLocator => "slider";
        public string containerXPathLocator => "//section[2]/div[contains(@class, 'container')]";
        public string deleteAccountTabXPathLocator => "//a[contains(., ' Delete Account')]";
        public string logoutTabXPathLocator => "//a[contains(., ' Logout')]";

        public IWebElement homeTab => driver.FindElement(By.XPath(homeTabXPathLocator));
        public IWebElement signUp_logInTab => driver.FindElement(By.XPath(signUp_LogInTabXPathLocator));
        
        public void CheckHomePageDisplayed()
        {
            Assert.Multiple(() =>
            {
                ClassicAssert.IsTrue(IsElementExist(By.XPath(homeTabXPathLocator)), $"Element by xPath {homeTabXPathLocator} was not found");
                ClassicAssert.IsTrue(IsElementExist(By.Id(sliderIdLocator)), $"Element by id {sliderIdLocator} was not found");
                ClassicAssert.IsTrue(IsElementExist(By.XPath(containerXPathLocator)), $"Element by xPath {containerXPathLocator} was not found");
            });
            ClassicAssert.AreEqual(homeTab.GetAttribute("style"), "color: orange;");
        }
    }
}
