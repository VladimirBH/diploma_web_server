using System.Text.Json;
using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using WebServer.Classes;
using WebServer.DataAccess.Contracts;
using WebServer.DataAccess.DBContexts;
using WebServer.DataAccess.Implementations.Entities;
using WebServer.Exceptions;

namespace WebServer.Controllers
{
    //[Authorize (Roles = "admin")]
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
        public List<User> Get()
        {
            return new List<User>(_iuserRepository.GetAllWithForeignKey());
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public User Get(int id)
        {
            return _iuserRepository.GetById(id);
        }
        
        // POST api/<UserController>/SignIn
        [AllowAnonymous]
        [HttpPost]
        public ActionResult<JsonDocument> SignIn(AuthClass dataAuth)
        {
            try
            {
                return _iuserRepository.Authorization(dataAuth);
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
            //return dataAuth.login;
            _iuserRepository.Add(user);
            _iuserRepository.SaveChanges();
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
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
