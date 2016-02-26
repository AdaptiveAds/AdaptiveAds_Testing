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
        /// Creates a new LoginCommand with the given username for a fluent implimentation of login to the system.
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
        /// <param name="IsDiplayed">The state to which the error message visibility will be compared to.</param>
        public static void ErrorMessage(bool IsDiplayed)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Enables a fluent method of implementing the login process.
    /// </summary>
    public class LoginCommand
    {
        #region Variables

        private string _userName = "";
        private string _password = "";
        
        private IWebElement loginInput;
        private IWebElement passwordInput;
        private IWebElement loginButton;

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
        /// <returns>Returns itself enabling continuation of fluent imlementation.</returns>
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
                loginInput = Driver.Instance.FindElement(By.Name(ConfigData.loginUsernameBoxName));
                passwordInput = Driver.Instance.FindElement(By.Name(ConfigData.loginPasswordBoxName));
                loginButton = Driver.Instance.FindElement(By.ClassName(ConfigData.loginButtonClass));
            }
            catch (NoSuchElementException e)
            {
                // Errors if elements have not been found.
                if (loginInput == null || passwordInput == null || loginButton == null)
                    throw new NotFoundException("Page elements not found.",
                          new NotFoundException("At least one of the following login elements were not found. " +
                          "Username, " +
                          "Password or " +
                          "Submit.",e));
            }
            
            // Login
            loginInput.SendKeys(_userName);
            passwordInput.SendKeys(_password);
            loginButton.Click();

            //Wait allows system time to process login.
            Thread.Sleep(2500);
        }

        #endregion //Methods
    }
}