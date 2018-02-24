using System.Runtime.Serialization.Json;
using System.IO;
using System.Text;
using System.Net.Http;

namespace Domain {
    // The extensions class to hold generic methods.
    public static class Extensions
    {
        // Gets the JSON representation from a serialized object.
        public static string ToJSON<T>(this T obj) where T : class
        {
            // Serializes the object using the JSON contract of the class.
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
            using (MemoryStream stream = new MemoryStream())
            {
                serializer.WriteObject(stream, obj);
                return Encoding.Default.GetString(stream.ToArray());
            }
        }

        // Gets the object intance from a json string.
        public static T FromJSON<T>(this T obj, string json) where T : class
        {
            // Deserializes the json to a object using the contract of the class.
            using (MemoryStream stream = new MemoryStream(Encoding.Unicode.GetBytes(json)))
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
                return serializer.ReadObject(stream) as T;
            }
        }

        // Gets the JSON string content from an object. 
        public static StringContent ToJSONStringContent<T>(this T obj) where T : class
        {
            return new StringContent(ToJSON(obj), Encoding.UTF8, "application/json");
        }
    }
}