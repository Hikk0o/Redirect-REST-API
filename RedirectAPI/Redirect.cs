using System.Text.RegularExpressions;
using RedirectAPI.Data;
using RedirectAPI.Data.DataEntity;

namespace RedirectAPI;

public static class Redirect
{
    /// Получение длинной ссылки из базы данных, используя короткую ссылку
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

    /// Добавление новой длинный ссылки в базу данных
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
    
    /// Получение изображения из базы данных, используя короткую ссылку
    public static string GetImg(ApplicationContext db, string shortUrl)
    {
        try
        {
            var image = db.Images.FirstOrDefault(img => img.ShortUrl == shortUrl);
            return image == null ? "NotFound" : Convert.ToBase64String(image.Data);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return "ServerError";
        }
    }
    
    /// Добавление изображения в базу данных
    public static string AddImg(ApplicationContext db, string data, User user)
    {
        data = Regex.Replace(data, @"^data:image\/png;base64,", "");
        if (!IsBase64String(data)) return "WrongData";
        try
        {
            var shortUrl = GenerateUrl();
            
            while (db.Links.FirstOrDefault(link => link.ShortUrl == shortUrl) != null)
            {
                shortUrl = GenerateUrl();
            }

            var bData = Convert.FromBase64String(data);
            var image = new Image { UserId = user.Id, Data = bData, ShortUrl = shortUrl };
            db.Images.Add(image);
            db.SaveChanges();
            
            return shortUrl;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return "ServerError";
        }
    }

    private static bool IsBase64String(this string base64String) {
        if (string.IsNullOrEmpty(base64String) || base64String.Length % 4 != 0
                                               || base64String.Contains(' ') || base64String.Contains('\t') || base64String.Contains('\r') || base64String.Contains('\n'))
            return false;

        try{
            Convert.FromBase64String(base64String);
            return true;
        }
        catch(Exception){
            // catch
        }
        return false;
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