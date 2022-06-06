using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebServer.Classes;
using WebServer.DataAccess.Contracts;
using WebServer.DataAccess.DBContexts;
using WebServer.DataAccess.Implementations.Entities;
using WebServer.Exceptions;

namespace WebServer.Controllers;

[Authorize (Roles = "admin")]
[Route("api/[controller]/[action]")]
[ApiController]
public class FurnaceController : Controller
{
    private readonly IFurnaceRepository _iFurnaceRepository;
    
        public FurnaceController(IFurnaceRepository iFurnaceRepository) 
        {
            _iFurnaceRepository = iFurnaceRepository;
        }
        
        [HttpGet]
        public ActionResult<JsonDocument> Get()
        {
            var jsonString = JsonSerializer.Serialize(_iFurnaceRepository.GetAll());
            var json = JsonDocument.Parse(jsonString);
            return json;
        }


        [HttpGet("{id}")]
        public ActionResult<JsonDocument> Get(int id)
        {
            var jsonString = JsonSerializer.Serialize(_iFurnaceRepository.GetById(id));
            var json = JsonDocument.Parse(jsonString);
            return json;
        }
        
        
        // POST api/<UserController>/CreateUser
        [HttpPost]
        public void Create(Furnace furnace)
        {
            _iFurnaceRepository.Add(furnace);
            _iFurnaceRepository.SaveChanges();
        }

        // PUT api/<UserController>/5
        [HttpPut]
        public void Update(Furnace furnace)
        {
            _iFurnaceRepository.Update(furnace);
            _iFurnaceRepository.SaveChanges();
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var user = _iFurnaceRepository.GetById(id);
            _iFurnaceRepository.Remove(user);
            _iFurnaceRepository.SaveChanges();
        }
}