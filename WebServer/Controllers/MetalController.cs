using Microsoft.AspNetCore.Mvc;
using WebServer.Classes;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MetalController : ControllerBase
    {
        // GET: api/<MetalController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<MetalController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return id.ToString();
        }

        // POST api/<MetalController>
        [HttpPost]
        public MetalStructure ProcessingOfMetalComposition (MetalStructure structure)
        {
            
            return structure;
        }

        // PUT api/<MetalController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<MetalController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
