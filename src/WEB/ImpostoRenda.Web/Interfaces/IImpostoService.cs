using ImpostoRenda.Web.Models;
using System.Threading.Tasks;

namespace ImpostoRenda.Web.Interfaces
{
    public interface IImpostoService
    {
        Task<ImpostoResult> CalcImpostoAsync(ImpostoRequest requestModel);
    }
}
