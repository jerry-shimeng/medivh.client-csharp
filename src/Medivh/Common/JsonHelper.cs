

namespace Medivh.Common
{
    internal static class JsonHelper
    {

        //public static Func<object, string> JsonSerializer { get; set; }
        //public static Func<string, object> JsonDeSerializer { get; set; }



        public static T JosnStringToObject<T>(string jsonStr)
        {
            //if (JsonDeSerializer == null)
            //{
            //    JavaScriptSerializer serializer = new JavaScriptSerializer();
            //    serializer.MaxJsonLength = Int32.MaxValue;
            //    var objs = serializer.Deserialize<object>(jsonStr);
            //    return objs;
            //}
            //else
            //{
            //    return JsonDeSerializer(jsonStr);
            //}

            return Medivh.Json.JsonConvert.DeserializeObject<T>(jsonStr);
        }

        public static string JsonObjectToString(object obj)
        {
            //if (JsonSerializer == null)
            //{
            //    JavaScriptSerializer serializer = new JavaScriptSerializer();
            //    serializer.MaxJsonLength = Int32.MaxValue;
            //    return serializer.Serialize(obj);
            //}
            //else
            //{
            //    return JsonSerializer(obj);
            //}

            return Medivh.Json.JsonConvert.SerializeObject(obj);
        }
    }
}
