using Application.Common.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Persistence.Repository
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected ApplicationDBContext dbContext { get; set; }
        public RepositoryBase(ApplicationDBContext context)
        {
            dbContext = context;
        }

        public IQueryable<T> FindAll() => dbContext.Set<T>().AsNoTracking();

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression) =>
            dbContext.Set<T>().Where(expression).AsNoTracking();

        public void Create(T entity) => dbContext.Set<T>().Add(entity);

        public void Update(T entity) => dbContext.Set<T>().Update(entity);

        public void Delete(T entity) => dbContext.Set<T>().Remove(entity);

       
    }
}
