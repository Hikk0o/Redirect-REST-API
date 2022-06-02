using Newtonsoft.Json;

namespace RedirectAPI;

public class Redirect
{
    public static bool IsDev { get; set; }
    private const string FileName = "config.json";
    private static readonly string PathToDataJson = IsDev? $"A:\\RiderProjects\\RedirectAPI\\RedirectAPI\\{FileName}" : $"./{FileName}";

    private static async Task<Dictionary<string,object>?> LoadShortsJson()
    {
        if (!File.Exists(PathToDataJson))
        {
            await Task.Run(() =>
            {
                File.WriteAllText(PathToDataJson, "{\n}");
            });
        }

        var json = await File.ReadAllTextAsync(PathToDataJson);
        var responseJson = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
        return responseJson;
        
    }
    private static void UpdateShortsJson(Dictionary<string, object> dict)
    {
        var entries = dict.Select(d =>
            $"\"{d.Key}\": \"{string.Join(",", d.Value)}\"");
        
        string newData = "{\n\t" + string.Join(",\n\t", entries) + "\n}";
        File.WriteAllText(PathToDataJson, newData);
        // Console.WriteLine("UpdateDataJson");
    }

    public static string GetUrl(string shortUrl)
    {
        var dictionary = LoadShortsJson().Result;
        if (dictionary == null) return "null";
        try
        {
            var url = (string)dictionary[shortUrl];
            return url;
        }
        catch (KeyNotFoundException)
        {
            return "KeyNotFound";
        }
        catch
        {
            return "ServerError";
        }
    }

    public static string SetNewUrl(string longUrl)
    {
        string numbers = "1234567890";
        string letters = "abcdefghijklmnopqrstuvwxyz";
        var chars = new char[8];
        var random = new Random();
        var finalChars = $"{letters.ToLower()}{letters.ToUpper()}{numbers}";
        
        for (int i = 0; i < chars.Length; i++)
        {
            chars[i] = finalChars[random.Next(finalChars.Length)];
        }

        if (GetUrl(new string(chars)) != "KeyNotFound") return SetNewUrl(longUrl);
        var shorts = LoadShortsJson().Result;
        if (shorts == null) return "ServerError";
        shorts[new string(chars)] = longUrl;
        UpdateShortsJson(shorts);

        return new string(chars);
    }

}