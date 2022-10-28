using E_CommerceAPI.Application.Abstractions.Storage;
using Microsoft.AspNetCore.Http;

namespace E_CommerceAPI.Infrastructure.Services.Storage
{
    public class StorageService : IStorageService
    {
        readonly IStorage _storage;
        public StorageService(IStorage storage)
        {
            _storage = storage;
        }

        public string StorageName { get => _storage.GetType().Name; }

        public async Task DeleteAsync(string pathOrConatinerName, string fileName)
        => await _storage.DeleteAsync(pathOrConatinerName, fileName);

        public List<string> GetFiles(string pathOrConatinerName)
        => _storage.GetFiles(pathOrConatinerName);

        public bool HasFile(string pathOrConatinerName, string fileName)
        => _storage.HasFile(pathOrConatinerName, fileName);

        public Task<List<(string fileName, string pathOrContainerName)>> UploadAsync(string pathOrContainerName, IFormFileCollection files)
        => _storage.UploadAsync(pathOrContainerName, files);
    }
}
