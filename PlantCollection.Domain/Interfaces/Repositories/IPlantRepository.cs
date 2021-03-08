using PlantCollection.Domain.Model.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PlantCollection.Domain.Model.Interfaces.Repositories
{
    public interface IPlantRepository
    {
        Task<IEnumerable<Plant>> GetAllAsync();
        Task<Plant> GetByIdAsync(Guid id);
        Task InsertAsync(Plant plant);
        Task UpdateAsync(Plant plant);
        Task DeleteAsync(Plant plant);
    }
}
