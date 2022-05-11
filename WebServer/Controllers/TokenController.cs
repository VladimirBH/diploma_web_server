using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using WebServer.DataAccess.Contracts;

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
    // GET: api/<TokenController>
    [HttpPost]
    public JObject RefreshAccess(string refreshToken)
    {
        return _iuserRepository.RefreshAccessToken(refreshToken);
    }
}