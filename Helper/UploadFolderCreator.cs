using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using MonevAtr.Models;

namespace MonevAtr
{
    public class UploadFolderCreator
    {
        public UploadFolderCreator(IHostingEnvironment environment)
        {
            hostingEnvironment = environment;
        }

        public void CreateUploadFolders()
        {
            foreach (string namaRtr in Enum.GetNames(typeof(JenisRtrEnum)))
            {
                string path = Path.Combine(
                    hostingEnvironment.WebRootPath,
                    "upload",
                    namaRtr);

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
            }
        }

        private readonly IHostingEnvironment hostingEnvironment;
    }
}