using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebServer.DataAccess.Contracts;

namespace WebServer.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserRepository _iuserRepository;

        public UserController(IUserRepository iuserRepository) 
        {
            _iuserRepository = iuserRepository;
        }
        // GET: api/<UserController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return (IEnumerable<string>)_iuserRepository.GetAll();
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return id.ToString();
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
