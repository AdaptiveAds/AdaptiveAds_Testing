using System;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using AdaptiveAds_TestFramework.PageFrameworks;

namespace AdaptiveAds_TestFramework.Helpers
{
    /// <summary>
    /// Contains functionality for browser automation.
    /// </summary>
    public static class Driver
    {
        #region Variables

        private static IWebDriver _instance;
        private static Period _waitPeriod;

        #endregion//Variables

        #region Properties

        /// <summary>
        /// Browser automation object.
        /// </summary>
        public static IWebDriver Instance
        {
            get { return _instance; }
            set { _instance = value; SetWait(WaitPeriod); }
        }

        /// <summary>
        /// Duration automation framework must wait before throwing an error.
        /// </summary>
        public static Period WaitPeriod
        {
            get { return _waitPeriod; }
            set
            {
                _waitPeriod = value;
                SetWait(_waitPeriod);
            }
        }

        #endregion//Properties

        #region Methods

        #region set-up and tear-down

        /// <summary>
        /// Sets up a new automation object.
        /// </summary>
        public static void Initialise()
        {
            Quit();
            Instance = new FirefoxDriver(new FirefoxBinary(ConfigData.FireFoxPath), new FirefoxProfile());
            Instance.Manage().Window.Maximize();
            WaitPeriod = ConfigData.DefaultWaitPeriod;
        }

        /// <summary>
        /// Disposes of the automation object.
        /// </summary>
        public static void Quit()
        {
            if (Instance != null)
                Instance.Quit();
        }

        #endregion //set-up and tear-down

        #region NavigableLocations

        /// <summary>
        /// Navigate browser to a given location.
        /// </summary>
        /// <param name="location">Location to navigate to.</param>
        /// <param name="logInIfNeeded">Logs in if authentication is required.</param>
        /// <param name="errorIfNotReached">Errors if the location was not reached.</param>
        public static void GoTo(Location location, bool logInIfNeeded, bool errorIfNotReached)
        {
            // Navigate browser to the location.
            Instance.Navigate().GoToUrl(Helper.RouteUrl(location));
            Thread.Sleep(1000);// wait for system to navigate
            bool needToLogIn = false;
            if (logInIfNeeded)
            {
                try
                {
                    IsAt(Location.Login);
                    needToLogIn = true;
                }
                catch
                {
                    // Not at login page so Login not needed.
                }
                if (needToLogIn)
                {
                    LoginPage.LoginAs(ConfigData.Username).WithPassword(ConfigData.Password).Login();
                    GoTo(location, false, errorIfNotReached);
                }
            }
            if (errorIfNotReached)
            {
                IsAt(location);
            }
        }

        /// <summary>
        /// Ensures the Driver is at the specified location, throws a WebDriverException if at another location.
        /// </summary>
        /// <param name="location">Location to check the browser is at.</param>
        public static void IsAt(Location location)
        {
            string expected = Helper.RouteUrl(location);
            string actual = Instance.Url;

            // Check the browser is at the correct location.
            if (actual != expected)
            {
                // Driver is not at the specified location.
                throw new WebDriverException("Incorrect location.",
                    new InvalidElementStateException(
                        "The given location did not match the browser." +
                        " Expected \"" + expected + "\" Actual \"" + actual + "\""));
            }
            ActionWait(Period.None, CheckForLavarelError);
        }

        /// <summary>
        /// Ensures the Driver is not at the specified location, throws a WebDriverException if at the location.
        /// </summary>
        /// <param name="location">Location to check the browser is not at.</param>
        public static void IsNotAt(Location location)
        {
            string expected = Helper.RouteUrl(location);
            string actual = Instance.Url;

            // Check the browser is not at the correct location.
            if (actual == expected)
            {
                // Driver is at the specified location.
                throw new WebDriverException("Incorrect location.",
                            new InvalidElementStateException(
                                "The given location matched the browser."));
            }
        }

        #endregion//NavigableLocations

        #region WaitHandling

        /// <summary>
        /// Performs an action with a temporary wait period.
        /// </summary>
        /// <param name="waitPeriod">Period to wait while executing action.</param>
        /// <param name="action">Action to execute.</param>
        public static void ActionWait(Period waitPeriod, Action action)
        {
            // Store the current wait period
            Period previousPeriod = WaitPeriod;
            // Run task with given wait period
            SetWait(waitPeriod);
            action();
            // Revert to the old wait period
            SetWait(previousPeriod);
        }

        /// <summary>
        /// Updates the Automation instance wait period.
        /// </summary>
        /// <param name="waitPeriod">New wait period.</param>
        private static void SetWait(Period waitPeriod)
        {
            int miliseconds;
            ConfigData.WaitPeriods.TryGetValue(waitPeriod, out miliseconds);

            // Set the drivers instance to use the wait period.
            if (Instance != null)
            {
                Instance.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromMilliseconds(miliseconds));
            }
        }

        #endregion//WaitHandling

        #region AppState

        /// <summary>
        /// Checks that the website hasn't crashed from the back end.
        /// </summary>
        public static void CheckForLavarelError()
        {
            bool error = false;
            try
            {
                Instance.FindElement(By.ClassName("exception_message"));
                error = true;
            }
            catch
            {
                // all good, could not find error content.
            }
            if (error)
            {
                throw new Exception("Lavarel threw an error");
            }
        }

        /// <summary>
        /// Clicks on the main menu button to open or close the main menu.
        /// </summary>
        public static void OpenCloseMenuBar()
        {
            try
            {
                IWebElement menuButton = Instance.FindElement(By.Name(ConfigData.MainMenuButtonName));
                menuButton.Click();
            }
            catch (Exception e)
            {

                throw new NoSuchElementException("Could not find the Menu button element.", e);
            }
        }

        /// <summary>
        /// Asserts the logged in state agents the parameter.
        /// </summary>
        /// <param name="checkLoggedIn">Parameter to check agents logged in state.</param>
        public static void LoggedIn(bool checkLoggedIn)
        {
            OpenCloseMenuBar();

            bool signInFound;
            bool signOutFound;
            bool isLoggedIn = false;

            try { Instance.FindElement(By.Name(ConfigData.SignInName)); signInFound = true; }
            catch { signInFound = false; }
            try { Instance.FindElement(By.Name(ConfigData.SignOutName)); signOutFound = true; }
            catch { signOutFound = false; }

            if (!signInFound && !signOutFound)
            {
                throw new ElementNotVisibleException("Unable to assert state due to unavailability of SignIn/Out links.");
            }

            if (signOutFound) isLoggedIn = true;
            if (signInFound) isLoggedIn = false;

            if (isLoggedIn != checkLoggedIn)
            {
                throw new Exception($"Logged in Expected: {checkLoggedIn} Actual: {isLoggedIn}");
            }
        }

        /// <summary>
        /// Signs out of the system.
        /// </summary>
        /// <param name="errorIfAlreadySignedOut">Determines whether to throw an error if already signed out.</param>
        public static void SignOut(bool errorIfAlreadySignedOut = true)
        {
            try
            {
                LoggedIn(true);
            }
            catch (Exception)
            {
                if (errorIfAlreadySignedOut)
                {
                    throw;
                }
                return;
            }

            IWebElement signOut = Instance.FindElement(By.Name(ConfigData.SignOutName));
            signOut.Click();
            Thread.Sleep(1000);// wait for system to logout
        }

        #endregion//AppState

        #endregion//Methods
    }
}
