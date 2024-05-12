using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Hotel.DataAccess.Interfaces
{
    /// <summary>
    /// A generic interface for repository operations, supporting CRUD functionality.
    /// </summary>
    /// <typeparam name="T">The type of the entity.</typeparam>
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter = null,
                                         Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                         string includeProperties = "");
        Task<T> GetByIdAsync(dynamic id);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        void Delete(T entity);

    }
}
