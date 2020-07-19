using ImpostoRenda.Data;
using ImpostoRenda.Data.Repository;
using ImpostoRenda.Domain.Interfaces.Repository;
using ImpostoRenda.Domain.Interfaces.Service;
using ImpostoRenda.Domain.Service;
using Microsoft.Extensions.DependencyInjection;

namespace ImpostoRenda.CrossCutting.IoC
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
