using System.Net;
using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebServer.DataAccess.Contracts;
using System.Text.Json;
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
    public IActionResult RefreshAccess()
    {
        var httpContext = new HttpContextAccessor();
        
        var refreshToken =  httpContext.HttpContext.Request.Headers.Authorization.ToString().Replace("Bearer ", "");
        Console.WriteLine(refreshToken);
        return new ObjectResult(_iuserRepository.RefreshPairTokens(refreshToken));
        
        //return new ObjectResult(refreshToken);
    }
}