using System.Text.Json;

namespace DCAPP.Infrastructure.Extensions;

public static class SessionExtensions
{
    public static void SetJson<T>(this ISession s, string key, T data)
    {
        var json = JsonSerializer.Serialize(data);

        s.SetString(key, json);
    }

    public static T? GetData<T>(this ISession s, string key)
    {
        var json = s.GetString(key);
        return json is null
        ? default
        : JsonSerializer.Deserialize<T>(json);
    }
}