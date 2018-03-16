namespace Aglive.Business.Infrastructure.Utilities
{
    using Newtonsoft.Json;
    using System;

    public static class JsonHelper
    {
        public static string JsonSerializer<T>(T t)
        {
            //var jsonString = JsonConvert.SerializeObject(t, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, Culture = CultureInfo.CurrentCulture, Converters = new List<JsonConverter> { new JsonDateTimeConverter() } });
            var jsonString = JsonConvert.SerializeObject(t);
            return jsonString;
        }

        public static T JsonDeserialize<T>(string jsonString)
        {
            try
            {
                var obj = JsonConvert.DeserializeObject<T>(jsonString);
                return obj;
            }
            catch (Exception ex)
            {
                return default(T);
            }
            //var obj = JsonConvert.DeserializeObject<T>(jsonString, new JsonSerializerSettings { Culture = CultureInfo.CurrentCulture });
        }
    }
}