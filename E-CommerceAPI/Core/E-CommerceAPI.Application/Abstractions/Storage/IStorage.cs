using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Application.Abstractions.Storage
{
    public interface IStorage
    {
        Task<List<(string fileName, string pathOrConatinerName)>> UploadAsync(string pathOrConatinerName, IFormFileCollection files);
        Task DeleteAsync(string pathOrConatinerName, string fileName);
        List<string> GetFiles(string pathOrConatinerName);
        bool HasFile(string pathOrConatinerName, string fileName);
    }
}
