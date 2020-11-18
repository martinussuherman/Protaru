using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Protaru.Models
{
    public partial class TugasUser
    {
        [NotMapped]
        public string DisplayBatasWaktu =>
            BatasWaktu == DateTime.MinValue ?
            string.Empty :
            BatasWaktu.ToString("yyyy-MM-dd");

        [NotMapped]
        public string DisplayBatasWaktuForView =>
            BatasWaktu == DateTime.MinValue ?
            string.Empty :
            BatasWaktu.ToString("dd-MM-yyyy");

    }
}