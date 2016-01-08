using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdaptiveAds_TestFramework.PageFrameworks
{
    public static class DashboardPage
    {
        private static string _url = "https://adaptiveads.uk/Dashboard/";

        /// <summary>
        /// TODO: Fill this in
        /// </summary>
        public static void GoTo()
        {
            Driver.Instance.Navigate().GoToUrl(_url);
        }
    }
}
