using System;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace AdaptiveAds_TestFramework
{
    public static class Driver
    {
        #region Variables

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

        static Driver()
        {
            NavigableLocations = new Collection<Route>
            {
                new Route(Location.Login, "/auth/login"),
                new Route(Location.Dashboard, "/dashboard"),
                new Route(Location.Adverts, "/dashboard/advert")
            };
        }

        #endregion

        #region Properties

        public static IWebDriver Instance { get; set; }

        public static Collection<Route> NavigableLocations { get; set; } 

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

        public static void Initialise()
        {
            Close();
            Instance = new FirefoxDriver(new FirefoxBinary(@"C:\Program Files (x86)\Mozilla Firefox\firefox.exe"), new FirefoxProfile());
            WaitPeriod = Period.None;
        }

        public static void Close()
        {
            if(Instance!=null)
                Instance.Close();
        }

        #endregion //setup and teardown

        #region NavigableLocations
        
        /// <summary>
        /// TODO: Fill this in
        /// </summary>
        public static void GoTo(Location location)
        {
            foreach (Route route in NavigableLocations)
            {
                if (route.Location == location)
                {
                    Driver.Instance.Navigate().GoToUrl(_address + route.UrlExtension);
                    //Driver navigated to the specified location.
                    return;
                }
            }
            //Did not find the location in the list.
            throw new NotFoundException("Route not found.",
                new NotImplementedException("Route not yet supported within Driver."));
        }

        /// <summary>
        /// Ensures the Driver is at the specified location. 
        /// </summary>
        /// <param name="location">Route to check the browser is at.</param>
        public static void IsAt(Location location)
        {
            foreach (Route route in NavigableLocations)
            {
                if (route.Location == location)
                {
                    if (Instance.Url != _address + route.UrlExtension)
                    {
                        //Driver is not at the specified location.
                        throw new WebDriverException("Incorrect location.",
                            new InvalidElementStateException(
                                "The location did not match the driver." +
                                " Expected \"" + _address + route.UrlExtension + "\" Actual \"" + Instance.Url + "\""));
                    }
                    //Driver is at the specified location.
                    return;
                }
            }
            //Did not find the location in the list.
            throw new NotFoundException("Route not found.",
                new NotImplementedException("Route not yet supported within Driver."));
        }

        /// <summary>
        /// Ensures the Driver is not at the specified location.  
        /// </summary>
        /// <param name="LocationName">Route to check the browser is not at.</param>
        public static void IsNotAt(Location LocationName)
        {
            foreach (Route location in NavigableLocations)
            {
                if (location.Location == LocationName)
                {
                    if (Instance.Url == _address + location.UrlExtension)
                    {
                        //Driver is at the specified location.
                        throw new WebDriverException("Incorrect location.",
                            new InvalidElementStateException(
                                "The location matched the driver."));
                    }
                    //Driver is not at the specified location.
                    return;
                }
            }
            //Did not find the location in the list.
            throw new NotFoundException("Route not found.",
                new NotImplementedException("Route not yet supported within Driver."));
        }

        #endregion//NavigableLocations

        #region WaitHandling

        public static void ActionWait(Period waitPeriod, Action action)
        {
            Period wp = WaitPeriod;
            SetWait(waitPeriod);
            action();
            SetWait(wp);
        }

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
