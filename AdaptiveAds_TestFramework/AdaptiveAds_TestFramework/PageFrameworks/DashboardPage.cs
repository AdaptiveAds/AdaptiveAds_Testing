using System;
using System.Threading;
using OpenQA.Selenium;
using AdaptiveAds_TestFramework.Helpers;

namespace AdaptiveAds_TestFramework.PageFrameworks
{
    /// <summary>
    /// Dashboard page interaction framework, allows for items on the Dashboard page to be interacted with and manipulated.
    /// </summary>
    public static class DashboardPage
    {
        private static IWebElement _linkObj;

        /// <summary>
        /// Selects the given link on the page. 
        /// </summary>
        /// <param name="link"></param>
        public static void Select(DashboardLink link)
        {
            // Ensure that dashboard is the current page.
            Driver.IsAt(Location.Dashboard);

            string linkName;

            ConfigData.DashboardLinks.TryGetValue(link, out linkName);

            // throw a NotImplementedException if elements was not in the links.
            if (string.IsNullOrWhiteSpace(linkName))
            {
                throw new NotImplementedException("The specified link is not yet implemented into the test framework.");
            }
            try
            {
                _linkObj = Driver.Instance.FindElement(By.Name(linkName));
                _linkObj.Click();
                Thread.Sleep(1000);//wait for page to change
            }
            catch (NoSuchElementException e)
            {
                // throw a NoSuchElementException if elements have not been found.
                if (_linkObj == null)
                    throw new NoSuchElementException("The specified link does not exist.",
                          new NoSuchElementException("User may not have permission to see this data.", e));
            }
        }
    }
}
