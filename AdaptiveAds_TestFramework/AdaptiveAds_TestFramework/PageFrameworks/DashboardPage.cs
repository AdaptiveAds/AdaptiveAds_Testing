using System;
using OpenQA.Selenium;
using AdaptiveAds_TestFramework.ConfigurationManager;
using AdaptiveAds_TestFramework.CustomItems;

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

        /// <summary>
        /// Selects the given link on the page. 
        /// </summary>
        /// <param name="link"></param>
        public static void Select(DashboardLink link)
        {
            // Ensure that dashboard is the current page.
            Driver.IsAt(Location.Dashboard);

            foreach(PairObject linkPair in DataReadWrite.ReadPairObjects("DashboardLinks"))
            {
                if ((DashboardLink)linkPair.Object1 == link)
                {
                    try
                    {
                        string searchString = (string)linkPair.Object2;

                        if (string.IsNullOrWhiteSpace(searchString))
                        {
                            throw new NotFoundException("Invalid search parameter.",
                                  new NotFoundException("The search paremeter provided is null or contains white space."));
                        }
                        linkObj = Driver.Instance.FindElement(By.Name(searchString));
                        linkObj.Click();
                        return;
                    }
                    catch (NoSuchElementException e)
                    {
                        // Errors if elements have not been found.
                        if (linkObj == null)
                            throw new NotFoundException("Link not found.",
                                  new NotFoundException("The specified link could not be found, user may not have permission to see this data.", e));
                    }
                }
            }
            throw new NotImplementedException("The specified link is not yet implimented into the test framework.");            
        }
    }
}
