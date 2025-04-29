using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Museum.Application.Common.Exceptions
{
    public class FileDeleteException : Exception
    {
        public FileDeleteException(string filePath, Exception ex)
            : base($"Error deleting a file on the path \"{filePath}\"", ex)
        {
            
        }
    }
}
