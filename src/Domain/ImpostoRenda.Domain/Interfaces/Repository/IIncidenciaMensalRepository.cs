using ImpostoRenda.Domain.Entities;
using ImpostoRenda.Domain.Interfaces.Entities;
using System.Threading.Tasks;

namespace ImpostoRenda.Domain.Interfaces.Repository
{
    public interface IIncidenciaMensalRepository : IRepositoryBase<IncidenciaMensal>
    {
        Task<IIncidenciaMensal>  RetornaIncidenciaMensalPorSalario(decimal salario);
    }
    
}
