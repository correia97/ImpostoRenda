using FluentAssertions;
using ImpostoRendaLB3.API.Controllers;
using ImpostoRendaLB3.Domain.Interfaces.Service;
using ImpostoRendaLB3.Domain.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Moq.AutoMock;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ImpostoRendaLB3.UnitTests.API.Controllers
{
    public class ValorIRControllerTests
    {
        private IImpostoService impostoService;
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
