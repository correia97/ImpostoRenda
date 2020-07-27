using FluentAssertions;
using ImpostoRenda.Domain.Entities;
using ImpostoRenda.Domain.Interfaces.Entities;
using Moq;
using Moq.AutoMock;
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
        public void IncidenciaMensal_ChamarcalCalcularDescontoDevolveUmValor()
        {
            var mock = new Mock<IIncidenciaMensal>();
            mock.Setup(foo => foo.CalcularDesconto(It.IsAny<decimal>())).Returns(100);

            var result = mock.Object.CalcularDesconto(100);

            result.Should().Be(100);
        }


        [Fact]
        public void IncidenciaMensal_MaiorQueZeoQuandoSalarioInformado()
        {
            var valorIncial = 1000M;
            var valorFinal = 2500M;
            var aliquota = 10M;
            var deduzir = 10;


            var inci = new IncidenciaMensal(valorIncial, valorFinal, aliquota, deduzir);

            var result = inci.CalcularDesconto(2300);

            inci.ParcelaDeduzir.Should().Be(deduzir);
            inci.ValorFinal.Should().Be(valorFinal);
            inci.ValorInicial.Should().Be(valorIncial);
            inci.Aliquota.Should().Be(aliquota);
            result.Should().Be(220);
        }

        [Fact]
        public void IncidenciaMensal_DeveSerZeroQuandoIncidenciaMensalNull()
        {
            var inci = new IncidenciaMensalNull();

            var result = inci.CalcularDesconto(2300);

            inci.ParcelaDeduzir.Should().Be(0);
            inci.ValorFinal.Should().Be(0);
            inci.ValorInicial.Should().Be(0);
            inci.Aliquota.Should().Be(0);
            result.Should().Be(0);
        }
    }
}
