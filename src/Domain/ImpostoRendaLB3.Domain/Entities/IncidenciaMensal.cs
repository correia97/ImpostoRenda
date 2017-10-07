using System;
using System.Collections.Generic;
using System.Text;

namespace ImpostoRendaLB3.Domain.Entities
{
    public class IncidenciaMensal : EntityBase
    {
        public IncidenciaMensal(decimal valorInicial, decimal valorFinal, decimal aliquota, decimal parcelaDeduzir)
        {
            ValorInicial = valorInicial;
            ValorFinal = valorFinal;
            Aliquota = aliquota;
            ParcelaDeduzir = parcelaDeduzir;
            Id = Guid.NewGuid();
        }
        protected IncidenciaMensal()
        {
        }
        public decimal ValorInicial { get; private set; }
        public decimal ValorFinal { get; private set; }
        public decimal Aliquota { get; private set; }
        public decimal ParcelaDeduzir { get; private set; }

        public decimal CalcularDesconto(decimal salario)
        {
 return Math.Round(((salario * Aliquota) / 100) - ParcelaDeduzir, 2);
        }
    }
}
