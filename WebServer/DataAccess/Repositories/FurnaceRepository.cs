using WebServer.DataAccess.Contracts;
using WebServer.DataAccess.DBContexts;
using WebServer.DataAccess.Implementations.Entities;

namespace WebServer.DataAccess.Repositories;

public class FurnaceRepository : GenericRepository<Furnace>, IFurnaceRepository
{
    public FurnaceRepository(ApplicationContext context, IConfiguration configuration) : base(context, configuration)
    {
    }
}