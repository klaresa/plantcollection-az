using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PlantCollection.Domain.Model.Interfaces.Infrastructure;
using PlantCollection.Domain.Model.Interfaces.Repositories;
using PlantCollection.Domain.Model.Interfaces.Services;
using PlantCollection.Domain.Services.Services;
using PlantCollection.Infrastructure.DataAccess.Repositories;
using PlantCollection.Infrastructure.Services.Blob;
using PlantCollection.Infrastructure.Services.Functions;
using PlantCollection.Infrastructure.Services.Queue;
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

            var connStringStorageAccount = configuration.GetValue<string>("ConnectionStringStorageAccount");

            services.AddScoped<IBlobService, BlobService>(provider =>
            new BlobService(connStringStorageAccount));

            services.AddScoped<IQueueService, QueueService>(provider =>
            new QueueService(connStringStorageAccount));

            services.AddScoped<IFunctionService, FunctionService>(provider =>
            new FunctionService(configuration.GetValue<string>("FunctionBaseAddress")));
        }
    }
}
