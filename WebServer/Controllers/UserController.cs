using System.Text.Json;
using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WebServer.Classes;
using WebServer.DataAccess.Contracts;
using WebServer.DataAccess.DBContexts;
using WebServer.DataAccess.Implementations.Entities;
using WebServer.Exceptions;

namespace WebServer.Controllers
{
    [Authorize (Roles = "admin")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserRepository _iuserRepository;
        private ApplicationContext _appContext;
        public UserController(IUserRepository iuserRepository, ApplicationContext appContext) 
        {
            _iuserRepository = iuserRepository;
            _appContext = appContext;
        }
        
        // GET: api/<UserController>
        [HttpGet]
        public ActionResult<JsonDocument> Get()
        {
            var jsonString = JsonConvert.SerializeObject(_iuserRepository.GetAllWithForeignKey());
            var json = JsonDocument.Parse(jsonString);
            return json;
        }


        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public ActionResult<JsonDocument> Get(int id)
        {
            var jsonString = JsonConvert.SerializeObject(_iuserRepository.GetById(id));
            var json = JsonDocument.Parse(jsonString);
            return json;
        }
        
        
        // GET api/<UserController>/5
        [Authorize]
        [HttpGet]
        public ActionResult<JsonDocument> GetCurrentUserInfo()
        {
            try
            {
                var httpContext = new HttpContextAccessor();
                var refreshToken =  httpContext.HttpContext.Request.Headers.Authorization.ToString().Replace("Bearer ", "");
                var jsonString = JsonConvert.SerializeObject(_iuserRepository.GetCurrentUserInfo(refreshToken));
                var json = JsonDocument.Parse(jsonString);
                return json;
            }
            catch (UserException ex)
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
                var jsonString = JsonConvert.SerializeObject(_iuserRepository.Authorization(dataAuth));
                var json = JsonDocument.Parse(jsonString);
                return json;
            }
            catch (UserException ex)
            {
                return StatusCode(403);
            }
        }
        
        // POST api/<UserController>/CreateUser
        [HttpPost]
        public void CreateUser(User user)
        {
            _iuserRepository.Add(user);
            _iuserRepository.SaveChanges();
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void Put(User user)
        {
            _iuserRepository.Update(user);
            _iuserRepository.SaveChanges();
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var user = _iuserRepository.GetById(id);
            _iuserRepository.Remove(user);
            _iuserRepository.SaveChanges();
        }
    }
}
