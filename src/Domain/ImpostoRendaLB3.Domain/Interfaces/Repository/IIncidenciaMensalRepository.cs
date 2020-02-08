using ImpostoRendaLB3.Domain.Entities;
using ImpostoRendaLB3.Domain.Interfaces.Entities;
using System.Threading.Tasks;

namespace ImpostoRendaLB3.Domain.Interfaces.Repository
{
    public interface IIncidenciaMensalRepository : IRepositoryBase<IncidenciaMensal>
    {
        Task<IIncidenciaMensal>  RetornaIncidenciaMensalPorSalario(decimal salario);
    }
    
}
