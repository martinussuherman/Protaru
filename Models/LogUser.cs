using System;

namespace Protaru.Models
{
    public partial class LogUser
    {
        public uint Id { get; set; }
        public string User { get; set; }
        public ushort JenisKegiatan { get; set; }
        public DateTime Waktu { get; set; }
    }
}
