using System;

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
        /// <summary>
        /// Selects the given link on the page. 
        /// </summary>
        /// <param name="link"></param>
        public static void Select(DashboardLink link)
        {
            // Ensure that dashboard is the current page.
            Driver.IsAt(Location.Dashboard);

            // Select the given link.
            switch (link)
            {
                //TODO: impliment selection of links
                default:
                    {
                        throw new NotImplementedException("The specified link is not yet implimented into the test framework.");
                    }
            }
        }
    }
}
