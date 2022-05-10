using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using WebServer.DataAccess.Contracts;
using WebServer.DataAccess.DBContexts;
using WebServer.DataAccess.Implementations.Entities;

namespace WebServer.DataAccess.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly ApplicationContext Context;
        protected readonly IConfiguration Configuration;

        public GenericRepository(ApplicationContext context, IConfiguration configuration)
        {
            Context = context;
            Configuration = configuration;
        }
        public void Add(T entity)
        {
            Context.Set<T>().Add(entity);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            Context.Set<T>().AddRange(entities);
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> expression)
        {
            return Context.Set<T>().Where(expression);
        }

        public List<T> GetAll()
        {
            return Context.Set<T>().ToList();
        }

        public T GetById(int id)
        {
            return Context.Set<T>().Find(id);
        }

        public void Remove(T entity)
        {
            Context.Set<T>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            Context.Set<T>().RemoveRange(entities);
        }

        public void SaveChanges()
        {
            Context.SaveChanges();
        }
    }
}
