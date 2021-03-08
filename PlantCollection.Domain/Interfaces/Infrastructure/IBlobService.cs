using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace PlantCollection.Domain.Model.Interfaces.Services
{
    public interface IBlobService
    {
        Task<string> UploadAsync(Stream stream);

        Task DeleteAsync(string blobName);
    }
}
