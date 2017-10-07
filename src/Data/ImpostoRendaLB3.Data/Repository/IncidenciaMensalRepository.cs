using ImpostoRendaLB3.Domain.Entities;
using ImpostoRendaLB3.Domain.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace ImpostoRendaLB3.Data.Repository
{
    public class IncidenciaMensalRepository : RepositoryBase<IncidenciaMensal>, IIncidenciaMensalRepository
    {
        public IncidenciaMensalRepository(IMongoDBInstance mongoDBInstance)
            : base(mongoDBInstance)
        {

        }

      public async Task<IncidenciaMensal>  RetornaIncidenciaMensalPorSalario(decimal salario)
      {
       Expression<Func<IncidenciaMensal, bool>> predicate = (x) => salario >= x.ValorInicial && salario <= x.ValorFinal;
                var filter = Builders<IncidenciaMensal>.Filter.Where(predicate);
            var result = DbContext.GetCollection<IncidenciaMensal>(collectionName).Find(filter);
            return await result.FirstOrDefaultAsync();
           
      }

    }
}
