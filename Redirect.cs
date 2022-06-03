using Newtonsoft.Json;

namespace RedirectAPI;

public class Redirect
{
    private static readonly bool IsDev = Program.App!.Environment.IsDevelopment();
    private const string UrlsJsonFileName = "config.json";
    private static readonly string PathToDataJson = IsDev? $@"A:\RiderProjects\RedirectAPI\RedirectAPI\{UrlsJsonFileName}" : $"./{UrlsJsonFileName}";
    private static Dictionary<string, object>? _shortsUrl = LoadShortsJson().Result;

    // Загрузка словаря в формате: ключ - короткая ссылка, полученное значение по ключу - длинная ссылка
    private static async Task<Dictionary<string,object>?> LoadShortsJson()
    {
        if (!File.Exists(PathToDataJson))
        {
            await Task.Run(() =>
            {
                File.WriteAllText(PathToDataJson, "{}");
            });
        }

        var json = await File.ReadAllTextAsync(PathToDataJson);
        var responseJson = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
        return responseJson;
    }
    
    // Обновление json словаря с ссылками
    private static void UpdateShortsJson(Dictionary<string, object> dict)
    {
        File.WriteAllText(PathToDataJson, JsonConvert.SerializeObject(dict));
    }

    // Получение длинной ссылки из короткой
    public static string GetUrl(string shortUrl)
    {
        // var dictionary = _shortsUrl;
        if (_shortsUrl == null) return "null";
        try
        {
            var url = (string)_shortsUrl[shortUrl];
            return url;
        }
        catch (KeyNotFoundException)
        {
            return "KeyNotFound";
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return "ServerError";
        }
    }

    // Добавление новой длинный ссылки в словарь
    // (генерируется новая короткая ссылка, которая используется в виде ключа в словаре к длинной ссылке)
    public static string AddUrl(string longUrl)
    {
        const string numbers = "1234567890";
        const string letters = "abcdefghijklmnopqrstuvwxyz";
        var chars = new char[8];
        var random = new Random();
        var finalChars = $"{letters.ToLower()}{letters.ToUpper()}{numbers}";
        
        for (int i = 0; i < chars.Length; i++)
        {
            chars[i] = finalChars[random.Next(finalChars.Length)];
        }

        var shortUrl = new string(chars);
        if (_shortsUrl == null) return "ServerError";
        if (GetUrl(shortUrl) != "KeyNotFound") return AddUrl(longUrl);
        // var shorts = _shortsUrl;
        _shortsUrl[shortUrl] = longUrl;
        UpdateShortsJson(_shortsUrl);

        return shortUrl;
    }

}