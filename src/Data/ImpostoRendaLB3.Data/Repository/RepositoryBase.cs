using ImpostoRendaLB3.Domain.Entities;
using ImpostoRendaLB3.Domain.Interfaces.Repository;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ImpostoRendaLB3.Data.Repository
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : EntityBase
    {
        protected IMongoDatabase DbContext { get; }
        protected  string collectionName { get; set; }

        public RepositoryBase(IMongoDBInstance mongoDBInstance)
        {
            if (DbContext == null)
                DbContext = mongoDBInstance.ReturnDB();
            collectionName = typeof(T).Name;
        }

        public async Task Delete(T entity)
        {
            var filter = Builders<T>.Filter.Eq(x => x.Id, entity.Id);
           await DbContext.GetCollection<T>(collectionName).DeleteOneAsync(filter);
        }

        public async Task<T> Find(Guid id)
        {
            var filter = Builders<T>.Filter.Eq(x => x.Id, id);
            var result = DbContext.GetCollection<T>(collectionName).Find(filter);
            return await result.FirstOrDefaultAsync();
        }

        public async Task<T> Get(Expression<Func<T, bool>> predicate)
        {
            var filter = Builders<T>.Filter.Where(predicate);
            var result = DbContext.GetCollection<T>(collectionName).Find(filter);
            return await result.FirstOrDefaultAsync();
        }

        public async Task<List<T>> GetAll()
        {
            var result = DbContext.GetCollection<T>(collectionName).AsQueryable();
            return await result.ToListAsync();
        }

        public async Task<List<T>> GetAll(Expression<Func<T, bool>> predicate)
        {
            var filter = Builders<T>.Filter.Where(predicate);
            var result = await DbContext.GetCollection<T>(collectionName).FindAsync(filter);
            return await result.ToListAsync();
        }

        public async Task Insert(T entity)
        {
            await DbContext.GetCollection<T>(collectionName).InsertOneAsync(entity);
        }

        public async Task Update(T entity)
        {
            var filter = Builders<T>.Filter.Eq(x => x.Id, entity.Id);
            await DbContext.GetCollection<T>(collectionName)
                                         .ReplaceOneAsync(filter, entity, new UpdateOptions { IsUpsert = true });
        }
    }
}
