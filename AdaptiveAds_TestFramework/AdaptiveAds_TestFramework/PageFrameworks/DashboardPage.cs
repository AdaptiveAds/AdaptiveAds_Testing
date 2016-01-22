using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdaptiveAds_TestFramework.PageFrameworks
{
        public enum DashboardLink
        {
            Adverts,
            Playlists
        }
    public static class DashboardPage
    {
        /// <summary>
        /// TODO: Fill this in
        /// </summary>
        public static void Select(DashboardLink link)
        {
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
