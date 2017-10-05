using ImpostoRendaLB3.Domain.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Threading.Tasks;
using ImpostoRendaLB3.Domain.Entities;

namespace ImpostoRendaLB3.Data.Repository
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : EntityBase
    {
        protected IMongoDatabase DbContext { get; }

        public RepositoryBase(IMongoDBInstance mongoDBInstance)
        {
            if (DbContext == null)
                DbContext = mongoDBInstance.ReturnDB();
        }

        public void Delete(T entity)
        {
            var filter = Builders<T>.Filter.Eq(x => x.Id, entity.Id);
            DbContext.GetCollection<T>(typeof(T).Name).DeleteOneAsync(filter);
        }

        public async Task<T> Find(Guid id)
        {
            var filter = Builders<T>.Filter.Eq(x => x.Id, id);
            var result = DbContext.GetCollection<T>(typeof(T).Name).Find(filter);
            return await result.FirstOrDefaultAsync();
        }

        public async Task<T> Get(Expression<Func<T, bool>> prediate)
        {
            var filter = Builders<T>.Filter.Where(prediate);
            var result = DbContext.GetCollection<T>(typeof(T).Name).Find(filter);
            return await result.FirstOrDefaultAsync();
        }

        public async Task<List<T>> GetAll()
        {
            var result = DbContext.GetCollection<T>(typeof(T).Name).AsQueryable();
            return await result.ToListAsync();
        }

        public async Task<List<T>> GetAll(Expression<Func<T, bool>> prediate)
        {
            var filter = Builders<T>.Filter.Where(prediate);
            var result = await DbContext.GetCollection<T>(typeof(T).Name).FindAsync(filter);
            return await result.ToListAsync();
        }

        public void Insert(T entity)
        {
            DbContext.GetCollection<T>(typeof(T).Name).InsertOneAsync(entity);
        }

        public void Update(T entity)
        {
            var filter = Builders<T>.Filter.Eq(x => x.Id, entity.Id);
            DbContext.GetCollection<T>(typeof(T).Name)
                                       .ReplaceOneAsync(filter, entity, new UpdateOptions { IsUpsert = true });
        }
    }
}
