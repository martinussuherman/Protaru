using MonevAtr.Models;

namespace Protaru.Models
{
    public enum JenisKegiatanEnum
    {
        None = JenisRtrEnum.None,
        RdtrT51Create = JenisRtrEnum.RdtrT51,
        RdtrT52Create = JenisRtrEnum.RdtrT52,
        RtrwT51Create = JenisRtrEnum.RtrwT51,
        RtrwT52Create = JenisRtrEnum.RtrwT52,
        RtrwT50Create = JenisRtrEnum.RtrwT50,
        RtrPulauT51Create = JenisRtrEnum.RtrPulauT51,
        RtrPulauT52Create = JenisRtrEnum.RtrPulauT52,
        RtrKsnT51Create = JenisRtrEnum.RtrKsnT51,
        RtrKsnT52Create = JenisRtrEnum.RtrKsnT52,
        RtrwnT51Create = JenisRtrEnum.RtrwnT51,
        RtrwnT52Create = JenisRtrEnum.RtrwnT52,
        RtrKpnT51Create = JenisRtrEnum.RtrKpnT51,
        RtrKpnT52Create = JenisRtrEnum.RtrKpnT52,
        RdtrT51Edit = RdtrT51Create + 1000,
        RdtrT52Edit = RdtrT52Create + 1000,
        RtrwT51Edit = RtrwT51Create + 1000,
        RtrwT52Edit = RtrwT52Create + 1000,
        RtrwT50Edit = RtrwT50Create + 1000,
        RtrPulauT51Edit = RtrPulauT51Create + 1000,
        RtrPulauT52Edit = RtrPulauT52Create + 1000,
        RtrKsnT51Edit = RtrKsnT51Create + 1000,
        RtrKsnT52Edit = RtrKsnT52Create + 1000,
        RtrwnT51Edit = RtrwnT51Create + 1000,
        RtrwnT52Edit = RtrwnT52Create + 1000,
        RtrKpnT51Edit = RtrKpnT51Create + 1000,
        RtrKpnT52Edit = RtrKpnT52Create + 1000,
    }
}