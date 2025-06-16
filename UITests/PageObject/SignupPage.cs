using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace UITests.PageObject
{

    public class SignupPage : BasePage
    {
        public SignupPage(IWebDriver driver) : base(driver) { }

        public string enterAccountInfoTextXPathLocator => "//b[text()='Enter Account Information']";
        public string passwordFieldIdLocator => "password";
        public string mrRadoBtnIdLocator => "uniform-id_gender1";
        public string mrsRadoBtnIdLocator => "uniform-id_gender2";
        public string daysIdLocator => "days";
        public string monthsIdLocator => "months";
        public string yearsIdLocator => "years";
        public string newsletterIdLocator => "newsletter";
        public string receiveOffersIdLocator => "optin";
        public string newsletterTextLableXPathLocator => "//div[@class='checkbox']/label[text()='Sign up for our newsletter!']";
        public string receiveOffersTextLableXPathLocator => "//div[@class='checkbox']/label[text()='Receive special offers from our partners!']";
        public string firstNameFieldIdLocator => "first_name";
        public string lastNameFieldIdLocator => "last_name";
        public string companyFieldIdLocator => "company";
        public string address1FieldIdLocator => "address1";
        public string address2FieldIdLocator => "address2";
        public string countryIdLocator => "country";
        public string stateFieldIdLocator => "state";
        public string cityFieldIdLocator => "city";
        public string zipcodeFieldIdLocator => "zipcode";
        public string mobileNumberFieldIdLocator => "mobile_number";
        public string createAccountBtnXPathLocator => "//button[@data-qa='create-account']";
        
        public IWebElement enterAccountInfoText => driver.FindElement(By.XPath(enterAccountInfoTextXPathLocator));
        public IWebElement mrRadioBtn => driver.FindElement(By.Id(mrRadoBtnIdLocator));
        public IWebElement mrsRadioBtn => driver.FindElement(By.Id(mrsRadoBtnIdLocator));
        public IWebElement passwordField => driver.FindElement(By.Id(passwordFieldIdLocator));
        public IWebElement daysDropDwn => driver.FindElement(By.Id(daysIdLocator));
        public IWebElement monthsDropDwn => driver.FindElement(By.Id(monthsIdLocator));
        public IWebElement yearsDropDwn => driver.FindElement(By.Id(yearsIdLocator));
        public IWebElement newsletterCheckBox => driver.FindElement(By.Id(newsletterIdLocator));
        public IWebElement receiveOffersCheckBox => driver.FindElement(By.Id(receiveOffersIdLocator));
        public IWebElement firstNameField => driver.FindElement(By.Id(firstNameFieldIdLocator));
        public IWebElement lastNameField => driver.FindElement(By.Id(lastNameFieldIdLocator));
        public IWebElement companyField => driver.FindElement(By.Id(companyFieldIdLocator));
        public IWebElement address1Field => driver.FindElement(By.Id(address1FieldIdLocator));
        public IWebElement address2Field => driver.FindElement(By.Id(address2FieldIdLocator));
        public IWebElement countryDropDwn => driver.FindElement(By.Id(countryIdLocator));
        public IWebElement stateField => driver.FindElement(By.Id(stateFieldIdLocator));
        public IWebElement cityField => driver.FindElement(By.Id(cityFieldIdLocator));
        public IWebElement zipcodeField => driver.FindElement(By.Id(zipcodeFieldIdLocator));
        public IWebElement mobileNumberField => driver.FindElement(By.Id(mobileNumberFieldIdLocator));
        public IWebElement createAccountButton => driver.FindElement(By.XPath(createAccountBtnXPathLocator));

        public void FillDataOnSignupPage(string name, string password, bool isMale, DateTime dateOfBirth, string lastName, string company, string address, string address2, string country, string state, string city, string zipcode, string mobileNumber, bool signUpNewsletters = true, bool receiveSpecialOffers = true)
        {
            //gender
            if (isMale) 
                mrRadioBtn.Click();
            else
                mrsRadioBtn.Click();

            passwordField.SendKeys(password);
            SelectElement selectDays = new SelectElement(daysDropDwn);
            selectDays.SelectByValue(dateOfBirth.Day.ToString());
            SelectElement selectMonths = new SelectElement(monthsDropDwn);
            selectMonths.SelectByValue(dateOfBirth.Month.ToString());
            SelectElement selectYears = new SelectElement(yearsDropDwn);
            selectYears.SelectByValue(dateOfBirth.Year.ToString());

            //operate checkboxes
            Assert.Multiple(() =>
            {
                CheckElementExist(By.XPath(newsletterTextLableXPathLocator));
                CheckElementExist(By.XPath(receiveOffersTextLableXPathLocator));
            });
            if (signUpNewsletters)
                newsletterCheckBox.Click();
            if (receiveSpecialOffers)
                receiveOffersCheckBox.Click();

            //fill other fields
            firstNameField.SendKeys(name);
            lastNameField.SendKeys(lastName);
            companyField.SendKeys(company);
            address1Field.SendKeys(address);
            address2Field.SendKeys(address2);
            SelectElement selectCountry = new SelectElement(countryDropDwn);
            selectCountry.SelectByValue(country);
            stateField.SendKeys(state);
            cityField.SendKeys(city);
            zipcodeField.SendKeys(zipcode);
            mobileNumberField.SendKeys(mobileNumber);

            //submit
            createAccountButton.Click();
        }
    }
}
