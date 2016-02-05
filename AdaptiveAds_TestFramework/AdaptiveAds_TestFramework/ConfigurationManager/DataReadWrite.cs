using System.IO;
using System.Collections.ObjectModel;
using System.Xml.Serialization;
using System.Reflection;
using AdaptiveAds_TestFramework.CustomItems;

namespace AdaptiveAds_TestFramework.ConfigurationManager
{
    public static class DataReadWrite
    {
        #region Data interaction

        private static T Read<T>(string filename)
        {
            string folderLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)+ "../../../../AdaptiveAds_TestFramework/Data";
            T result = default(T);
            if (File.Exists(folderLocation + "/" + filename + ".xml"))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                FileStream stream = new FileStream(folderLocation + "/" + filename + ".xml", FileMode.Open, FileAccess.Read);
                result = (T)serializer.Deserialize(stream);
                stream.Close();
            }
            return result;
        }

        private static void Write(object data, string filename)
        {
            string folderLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "../../../../AdaptiveAds_TestFramework/Data";

            XmlSerializer serializer = new XmlSerializer(data.GetType());
            if (!Directory.Exists(folderLocation))
            {
                Directory.CreateDirectory(folderLocation);
            }
            StreamWriter writer = new StreamWriter(folderLocation + "/" + filename + ".xml", false);
            serializer.Serialize(writer, data);
            writer.Close();
        }

        #endregion //Data interaction

        #region DashboardLinks

        public static Collection<PairObject> ReadPairObjects(string pairType)
        {
            return Read<Collection<PairObject>>(pairType);
        }

        public static void WritePairObjects(Collection<PairObject> data,string pairType)
        {
            Write(data, pairType);
        }

        #endregion //DashboardLinks
        
    }
}
