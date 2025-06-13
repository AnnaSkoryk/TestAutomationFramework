using NUnit.Framework.Legacy;
using OpenQA.Selenium;

namespace UITests.PageObject
{
    public class BasePage
    {
        protected IWebDriver driver { get; set; }

        public BasePage(IWebDriver driver)
        {
             this.driver = driver;
        }

        public void CheckElementExist(By by)
        {
            ClassicAssert.IsTrue(IsElementExist(by), $"Element is not exist: {by.ToString()}");
        }

        public bool IsElementExist(By by)
        {
            try
            {
                return driver.FindElement(by).Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
            catch (NullReferenceException)
            {
                Assert.Fail("driver is null!");
                return false;
            }
        }

        public void CheckElementText(By by, string expectedText)
        {
            ClassicAssert.AreEqual(expectedText.ToLower(), GetElementText(by), $"Text of element {by.ToString()} is not matching.");
        }

        public string GetElementText(By by)
        {
            IWebElement element = driver.FindElement(by);
            return element.Text.Trim().ToLower();
        }
    }
}
