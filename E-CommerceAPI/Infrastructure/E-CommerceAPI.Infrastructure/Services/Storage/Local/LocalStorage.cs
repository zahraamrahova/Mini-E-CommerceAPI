using E_CommerceAPI.Application.Abstractions.Storage;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Infrastructure.Services.Storage.Local
{
    public class LocalStorage : IStorage
    {
        readonly private IWebHostEnvironment _webHostEnvironment;
        public LocalStorage(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task DeleteAsync(string pathOrConatinerName, string fileName)
        => File.Delete($"{pathOrConatinerName}\\{fileName}");

        public List<string> GetFiles(string pathOrConatinerName)
        {
            DirectoryInfo directory = new DirectoryInfo(pathOrConatinerName);
            return directory.GetFiles().Select(f => f.Name).ToList();
        }

        public bool HasFile(string pathOrConatinerName, string fileName)=>
            File.Exists($"{pathOrConatinerName}\\{fileName}");


        public async Task<List<(string fileName, string pathOrConatinerName)>> UploadAsync(string pathOrConatinerName, IFormFileCollection files)
        {
            string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, pathOrConatinerName);
            if (!Directory.Exists(uploadPath))
            { Directory.CreateDirectory(uploadPath); }
            List<(string fileName, string path)> datas = new List<(string fileName, string path)>();
            foreach (IFormFile file in files)
            {
                //string fileNewName = await FileRenameAsync(uploadPath, file.FileName);
                await CopyFileAsync($"{uploadPath}\\{file.Name}", file);
                datas.Add((file.Name, $"{pathOrConatinerName}\\{file.Name}"));
            }
      
            return datas;
            //todo please throw exception
        }
         async Task<bool> CopyFileAsync(string path, IFormFile file)
        {
            try
            {
                await using FileStream fileStream = new(path, FileMode.Create, FileAccess.Write, FileShare.None, 1024 * 1024, useAsync: false);
                await file.CopyToAsync(fileStream);
                await fileStream.FlushAsync();
                return true;
            }
            catch (Exception ex)
            {
                //todo log!;
                throw ex;
            }
        }
    }
}
