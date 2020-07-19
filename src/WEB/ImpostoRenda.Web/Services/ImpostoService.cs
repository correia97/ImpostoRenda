using Flurl;
using Flurl.Http;
using ImpostoRenda.Web.Interfaces;
using ImpostoRenda.Web.Models;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace ImpostoRenda.Web.Services
{
    public class ImpostoService : IImpostoService
    {
        private string BaseUrl { get; set; }
        public ImpostoService(IConfiguration configuration)
        {
            BaseUrl = configuration.GetSection("api").Value;
        }
        public async Task<ImpostoResult> CalcImpostoAsync(ImpostoRequest requestModel)
        {
            var result = await BaseUrl
                            .AppendPathSegment("api/ValorIR")
                            .PostJsonAsync(requestModel)
                            .ReceiveJson<ImpostoResult>();

            return result;
        }
    }
}
