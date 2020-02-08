using ImpostoRendaLB3.Data;
using ImpostoRendaLB3.Data.Repository;
using ImpostoRendaLB3.Domain.Interfaces.Repository;
using ImpostoRendaLB3.Domain.Interfaces.Service;
using ImpostoRendaLB3.Domain.Service;
using Microsoft.Extensions.DependencyInjection;

namespace ImpostoRendaLB3.CrossCutting.IoC
{
    public static class BootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IIncidenciaMensalRepository, IncidenciaMensalRepository>();
            services.AddScoped<IImpostoService, ImpostoService>();
            services.AddScoped<IMongoDBInstance, MongoDBInstance>();            
        }
    }
}
