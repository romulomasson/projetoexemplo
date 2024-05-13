using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exemplo.Domain.Interfaces.Services
{
    public interface IBlobStorageService
    {
        void InitBlob(string blobConnectionString, string containerName);
        string UploadFileToBlobSync(string directoryName, string strFileName, byte[] fileData, string fileMimeType);
        string UploadFileToBlobWithContainerSync(string containerName, string directoryName, string strFileName, byte[] fileData, string fileMimeType);
        void DeleteBlobDataSync(string directory, string fileUrl);
        Task<string> UploadFileToBlobAsync(string directoryName, string strFileName, byte[] fileData, string fileMimeType);
        Task<Stream> GetFile(string urlFile);
    }
}


