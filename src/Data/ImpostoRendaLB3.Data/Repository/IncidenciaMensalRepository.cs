using ImpostoRendaLB3.Domain.Entities;
using ImpostoRendaLB3.Domain.Interfaces.Entities;
using ImpostoRendaLB3.Domain.Interfaces.Repository;
using MongoDB.Driver;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

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
            var result = await DbContext.GetCollection<IncidenciaMensal>(collectionName).Find(filter).ToListAsync();
            var incidencia = result.FirstOrDefault();
            if (incidencia != null)
                return incidencia;

            return new IncidenciaMensalNull();
        }

    }
}
