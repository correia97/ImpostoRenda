using ImpostoRendaLB3.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ImpostoRendaLB3.Domain.Interfaces.Repository
{
    public interface IIncidenciaMensalRepository : IRepositoryBase<IncidenciaMensal>
    {
        Task<IncidenciaMensal>  RetornaIncidenciaMensalPorSalario(decimal salario);
    }
    
}
