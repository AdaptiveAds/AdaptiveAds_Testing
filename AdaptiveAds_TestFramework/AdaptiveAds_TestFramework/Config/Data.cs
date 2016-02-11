using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdaptiveAds_TestFramework.Config
{
    #region Enums

    /// <summary>
    /// Location in the framework supported in the test framework.
    /// </summary>
    public enum Location
    {
        Login,
        Dashboard,
        Adverts,
        Playlists
    }

    /// <summary>
    /// Period of time.
    /// </summary>
    public enum Period
    {
        None,
        Short,
        Medium,
        Long
    }

    /// <summary>
    /// Links available on the Dashboard page.
    /// </summary>
    public enum DashboardLink
    {
        Adverts,
        Playlists
    } 

    #endregion

    public static class Data
    {
        #region Driver

        public static string FireFoxPath = @"C:\Program Files (x86)\Mozilla Firefox\firefox.exe";

        public static string URL_Address = "http://adaptiveads.uk";

        public static Period DefaultWaitPeriod = Period.None;
                
        /// <summary>
        /// Routes, dictionary of locations and extensions
        /// </summary>
        public static Dictionary<Location, string> Routes = new Dictionary<Location, string>()
            {
                {Location.Adverts,"/dashboard/advert"},
                {Location.Dashboard,"/dashboard"},
                {Location.Login,"/auth/login"}
            };
        
        /// <summary>
        /// WaitPeriods, dictionary of wait names and associated time in millisecconds
        /// </summary>
        public static Dictionary<Period, int> WaitPeriods = new Dictionary<Period, int>()
            {
                {Period.None,0},
                {Period.Short,500},
                {Period.Medium,2500},
                {Period.Long,5000}
            };

        #endregion //Driver
        
        #region Login

        public static string loginUsernameBoxName = "username";
        public static string loginPasswordBoxName = "password";
        public static string loginButtonClass = "submit";

        #endregion //Login
        
        #region Dashboard

        /// <summary>
        /// DashboardLinks, dictionary of link types and link id's
        /// </summary>
        public static Dictionary<DashboardLink, string> DashboardLinks = new Dictionary<DashboardLink, string>()
            {
                {DashboardLink.Adverts,"ulAdverts" },
                {DashboardLink.Playlists,"ulPlaylists" }
            };

        #endregion //Dashboard
    }
}
