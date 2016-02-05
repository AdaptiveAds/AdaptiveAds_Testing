using System;
using System.Collections.ObjectModel;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using AdaptiveAds_TestFramework.CustomItems;

namespace AdaptiveAds_TestFramework
{
    /// <summary>
    /// Contains functionality for browser automation.
    /// </summary>
    public static class Driver
    {
        #region Variables

        /// <summary>
        /// Period of time.
        /// </summary>
        public enum Period
        {
            None,
            Short,
            Default,
            Long
        }

        private static Period _waitPeriod;

        private static string _address = "http://adaptiveads.uk";

        #endregion//Variables

        #region Constructor

        /// <summary>
        /// Constructs class with list Routes
        /// </summary>
        static Driver()
        {
            Routes = new Collection<Route>
            {
                new Route(Location.Login, "/auth/login"),
                new Route(Location.Dashboard, "/dashboard"),
                new Route(Location.Adverts, "/dashboard/advert")
            };
        }

        #endregion

        #region Properties

        /// <summary>
        /// Browser automation object.
        /// </summary>
        public static IWebDriver Instance { get; set; }
        
        private static Collection<Route> Routes { get; set; } 

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
            Instance = new FirefoxDriver(new FirefoxBinary(@"C:\Program Files (x86)\Mozilla Firefox\firefox.exe"), new FirefoxProfile());
            WaitPeriod = Period.None;
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
            // Search for location in Routes.
            foreach (Route route in Routes)
            {
                if (route.Location == location)
                {
                    // Navigate browser to the location.
                    Driver.Instance.Navigate().GoToUrl(_address + route.UrlExtension);
                    return;
                }
            }
            // Did not find the location in the Routes.
            throw new NotFoundException("Route not found.",
                new NotImplementedException("Location not yet supported within Driver."));
        }

        /// <summary>
        /// Ensures the Driver is at the specified location. 
        /// </summary>
        /// <param name="location">Location to check the browser is at.</param>
        public static void IsAt(Location location)
        {
            // Search for location in Routes.
            foreach (Route route in Routes)
            {
                if (route.Location == location)
                {
                    // Check the browser is at the correct location.
                    if (Instance.Url != _address + route.UrlExtension)
                    {
                        //Driver is not at the specified location.
                        throw new WebDriverException("Incorrect location.",
                            new InvalidElementStateException(
                                "The given location did not match the browser." +
                                " Expected \"" + _address + route.UrlExtension + "\" Actual \"" + Instance.Url + "\""));
                    }
                    //Driver is at the specified location.
                    return;
                }
            }
            //Did not find the location in Routes.
            throw new NotFoundException("Route not found.",
                new NotImplementedException("Location not yet supported within Driver."));
        }

        /// <summary>
        /// Ensures the Driver is not at the specified location.  
        /// </summary>
        /// <param name="Location">Location to check the browser is not at.</param>
        public static void IsNotAt(Location Location)
        {
            // Search for location in Routes.
            foreach (Route location in Routes)
            {
                if (location.Location == Location)
                {
                    // Check the browser is not at the correct location.
                    if (Instance.Url == _address + location.UrlExtension)
                    {
                        //Driver is at the specified location.
                        throw new WebDriverException("Incorrect location.",
                            new InvalidElementStateException(
                                "The given location matched the browser."));
                    }
                    //Driver is not at the specified location.
                    return;
                }
            }
            //Did not find the location in Routes.
            throw new NotFoundException("Route not found.",
                new NotImplementedException("Location not yet supported within Driver."));
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
        /// <param name="wp">New wait period.</param>
        private static void SetWait(Period wp)
        {
            int miliseconds;
            switch (wp)
            {
                case Period.None:
                    miliseconds = 0;
                    break;

                case Period.Short:
                    miliseconds = 500;
                    break;

                case Period.Long:
                    miliseconds = 5000;
                    break;

                case Period.Default:
                default:
                    miliseconds = 2500;
                    break;
            }
            Instance.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromMilliseconds(miliseconds));
        }

        #endregion//WaitHandling

        #endregion//Methods
    }
}
