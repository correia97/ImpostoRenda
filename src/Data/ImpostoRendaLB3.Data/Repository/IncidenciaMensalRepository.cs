using ImpostoRendaLB3.Domain.Entities;
using ImpostoRendaLB3.Domain.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Driver;
using ImpostoRendaLB3.Domain.Interfaces.Entities;

namespace ImpostoRendaLB3.Data.Repository
{
    public class IncidenciaMensalRepository : RepositoryBase<IncidenciaMensal>, IIncidenciaMensalRepository
    {
        public IncidenciaMensalRepository(IMongoDBInstance mongoDBInstance)
            : base(mongoDBInstance)
        {

        }

        public async Task<IIncidenciaMensal> RetornaIncidenciaMensalPorSalario(decimal salario)
        {
            Expression<Func<IncidenciaMensal, bool>> predicate = (x) => salario >= x.ValorInicial && salario <= x.ValorFinal;
            var filter = Builders<IncidenciaMensal>.Filter.Where(predicate);
            var incidencia = await DbContext.GetCollection<IncidenciaMensal>(collectionName).Find(filter).FirstOrDefaultAsync();
            if (incidencia != null)
                return incidencia;

            return new IncidenciaMensalNull();
        }

    }
}
