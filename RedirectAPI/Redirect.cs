using RedirectAPI.Data;

namespace RedirectAPI;

public static class Redirect
{
    // Получение длинной ссылки из короткой из базы данных (PostgreSQL)
    public static string GetUrl(ApplicationContext db, string shortUrl)
    {
        try
        {
            var longUrl = db.Links.FirstOrDefault(link => link.ShortUrl == shortUrl);
            return longUrl == null ? "NotFound" : longUrl.LongUrl;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return "ServerError";
        }
    }

    // Добавление новой длинный ссылки в базу данных (PostgreSQL)
    public static string AddUrl(ApplicationContext db, string longUrl, User user)
    {
        if (!IsUrl(longUrl)) return "WrongUrl";
        try
        {
            var shortUrl = GenerateUrl();
            
            while (db.Links.FirstOrDefault(link => link.ShortUrl == shortUrl) != null)
            {
                shortUrl = GenerateUrl();
            }

            var link = new Link { UserId = user.Id, LongUrl = longUrl, ShortUrl = shortUrl };
            db.Links.Add(link);
            db.SaveChanges();
            
            return shortUrl;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return "ServerError";
        }
    }

    private static bool IsUrl(string url)
    {
        return Uri.IsWellFormedUriString(url, UriKind.Absolute);
    }

    private static string GenerateUrl()
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
        return shortUrl;
    }
}