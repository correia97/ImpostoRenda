namespace ImpostoRendaLB3.Domain.ViewModel
{
    public class DescontoResult
    {
        public DescontoResult(decimal aliquota, decimal salario, decimal desconto)
        {
            Aliquota = aliquota;
            Salario = salario;
            ValorDesconto = desconto;
        }
        public decimal Aliquota { get; private set; }
        public decimal Salario { get; private set; }
        public decimal ValorDesconto { get; private set; }
        public string Message
        {
            get
            {
                return Aliquota > 0 ? "Valor Calculado com Sucesso!" : "Não existe Aliquota para o salário informado";
            }
        }

    }
}
