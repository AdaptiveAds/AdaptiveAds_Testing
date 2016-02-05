namespace AdaptiveAds_TestFramework.CustomItems
{
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
    /// Holds a location and its URL extension.
    /// </summary>
    public class Route
    {
        /// <summary>
        /// Stores a location and its URL extension.
        /// </summary>
        /// <param name="location">Location to store.</param>
        /// <param name="urlExtension">URL extension for the location.</param>
        public Route(Location location, string urlExtension)
        {
            _location = location;
            _urlExtension = urlExtension;
        }

        private readonly Location _location;
        private readonly string _urlExtension;

        /// <summary>
        /// Location of the route.
        /// </summary>
        public Location Location
        {
            get { return _location; }
        }

        /// <summary>
        /// Extension of the route.
        /// </summary>
        public string UrlExtension
        {
            get { return _urlExtension; }
        }
    }
}