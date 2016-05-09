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
        Playlists,
        ///<summary>Here users can view a list of locations and from here perform actions such as create, edit and delete.</summary>
        Locations,
        ///<summary>Here users can view a list of departments and from here perform actions such as create, edit and delete.</summary>
        Departments,
        ///<summary>Here users can view a list of screens and from here perform actions such as create, edit and delete.</summary>
        Screens,
        ///<summary>Here users can view a list of users and from here perform actions such as create, edit and delete.</summary>
        Users,
        ///<summary>Here users can view a list of templates and from here perform actions such as create, edit and delete.</summary>
        Templates,
        ///<summary>Here users can view a list of skins and from here perform actions such as create, edit and delete.</summary>
        Skins,
        ///<summary>Here users can view a list of privileges and from here perform actions such as create, edit and delete.</summary>
        Privileges
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
        Playlists,
        ///<summary>A link from the Dashboard to the Locations page</summary>
        Locations,
        ///<summary>A link from the Dashboard to the Departments page</summary>
        Departments,
        ///<summary>A link from the Dashboard to the Screens page</summary>
        Screens,
        ///<summary>A link from the Dashboard to the Users page</summary>
        Users,
        ///<summary>A link from the Dashboard to the Templates page</summary>
        Templates,
        ///<summary>A link from the Dashboard to the Skins page</summary>
        Skins,
        ///<summary>A link from the Dashboard to the Privileges page</summary>
        Privileges
    }

    #endregion //Enums

    /// <summary>
    /// Contains configuration settings data for automation.
    /// </summary>
    public static class ConfigData
    {
        #region Driver

        /// <summary>
        /// Path of the firefox.exe installed on the local machine.
        /// </summary>
        public static string FireFoxPath = @"C:\Program Files (x86)\Mozilla Firefox\firefox.exe";

        /// <summary>
        /// URL of the application, This should be "http://adaptiveads.uk" and modified for local testing only.
        /// Changes to this should only be committed to production if the default location of the application changes.
        /// </summary>
        public static string UrlAddress = "http://localhost";

        /// <summary>
        /// Time for the automation framework to wait before throwing an error as default.
        /// </summary>
        public static Period DefaultWaitPeriod = Period.None;

        /// <summary>
        /// Routes, dictionary of locations and URL extensions.
        /// </summary>
        public static Dictionary<Location, string> Routes = new Dictionary<Location, string>(){
                {Location.Adverts,"/dashboard/advert"},
                {Location.Dashboard,"/dashboard"},
                {Location.Login,"/login"},
                {Location.Playlists,"/dashboard/playlist"},
                {Location.Locations,"/dashboard/settings/locations"},
                {Location.Departments,"/dashboard/settings/departments"},
                {Location.Screens,"/dashboard/settings/screens"},
                {Location.Users,"/dashboard/settings/users"},
                {Location.Templates,"/dashboard/settings/templates"},
                {Location.Skins,"/dashboard/settings/skins"},
                {Location.Privileges,"/dashboard/settings/privileges"}
            };

        /// <summary>
        /// WaitPeriods, dictionary of periods and associated time in milliseconds.
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
        /// Name of the SignIn link.
        /// </summary>
        public static string SignInName = "lnkSignIn";

        /// <summary>
        /// Name of the SignOut link.
        /// </summary>
        public static string SignOutName = "lnkSignOut";

        /// <summary>
        /// A valid username for the system.
        /// </summary>
        public static string Username = "dev";

        /// <summary>
        /// A valid password given the username for the system.
        /// </summary>
        public static string Password = "password";

        /// <summary>
        /// Name of the login page username input.
        /// </summary>
        public static string LoginUsernameBoxName = "login";

        /// <summary>
        /// Name of the login page password input.
        /// </summary>
        public static string LoginPasswordBoxName = "password";

        /// <summary>
        /// Class name of the login page submit button.
        /// </summary>
        public static string LoginButtonClass = "submit";

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
                {DashboardLink.Playlists,"liPlaylist" },
                {DashboardLink.Locations,"liLocations" },
                {DashboardLink.Departments,"liDepartments" },
                {DashboardLink.Screens,"liScreens" },
                {DashboardLink.Users,"liUsers" },
                {DashboardLink.Templates,"liTemplates" },
                {DashboardLink.Skins,"liSkins" },
                {DashboardLink.Privileges,"liPrivileges" }
            };

        #endregion //Dashboard

        #region Department

        #region Add

        /// <summary>
        /// Name of the button to add a department.
        /// </summary>
        public static string DepartmentAdd = "btnAddDepartment";

        /// <summary>
        /// Name of the text input for department name.
        /// </summary>
        public static string DepartmentAddName = "txtDepartmentName";

        /// <summary>
        /// Name of the button to save a department.
        /// </summary>
        public static string DepartmentAddSave = "btnSave";

        #endregion //Add

        #region Edit

        /// <summary>
        /// Name of the button to edit a department.
        /// </summary>
        public static string DepartmentEdit = "btnEdit";

        /// <summary>
        /// Name of the text input for editing department name.
        /// </summary>
        public static string DepartmentEditName = "txtDepartmentName";

        /// <summary>
        /// Name of the button to save an edit.
        /// </summary>
        public static string DepartmentEditSave = "btnSave";

        #endregion //Edit

        #region Delete

        /// <summary>
        /// Name of the button to delete a department.
        /// </summary>
        public static string DepartmentDelete = "btnDelete";

        /// <summary>
        /// Name of the button to confirm a delete.
        /// </summary>
        public static string DepartmentDeleteConfirm = "btnSave";

        /// <summary>
        /// Name of the button to cancel a delete.
        /// </summary>
        public static string DepartmentDeleteCancel = "btnCancel";

        #endregion //Delete

        /// <summary>
        /// Name of the container for departments.
        /// </summary>
        public static string DepartmentsContainer = "lstDepartmentItems";

        #endregion //Department

    }
}
