using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace kursesCsharp.PageObjects
{
    public class MainMenuPage
    {
        private IWebDriver _webDriver;
        private WebDriverWait _wait;

        public By IFrameCookies = By.Id("sp_message_iframe_783538");
        public By CookieButton = By.XPath("//button[@aria-label='I agree']");
        public By SignUpButton = By.XPath("//span[@id='idcta-username']");
        public By ForecastLink => By.XPath("//a[@class='forecast--link']");
        public By EnterCityInput => By.XPath("//input[@id='ls-c-search__input-label']");
        public By SearchCityButton => By.XPath("//button[@class='ls-c-search__submit ls-o-btn--right']");
        public By HomeButton => By.XPath("//li[@class='orb-nav-homedotcom']");
        public By NewsButton => By.XPath("//li[@class='orb-nav-newsdotcom']");
        public By SportButton => By.XPath("//li[@class='orb-nav-sport']");
        public By ReelButton => By.XPath("//li[@class='orb-nav-reeldotcom']");
        public By WorkLifeButton => By.XPath("//li[@class='orb-nav-worklife']");
        public By TravelButton => By.XPath("//li[@class='orb-nav-traveldotcom']");
        public By FutureButton => By.XPath("//li[@class='orb-nav-future']");
        public By CultureButton => By.XPath("//li[@class='orb-nav-culture']");
        public By CookieContinueButton => By.XPath("//button[@id='bbccookies-continue-button']");
        public By DayWeather => By.XPath("//a[@id='daylink-0']");
        public By SearchBBCButton => By.XPath("//a[@id='orbit-search-button']");
        public By SearchBBCInput => By.XPath("//input[@id='searchInput']");
        public By SearchInfoButton => By.XPath("//button[@id='searchButton']");

        public MainMenuPage(IWebDriver webDriver, WebDriverWait wait)
        {
            _webDriver = webDriver;
            _wait = wait;
        }

        public AuthorizationPage SignIn()
        {
            //IWebElement iframeElement = _wait.Until(ExpectedConditions.ElementExists(IFrameCookies));
            //_webDriver.SwitchTo().Frame(iframeElement);
            //var cookieButton = _wait.Until(ExpectedConditions.ElementToBeClickable(CookieButton));
            //cookieButton.Click();
            //_webDriver.SwitchTo().DefaultContent();
            _webDriver.FindElement(SignUpButton).Click();
            return new AuthorizationPage(_webDriver, _wait);
        }
    }
}
