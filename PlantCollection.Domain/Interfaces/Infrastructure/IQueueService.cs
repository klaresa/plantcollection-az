using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PlantCollection.Domain.Model.Interfaces.Infrastructure
{
    public interface IQueueService
    {
        public Task SendAsync(string messageText);
    }
}
