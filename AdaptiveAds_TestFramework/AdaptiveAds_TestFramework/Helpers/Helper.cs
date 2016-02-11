using AdaptiveAds_TestFramework.Config;
using System.Collections.Generic;

namespace AdaptiveAds_TestFramework.Helpers
{
    public static class Helper
    {
        public static string RouteURL(Location location)
        {
            string URL = "";
            Data.Routes.TryGetValue(location, out URL);

            if (string.IsNullOrWhiteSpace(URL))
            {
                throw new KeyNotFoundException("The given location was not found within Routes.");
            }
            return Data.URL_Address + URL;
        }

        public static string RouteURLExtension(Location location)
        {
            string extension = "";
            Data.Routes.TryGetValue(location, out extension);

            if (string.IsNullOrWhiteSpace(extension))
            {
                throw new KeyNotFoundException("The given location was not found within Routes.");
            }
            return extension;
        }

        public static Location RouteLocation(string URL)
        {
            foreach (KeyValuePair<Location, string> route in Data.Routes)
            {
                if (route.Value == URL || route.Value == Data.URL_Address + URL)
                {
                    return route.Key;
                }
            }
            throw new KeyNotFoundException("The given URL was not found within Routes.");
        }
    }
}
