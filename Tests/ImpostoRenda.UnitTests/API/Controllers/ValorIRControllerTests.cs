using FluentAssertions;
using ImpostoRenda.API.Controllers;
using ImpostoRenda.Domain.Interfaces.Service;
using ImpostoRenda.Domain.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Moq.AutoMock;
using System;
using Xunit;

namespace ImpostoRenda.UnitTests.API.Controllers
{
    public class ValorIRControllerTests
    {
        private AutoMocker autoMocker;
        private ValorIRController controller;

        public ValorIRControllerTests()
        {
            autoMocker = new AutoMocker();
            controller = autoMocker.CreateInstance<ValorIRController>();
        }

        [Fact]
        public async void PostSucessoQuandoSalarioMaiorQueZero()
        {
            autoMocker.GetMock<IImpostoService>().Setup(x => x.CalcularDesconto(It.IsAny<decimal>())).ReturnsAsync(new DescontoResult(7.5M, 10000, 0));
            var result = await controller.Post(new CalcularDescontoIR { Salario = 10000M });
            result.Should().BeOfType<OkObjectResult>();
        }


        [Fact]
        public async void PostErroQuandoSalarioNaoInformado()
        {
            autoMocker.GetMock<IImpostoService>().Setup(x => x.CalcularDesconto(It.IsAny<decimal>())).ThrowsAsync(new Exception());
            var result = await controller.Post(null);
            result.Should().BeOfType<BadRequestObjectResult>();
        }
    }
}
