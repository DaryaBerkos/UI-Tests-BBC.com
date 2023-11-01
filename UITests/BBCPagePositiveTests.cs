using kursesCsharp.PageObjects;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

namespace kursesCsharp
{
    public class BBCPagePositiveTests
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
        public void SignIn_ValidCredentials_SuccessfulSignInTest()
        {
            // Arrange
            _mainPage.SignIn();

            // Act
            _authorizationPage.LogIn(_userName, _userPassword);

            // Assert
            string yourAccountText = _webDriver.FindElement(_authorizationPage.YourAccountButton).Text;
            Assert.IsTrue(yourAccountText.Contains("Your account"), "Text 'Your account' is not visible on the page.");
        }     

        [Test]
        public void TogglePasswordVisibility_CheckVisibilityOfPasswordTest()
        {
            // Arrange
            _mainPage.SignIn();

            // Act
            wait.Until(ExpectedConditions.ElementToBeClickable(_authorizationPage.LoginInput)).SendKeys(_userName);
            _webDriver.FindElement(_authorizationPage.SubmitButton).Click();
            wait.Until(ExpectedConditions.ElementToBeClickable(_authorizationPage.PasswordInput)).SendKeys(_userPassword);

            _webDriver.FindElement(_authorizationPage.VisibilityPasswordText).Click();

            string passwordInputValue = _webDriver.FindElement(_authorizationPage.PasswordInput).GetAttribute("value");
            string expectedPasswordValue = "password1210";

            // Assert
            Assert.AreEqual(expectedPasswordValue, passwordInputValue, "Password input field does not contain the expected value.");
        }

        [Test]
        public void ProblemsLoggingIn_CheckButtonProblemsLoggingInTest()
        {
            // Arrange
            _mainPage.SignIn();

            // Act
            _authorizationPage.ProblemsLogginInButtonClick();

            // Assert
            bool isNeedHelpTextVisible = wait.Until(ExpectedConditions.ElementIsVisible(_authorizationPage.NeedHelpText)).Displayed;
            Assert.IsTrue(isNeedHelpTextVisible, "The 'Need Help' text is not visible on the page.");
        }

        [Test]
        public void CheckClickability_ProblemsLoggingInButtonsAccess_Test()
        {
            // Arrange
            _mainPage.SignIn();

            // Act
            _authorizationPage.ProblemsLogginInButtonClick();

            bool isNeedHelpTextVisible = wait.Until(ExpectedConditions.ElementIsVisible(_authorizationPage.NeedHelpText)).Displayed;
            Assert.IsTrue(isNeedHelpTextVisible, "The 'Need Help' text is not visible on the page.");

            // Assert
            wait.Until(ExpectedConditions.ElementToBeClickable(_authorizationPage.ForgottenPasswordButton));

            wait.Until(ExpectedConditions.ElementToBeClickable(_authorizationPage.ForgottenUserNameButton));

            wait.Until(ExpectedConditions.ElementToBeClickable(_authorizationPage.NoneOfTheAboveButton));
        }

        [Test]
        public void ForgottenPassword_CheckForgottenPassword_ProblemsLoggingInButtonsTest()
        {
            // Arrange
            _mainPage.SignIn();

            // Act
            _authorizationPage.ProblemsLogginInButtonClick();

            bool isNeedHelpTextVisible = wait.Until(ExpectedConditions.ElementIsVisible(_authorizationPage.NeedHelpText)).Displayed;
            Assert.IsTrue(isNeedHelpTextVisible, "The 'Need Help' text is not visible on the page.");

            _webDriver.FindElement(_authorizationPage.ForgottenPasswordButton).Click();

            bool isyourEmailAddressTextVisible = wait.Until(ExpectedConditions.ElementIsVisible(_authorizationPage.YourEmailAddressText)).Displayed;
            Assert.IsTrue(isyourEmailAddressTextVisible, "The 'What is your email address' text is not visible on the page.");

            _webDriver.FindElement(_authorizationPage.EmailInput).Click();
            _webDriver.FindElement(_authorizationPage.EmailInput).SendKeys("berkos@gmail.com");
            _webDriver.FindElement(_authorizationPage.SendPasswordResetEmailButton).Click();

            // Assert
            bool isCheckYourInboxTextVisible = wait.Until(ExpectedConditions.ElementIsVisible(_authorizationPage.CheckYourInboxText)).Displayed;
            Assert.IsTrue(isCheckYourInboxTextVisible, "The 'Please check your inbox' text is not visible on the page.");
        }

        [Test]
        public void ForgottenUserName_CheckForgottenUserName_ProblemsLoggingInButtonsTest()
        {
            // Arrange
            _mainPage.SignIn();

            // Act
            _authorizationPage.ForgottenUserNameButtonClick();

            wait.Until(ExpectedConditions.ElementIsVisible(_authorizationPage.ForgottenUserNameText));

            wait.Until(ExpectedConditions.ElementToBeClickable(_authorizationPage.RegisterAccountButton));

            // Assert
            bool isWrongEmailTextVisible = wait.Until(ExpectedConditions.ElementIsVisible(_authorizationPage.SorryText)).Displayed;
            Assert.IsTrue(isWrongEmailTextVisible, "The 'Sorry, unless you can remember it you'll have to register for a new account' text is not visible on the page.");
        }

        [Test]
        public void ForgottenUserName_YoungerAgeRegisterAccount_ProblemsLoggingInButtonsTest()
        {
            // Arrange
            _mainPage.SignIn();

            // Act
            _authorizationPage.ForgottenUserNameButtonClick();

            _authorizationPage.RegisterAccountButtonClick();

            bool isYourAgeText = wait.Until(ExpectedConditions.ElementIsVisible(_authorizationPage.YourAgeText)).Displayed;
            Assert.IsTrue(isYourAgeText, "The 'Сколько вам лет?' text is not displayed");

            _authorizationPage.InvisibilitySpinner();

            wait.Until(ExpectedConditions.ElementIsVisible(_authorizationPage.Under16Button)).Click();

            // Assert
            bool isSorryTextVisible = wait.Until(ExpectedConditions.ElementIsVisible(_authorizationPage.SorryOnly16sText)).Displayed;
            Assert.IsTrue(isSorryTextVisible, "The 'Sorry, only 16s and over can register outside the UK' text is not visible on the page.");

            _authorizationPage.InvisibilitySpinner();

            wait.Until(ExpectedConditions.ElementToBeClickable(_authorizationPage.OKButton)).Click();
        }

        [Test]
        public void ForgottenUserName_OlderAgeRegisterAccount_ProblemsLoggingInButtonsTest()
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

            wait.Until(ExpectedConditions.ElementToBeClickable(_authorizationPage.DayInput)).SendKeys("12");

            _webDriver.FindElement(_authorizationPage.MonthInput).SendKeys("10");
            _webDriver.FindElement(_authorizationPage.YearInput).SendKeys("1998");
            _webDriver.FindElement(_authorizationPage.SubmitButton).Click();

            // Assert
            bool isRegisterTextVisible = wait.Until(ExpectedConditions.ElementIsVisible(_authorizationPage.RegisterAccountText)).Displayed;
            Assert.IsTrue(isRegisterTextVisible, "The 'Зарегистрироваться на Би-би-си' text is not visible on the page.");
        }     

        [Test]
        public void CheckUnder16YearsOldUserRegister_ProblemsLoggingInButtonsTest()
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

            wait.Until(ExpectedConditions.ElementToBeClickable(_authorizationPage.DayInput)).SendKeys("12");
            _webDriver.FindElement(_authorizationPage.MonthInput).SendKeys("10");
            _webDriver.FindElement(_authorizationPage.YearInput).SendKeys("2021");
            _webDriver.FindElement(_authorizationPage.SubmitButton).Click();

            // Assert
            bool isWrongDataText = wait.Until(ExpectedConditions.ElementIsVisible(_authorizationPage.FormMessage)).Displayed;
            Assert.IsTrue(isWrongDataText, "The 'Sorry, you need to be 16 or over.' text is not displayed");
        }

        [Test]
        public void NoneOfTheAbove_CheckNoneOfTheAbove_ProblemsLoggingInButtons_Test()
        {
            // Arrange
            _mainPage.SignIn();

            // Act
            _authorizationPage.ProblemsLogginInButtonClick();

            bool isNeedHelpTextVisible = wait.Until(ExpectedConditions.ElementIsVisible(_authorizationPage.NeedHelpText)).Displayed;
            Assert.IsTrue(isNeedHelpTextVisible, "The 'Need Help' text is not visible on the page.");

            _webDriver.FindElement(_authorizationPage.NoneOfTheAboveButton).Click();

            Actions actions = new Actions(_webDriver);
            actions.ScrollByAmount(0, 10000).Perform();

            // Assert
            bool isUsingTheBBCLink = wait.Until(ExpectedConditions.ElementIsVisible(_authorizationPage.UsingTheBBCLink)).Enabled;
            Assert.IsTrue(isUsingTheBBCLink, "The link is not enabled");
        }

        [Test]
        public void TheWeather_CheckTheWeather_Test()
        {
            // Arrange
            _mainPage.SignIn();

            _authorizationPage.LogIn(_userName, _userPassword);

            // Act
            string yourAccountText = _webDriver.FindElement(_authorizationPage.YourAccountButton).Text;
            Assert.IsTrue(yourAccountText.Contains("Your account"), "Text 'Your account' is not visible on the page.");

            Actions actions = new Actions(_webDriver);
            actions.ScrollByAmount(0, 1200).Perform();

            wait.Until(ExpectedConditions.ElementToBeClickable(_mainPage.ForecastLink)).Click();

            wait.Until(ExpectedConditions.ElementToBeClickable(_mainPage.EnterCityInput)).SendKeys("Gdansk");

            _webDriver.FindElement(_mainPage.SearchCityButton).Click();

            //wait.Until(ExpectedConditions.ElementToBeClickable(_mainPage.CookieContinueButton)).Click();

            // Assert
            wait.Until(ExpectedConditions.ElementIsVisible(_mainPage.DayWeather)); 
        }

        [Test]
        public void CheckClickability_HeadersButtons_Test()
        {
            // Arrange
            _mainPage.SignIn();

            _authorizationPage.LogIn(_userName, _userPassword);

            // Act
            string yourAccountText = _webDriver.FindElement(_authorizationPage.YourAccountButton).Text;
            Assert.IsTrue(yourAccountText.Contains("Your account"), "Text 'Your account' is not visible on the page.");

            var homeButton = _webDriver.FindElement(_mainPage.HomeButton);
            Assert.IsTrue(homeButton.Enabled);
            var newsButton = _webDriver.FindElement(_mainPage.NewsButton);
            Assert.IsTrue(newsButton.Enabled);
            var sportButton = _webDriver.FindElement(_mainPage.SportButton);
            Assert.IsTrue(sportButton.Enabled);
            var reelButton = _webDriver.FindElement(_mainPage.ReelButton);
            Assert.IsTrue(reelButton.Enabled);
            var workLifeButton = _webDriver.FindElement(_mainPage.WorkLifeButton);
            Assert.IsTrue(workLifeButton.Enabled);
            var travelButton = _webDriver.FindElement(_mainPage.TravelButton);
            Assert.IsTrue(travelButton.Enabled);
            var futureButton = _webDriver.FindElement(_mainPage.FutureButton);
            Assert.IsTrue(futureButton.Enabled);
            var cultureButton = _webDriver.FindElement(_mainPage.CultureButton);
            Assert.IsTrue(cultureButton.Enabled);
        }

        [Test]
        public void CheckSearchBBC_Test()
        {
            // Arrange
            _mainPage.SignIn();

            _authorizationPage.LogIn(_userName, _userPassword);

            // Act
            string yourAccountText = _webDriver.FindElement(_authorizationPage.YourAccountButton).Text;
            Assert.IsTrue(yourAccountText.Contains("Your account"), "Text 'Your account' is not visible on the page.");

            _webDriver.FindElement(_mainPage.SearchBBCButton).Click();
            wait.Until(ExpectedConditions.ElementToBeClickable(_mainPage.SearchBBCInput)).SendKeys("Gdansk");
            _webDriver.FindElement(_mainPage.SearchInfoButton).Click();
        }

        [Test]
        public void SignOut_CheckSignOut_Test()
        {
            // Arrange
            _mainPage.SignIn();

            _authorizationPage.LogIn(_userName, _userPassword);

            // Act
            wait.Until(ExpectedConditions.ElementToBeClickable(_authorizationPage.YourAccountButton)).Click();

            wait.Until(ExpectedConditions.ElementToBeClickable(_authorizationPage.SignOutButton)).Click();

            // Assert
            bool isSignOutTextVisible = wait.Until(ExpectedConditions.ElementIsVisible(_authorizationPage.SignOutText)).Displayed;
            Assert.IsTrue(isSignOutTextVisible, "The 'You've signed out, sorry to see you go.' text is not visible on the page.");
        }

        [TearDown]
        public void TearDown()
        {
            _webDriver.Quit();
        }
    }
}
