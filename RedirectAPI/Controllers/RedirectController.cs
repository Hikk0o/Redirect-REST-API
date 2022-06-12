using Microsoft.AspNetCore.Mvc;
using RedirectAPI.Data;

namespace RedirectAPI.Controllers;

[ApiController]
[Route("api/[Controller]/{shortUrl}")]
public class RedirectController : ControllerBase
{
    private readonly ApplicationContext _db;
    public RedirectController(ApplicationContext db)
    {
        _db = db;
    }
    
    // Get для получения длинной ссылки из короткой
    [HttpGet(Name = "GetUrlFromShort")]
    public string Get(string shortUrl)
    {
        // Response.Redirect(RedirectAPI.Redirect.GetUrl(_db, shortUrl));
        return RedirectAPI.Redirect.GetUrl(_db, shortUrl);
    }
    
    // Post для добавления новой длинной ссылки, вернет короткую
    [HttpPost(Name = "AddUrlFromShort")]
    public string Post(string shortUrl)
    {
        var remoteIp = Request.HttpContext.Connection.RemoteIpAddress!.MapToIPv4().ToString();
        var user = _db.Users.FirstOrDefault(userDb => userDb.Ip == remoteIp);
        if (user != null) return RedirectAPI.Redirect.AddUrl(_db, shortUrl, user);
        {
            user = new User { Ip = remoteIp };
            _db.Users.Add(user);
            _db.SaveChanges();
            user = _db.Users.FirstOrDefault(userDb => userDb.Ip == remoteIp)!;
            return RedirectAPI.Redirect.AddUrl(_db, shortUrl, user);
        }

        
    }
}