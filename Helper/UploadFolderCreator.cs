using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using MonevAtr.Models;

namespace MonevAtr
{
    public class UploadFolderCreator
    {
        public UploadFolderCreator(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public void CreateUploadFolders()
        {
            foreach (string namaRtr in Enum.GetNames(typeof(JenisRtrEnum)))
            {
                string path = Path.Combine(
                    _environment.WebRootPath,
                    "upload",
                    namaRtr);

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
            }
        }

        private readonly IWebHostEnvironment _environment;
    }
}