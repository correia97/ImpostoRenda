using FluentAssertions;
using ImpostoRendaLB3.Domain.Entities;
using ImpostoRendaLB3.Domain.Interfaces.Repository;
using ImpostoRendaLB3.Domain.Interfaces.Service;
using ImpostoRendaLB3.Domain.Service;
using Moq;
using Moq.AutoMock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Xunit;

namespace ImpostoRendaLB3.UnitTests.Domain.Service
{
    public class ImpostoServiceTests
    {
        private IImpostoService impostoService;
        private AutoMocker autoMocker;
        List<IncidenciaMensal> listIncidenciaMensal { get; set; }
        public ImpostoServiceTests()
        {
            autoMocker = new AutoMocker();
            impostoService = autoMocker.CreateInstance<ImpostoService>();
            PreencherLista();
        }
        public void PreencherLista()
        {
            listIncidenciaMensal = new List<IncidenciaMensal>();
            listIncidenciaMensal.Add(new IncidenciaMensal(0, 1903.98M, 0, 0));
            listIncidenciaMensal.Add(new IncidenciaMensal(1903.99M, 2826.65M, 7.5M, 142.8M));
            listIncidenciaMensal.Add(new IncidenciaMensal(2826.66M, 3751.05M, 15M, 354.8M));
            listIncidenciaMensal.Add(new IncidenciaMensal(3751.06M, 4664.68M, 22.5M, 636.13M));
            listIncidenciaMensal.Add(new IncidenciaMensal(4664.68M, decimal.MaxValue, 27.5M, 869.36M));
        }


        [Theory]
        [InlineData(0, 0)]
        [InlineData(100, 0)]
        [InlineData(1903.98, 0)]
        [InlineData(2000, 7.2)]
        [InlineData(2826.66, 69.2)]
        [InlineData(3751.06, 207.86)]
        [InlineData(4664.69, 413.43)]
        [InlineData(10000, 1880.64)]
        public async void CalcularDescontoSucesso(decimal salario, decimal valorEsperado)
        {
            var incidencia = listIncidenciaMensal.FirstOrDefault(p => salario >= p.ValorInicial && salario <= p.ValorFinal);
            autoMocker.GetMock<IIncidenciaMensalRepository>()
                .Setup(x => x.Get(It.IsAny<Expression<Func<IncidenciaMensal, bool>>>())).ReturnsAsync(incidencia);

            var result = await impostoService.CalcularDesconto(salario);
            result.ValorDesconto.Should().Be(valorEsperado);
        }
        [Fact]        
        public void CalcularDescontoDeveChamarRepositoty()
        { 
            impostoService.CalcularDesconto(100);
            autoMocker.GetMock<IIncidenciaMensalRepository>().Verify(x => x.Get(It.IsAny<Expression<Func<IncidenciaMensal, bool>>>()), Times.Once());            
        }
        [Fact]
        public async void CalcularDescontoQuandoAliquotaZeroRetornaZero()
        {
            var salario = 0M;
            var incidencia =  listIncidenciaMensal.FirstOrDefault(p => salario >= p.ValorInicial && salario <= p.ValorFinal);
            autoMocker.GetMock<IIncidenciaMensalRepository>()
                .Setup(x => x.Get(It.IsAny<Expression<Func<IncidenciaMensal, bool>>>())).ReturnsAsync(incidencia);

            var result = await impostoService.CalcularDesconto(salario);
            result.ValorDesconto.Should().Be(0M);
        }
    }
}
