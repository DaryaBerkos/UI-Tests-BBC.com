using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace kursesCsharp.PageObjects
{
    public class AuthorizationPage
    {
        private IWebDriver _webDriver;
        private WebDriverWait _wait;

        public By PasswordInput = By.XPath("//input[@id='password-input']");
        public By LogInButton = By.XPath("//button[@id='submit-button']");
        public By SubmitButton = By.XPath("//button[@id='submit-button']");
        public By LoginInput => By.XPath("//input[@id='user-identifier-input']");
        public By YourAccountButton => By.XPath("//span[@id='idcta-username']");
        public By SignOutButton => By.XPath("//span[contains(text(),'Sign out')]");
        public By SignOutText => By.XPath("//h1[contains(text(),'signed out')]");
        public By MessageCheckCorrectDataText => By.XPath("//div[@id='form-message-general']");
        public By MessageWrongPasswordText => By.XPath("//div[@data-bbc-title='password-error']");
        public By VisibilityPasswordText => By.XPath("//button[@id='toggle-password-type']");
        public By ProblemsLoggingInButton => By.XPath("//p[@class='button__label']");
        public By NeedHelpText => By.XPath("//span[contains(text(),'Need help')]");
        public By ForgottenPasswordButton => By.XPath("//span[contains(text(),'forgotten my password')]/parent::a");
        public By ForgottenUserNameButton => By.XPath("//span[contains(text(),'forgotten my username')]/parent::a");
        public By NoneOfTheAboveButton => By.XPath("//span[contains(text(),'None')]/parent::a");
        public By YourEmailAddressText => By.XPath("//span[contains(text(),'your email address')]");
        public By EmailInput => By.XPath("//input[@id='user-identifier-input']");
        public By SendPasswordResetEmailButton => By.XPath("//button[@id='submit-button']");
        public By CheckYourInboxText => By.XPath("//span[text()='Please check your inbox']");
        public By WrongEmailText => By.XPath("//span[text()='Sorry, that email doesn’t look right.']");
        public By ForgottenUserNameText => By.XPath("//span[text()='Forgotten your username or email?']");
        public By SorryText => By.XPath("//span[contains(text(),'Sorry')]");
        public By RegisterAccountButton => By.XPath("//span[contains(text(),'Register for a new account')]/parent::a");
        public By YourAgeText => By.XPath("//span[text()='Сколько вам лет?']");
        public By Under16Button => By.XPath("//span[text()='Under 16']/parent::a");
        public By SorryOnly16sText => By.XPath("//span[contains(text(),'Sorry, only 16s')]");
        public By OKButton => By.XPath("//a[@id='return-to-ptrt']");
        public By Spinner => By.CssSelector("div.spinner.spinner--fill");
        public By Over16Button => By.XPath("//span[text()='16 or over']/parent::a");
        public By RegisterAccountText => By.XPath("//h1[@class='sb-heading--headlineSmall sb-heading--bold']");
        public By UsingTheBBCLink => By.XPath("//a[@href='/usingthebbc/']");
        public By DayInput => By.XPath("//input[@id='day-input']");
        public By MonthInput => By.XPath("//input[@id='month-input']");
        public By YearInput => By.XPath("//input[@id='year-input']");
        public By FormMessage => By.XPath("//p[@class='form-message__text']");

        public AuthorizationPage(IWebDriver webDriver, WebDriverWait wait)
        {
            _webDriver = webDriver;
            _wait = wait;
        }

        public void LogIn(string _userName, string _userPassword)
        {
            var emailInput = _wait.Until(ExpectedConditions.ElementToBeClickable(LoginInput));
            emailInput.SendKeys(_userName);
            _webDriver.FindElement(SubmitButton).Click();
            _webDriver.FindElement(PasswordInput).SendKeys(_userPassword);
            _webDriver.FindElement(LogInButton).Click();
        }

        public void ForgottenUserNameButtonClick()
        {
            _wait.Until(ExpectedConditions.ElementToBeClickable(ProblemsLoggingInButton)).Click();

            _wait.Until(ExpectedConditions.ElementToBeClickable(ForgottenUserNameButton)).Click();
        }

        public void ProblemsLogginInButtonClick()
        {
            _wait.Until(ExpectedConditions.ElementToBeClickable(ProblemsLoggingInButton)).Click();
        }

        public void RegisterAccountButtonClick()
        {
            _wait.Until(ExpectedConditions.ElementToBeClickable(RegisterAccountButton)).Click();
        }

        public void InvisibilitySpinner()
        {
            _wait.Until(ExpectedConditions.InvisibilityOfElementLocated(Spinner));
        }
    }
}
