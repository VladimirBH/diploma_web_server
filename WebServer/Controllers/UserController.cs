using System.Security.Authentication;
using System.Text.Json;
using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebServer.Classes;
using WebServer.DataAccess.Contracts;
using WebServer.DataAccess.DBContexts;
using WebServer.DataAccess.Implementations.Entities;
namespace WebServer.Controllers
{
    [Authorize (Roles = "admin")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserRepository _iUserRepository;
        public UserController(IUserRepository iUserRepository) 
        {
            _iUserRepository = iUserRepository;
        }
        
        // GET: api/<UserController>
        [HttpGet]
        public ActionResult<JsonDocument> Get()
        {
            try
            {
                var jsonString = JsonSerializer.Serialize(_iUserRepository.GetAllWithForeignKey());
                var json = JsonDocument.Parse(jsonString);
                return json;
            }
            catch (AuthenticationException ex)
            {
                return StatusCode(401);
            }
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public ActionResult<JsonDocument> Get(int id)
        {
            try
            {
                var jsonString = JsonSerializer.Serialize(_iUserRepository.GetById(id));
                var json = JsonDocument.Parse(jsonString);
                return json;
            }
            catch (AuthenticationException ex)
            {
                return StatusCode(401);
            }
        }

        // GET api/<UserController>/GetCurrentUserInfo
        [Authorize]
        [HttpGet]
        public ActionResult<JsonDocument> GetCurrentUserInfo()
        {
            try
            {
                var httpContext = new HttpContextAccessor();
                var accessToken =  httpContext.HttpContext.Request.Headers.Authorization.ToString().Replace("Bearer ", "");
                var jsonString = JsonSerializer.Serialize(_iUserRepository.GetCurrentUserInfo(accessToken));
                var json = JsonDocument.Parse(jsonString);
                return json;
            }
            catch (AuthenticationException ex)
            {
                return StatusCode(401);
            }
        }
        
        // POST api/<UserController>/SignIn
        [AllowAnonymous]
        [HttpPost]
        public ActionResult<JsonDocument> SignIn(AuthClass dataAuth)
        {
            try
            {
                var jsonString = JsonSerializer.Serialize(_iUserRepository.Authorization(dataAuth));
                var json = JsonDocument.Parse(jsonString);
                return json;
            }
            catch (AuthenticationException ex)
            {
                return StatusCode(403);
            }
        }
        
        // POST api/<UserController>/CreateUser
        [HttpPost]
        public void CreateUser(User user)
        {
            _iUserRepository.Add(user);
            _iUserRepository.SaveChanges();
        }

        // PUT api/<UserController>/5
        [HttpPut]
        public void Put(User user)
        {
            _iUserRepository.Update(user);
            _iUserRepository.SaveChanges();
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var user = _iUserRepository.GetById(id);
            _iUserRepository.Remove(user);
            _iUserRepository.SaveChanges();
        }
    }
}
