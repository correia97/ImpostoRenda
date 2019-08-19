using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImpostoRendaLB3.Web.Models
{
    public class ImpostoResult
    {

            public decimal aliquota { get; set; }
            public decimal salario { get; set; }
            public decimal valorDesconto { get; set; }
            public string message { get; set; }
        

    }
}
