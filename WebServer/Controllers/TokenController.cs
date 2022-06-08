using System.Net;
using System.Security.Authentication;
using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebServer.DataAccess.Contracts;
using System.Text.Json;

namespace WebServer.Controllers;

[Authorize]
[Route("api/[controller]/[action]")]
[ApiController]
public class TokenController : Controller
{
    private readonly IUserRepository _iuserRepository;
    public TokenController(IUserRepository iuserRepository) 
    {
        _iuserRepository = iuserRepository;
    }
    // GET: api/<TokenController>?refreshToken=
    [HttpGet]
    public ActionResult<JsonDocument> RefreshAccess()
    {
        var httpContext = new HttpContextAccessor();
        var refreshToken =  httpContext.HttpContext.Request.Headers.Authorization.ToString().Replace("Bearer ", "");
        try
        {
            var jsonString = JsonSerializer.Serialize(_iuserRepository.RefreshPairTokens(refreshToken));
            var json = JsonDocument.Parse(jsonString);
            return json;
        }
        catch (AuthenticationException ex)
        {
            return StatusCode(403);
        }

    }
}