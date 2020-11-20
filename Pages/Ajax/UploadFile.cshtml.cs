using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MonevAtr.Pages.Ajax
{
    [Authorize]
    [RequestFormLimits(MultipartBodyLengthLimit = long.MaxValue)]
    [RequestSizeLimit(long.MaxValue)]
    public class UploadFileModel : PageModel
    {
        public UploadFileModel(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        [BindProperty]
        public string NamaRtr { get; set; }

        [BindProperty]
        public IFormFile UploadFile { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (await CopyUploadedFile())
            {
                string path = $"/upload/{NamaRtr}/{UploadFile.FileName}";
                return new JsonResult(path);
            }

            return NotFound();
        }

        private async Task<bool> CopyUploadedFile()
        {
            if (UploadFile == null ||
                String.IsNullOrEmpty(UploadFile.FileName) ||
                UploadFile.Length == 0)
            {
                return false;
            }

            string filePath = Path.Combine(
                _environment.WebRootPath,
                "upload",
                NamaRtr,
                UploadFile.FileName);

            using (FileStream stream = new FileStream(filePath, FileMode.Create))
            {
                await UploadFile.CopyToAsync(stream);
            }

            return true;
        }

        private readonly IWebHostEnvironment _environment;
    }
}