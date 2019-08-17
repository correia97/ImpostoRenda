using ImpostoRendaLB3.Domain.Interfaces.Service;
using ImpostoRendaLB3.Domain.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ImpostoRendaLB3.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValorIRController : ControllerBase
    {
        private readonly IImpostoService _impostoService;
        public ValorIRController(IImpostoService impostoService)
        {
            _impostoService = impostoService;
        }

        // POST api/values
        [HttpPost]
        [ProducesResponseType(typeof(DescontoResult), 200)]
        [ProducesResponseType(typeof(string), 500)]
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
