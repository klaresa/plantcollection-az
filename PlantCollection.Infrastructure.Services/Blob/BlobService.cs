using Azure.Storage.Blobs;
using PlantCollection.Domain.Model.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace PlantCollection.Infrastructure.Services.Blob
{
    public class BlobService : IBlobService
    {
        private readonly BlobServiceClient _blobServiceClient;
        private const string _container = "plantscontainer";

        public BlobService(string storageAccount)
        {
            _blobServiceClient = new BlobServiceClient(storageAccount);
        }

        public async Task<string> UploadAsync(Stream stream)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(_container);

            if (!await containerClient.ExistsAsync())
            {
                await containerClient.CreateIfNotExistsAsync();
                await containerClient.SetAccessPolicyAsync(Azure.Storage.Blobs.Models.PublicAccessType.BlobContainer);
            }

            var blobClient = containerClient.GetBlobClient($"{Guid.NewGuid()}.jpg");

            await blobClient.UploadAsync(stream, true);

            return blobClient.Uri.ToString();
        }

        public async Task DeleteAsync(string blobName)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(_container);

            var blob = new BlobClient(new Uri(blobName));

            var blobClient = containerClient.GetBlobClient(blob.Name);

            await blobClient.DeleteIfExistsAsync();
        }

    }
}
