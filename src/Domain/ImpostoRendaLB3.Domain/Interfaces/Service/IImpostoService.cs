using ImpostoRendaLB3.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ImpostoRendaLB3.Domain.Interfaces.Service
{
    public interface IImpostoService
    {
        Task<DescontoResult> CalcularDesconto(decimal salario);
    }
}
