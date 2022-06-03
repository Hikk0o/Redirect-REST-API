using Microsoft.AspNetCore.Mvc;

namespace RedirectAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RedirectController : ControllerBase
{
    // Get для получения длинной ссылки из короткой
    [HttpGet(Name = "GetUrlFromShort")]
    public string Get(string r)
    {
        return RedirectAPI.Redirect.GetUrl(r);
    }
    
    // Post для добавления новой длинной ссылки, вернет короткую
    [HttpPost(Name = "AddUrlFromShort")]
    public string Post(string r)
    {
        return RedirectAPI.Redirect.AddUrl(r);
    }
}