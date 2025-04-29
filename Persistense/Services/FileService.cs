using Museum.Application.Interfaces;
using Museum.Application.Common.Exceptions;

namespace Museum.Persistense.Services
{
    public class FileService : IFileService
    {
        private readonly string _uploadsPath;

        public FileService(string uploadPath)
        {
            _uploadsPath = uploadPath;
            Directory.CreateDirectory(_uploadsPath);
        }

        public async Task<string> UploadFileAsync(Stream fileStream, string fileName,
            CancellationToken cancellationToken)
        {
            if(string.IsNullOrWhiteSpace(fileName))
                throw new ArgumentNullException(nameof(fileName));

            var uniqueFileName = $"{Guid.NewGuid()}_{fileName}";
            var filePath = Path.Combine(_uploadsPath, uniqueFileName).TrimStart('/');

            try
            {
                if (fileStream == null || fileStream.Length == 0)
                    throw new ArgumentException("File stream is empty");

                using (var outputStream = new FileStream(filePath, FileMode.Create))
                {
                    await fileStream.CopyToAsync(outputStream, cancellationToken);
                }
                return filePath;
            } 
            catch (Exception ex)
            {
                throw new FileUploadException(filePath, ex);
            }
        }

        public async Task DeleteFileAsync(string filePath, CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(filePath))
                    throw new ArgumentNullException(nameof(filePath));

                var fullPath = Path.Combine(_uploadsPath, Path.GetFileName(filePath));

                if (File.Exists(fullPath))
                {
                    await Task.Run(() => File.Delete(fullPath), cancellationToken);
                }
            } 
            catch (Exception ex)
            {
                throw new FileDeleteException(filePath, ex);
            }
        }

        public async Task DeleteFileRangeAsync(IEnumerable<string> filePaths, CancellationToken cancellationToken)
        {
            foreach(var filePath in filePaths)
            {
                try 
                {
                    if (string.IsNullOrWhiteSpace(filePath))
                        throw new ArgumentNullException(nameof(filePath));

                    var fullPath = Path.Combine(_uploadsPath, Path.GetFileName(filePath));

                    if (File.Exists(fullPath))
                    {
                        await Task.Run(() => File.Delete(fullPath), cancellationToken);
                    }
                } catch (Exception ex)
                {
                    throw new FileDeleteException(filePath, ex);
                }
            }
        }
    }
}
