using FluentAssertions;
using ImpostoRendaLB3.Data.Repository;
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
            autoMocker.GetMock<IIncidenciaMensalRepository>()
                            .Setup(x => x.GetAll()).ReturnsAsync(new List<IncidenciaMensal>() { new IncidenciaMensal(0, 0, 0, 0) });
        }

        [Theory]
        [InlineData(0, 0, 0, 0)]
        [InlineData(100, 0, 0, 0)]
        [InlineData(1903.97, 0, 0, 0)]
        [InlineData(2000, 7.2, 7.5, 142.8)]
        [InlineData(2826.66, 69.2, 15, 354.8)]
        [InlineData(3751.06, 207.86, 22.5, 636.13)]
        [InlineData(4664.69, 413.43, 27.5, 869.36)]
        [InlineData(10000, 1880.64, 27.5, 869.36)]
        public async void CalcularDescontoSucesso(decimal salario, decimal valorEsperado, decimal aliquota, decimal parcelaDeduzir)
        {
            if (salario > 0)
                autoMocker.GetMock<IIncidenciaMensalRepository>()
                    .Setup(x => x.RetornaIncidenciaMensalPorSalario(salario)).ReturnsAsync(new IncidenciaMensal(salario - 1, salario, aliquota, parcelaDeduzir));
            else
                autoMocker.GetMock<IIncidenciaMensalRepository>()
                    .Setup(x => x.RetornaIncidenciaMensalPorSalario(salario)).ReturnsAsync(new IncidenciaMensalNull());

            var result = await impostoService.CalcularDesconto(salario);
            result.ValorDesconto.Should().Be(valorEsperado);
        }
        [Fact]
        public void CalcularDescontoDeveChamarRepositoty()
        {
            var salario = 100;
            impostoService.CalcularDesconto(salario);
            autoMocker.GetMock<IIncidenciaMensalRepository>().Verify(x => x.RetornaIncidenciaMensalPorSalario(salario), Times.Once());
        }

    }
}
