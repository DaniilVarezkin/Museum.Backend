using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Museum.Application.Common.Exceptions
{
    public class FileUploadException : Exception
    {
        public FileUploadException(string filePath)
            : base($"Failed to upload file: \"{filePath}\"") { }
    }
}
