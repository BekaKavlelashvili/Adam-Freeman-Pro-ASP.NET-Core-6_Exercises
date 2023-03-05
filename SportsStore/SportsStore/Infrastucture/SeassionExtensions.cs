using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace SportsStore.Infrastucture
{
    public static class SeassionExtensions
    {
        public static void SetJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonSerializer.Serialize(value));
        }

        public static T? GetJson<T>(this ISession session, string key)
        {
            var seassionData = session.GetString(key);
            return seassionData == null ? default(T) : JsonSerializer.Deserialize<T>(seassionData);
        }
    }
}
