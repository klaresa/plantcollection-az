using PlantCollection.Domain.Model.Entities;
using PlantCollection.Domain.Model.Interfaces.Repositories;
using PlantCollection.Domain.Model.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace PlantCollection.Domain.Services.Services
{
    public class PlantService : IPlantService
    {
        private readonly IPlantRepository _repository;
        private readonly IBlobService _blobService;

        public PlantService(IPlantRepository repository, IBlobService blobService)
        {
            _repository = repository;
            _blobService = blobService;
        }

        public async Task<IEnumerable<Plant>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Plant> GetByIdAsync(string id)
        {
            return await _repository.GetByIdAsync(Guid.Parse(id));
        }

        public async Task InsertAsync(Plant plant, Stream stream)
        {
            var newUri = await _blobService.UploadAsync(stream);
            plant.ImageUri = newUri;

            await _repository.InsertAsync(plant);
        }

        public async Task UpdateAsync(Plant plant, Stream stream)
        {
            if (plant.ImageUri != null)
            {
                await _blobService.DeleteAsync(plant.ImageUri);
            }
            var newUri = await _blobService.UploadAsync(stream);
            plant.ImageUri = newUri;

            await _repository.UpdateAsync(plant);
        }

        public async Task DeleteAsync(Plant plant)
        {
            await _blobService.DeleteAsync(plant.ImageUri);
            await _repository.DeleteAsync(plant);
        }
    }
}
