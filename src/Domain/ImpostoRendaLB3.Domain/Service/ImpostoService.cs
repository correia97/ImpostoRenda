using ImpostoRendaLB3.Domain.Entities;
using ImpostoRendaLB3.Domain.Interfaces.Repository;
using ImpostoRendaLB3.Domain.Interfaces.Service;
using ImpostoRendaLB3.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ImpostoRendaLB3.Domain.Service
{
    public class ImpostoService : IImpostoService
    {
        private readonly IIncidenciaMensalRepository _incidenciaMensalRepository;
        public ImpostoService(IIncidenciaMensalRepository incidenciaMensalRepository)
        {
            _incidenciaMensalRepository = incidenciaMensalRepository;
            IniciarColecao();
        }

        private void IniciarColecao()
        {
            var result = _incidenciaMensalRepository.GetAll().Result;
            if (result == null || result.Count <= 0)
            {           
                _incidenciaMensalRepository.Insert(new IncidenciaMensal(0, 1903.98M, 0, 0));
                _incidenciaMensalRepository.Insert(new IncidenciaMensal(1903.99M, 2826.65M, 7.5M, 142.8M));
                _incidenciaMensalRepository.Insert(new IncidenciaMensal(2826.66M, 3751.05M, 15M, 354.8M));
                _incidenciaMensalRepository.Insert(new IncidenciaMensal(3751.06M, 4664.68M, 22.5M, 636.13M));
                _incidenciaMensalRepository.Insert(new IncidenciaMensal(4664.68M, decimal.MaxValue, 27.5M, 869.36M));
            }
        }

        public async Task<DescontoResult> CalcularDesconto(decimal salario)
        {
            var incidencia = await _incidenciaMensalRepository.RetornaIncidenciaMensalPorSalario(salario);
            if (incidencia == null || incidencia.Aliquota == 0)
                return new DescontoResult(0, salario, 0); 
           var desconto = incidencia.CalcularDesconto(salario);

            return new DescontoResult(incidencia.Aliquota, salario, desconto);
        }
   
    }
}
