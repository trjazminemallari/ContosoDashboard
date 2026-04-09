using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace ContosoDashboard.Services
{
    public class LocalFileStorageService : IFileStorageService
    {
        private readonly string _basePath;

        public LocalFileStorageService(IConfiguration configuration)
        {
            _basePath = configuration["FileStorage:BasePath"] ?? "AppData/uploads";
        }

        public async Task<string> UploadAsync(Stream fileStream, string fileName, string contentType)
        {
            var filePath = Path.Combine(_basePath, fileName);
            Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);
            using (var output = File.Create(filePath))
            {
                await fileStream.CopyToAsync(output);
            }
            // Return only the file name, not the full path
            return fileName;
        }

        public Task DeleteAsync(string filePath)
        {
            var fullPath = Path.Combine(_basePath, filePath);
            if (File.Exists(fullPath))
                File.Delete(fullPath);
            return Task.CompletedTask;
        }

        public Task<Stream> DownloadAsync(string filePath)
        {
            var fullPath = Path.Combine(_basePath, filePath);
            Stream stream = File.OpenRead(fullPath);
            return Task.FromResult(stream);
        }

        public Task<string> GetUrlAsync(string filePath, TimeSpan expiration)
        {
            // Local implementation does not support signed URLs
            return Task.FromResult(string.Empty);
        }
    }
}
