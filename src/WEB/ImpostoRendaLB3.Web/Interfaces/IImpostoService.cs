using ImpostoRendaLB3.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImpostoRendaLB3.Web.Interfaces
{
   public interface IImpostoService
    {
        Task<ImpostoResult> CalcImpostoAsync(ImpostoRequest requestModel);
    }
}
