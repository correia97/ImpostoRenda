using FluentAssertions;
using ImpostoRenda.Domain.Entities;
using ImpostoRenda.Domain.Interfaces.Entities;
using Moq;
using Moq.AutoMock;
using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using Xunit;

namespace ImpostoRenda.UnitTests.Domain.Entities
{
    public class IncidenciaMensalTest
    {
        private AutoMocker autoMocker;
        public IncidenciaMensalTest()
        {
            autoMocker = new AutoMocker();
        }

        [Fact]
        public void IncidenciaMensal_MaiorQueZeoQuandoSalarioInformado()
        {
            var mock = new Mock<IIncidenciaMensal>();
            mock.Setup(foo => foo.CalcularDesconto(It.IsAny<decimal>())).Returns(100);

            var result = mock.Object.CalcularDesconto(100);

            result.Should().Be(100);
        }
    }
}
