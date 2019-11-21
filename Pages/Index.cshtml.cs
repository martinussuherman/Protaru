using System;
using Protaru.Areas.Page;

namespace MonevAtr.Pages
{
    public class IndexModel : ProtaruPageModel
    {
        public void OnGet()
        {
            Title = "Kementerian Agraria dan Tata Ruang";
            ActiveMenu = ActiveMenu.Home;
        }
    }
}