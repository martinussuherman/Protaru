using MonevAtr.Models;

namespace Protaru.Components.Rtr
{
    public partial class DetailTitle
    {
        private string DisplayTitle()
        {
            if (Rtr.KodeJenisAtr == (int)JenisRtrEnum.RtrPulauT51 ||
                Rtr.KodeJenisAtr == (int)JenisRtrEnum.RtrPulauT52)
            {
                return Rtr.DisplayNamaPulau;
            }

            if (Rtr.KodeJenisAtr == (int)JenisRtrEnum.RtrKpnT51 ||
                Rtr.KodeJenisAtr == (int)JenisRtrEnum.RtrKpnT52 ||
                Rtr.KodeJenisAtr == (int)JenisRtrEnum.RtrKsnT51 ||
                Rtr.KodeJenisAtr == (int)JenisRtrEnum.RtrKsnT52)
            {
                return Rtr.DisplayNamaKawasan;
            }

            return Rtr.DisplayNamaProvinsi;
        }
    }
}