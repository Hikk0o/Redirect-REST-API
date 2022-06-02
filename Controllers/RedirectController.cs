using Microsoft.AspNetCore.Mvc;

namespace RedirectAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RedirectController : ControllerBase
{
    [HttpGet(Name = "GetUrlFromShort")]
    public string Get(string r)
    {
        return RedirectAPI.Redirect.GetUrl(r);
    }
    
    [HttpPost(Name = "SetUrlFromShort")]
    public string Post(string r)
    {
        return RedirectAPI.Redirect.SetNewUrl(r);
    }
}