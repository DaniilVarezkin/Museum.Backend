using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Museum.Application.Interfaces
{
    public interface IFileService
    {
        Task<string> UploadFileAsync(Stream fileStream, string fileName, CancellationToken cancellationToken);

        Task DeleteFileAsync(string filePath, CancellationToken cancellationToken);

        Task DeleteFileRangeAsync(IEnumerable<string> filePaths, CancellationToken cancellationToken);
    }
}
