using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace Wallpaper
{
    /// <summary>
    /// Extension methods
    /// </summary>
    public static class EXT
    {
        /// <summary>
        /// Serializes any object into XML
        /// </summary>
        /// <param name="o">Object to serialize</param>
        /// <returns>XML string</returns>
        public static string Serialize(this object o)
        {
            var XML = new XmlSerializer(o.GetType());
            using (var MS = new MemoryStream())
            {
                XML.Serialize(MS, o);
                return Encoding.UTF8.GetString(MS.ToArray());
            }
        }

        /// <summary>
        /// Deserializes any object from an XML string
        /// </summary>
        /// <typeparam name="T">Type of root object to deserialize</typeparam>
        /// <param name="s">XML string</param>
        /// <returns>Deserialized object</returns>
        public static T Deserialize<T>(this string s)
        {
            var XML = new XmlSerializer(typeof(T));
            using (var MS = new MemoryStream(Encoding.UTF8.GetBytes(s), false))
            {
                return (T)XML.Deserialize(MS);
            }
        }
    }
}
