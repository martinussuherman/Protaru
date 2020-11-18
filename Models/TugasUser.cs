using System;

namespace Protaru.Models
{
    public partial class TugasUser
    {
        public uint Id { get; set; }
        public string User { get; set; }
        public byte Jumlah { get; set; }
        public DateTime BatasWaktu { get; set; }
    }
}
