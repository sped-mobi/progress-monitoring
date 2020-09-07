using System.Xml;
using System.Xml.Serialization;

namespace Gisd.Sped.Progress
{
    public static class ConfigurationSerializer
    {
        public static Configuration Deserialize(string filePath)
        {
            using (XmlReader reader = XmlReader.Create(filePath))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Configuration));
                var retVal = (Configuration)serializer.Deserialize(reader);
                return retVal;
            }
        }

        public static void Serialize(Configuration configuration, string filePath)
        {
            using (XmlWriter writer = XmlWriter.Create(filePath))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Configuration));
                serializer.Serialize(writer, configuration);
            }
        }
    }
}
