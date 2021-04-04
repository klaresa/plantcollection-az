using PlantCollection.Domain.Model.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace PlantCollection.Domain.Model.Interfaces.Services
{
    public interface IPlantService
    {
        Task<IEnumerable<Plant>> GetAllAsync();
        Task<Plant> GetByIdAsync(string id);
        Task InsertAsync(Plant plant, Stream stream);
        Task UpdateAsync(Plant plant, Stream stream);
        Task DeleteAsync(Plant plant);
        Task IncreaseView(string id);
    }
}
