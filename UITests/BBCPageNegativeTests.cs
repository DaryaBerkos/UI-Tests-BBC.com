using kursesCsharp.PageObjects;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

namespace kursesCsharp
{
    public class BBCPageNegativeTests
    {
        private IWebDriver _webDriver;
        private WebDriverWait wait;
        private MainMenuPage _mainPage;
        private AuthorizationPage _authorizationPage;

        private const string _userName = "solozhenkova121998@gmail.com";
        private const string _userPassword = "password1210";

        [SetUp]
        public void SetUp()
        {
            _webDriver = new OpenQA.Selenium.Chrome.ChromeDriver();
            wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(10));
            Actions actions = new Actions(_webDriver);
            _webDriver.Navigate().GoToUrl("https://www.bbc.com/");
            _webDriver.Manage().Window.Maximize();
            _mainPage = new MainMenuPage(_webDriver, wait);
            _authorizationPage = new AuthorizationPage(_webDriver, wait);
        }

        [Test]
        public void SignIn_InvalidUserName_InvalidAuthorizationTest()
        {
            // Arrange
            _mainPage.SignIn();

            // Act
            wait.Until(ExpectedConditions.ElementToBeClickable(_authorizationPage.LoginInput)).SendKeys("invalidUserName");
            _webDriver.FindElement(_authorizationPage.SubmitButton).Click();

            // Assert
            string messageText = _webDriver.FindElement(_authorizationPage.MessageCheckCorrectDataText).Text;
            Assert.IsTrue(messageText.Contains("We don’t recognise that email or username. You can try again or "), "Text is not visible on the page..");
        }

        [Test]
        public void SignIn_MissingPassword_InvalidAuthorizationTest()
        {
            // Arrange
            _mainPage.SignIn();

            // Act
            wait.Until(ExpectedConditions.ElementToBeClickable(_authorizationPage.LoginInput)).SendKeys(_userName);
            _webDriver.FindElement(_authorizationPage.LogInButton).Click();
            _webDriver.FindElement(_authorizationPage.LogInButton).Click();

            // Assert
            string messageText = _webDriver.FindElement(_authorizationPage.MessageCheckCorrectDataText).Text;
            Assert.IsTrue(messageText.Contains("Извините, данные не сходятся"), "Text 'Извините, данные не сходятся' is not visible on the page..");

            string messageDataText = _webDriver.FindElement(_authorizationPage.MessageWrongPasswordText).Text;
            Assert.IsTrue(messageDataText.Contains("Чего-то не хватает"), "Text 'Чего-то не хватает' is not visible on the page..");
        }

        [Test]
        public void SignIn_ShortPassword_InvalidAuthorizationTest()
        {
            // Arrange
            _mainPage.SignIn();

            // Act
            _authorizationPage.LogIn(_userName, "123");

            // Assert
            string messageText = _webDriver.FindElement(_authorizationPage.MessageWrongPasswordText).Text;
            Assert.IsTrue(messageText.Contains("Извините, этот пароль слишком короткий"), "Text 'Извините, этот пароль слишком короткий' is not visible on the page..");
            Assert.IsTrue(messageText.Contains("В нём должно быть не менее 8 символов."), "Text 'В нём должно быть не менее 8 символов.' is not visible on the page..");
        }

        [Test]
        public void SignIn_PasswordWithNumbers_InvalidAuthorizationTest()
        {
            // Arrange
            _mainPage.SignIn();

            // Act
            _authorizationPage.LogIn(_userName, "123456789");

            // Assert
            string messageText = _webDriver.FindElement(_authorizationPage.MessageWrongPasswordText).Text;
            Assert.IsTrue(messageText.Contains("Извините, этот пароль недействителен"), "Text 'Извините, этот пароль недействителен' is not visible on the page..");
            Assert.IsTrue(messageText.Contains("Пожалуйста, включите одну букву."), "Text 'Пожалуйста, включите одну букву.' is not visible on the page..");
        }   

        [Test]
        public void ForgottenPassword_WrongEmail_ProblemsLoggingInTest()
        {
            // Arrange
            _mainPage.SignIn();

            // Act
            _authorizationPage.ProblemsLogginInButtonClick();

            bool isNeedHelpTextVisible = wait.Until(ExpectedConditions.ElementIsVisible(_authorizationPage.NeedHelpText)).Displayed;
            Assert.IsTrue(isNeedHelpTextVisible, "The 'Need Help' text is not visible on the page.");

            wait.Until(ExpectedConditions.ElementToBeClickable(_authorizationPage.ForgottenPasswordButton)).Click();

            wait.Until(ExpectedConditions.ElementToBeClickable(_authorizationPage.EmailInput)).Click();
            _webDriver.FindElement(_authorizationPage.EmailInput).SendKeys("123");
            _webDriver.FindElement(_authorizationPage.SendPasswordResetEmailButton).Click();

            // Assert
            bool isWrongEmailTextVisible = wait.Until(ExpectedConditions.ElementIsVisible(_authorizationPage.WrongEmailText)).Displayed;
            Assert.IsTrue(isWrongEmailTextVisible, "The 'Sorry, that email doesn’t look right.' text is not visible on the page.");
        }

        [Test]
        public void ForgottenUserName_InvalidDateOfBirth_ProblemsLoggingInButtonsTest()
        {
            // Arrange
            _mainPage.SignIn();

            // Act
            _authorizationPage.ForgottenUserNameButtonClick();

            _authorizationPage.RegisterAccountButtonClick();

            bool isYourAgeText = wait.Until(ExpectedConditions.ElementIsVisible(_authorizationPage.YourAgeText)).Displayed;
            Assert.IsTrue(isYourAgeText, "The 'Сколько вам лет?' text is not displayed");

            _authorizationPage.InvisibilitySpinner();

            wait.Until(ExpectedConditions.ElementIsVisible(_authorizationPage.Over16Button)).Click();

            _authorizationPage.InvisibilitySpinner();

            wait.Until(ExpectedConditions.ElementToBeClickable(_authorizationPage.DayInput)).SendKeys("dfgh");

            _webDriver.FindElement(_authorizationPage.MonthInput).SendKeys("ghdfth");
            _webDriver.FindElement(_authorizationPage.YearInput).SendKeys("dfghdf");
            _webDriver.FindElement(_authorizationPage.SubmitButton).Click();

            // Assert
            bool isWrongDataTextt = wait.Until(ExpectedConditions.ElementIsVisible(_authorizationPage.FormMessage)).Displayed;
            Assert.IsTrue(isWrongDataTextt, "The 'Oops, that date doesn't look right.' text is not displayed");
        }

        [TearDown]
        public void TearDown()
        {
            _webDriver.Quit();
        }
    }
}

