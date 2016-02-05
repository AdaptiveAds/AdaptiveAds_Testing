namespace AdaptiveAds_TestFramework.CustomItems
{
    public class PairObject
    {
        public PairObject() { }

        public PairObject(object object1, object object2)
        {
            Object1 = object1;
            Object2 = object2;
        }

        public object Object1 { get; set; }

        public object Object2 { get; set; }
    }
}
