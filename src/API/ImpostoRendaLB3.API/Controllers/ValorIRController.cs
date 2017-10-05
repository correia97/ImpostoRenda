using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ImpostoRendaLB3.Domain.Interfaces.Service;
using Swashbuckle.AspNetCore.SwaggerGen;
using ImpostoRendaLB3.Domain.ViewModel;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ImpostoRendaLB3.API.Controllers
{
    [Route("api/[controller]")]
    public class ValorIRController : Controller
    {
        private readonly IImpostoService _impostoService;
        public ValorIRController(IImpostoService impostoService)
        {
            _impostoService = impostoService;
        }

        // POST api/values
        [HttpPost]
        [SwaggerResponse(200,typeof(DescontoResult))]
        [SwaggerResponse(500, typeof(string))]
        public async Task<IActionResult> Post([FromBody]CalcularDescontoIR calcDesconto)
        {
            try
            {
                var result = await _impostoService.CalcularDesconto(calcDesconto.Salario);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                
            }
        }
    }
}
