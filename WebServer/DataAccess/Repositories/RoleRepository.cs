using Microsoft.EntityFrameworkCore;
using WebServer.DataAccess.Contracts;
using WebServer.DataAccess.DBContexts;
using WebServer.DataAccess.Implementations.Entities;

namespace WebServer.DataAccess.Repositories;

public class RoleRepository : GenericRepository<Role>, IRoleRepository
{
    public RoleRepository(ApplicationContext context, IConfiguration configuration) : base(context, configuration)
    {
    }

    public void Update(Role role)
    {
        Context.Roles.Update(role);
    }
}