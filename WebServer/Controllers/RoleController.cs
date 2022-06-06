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
    private readonly IRoleRepository _iRoleRepository;

    public RoleController(IRoleRepository iRoleRepository)
    {
        _iRoleRepository = iRoleRepository;
    }

    // GET: api/<UserController>
    [HttpGet]
    public ActionResult<JsonDocument> Get()
    {
        var jsonString = JsonSerializer.Serialize(_iRoleRepository.GetAll());
        var json = JsonDocument.Parse(jsonString);
        return json;
    }

    // GET api/<UserController>/5
    [HttpGet("{id}")]
    public ActionResult<JsonDocument> Get(int id)
    {
        var jsonString = JsonSerializer.Serialize(_iRoleRepository.GetById(id));
        var json = JsonDocument.Parse(jsonString);
        return json;
    }

    [HttpPost]
    public void CreateRole(Role role)
    {
        _iRoleRepository.Add(role);
    }

    // PUT api/<UserController>/5
    [HttpPut("{id}")]
    public void Put(Role role)
    {
        _iRoleRepository.Update(role);
        _iRoleRepository.SaveChanges();
    }

    // DELETE api/<UserController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
        _iRoleRepository.Remove(_iRoleRepository.GetById(id));
        _iRoleRepository.SaveChanges();
    }

}