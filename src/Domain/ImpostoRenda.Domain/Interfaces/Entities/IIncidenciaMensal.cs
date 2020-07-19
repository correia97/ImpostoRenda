namespace ImpostoRenda.Domain.Interfaces.Entities
{
    public interface IIncidenciaMensal
    {
        decimal ValorInicial { get;  set; }
        decimal ValorFinal { get; set; }
        decimal Aliquota { get; set; }
        decimal ParcelaDeduzir { get; set; }
        decimal CalcularDesconto(decimal salario);
    }
}
