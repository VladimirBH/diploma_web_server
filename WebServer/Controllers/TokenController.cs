using System.Net;
using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebServer.DataAccess.Contracts;
using System.Text.Json;
using Newtonsoft.Json.Linq;

namespace WebServer.Controllers;

[Authorize]
[Route("api/[controller]/[action]")]
[ApiController]
public class TokenController
{
    private readonly IUserRepository _iuserRepository;
    public TokenController(IUserRepository iuserRepository) 
    {
        _iuserRepository = iuserRepository;
    }
    // GET: api/<TokenController>?refreshToken=
    [HttpGet]
    public JsonDocument RefreshAccess()
    {
        var httpContext = new HttpContextAccessor();
        var refreshToken =  httpContext.HttpContext.Request.Headers.Authorization.ToString().Replace("Bearer ", "");
        return _iuserRepository.RefreshPairTokens(refreshToken);
    }
}