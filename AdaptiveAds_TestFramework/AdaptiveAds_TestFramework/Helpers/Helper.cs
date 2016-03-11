using System;
using System.Collections.Generic;
using OpenQA.Selenium;

namespace AdaptiveAds_TestFramework.Helpers
{
    /// <summary>
    /// Contains methods to help with operations in multiple locations throughout the framework.
    /// </summary>
    public static class Helper
    {
        /// <summary>
        /// Gets the full URL path of the provided location, throws a NotImplementedException location is not set-up in ConfigData.Routes.
        /// </summary>
        /// <param name="location">Location from which to find the URL.</param>
        /// <returns>The full URL of the location.</returns>
        public static string RouteUrl(Location location)
        {
            string extension;

            //try to find the locations URL extension
            ConfigData.Routes.TryGetValue(location, out extension);

            //throw a NotImplementedException if a route was not round
            if (string.IsNullOrWhiteSpace(extension))
            {
                throw new NotImplementedException("The given location is not set-up in  ConfigData.Routes.");
            }
            //return the full URL of the location
            return ConfigData.UrlAddress + extension;
        }

        /// <summary>
        /// Gets the URL extension path of the provided location, throws a NotImplementedException location is not set-up in ConfigData.Routes.
        /// </summary>
        /// <param name="location">Location from which to find the URL.</param>
        /// <returns>The URL extension of the location.</returns>
        public static string RouteUrlExtension(Location location)
        {
            string extension;

            //try to find the locations URL extension
            ConfigData.Routes.TryGetValue(location, out extension);

            //throw a NotImplementedException if a route was not round
            if (string.IsNullOrWhiteSpace(extension))
            {
                throw new NotImplementedException("The given location is not set-up in  ConfigData.Routes.");
            }
            //return the extension of the location
            return extension;
        }

        /// <summary>
        /// Gets the location of a given URL, throws a NoSuchElementException if not found.
        /// </summary>
        /// <param name="url">The full or extension URL from which to find the location of.</param>
        /// <returns>The location of the given URL.</returns>
        public static Location RouteLocation(string url)
        {
            //search routes for one that matches the given parameter
            foreach (KeyValuePair<Location, string> route in ConfigData.Routes)
            {
                if (route.Value == url || ConfigData.UrlAddress + route.Value == url)
                {
                    //return the location of the route
                    return route.Key;
                }
            }
            //throw a NoSuchElementException if a route was not round
            throw new NoSuchElementException("The given URL does not exist within ConfigData.Routes.");
        }
    }
}
