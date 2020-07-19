using ImpostoRenda.Domain.ViewModel;
using System.Threading.Tasks;

namespace ImpostoRenda.Domain.Interfaces.Service
{
    public interface IImpostoService
    {
        Task<DescontoResult> CalcularDesconto(decimal salario);
    }
}
