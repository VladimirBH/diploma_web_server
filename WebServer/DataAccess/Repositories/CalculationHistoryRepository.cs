using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using WebServer.Classes;
using WebServer.DataAccess.Contracts;
using WebServer.DataAccess.DBContexts;
using WebServer.DataAccess.Implementations.Entities;

namespace WebServer.DataAccess.Repositories;

public class CalculationHistoryRepository : GenericRepository<CalculationHistory>, ICalculationHistoryRepository
{
    public CalculationHistoryRepository(ApplicationContext context, IConfiguration configuration) : base(context, configuration)
    {
    }
    
    public IIncludableQueryable<CalculationHistory, User?> GetAllWithForeignKey()
    {
        return Context.CalculationHistories.Include(x => x.Furnace).Include(x => x.User);
    }

}