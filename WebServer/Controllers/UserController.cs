using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebServer.DataAccess.Contracts;
using WebServer.DataAccess.DBContexts;
using WebServer.DataAccess.Implementations.Entities;

namespace WebServer.Controllers
{
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
            return (List<User>)_iuserRepository.GetAll();
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public User Get(int id)
        {
            return _iuserRepository.GetById(id);
        }

        // POST api/<UserController>
        /*[HttpPost]
        public MetalStructure ProcessingOfMetalComposition(MetalStructure structure)
        {

            return structure;
        }*/

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
