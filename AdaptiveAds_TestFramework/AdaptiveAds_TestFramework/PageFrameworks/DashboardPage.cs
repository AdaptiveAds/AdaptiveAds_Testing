using System;
using OpenQA.Selenium;
using AdaptiveAds_TestFramework.Config;
using AdaptiveAds_TestFramework.CustomItems;

namespace AdaptiveAds_TestFramework.PageFrameworks
{

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

            string linkName = "";

            Data.DashboardLinks.TryGetValue(link, out linkName);

            if (string.IsNullOrWhiteSpace(linkName))
            {
                throw new NotImplementedException("The specified link is not yet implimented into the test framework.");
            }
            try
            {
                linkObj = Driver.Instance.FindElement(By.Name(linkName));
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
}
