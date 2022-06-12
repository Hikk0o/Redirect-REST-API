using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RedirectAPI.Data;

namespace RedirectAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RedirectController : ControllerBase
{
    private readonly ApplicationContext _db;
    public RedirectController(ApplicationContext db)
    {
        _db = db;
    }
    
    // Get для получения длинной ссылки из короткой
    [HttpGet(Name = "GetUrlFromShort")]
    public string Get(string r)
    {
        return RedirectAPI.Redirect.GetUrl(_db, r);
    }
    
    // Post для добавления новой длинной ссылки, вернет короткую
    [HttpPost(Name = "AddUrlFromShort")]
    public string Post(string r)
    {
        var remoteIp = Request.HttpContext.Connection.RemoteIpAddress!.MapToIPv4().ToString();
        var user = _db.Users.FirstOrDefault(userDb => userDb.Ip == remoteIp);
        if (user == null)
        {
            Console.WriteLine("User not found");
            user = new User { Ip = Request.HttpContext.Connection.RemoteIpAddress!.MapToIPv4().ToString() };
            _db.Users.Add(user);
            _db.SaveChanges();
            user = _db.Users.FirstOrDefault(userDb => userDb.Ip == remoteIp)!;
        }

        return RedirectAPI.Redirect.AddUrl(_db, r, user);
    }
}