using System.Web.Script.Serialization;

namespace Medivh.Common
{
    internal static class JsonHelper
    {
        public static T JosnStringToObject<T>(this string jsonStr)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            var objs = serializer.Deserialize<T>(jsonStr);
            return objs;
        }

        public static string JsonObjectToString(this object obj)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Serialize(obj);
        }
    }
}
