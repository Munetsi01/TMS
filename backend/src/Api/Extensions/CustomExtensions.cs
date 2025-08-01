
using Newtonsoft.Json;

namespace Api.Extensions
{
    public static class CustomExtensions
    {
        public static string ToTimeStampFormatString(this DateTime dateTime) => dateTime.ToString(Utilities.LongDateFormat);

        public static string ToJsonString(this object obj)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.Formatting = Formatting.None;
            return Newtonsoft.Json.JsonConvert.SerializeObject(obj, settings);
        }
    }
}
