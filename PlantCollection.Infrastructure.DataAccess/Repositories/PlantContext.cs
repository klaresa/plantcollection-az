using Microsoft.EntityFrameworkCore;
using PlantCollection.Domain.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlantCollection.Infrastructure.DataAccess.Repositories
{
    public class PlantContext : DbContext
    {
        public PlantContext(DbContextOptions<PlantContext> options)
            : base(options)
        {
        }

        public DbSet<Plant> Plant { get; set; }
    }
}
