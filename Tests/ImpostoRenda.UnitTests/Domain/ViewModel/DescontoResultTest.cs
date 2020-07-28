using FluentAssertions;
using ImpostoRenda.Domain.ViewModel;
using Xunit;

namespace ImpostoRenda.UnitTests.Domain.ViewModel
{
    public class DescontoResultTest
    {
        [Fact]
        public void DescontoResultMessageQuandoAliquotaZero()
        {
            var aliquota = 0M;
            var salario = 0M;
            var desconto = 0M;

            var data = new DescontoResult(aliquota, salario, desconto);

            data.Aliquota.Should().Be(aliquota);
            data.Salario.Should().Be(salario);
            data.ValorDesconto.Should().Be(desconto);
            data.Message.Should().Be("Não existe Aliquota para o salário informado");

        }

        [Fact]
        public void DescontoResultMessageQuandoAliquotaMaiorZero()
        {
            var aliquota = 10M;
            var salario = 10M;
            var desconto = 10M;

            var data = new DescontoResult(aliquota, salario, desconto);

            data.Aliquota.Should().Be(aliquota);
            data.Salario.Should().Be(salario);
            data.ValorDesconto.Should().Be(desconto);
            data.Message.Should().Be("Valor Calculado com Sucesso!");

        }
    }
}
