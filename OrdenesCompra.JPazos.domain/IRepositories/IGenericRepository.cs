using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OrdenesCompra.JPazos.domain.IRepositories
{
    public interface IGenericRepository<T> where T : class
    {
        void Add(T entity);
        void AddRange(IEnumerable<T> entities);
        Task<T> Find(Expression<Func<T, bool>> expression);
        Task<IEnumerable<T>> List(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "");
        IQueryable<T> Query(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "");
        Task<T> GetById(Guid id);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
        void Update(T entityToUpdate);
        void Update(T entityToUpdate, Func<T, string> getKey);
        void Unmark(T entity);
        IQueryable<T> Queryable();
        Task Save();
    }
}
