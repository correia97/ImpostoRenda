using ImpostoRendaLB3.Domain.Interfaces.Entities;
using System;

namespace ImpostoRendaLB3.Domain.Entities
{
    public class IncidenciaMensal : EntityBase, IIncidenciaMensal
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
        
        
        public decimal ValorInicial { get => _ValorInicial; set => _ValorInicial = value; }
        public decimal ValorFinal { get => _ValorFinal; set => _ValorFinal =value; }
        public decimal Aliquota { get => _Aliquota; set => _Aliquota = value; }
        public decimal ParcelaDeduzir { get => _ParcelaDeduzir; set => _ParcelaDeduzir = value; }



        private decimal _ValorInicial { get;  set; }
        private decimal _ValorFinal { get;  set; }
        private decimal _Aliquota { get;  set; }
        private decimal _ParcelaDeduzir { get;  set; }

        public decimal CalcularDesconto(decimal salario)
        {
            var desconto = (salario * Aliquota) / 100;
            return Math.Round(desconto - ParcelaDeduzir, 2);
        }
    }
}
