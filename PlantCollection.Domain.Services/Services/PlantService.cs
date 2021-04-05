using PlantCollection.Domain.Model.Entities;
using PlantCollection.Domain.Model.Interfaces.Infrastructure;
using PlantCollection.Domain.Model.Interfaces.Repositories;
using PlantCollection.Domain.Model.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PlantCollection.Domain.Services.Services
{
    public class PlantService : IPlantService
    {
        private readonly IPlantRepository _repository;
        private readonly IBlobService _blobService;
        private readonly IFunctionService _functionService;
        private readonly IQueueService _queueService;


        public PlantService(IPlantRepository repository,
            IBlobService blobService,
            IFunctionService functionService,
            IQueueService queueService
            )
        {
            _repository = repository;
            _blobService = blobService;
            _functionService = functionService;
            _queueService = queueService;
        }

        public async Task<IEnumerable<Plant>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Plant> GetByIdAsync(string id)
        {
            var plant = await _repository.GetByIdAsync(Guid.Parse(id));

            return plant;
        }

        public async Task InsertAsync(Plant plant, Stream stream)
        {
            var newUri = await _blobService.UploadAsync(stream);
            plant.ImageUri = newUri;

            await _repository.InsertAsync(plant);
        }

        public async Task UpdateAsync(Plant plant, Stream stream)
        {
            if (stream != null)
            {
                await _blobService.DeleteAsync(plant.ImageUri);

                var newUri = await _blobService.UploadAsync(stream);
                plant.ImageUri = newUri;
            }

            await _repository.UpdateAsync(plant);
        }

        public async Task DeleteAsync(Plant plant)
        {
            if (plant != null)
            {
                await _blobService.DeleteAsync(plant.ImageUri);
                await _repository.DeleteAsync(plant);
            }
        }

        public async Task IncreaseView(string id)
        {
            // ------- INSERCAO MANUAL -------
            //var plant =  await _repository.GetByIdAsync(Guid.Parse(id));
            //plant.PageViews += 1;
            //await _repository.UpdateAsync(plant);



            // ------- INSERCAO FUNCTION SERVICE -------
            //var plant = await _repository.GetByIdAsync(Guid.Parse(id));
            //await _functionService.InvokeAsync(plant);




            // ------- INSERCAO QUEUE -------
            //var plant = _repository.GetByIdAsync(Guid.Parse(id));
            //var response = plant.Result;

            var serialize = JsonSerializer.SerializeToUtf8Bytes(id);
            string stringify = Convert.ToBase64String(serialize);

            await _queueService.SendAsync(stringify);
        }
    }
}
