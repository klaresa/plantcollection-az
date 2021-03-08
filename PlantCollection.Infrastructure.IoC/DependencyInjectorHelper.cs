using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PlantCollection.Domain.Model.Interfaces.Repositories;
using PlantCollection.Domain.Model.Interfaces.Services;
using PlantCollection.Domain.Services.Services;
using PlantCollection.Infrastructure.DataAccess.Repositories;
using PlantCollection.Infrastructure.Services.Blob;
using System;

namespace PlantCollection.Infrastructure.IoC
{
    public class DependencyInjectorHelper
    {
        public static void Register(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<PlantContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("PlantContext")));

            services.AddScoped<IPlantRepository, PlantRepository>();
            services.AddScoped<IPlantService, PlantService>();

            services.AddScoped<IBlobService, BlobService>(provider =>
            new BlobService(configuration.GetValue<string>("ConnectionStringStorageAccount")));
        }
    }
}
