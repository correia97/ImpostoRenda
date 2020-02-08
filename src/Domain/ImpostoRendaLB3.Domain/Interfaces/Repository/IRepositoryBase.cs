using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ImpostoRendaLB3.Domain.Interfaces.Repository
{
    public interface IRepositoryBase<T> where T : class
    {
        Task<T> Get(Expression<Func<T, bool>> predicate);
        Task<List<T>> GetAll();
        Task<List<T>> GetAll(Expression<Func<T, bool>> predicate);
        Task<T> Find(Guid id);
        Task Update(T entity);
        Task Insert(T entity);
        Task Delete(T entity);
    }
}
