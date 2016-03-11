﻿using System;
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
        /// Duration automation famework must wait before erroring.
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

        #region setup and teardown

        /// <summary>
        /// Sets up a new automation object.
        /// </summary>
        public static void Initialise()
        {
            Quit();
            Instance = new FirefoxDriver(new FirefoxBinary(ConfigData.FireFoxPath), new FirefoxProfile());
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

        #endregion //setup and teardown

        #region NavigableLocations
        /// <summary>
        /// Navigate browser to a given location.
        /// </summary>
        /// <param name="location">Location to navigate to.</param>
        /// <param name="LogInIfNeeded">Logs in if authentication is required.</param>
        public static void GoTo(Location location, bool LogInIfNeeded = false)
        {
            // Navigate browser to the location.
            Instance.Navigate().GoToUrl(Helper.RouteURL(location));
            if (!LogInIfNeeded) return;
            try
            {
                IsAt(Location.Login);
            }
            catch
            {
                // Not at login page so Login not needed.
                return;
            }
            LoginPage.LoginAs("dev").WithPassword("password").Login();
            GoTo(location);
        }

        /// <summary>
        /// Ensures the Driver is at the specified location, throws a WebDriverException if at another location.
        /// </summary>
        /// <param name="location">Location to check the browser is at.</param>
        public static void IsAt(Location location)
        {
            string expected = Helper.RouteURL(location);
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
        }

        /// <summary>
        /// Ensures the Driver is not at the specified location, throws a WebDriverException if at the location.
        /// </summary>
        /// <param name="location">Location to check the browser is not at.</param>
        public static void IsNotAt(Location location)
        {
            string expected = Helper.RouteURL(location);
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

        /// <summary>
        /// Asserts the logged in state agents the parameter.
        /// </summary>
        /// <param name="CheckLoggedIn">Parameter to check agents logged in state.</param>
        public static void LoggedIn(bool CheckLoggedIn)
        {
            IWebElement SignIn = null;
            IWebElement SignOut = null;
            bool IsLoggedIn = false;

            try { SignIn = Instance.FindElement(By.Name("lnkSignIn")); } catch { }
            try { SignOut = Instance.FindElement(By.Name("lnkSignOut")); } catch { }

            if (SignIn == null && SignOut == null)
            {
                throw new ElementNotVisibleException("Unable to assert state due to unavailability of SignIn/Out links.");
            }

            if (SignIn != null) IsLoggedIn = false;
            if (SignOut != null) IsLoggedIn = true;

            if (IsLoggedIn != CheckLoggedIn)
            {
                throw new Exception(string.Format("Logged in Expected: {0} Actual: {1}", CheckLoggedIn, IsLoggedIn));
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
            catch (Exception e)
            {
                if (errorIfAlreadySignedOut)
                {
                    throw e;
                }
                return;
            }

            IWebElement SignOut = Instance.FindElement(By.Name("lnkSignOut"));
            SignOut.Click();

        }

        #endregion//Methods
    }
}
