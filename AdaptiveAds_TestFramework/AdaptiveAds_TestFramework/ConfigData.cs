using System.Collections.Generic;

namespace AdaptiveAds_TestFramework
{
    #region Enums

    /// <summary>
    /// Location in the application supported by the test framework.
    /// </summary>
    public enum Location
    {
        ///<summary>Login page where users verify their eligibility to use the system.</summary>
        Login,
        ///<summary>This is the main hub for the application. From here users can navigate to most places in the application.</summary>
        Dashboard,
        ///<summary>Here users can view a list of adverts and from here perform actions such as create, edit and delete.</summary>
        Adverts,
        ///<summary>Here users can view a list of playlists and from here perform actions such as create, edit and delete.</summary>
        Playlists
    }

    /// <summary>
    /// Period of time.
    /// </summary>
    public enum Period
    {
        ///<summary>An empty time period.</summary>
        None,
        ///<summary>A time period of Short length.</summary>
        Short,
        ///<summary>A time period of Medium length.</summary>
        Medium,
        ///<summary>A time period of Long length.</summary>
        Long
    }

    /// <summary>
    /// Links available on the Dashboard page.
    /// </summary>
    public enum DashboardLink
    {
        ///<summary>A link from the Dashboard to the Adverts page</summary>
        Adverts,
        ///<summary>A link from the Dashboard to the Playlists page</summary>
        Playlists
    }

    #endregion

    /// <summary>
    /// Contains configuration settings data for automation.
    /// </summary>
    public static class ConfigData
    {
        #region Driver

        /// <summary>
        /// Path of the firefox.exe installed on the local machiene.
        /// </summary>
        public static string FireFoxPath = @"C:\Program Files (x86)\Mozilla Firefox\firefox.exe";

        /// <summary>
        /// URL of the application, This should be "http://adaptiveads.uk" and modified for local testing only.
        /// Changes to this should only be committed to production if the default location of the application changes.
        /// </summary>
        public static string URL_Address = "http://localhost";

        /// <summary>
        /// Time for the automation framework to wait before erroring as default.
        /// </summary>
        public static Period DefaultWaitPeriod = Period.None;

        /// <summary>
        /// Routes, dictionary of locations and URL extensions.
        /// </summary>
        public static Dictionary<Location, string> Routes = new Dictionary<Location, string>(){
                {Location.Adverts,"/dashboard/advert"},
                {Location.Dashboard,"/dashboard"},
                {Location.Login,"/login"},
                {Location.Playlists,"/dashboard/playlist"}
            };

        /// <summary>
        /// WaitPeriods, dictionary of periods and associated time in millisecconds.
        /// </summary>
        public static Dictionary<Period, int> WaitPeriods = new Dictionary<Period, int>(){
                {Period.None,0},
                {Period.Short,500},
                {Period.Medium,2500},
                {Period.Long,5000}
        };

        #endregion //Driver

        #region Login

        /// <summary>
        /// Name of the login page username input.
        /// </summary>
        public static string loginUsernameBoxName = "login";

        /// <summary>
        /// Name of the login page password input.
        /// </summary>
        public static string loginPasswordBoxName = "password";

        /// <summary>
        /// Class name of the login page submit button.
        /// </summary>
        public static string loginButtonClass = "submit";

        /// <summary>
        /// Class name of the login page error message.
        /// </summary>
        public static string ErrorMessageClass = "alert-danger";

        #endregion //Login

        #region Dashboard

        /// <summary>
        /// DashboardLinks, dictionary of DashboardLink types and link names.
        /// </summary>
        public static Dictionary<DashboardLink, string> DashboardLinks = new Dictionary<DashboardLink, string>()
            {
                {DashboardLink.Adverts,"liAdvert" },
                {DashboardLink.Playlists,"liPlaylist" }
            };

        #endregion //Dashboard
    }
}
