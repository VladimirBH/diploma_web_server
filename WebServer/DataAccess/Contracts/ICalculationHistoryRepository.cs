using Microsoft.EntityFrameworkCore.Query;
using WebServer.Classes;
using WebServer.DataAccess.Implementations.Entities;

namespace WebServer.DataAccess.Contracts;

public interface ICalculationHistoryRepository : IGenericRepository<CalculationHistory>
{
    IIncludableQueryable<CalculationHistory, User?> GetAllWithForeignKey();
}