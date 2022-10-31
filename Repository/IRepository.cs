using System.Linq.Expressions;

namespace APICatalogo.Repository
{
    public interface IRepository<T>
    {
        IQueryable<T> Get();
        IQueryable<T> FindMany(Expression<Func<T, bool>> expression);
        Task<T> GetById(Expression<Func<T, bool>> predicate); 
        void Add(T entity); 
        void Update(T entity);  
        void Delete(T entity);  

    }
}
