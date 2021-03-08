using Microsoft.EntityFrameworkCore;
using PlantCollection.Domain.Model.Entities;
using PlantCollection.Domain.Model.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PlantCollection.Infrastructure.DataAccess.Repositories
{
    public class PlantRepository : IPlantRepository
    {
        private readonly PlantContext _context;

        public PlantRepository(PlantContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Plant>> GetAllAsync()
        {
            return await _context.Plant.ToListAsync();
        }

        public async Task<Plant> GetByIdAsync(Guid id)
        {
            return await _context.Plant.SingleOrDefaultAsync(m => m.Id == id);
        }

        public async Task InsertAsync(Plant plant)
        {
            _context.Add(plant);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Plant plant)
        {
            _context.Update(plant);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Plant plant)
        {
            _context.Remove(plant);
            await _context.SaveChangesAsync();
        }
    }
}
