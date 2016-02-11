using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using AdaptiveAds_TestFramework.Config;
using AdaptiveAds_TestFramework.Helpers;

namespace AdaptiveAds_TestFramework.CustomItems
{
    /// <summary>
    /// Contains functionality for browser automation.
    /// </summary>
    public static class Driver
    {
        #region Variables

        private static Period _waitPeriod;

        #endregion//Variables
        
        #region Properties

        /// <summary>
        /// Browser automation object.
        /// </summary>
        public static IWebDriver Instance { get; set; }

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
            Close();
            Instance = new FirefoxDriver(new FirefoxBinary(Data.FireFoxPath), new FirefoxProfile());
            WaitPeriod = Data.DefaultWaitPeriod;
        }

        /// <summary>
        /// Disposes of the automation object.
        /// </summary>
        public static void Close()
        {
            if(Instance!=null)
                Instance.Close();
        }

        #endregion //setup and teardown

        #region NavigableLocations

        /// <summary>
        /// Navigate browser to a given location.
        /// </summary>
        /// <param name="location">Location to navigate to.</param>
        public static void GoTo(Location location)
        {
            // Navigate browser to the location.
            Driver.Instance.Navigate().GoToUrl(Helper.RouteURL(location));
        }

        /// <summary>
        /// Ensures the Driver is at the specified location. 
        /// </summary>
        /// <param name="location">Location to check the browser is at.</param>
        public static void IsAt(Location location)
        {
            string expected = Helper.RouteURL(location);
            string actual = Instance.Url;

            // Check the browser is at the correct location.
            if (actual !=expected)
            {
                //Driver is not at the specified location.
                throw new WebDriverException("Incorrect location.",
                    new InvalidElementStateException(
                        "The given location did not match the browser." +
                        " Expected \"" + expected + "\" Actual \"" + actual + "\""));
            }
        }

        /// <summary>
        /// Ensures the Driver is not at the specified location.  
        /// </summary>
        /// <param name="location">Location to check the browser is not at.</param>
        public static void IsNotAt(Location location)
        {
            string expected = Helper.RouteURL(location);
            string actual = Instance.Url;

            // Check the browser is not at the correct location.
            if (actual == expected)
            {
                //Driver is at the specified location.
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
            Period previousPeriod = WaitPeriod;
            SetWait(waitPeriod);
            action();
            SetWait(previousPeriod);
        }

        /// <summary>
        /// Updates the Automation instance wait period.
        /// </summary>
        /// <param name="waitPeriod">New wait period.</param>
        private static void SetWait(Period waitPeriod)
        {
            int miliseconds;
            Data.WaitPeriods.TryGetValue(waitPeriod, out miliseconds);

            Instance.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromMilliseconds(miliseconds));
        }

        #endregion//WaitHandling

        #endregion//Methods
    }
}
