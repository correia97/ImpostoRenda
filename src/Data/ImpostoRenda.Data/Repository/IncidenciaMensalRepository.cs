using ImpostoRenda.Domain.Entities;
using ImpostoRenda.Domain.Interfaces.Entities;
using ImpostoRenda.Domain.Interfaces.Repository;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ImpostoRenda.Data.Repository
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

            var result1 = await DbContext.GetCollection<IncidenciaMensal>(collectionName).Find(filter).ToListAsync();
            var yyyy = result1.FirstOrDefault();

            var result = await DbContext.GetCollection<IncidenciaMensal>(collectionName).Find(x => true).ToListAsync();
            var incidencia = result.FirstOrDefault(x => salario >= x.ValorInicial && salario <= x.ValorFinal);
            if (incidencia != null)
                return incidencia;

            return new IncidenciaMensalNull();
        }

    }


}
