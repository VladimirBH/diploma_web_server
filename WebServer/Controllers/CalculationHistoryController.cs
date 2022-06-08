using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebServer.Classes;
using WebServer.DataAccess.Contracts;
using WebServer.DataAccess.DBContexts;
using WebServer.DataAccess.Implementations.Entities;

namespace WebServer.Controllers;

[Authorize]
[Route("api/[controller]/[action]")]
[ApiController]
public class CalculationHistoryController : Controller
{
    private readonly ICalculationHistoryRepository _iCalculationHistoryRepository;
    
    public CalculationHistoryController(ICalculationHistoryRepository iCalculationHistoryRepository) 
    {
        _iCalculationHistoryRepository = iCalculationHistoryRepository;
    }
    
    [Authorize (Roles = "admin")]
    [HttpGet]
    public ActionResult<JsonDocument> Get()
    {
        var jsonString = JsonSerializer.Serialize(_iCalculationHistoryRepository.GetAllWithForeignKey());
        var json = JsonDocument.Parse(jsonString);
        return json;
    }

    [Authorize (Roles = "admin")]
    [HttpGet("{id}")]
    public ActionResult<JsonDocument> Get(int id)
    {
        var jsonString = JsonSerializer.Serialize(_iCalculationHistoryRepository.GetById(id));
        var json = JsonDocument.Parse(jsonString);
        return json;
    }
    
 
    [HttpPost]
    public void Create(CalculationHistory calculationHistory)
    {
        _iCalculationHistoryRepository.Add(calculationHistory);
        _iCalculationHistoryRepository.SaveChanges();
    }
}