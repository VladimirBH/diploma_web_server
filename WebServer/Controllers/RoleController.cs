using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebServer.DataAccess.Contracts;
using WebServer.DataAccess.DBContexts;
using WebServer.DataAccess.Implementations.Entities;


namespace WebServer.Controllers;

[Authorize (Roles = "admin")]
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
    public ActionResult<JsonDocument> Get()
    {
        var jsonString = JsonSerializer.Serialize(_iroleRepository.GetAll());
        var json = JsonDocument.Parse(jsonString);
        return json;
    }

    // GET api/<UserController>/5
    [HttpGet("{id}")]
    public ActionResult<JsonDocument> Get(int id)
    {
        var jsonString = JsonSerializer.Serialize(_iroleRepository.GetById(id));
        var json = JsonDocument.Parse(jsonString);
        return json;
    }

    [HttpPost]
    public void CreateRole(Role role)
    {
        _iroleRepository.Add(role);
    }

    // PUT api/<UserController>/5
    [HttpPut("{id}")]
    public void Put(Role role)
    {
        _iroleRepository.Update(role);
        _iroleRepository.SaveChanges();
    }

    // DELETE api/<UserController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
        _iroleRepository.Remove(_iroleRepository.GetById(id));
        _iroleRepository.SaveChanges();
    }

}