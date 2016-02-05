using OpenQA.Selenium;
using System;
using System.Collections.Generic;

namespace AdaptiveAds_TestFramework.PageFrameworks
{
    /// <summary>
    /// Links available on the Dashboard page.
    /// </summary>
    public enum DashboardLink
    {
        Adverts,
        Playlists
    }

    /// <summary>
    /// Dashboard page interaction framework, allows for items on the Dashboard page to be interacted with and manipulated.
    /// </summary>
    public static class DashboardPage
    {
        private static IWebElement linkObj;

        static DashboardPage()
        {
            DashboardLinks = new Dictionary<DashboardLink, string>();
            DashboardLinks.Add(DashboardLink.Adverts, "ulAdverts");
            DashboardLinks.Add(DashboardLink.Playlists, "ulPlaylists");
        }

        /// <summary>
        /// TODO: Remove and create config class to hold this data.
        /// </summary>
        private static Dictionary<DashboardLink,string> DashboardLinks { get; set; }

        /// <summary>
        /// Selects the given link on the page. 
        /// </summary>
        /// <param name="link"></param>
        public static void Select(DashboardLink link)
        {
            // Ensure that dashboard is the current page.
            Driver.IsAt(Location.Dashboard);

            foreach(KeyValuePair<DashboardLink,string> linkPair in DashboardLinks)
            {
                if (linkPair.Key == link)
                {
                    try
                    {
                        linkObj = Driver.Instance.FindElement(By.Name(linkPair.Value));
                        linkObj.Click();
                        return;
                    }
                    catch (NoSuchElementException e)
                    {
                        // Errors if elements have not been found.
                        if (linkObj == null)
                            throw new NotFoundException("Link not found.",
                                  new NotFoundException("The specified link could not be found, user may not have permission to see this data.",e));
                    }
                }
            }
            throw new NotImplementedException("The specified link is not yet implimented into the test framework.");            
        }
    }
}
