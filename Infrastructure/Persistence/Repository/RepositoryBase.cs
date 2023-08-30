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

        public virtual IQueryable<T> Query() => dbContext.Set<T>();

        public  IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression) =>
            dbContext.Set<T>().Where(expression);

        public void Create(T entity) => dbContext.Set<T>().Add(entity);

        public void Update(T entity) => dbContext.Set<T>().Update(entity);

        public void Delete(T entity) => dbContext.Set<T>().Remove(entity);

       
    }
}
