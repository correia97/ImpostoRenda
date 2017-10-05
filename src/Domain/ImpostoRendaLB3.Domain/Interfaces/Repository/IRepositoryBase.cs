using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ImpostoRendaLB3.Domain.Interfaces.Repository
{
    public interface IRepositoryBase<T> where T : class
    {
        Task<T> Get(Expression<Func<T, bool>> prediate);
        Task<List<T>> GetAll();
        Task<List<T>> GetAll(Expression<Func<T, bool>> prediate);
        Task<T> Find(Guid id);
        void Update(T entity);
        void Insert(T entity);
        void Delete(T entity);
    }
}
