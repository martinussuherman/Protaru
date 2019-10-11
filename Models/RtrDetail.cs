using System.Collections.Generic;

namespace MonevAtr.Models
{
    public class RtrDetail
    {
        public Atr Rtr { get; set; }

        public List<KelompokDokumen> KelompokDokumenList { get; set; }

        public List<AtrDokumen> RtrDokumenList { get; set; }
    }
}