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
public class CalculationHistoryController : Controller
{
    private readonly ICalculationHistoryRepository _iCalculationHistoryRepository;
    
    public CalculationHistoryController(ICalculationHistoryRepository iCalculationHistoryRepository) 
    {
        _iCalculationHistoryRepository = iCalculationHistoryRepository;
    }
        
    [HttpGet]
    public ActionResult<JsonDocument> Get()
    {
        var jsonString = JsonSerializer.Serialize(_iCalculationHistoryRepository.GetAll());
        var json = JsonDocument.Parse(jsonString);
        return json;
    }


    [HttpGet("{id}")]
    public ActionResult<JsonDocument> Get(int id)
    {
        var jsonString = JsonSerializer.Serialize(_iCalculationHistoryRepository.GetById(id));
        var json = JsonDocument.Parse(jsonString);
        return json;
    }
}