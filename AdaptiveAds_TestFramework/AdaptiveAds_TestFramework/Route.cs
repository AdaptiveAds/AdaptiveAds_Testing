namespace AdaptiveAds_TestFramework
{
        public enum Location
        {
            Login,
            Dashboard,
            Advert
        }
    public class Route
    {

        public Route(Location location, string urlExtension)
        {
            _location = location;
            _urlExtension = urlExtension;
        }

        private readonly Location _location;
        private readonly string _urlExtension;

        public Location Location
        {
            get { return _location; }
        }

        public string UrlExtension {
            get { return _urlExtension; }
        }
    }
}