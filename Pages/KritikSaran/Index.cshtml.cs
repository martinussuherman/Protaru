using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MonevAtr.Models;
using PaulMiami.AspNetCore.Mvc.Recaptcha;

namespace MonevAtr.Pages.KritikSaran
{
    [ValidateRecaptcha]
    public class IndexModel : PageModel
    {
        public IndexModel(PomeloDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ViewModel Input { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Protaru.Models.Saran save = new Protaru.Models.Saran
            {
                Email = Input.Email,
                Nama = Input.Nama,
                Isi = Input.Isi
            };

            await _context.Saran.AddAsync(save);
            await _context.SaveChangesAsync();
            return RedirectToPage("/Index");
        }

        public class ViewModel
        {
            [Required(ErrorMessage = "Email harus diisi.")]
            [EmailAddress(ErrorMessage = "Isian harus berupa alamat email.")]
            public string Email { get; set; }

            [Required(ErrorMessage = "Nama harus diisi.")]
            [DataType(DataType.Text)]
            public string Nama { get; set; }

            [Required(ErrorMessage = "Kritik & saran harus diisi.")]
            [MaxLength(30000, ErrorMessage = "Kritik & saran maksimum {1} karakter.")]
            [DataType(DataType.Text)]
            public string Isi { get; set; }
        }

        private readonly PomeloDbContext _context;
    }
}