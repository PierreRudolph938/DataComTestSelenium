using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace DataComSeleniumTestProject
{
    public class Tests
    {
        private const string _driverPath = "C:\\Selenium";
        private IWebDriver? driver = null;

        [SetUp]
        public void Setup()
        {
            IWebDriver driver = new ChromeDriver(_driverPath + "\\drivers");
            driver.Manage().Window.Maximize();
            driver.Url = "https://datacom.com/nz/en/contact-us";
        }

        [TearDown]
        public void TearDown()
        {
            if (driver != null)
                driver.Dispose();
        }

            [Test]
        public void TestLoad_GetInTouchPage()
        {
            if (driver == null) return;

            //Except all cookies
            var acceptCookies = driver.FindElement(By.XPath("//button[text()='Accept all']"));

            if (acceptCookies.Displayed)
                acceptCookies.Click();

            var datacomLogo = driver.FindElement(By.Name("Datacom logo"));

            Assert.True(datacomLogo.Displayed);
            Assert.Equals(datacomLogo.GetAttribute("src"), "https://assets.datacom.com/is/content/datacom/Datacom-Primary-Logo-RGB?$header-mega-logo$");
        }

        [Test]
        public void TestNewZealandRegionalContactDetails()
        {
            if (driver == null) return;

            var element = driver.FindElement(By.LinkText("New Zealand"));
            element.Click();
            Assert.True(element.Displayed);

            //Auckland
            var aucklandContact = driver.FindElement(By.LinkText("58 Gaunt Street, Auckland CBD"));
            Assert.Equals(aucklandContact.Text, "58 Gaunt Street, Auckland CBD, Auckland 1010");

            //Christchurch
            Assert.True(driver.FindElement(By.LinkText("Christchurch")).Displayed);

            var christchurchContact = driver.FindElement(By.Id("#section-1")); //Locator needs to be refined
            Assert.Equals(christchurchContact.Text, "67 Gloucester Street, Christchurch 8013");

            //Wellington
            Assert.True(driver.FindElement(By.LinkText("Wellington")).Displayed);

            var wellingtonContact = driver.FindElement(By.Id("section-9")); //Locator needs to be refined
            Assert.Equals(wellingtonContact.Text, "55 Featherston Street, Pipitea, Wellington 6011");
        }


        [Test]
        public void TestAustraliaRegionalContactDetails()
        {
            if (driver == null) return;

            var element = driver.FindElement(By.LinkText("Australia"));
            element.Click();
            Assert.True(element.Displayed);

            //Adelaide
            var adelaideContact = driver.FindElement(By.LinkText("118 Franklin Street, Adelaide"));
            Assert.Equals(adelaideContact.Text, "118 Franklin Street, Adelaide, South Australia 5000");
        }

        [Test]
        public void TestContactUs()
        {
            if (driver == null) return;

            driver.FindElement(By.Id("#cmp-mrkto-modal-thank-you")).Click();    //GetByText("Contact us")

            var firstName = driver.FindElement(By.Name("*First name"));
            firstName.Click();
            firstName.SendKeys("Anton");

            var lastName = driver.FindElement(By.Name("*Last name"));
            lastName.Click();
            lastName.SendKeys("Ohlson");

            var businessEmail = driver.FindElement(By.Name("*Business email"));
            businessEmail.Click();
            businessEmail.SendKeys("anton.ohlson@yahoo.com");

            var phoneNumber = driver.FindElement(By.Name("*Phone number"));
            phoneNumber.Click();
            phoneNumber.SendKeys("0210687777");

            var jobTitle = driver.FindElement(By.Name("*Job title"));
            jobTitle.Click();
            jobTitle.SendKeys("QA Analyst");

            var companyName = driver.FindElement(By.Name("*Company name"));
            companyName.Click();
            companyName.SendKeys("CountDown");

            var country = driver.FindElement(By.Name("*Country"));
            country.Click();
            country.SendKeys("New Zealand");

            SelectElement stateList = new SelectElement(driver.FindElement(By.Name("*State")));
            stateList.SelectByText("Christchurch");

            SelectElement careerList = new SelectElement(driver.FindElement(By.Name("*How can we help you?")));
            careerList.SelectByText("Careers");

            SelectElement careerType = new SelectElement(driver.FindElement(By.Name("*What type of career are you looking for?")));
            careerType.SelectByText("Internship");

            var role = driver.FindElement(By.Name("*What role/business area are you interested in?"));
            role.Click();
            role.SendKeys("QA Analyst");

            var submit = driver.FindElement(By.Name("Submit"));
            submit.Click();
        }
    }
}