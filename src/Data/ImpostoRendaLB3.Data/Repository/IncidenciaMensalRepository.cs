using ImpostoRendaLB3.Domain.Entities;
using ImpostoRendaLB3.Domain.Interfaces.Entities;
using ImpostoRendaLB3.Domain.Interfaces.Repository;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
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
            var result = await DbContext.GetCollection<IncidenciaMensal>(collectionName).Find(x => true).ToListAsync();
            var incidencia = result.FirstOrDefault(x => salario >= x.ValorInicial && salario <= x.ValorFinal);
            if (incidencia != null)
                return incidencia;

            return new IncidenciaMensalNull();
        }

    }


}
