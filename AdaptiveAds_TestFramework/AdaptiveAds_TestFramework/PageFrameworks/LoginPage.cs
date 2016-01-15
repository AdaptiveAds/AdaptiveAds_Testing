using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Threading;

namespace AdaptiveAds_TestFramework.PageFrameworks
{
    /// <summary>
    /// TODO: Fill this in
    /// </summary>
    public static class LoginPage
    {
        private static string _url = "http://adaptiveads.uk/auth/login";

        /// <summary>
        /// TODO: Fill this in
        /// </summary>
        public static void GoTo()
        {
            Driver.Instance.Navigate().GoToUrl(_url);
            //var wait = new WebDriverWait(Driver.Instance, TimeSpan.FromSeconds(10));

            //wait.Until(d =>)

        }

        /// <summary>
        /// TODO: Fill this in
        /// </summary>
        /// <param name="username">TODO: Fill this in</param>
        /// <returns>TODO: Fill this in</returns>
        public static LoginCommand LoginAs(string username)
        {
            return new LoginCommand(username);
        }
        
        public static bool ShowingErrorMessage()
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// TODO: Fill this in
    /// </summary>
    public class LoginCommand
    {
        #region Variables

        private string _userName = "";
        private string _password = "";

        private string loginInputName = "username";
        private string passwordInputName = "password";
        private string loginButtonClass = "submit";

        private IWebElement loginInput;
        private IWebElement passwordInput;
        private IWebElement loginButton;

        #endregion //Variables

        #region Methods

        /// <summary>
        /// TODO: Fill this in
        /// </summary>
        /// <param name="userName">TODO: Fill this in</param>
        public LoginCommand(string userName)
        {
            _userName = userName;
        }

        /// <summary>
        /// TODO: Fill this in
        /// </summary>
        /// <param name="password">TODO: Fill this in</param>
        /// <returns></returns>
        public LoginCommand WithPassword(string password)
        {
            _password = password;
            return this;
        }

        /// <summary>
        /// TODO: Fill this in
        /// </summary>
        public void Login()
        {
            // Ensure application is at the correct page.
            Driver.IsAt(Location.Login);

            // Attempt to find elements on the page
            try
            {
                loginInput = Driver.Instance.FindElement(By.Name(loginInputName));
                passwordInput = Driver.Instance.FindElement(By.Name(passwordInputName));
                loginButton = Driver.Instance.FindElement(By.ClassName(loginButtonClass));
            }
            catch (NoSuchElementException e)
            {
                // Ensure elements have been found
                if (loginInput == null || passwordInput == null || loginButton == null)
                    throw new NotFoundException("Page elements not found",
                          new NotFoundException("At least one of the following login elements were not found. " +
                          "Username, " +
                          "Password or " +
                          "Submit."));
            }


            // Login
            loginInput.SendKeys(_userName);
            passwordInput.SendKeys(_password);
            loginButton.Click();

            Thread.Sleep(5000);
        }

        #endregion //Methods
    }
}
