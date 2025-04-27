using Museum.Application.Interfaces;
using Museum.Application.Common.Exceptions;

namespace Museum.Persistense.Services
{
    public class FileService : IFileService
    {
        private readonly string _uploadsPath;

        public FileService(string? uploadPath)
        {
            if (uploadPath == null) uploadPath = "wwwroot/uploads";

            _uploadsPath = uploadPath;
            Directory.CreateDirectory(_uploadsPath);
        }

        public async Task<string> UploadFileAsync(Stream fileStream, string fileName,
            CancellationToken cancellationToken)
        {
            if (fileStream == null || fileStream.Length == 0)
                throw new ArgumentException("File stream is empty");

            var uniqueFileName = $"{Guid.NewGuid()}_{fileName}";
            var filePath = Path.Combine(_uploadsPath, uniqueFileName);

            try
            {
                using (var outputStream = new FileStream(filePath, FileMode.Create))
                {
                    await fileStream.CopyToAsync(outputStream, cancellationToken);
                }
                return filePath;
            } 
            catch (Exception ex)
            {
                throw new FileUploadException(filePath);
            }
        }

        public void DeleteFile(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentNullException(nameof(filePath));

            var fullPath = Path.Combine(_uploadsPath, Path.GetFileName(filePath));

            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }
    }
}
