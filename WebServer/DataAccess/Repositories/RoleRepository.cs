using WebServer.DataAccess.Contracts;
using WebServer.DataAccess.Implementations.Entities;
using WebServer.DBContext;

namespace WebServer.DataAccess.Repositories;

public class RoleRepository : GenericRepository<Role>, IRoleRepository
{
    private readonly ApplicationContext _appContext;
    public RoleRepository(ApplicationContext context) : base(context)
    {
        this._appContext = context;
    }
}