using System;
using OpenQA.Selenium;
using AdaptiveAds_TestFramework.CustomItems;

namespace AdaptiveAds_TestFramework.PageFrameworks
{
    /// <summary>
    /// Dashboard page interaction framework, allows for items on the Dashboard page to be interacted with and manipulated.
    /// Throws a NotImplementedException if the link is not set up in ConfigData.DashboardLinks.
    /// Throws a NoSuchElementException if the link items are not found on the page.
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

            ConfigData.DashboardLinks.TryGetValue(link, out linkName);

            // throw a NotImplementedException if elements was not in the links.
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
                // throw a NoSuchElementException if elements have not been found.
                if (linkObj == null)
                    throw new NoSuchElementException("The specified link does not exist.",
                          new NoSuchElementException("User may not have permission to see this data.", e));
            }
        }
    }
}
