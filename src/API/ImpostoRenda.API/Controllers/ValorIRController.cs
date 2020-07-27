using ImpostoRenda.Domain.Interfaces.Service;
using ImpostoRenda.Domain.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ImpostoRenda.API.Controllers
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


        [HttpPost]
        [ProducesResponseType(typeof(DescontoResult), 200)]
        [ProducesResponseType(typeof(string), 500)]
        public async Task<IActionResult> Post([FromBody] CalcularDescontoIR calcDesconto)
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
