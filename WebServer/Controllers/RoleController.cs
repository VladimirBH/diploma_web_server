using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebServer.DataAccess.Contracts;
using WebServer.DataAccess.DBContexts;
using WebServer.DataAccess.Implementations.Entities;


namespace WebServer.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class RoleController
{
    private readonly IRoleRepository _iroleRepository;
    private ApplicationContext _appContext;

    public RoleController(IRoleRepository iroleRepository, ApplicationContext appContext)
    {
        _iroleRepository = iroleRepository;
        _appContext = appContext;
    }

    // GET: api/<UserController>
    [HttpGet]
    public List<Role> Get()
    {

        return (List<Role>)_iroleRepository.GetAll();
    }

    // GET api/<UserController>/5
    [HttpGet("{id}")]
    public Role Get(int id)
    {
        return _iroleRepository.GetById(id);
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