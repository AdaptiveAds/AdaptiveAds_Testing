using System;
using OpenQA.Selenium;
using System.Threading;
using AdaptiveAds_TestFramework.Helpers;

namespace AdaptiveAds_TestFramework.PageFrameworks
{
    /// <summary>
    /// Login page interaction framework, allows for items on the Login page to be interacted with and manipulated.
    /// </summary>
    public static class LoginPage
    {
        /// <summary>
        /// Creates a new LoginCommand with the given username for a fluent implementation of login to the system.
        /// LoginAs(Username).WithPassword(Password).Login();
        /// </summary>
        /// <param name="username">Username to login to the system with.</param>
        /// <returns>LoginCommand with a stored set username.</returns>
        public static LoginCommand LoginAs(string username)
        {
            return new LoginCommand(username);
        }

        /// <summary>
        /// Validates the state of the error message matches that of the IsDiplayed parameter.
        /// </summary>
        /// <param name="isDiplayed">The state to which the error message visibility will be compared to.</param>
        public static void ErrorMessage(bool isDiplayed)
        {
            bool shown;
            try
            {
                Driver.Instance.FindElement(By.ClassName(ConfigData.ErrorMessageClass));
                shown = true;
            }
            catch
            {
                shown = false;
            }

            if (shown == isDiplayed) return;

            throw new Exception("The error message visibility did not match that expected. "
                + $"Expected {isDiplayed} actual {shown}", null);



        }
    }

    /// <summary>
    /// Enables a fluent method of implementing the login process.
    /// </summary>
    public class LoginCommand
    {
        #region Variables

        private readonly string _userName;
        private string _password;

        private IWebElement _loginInput;
        private IWebElement _passwordInput;
        private IWebElement _loginButton;

        #endregion //Variables

        #region Methods

        /// <summary>
        /// Must take a username on construction of the object.
        /// </summary>
        /// <param name="userName">Username to login with.</param>
        public LoginCommand(string userName)
        {
            _userName = userName;
        }

        /// <summary>
        /// Sets the password to used in the login process.
        /// </summary>
        /// <param name="password">Password to login with.</param>
        /// <returns>Returns itself enabling continuation of fluent implementation.</returns>
        public LoginCommand WithPassword(string password)
        {
            _password = password;
            return this;
        }

        /// <summary>
        /// Automates the login process.
        /// Throws a WebDriverException if not at the login page.
        /// Throws a NotFoundException if the login items can not be found.
        /// </summary>
        public void Login()
        {
            // Ensure application is at the correct page.
            Driver.IsAt(Location.Login);

            // Attempt to find elements on the page.
            try
            {
                _loginInput = Driver.Instance.FindElement(By.Name(ConfigData.LoginUsernameBoxName));
                _passwordInput = Driver.Instance.FindElement(By.Name(ConfigData.LoginPasswordBoxName));
                _loginButton = Driver.Instance.FindElement(By.ClassName(ConfigData.LoginButtonClass));
            }
            catch (NoSuchElementException e)
            {
                // Errors if elements have not been found.
                if (_loginInput == null || _passwordInput == null || _loginButton == null)
                    throw new NotFoundException("Page elements not found.",
                          new NotFoundException("At least one of the following login elements were not found. " +
                          "Username, " +
                          "Password or " +
                          "Submit.", e));
            }

            // Login
            _loginInput.SendKeys(_userName);
            _passwordInput.SendKeys(_password);
            _loginButton.Click();

            //Wait allows system time to process login.
            Thread.Sleep(2500);
        }

        #endregion //Methods
    }
}