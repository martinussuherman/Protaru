using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Itm.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace MonevAtr.Models
{
    public class SelectListUtilities
    {
        public SelectListUtilities(PomeloDbContext context)
        {
            _context = context;
        }

        public SelectList EmptyKabupatenKota
        {
            get
            {
                IList<KabupatenKota> list = new List<KabupatenKota>();
                InsertPilihKabupatenKota(list);
                return new SelectList(list, "Kode", "Nama");
            }
        }

        public SelectList StatusRevisiRtrRegular =>
            new SelectList(StatusRegularItems(), _valueProperty, _textProperty);

        public SelectList StatusRevisiRtrRevisi =>
            new SelectList(StatusRevisiItems(), _valueProperty, _textProperty);

        public IEnumerable<SelectListItem> StatusRegularItems()
        {
            return _regularItems.Prepend(_regularTitle);
        }
        public IEnumerable<SelectListItem> StatusRevisiItems()
        {
            return _revisiItems.Prepend(_revisiTitle);
        }

        public IEnumerable<SelectListItem> InputTahunRequired(int value = 0)
        {
            return InputTahun(value).Prepend(_yearInputTitleRequired);
        }

        public IEnumerable<SelectListItem> InputTahunOptional(int value = 0)
        {
            return InputTahun(value).Prepend(_yearInputTitleOptional);
        }

        public async Task<IEnumerable<SelectListItem>> InputProvinsiAsync()
        {
            var list = await _context.Provinsi
                .Where(p => p.Kode > 0)
                .OrderBy(p => p.Nama)
                .AsNoTracking()
                .Select(c => new SelectListItem
                {
                    Value = c.Kode.ToString(),
                    Text = c.Nama
                })
                .ToListAsync();

            return list.Prepend(_provinsiTitleRequired);
        }

        public IEnumerable<SelectListItem> InputKabupatenKota()
        {
            return new List<SelectListItem>
            {
                _kabupatenKotaTitleRequired
            };
        }

        public async Task<SelectList> Users(IdentityDbContext context)
        {
            var list = await context.Users
                .Select(e => e.UserName)
                .OrderBy(e => e)
                .AsNoTracking()
                .ToListAsync();

            return new SelectList(list);
        }

        public async Task<SelectList> UserRoles(IdentityDbContext context)
        {
            List<ApplicationRole> list = await context.Roles
                .OrderBy(e => e.Name)
                .AsNoTracking()
                .ToListAsync();

            return new SelectList(list, "Name", "Name");
        }

        public async Task<SelectList> Provinsi()
        {
            IList<Provinsi> list = await _context.Provinsi
                .Where(p => p.Kode > 0)
                .OrderBy(p => p.Nama)
                .AsNoTracking()
                .ToListAsync();

            Provinsi pilih = new Provinsi
            {
                Kode = 0,
                Nama = "Pilih Provinsi"
            };

            list.Insert(0, pilih);
            return new SelectList(list, "Kode", "Nama");
        }

        public async Task<SelectList> KabupatenKota()
        {
            IList<KabupatenKota> list = await _context.KabupatenKota
                .OrderBy(k => k.Nama)
                .AsNoTracking()
                .ToListAsync();

            InsertPilihKabupatenKota(list);
            return new SelectList(list, "Kode", "Nama");
        }

        public async Task<SelectList> KabupatenKota(int kodeProvinsi)
        {
            IList<KabupatenKota> list = await _context.KabupatenKota
                .Where(k => k.KodeProvinsi == kodeProvinsi)
                .OrderBy(k => k.Nama)
                .AsNoTracking()
                .ToListAsync();

            InsertPilihKabupatenKota(list);
            return new SelectList(list, "Kode", "Nama");
        }

        public async Task<SelectList> Pulau()
        {
            IList<Pulau> list = await _context.Pulau
                .Where(p => p.Kode > 0)
                .OrderBy(p => p.Nama)
                .AsNoTracking()
                .ToListAsync();

            Pulau pilih = new Pulau
            {
                Kode = 0,
                Nama = "Pilih Pulau"
            };

            list.Insert(0, pilih);
            return new SelectList(list, "Kode", "Nama");
        }

        public async Task<SelectList> Kawasan()
        {
            List<Kawasan> list = await _context.Kawasan
                .Where(q => q.Kode > 0)
                .OrderBy(q => q.Nama)
                .AsNoTracking()
                .ToListAsync();

            await UpdateNamaKawasan(list);

            Kawasan pilih = new Kawasan
            {
                Kode = 0,
                Nama = "Pilih Kawasan"
            };

            list.Insert(0, pilih);
            return new SelectList(list, "Kode", "Nama");
        }

        public async Task<List<int>> TahunRekomendasiGubernurListAsync()
        {
            return await _context.AtrDokumen
                .Where(a =>
                    a.KodeDokumen == 24 ||
                    a.KodeDokumen == 58 ||
                    a.KodeDokumen == 97 ||
                    a.KodeDokumen == 132 ||
                    a.KodeDokumen == 171)
                .AsNoTracking()
                .Select(a => a.Tanggal.Year)
                .Distinct()
                .OrderBy(a => a)
                .ToListAsync();
        }

        public async Task<List<int>> TahunPermohonanPersetujuanSubstansiListAsync()
        {
            return await _context.AtrDokumen
                .Where(a =>
                    a.KodeDokumen == 25 ||
                    a.KodeDokumen == 59 ||
                    a.KodeDokumen == 98 ||
                    a.KodeDokumen == 133 ||
                    a.KodeDokumen == 172)
                .AsNoTracking()
                .Select(a => a.Tanggal.Year)
                .Distinct()
                .OrderBy(a => a)
                .ToListAsync();
        }

        public async Task<List<int>> TahunMasukLoketListAsync()
        {
            return await _context.AtrDokumen
                .Where(a =>
                    a.KodeDokumen == 29 ||
                    a.KodeDokumen == 63 ||
                    a.KodeDokumen == 99 ||
                    a.KodeDokumen == 137 ||
                    a.KodeDokumen == 176)
                .AsNoTracking()
                .Select(a => a.Tanggal.Year)
                .Distinct()
                .OrderBy(a => a)
                .ToListAsync();
        }

        public async Task<List<int>> TahunRapatLintasSektorListAsync()
        {
            return await _context.AtrDokumen
                .Where(a =>
                    a.KodeDokumen == 30 ||
                    a.KodeDokumen == 64 ||
                    a.KodeDokumen == 100 ||
                    a.KodeDokumen == 138 ||
                    a.KodeDokumen == 177)
                .AsNoTracking()
                .Select(a => a.Tanggal.Year)
                .Distinct()
                .OrderBy(a => a)
                .ToListAsync();
        }

        public async Task<List<int>> TahunPersetujuanSubstansiListAsync()
        {
            return await _context.AtrDokumen
                .Where(a =>
                    a.KodeDokumen == 31 ||
                    a.KodeDokumen == 65 ||
                    a.KodeDokumen == 101 ||
                    a.KodeDokumen == 139 ||
                    a.KodeDokumen == 178)
                .AsNoTracking()
                .Select(a => a.Tanggal.Year)
                .Distinct()
                .OrderBy(a => a)
                .ToListAsync();
        }

        public async Task<SelectList> TahunRapatLintasSektorDanPersetujuanSubstansi()
        {
            List<int> list = await _context.AtrDokumen
                .Where(
                    a => a.KodeDokumen == 30 ||
                    a.KodeDokumen == 64 ||
                    a.KodeDokumen == 100 ||
                    a.KodeDokumen == 31 ||
                    a.KodeDokumen == 65 ||
                    a.KodeDokumen == 101)
                .AsNoTracking()
                .Select(a => a.Tanggal.Year)
                .Distinct()
                .OrderBy(a => a)
                .ToListAsync();

            return Tahun(list, "Pilih Tahun");
        }

        public async Task<List<int>> TahunPerdaAsync(JenisRtrEnum jenis)
        {
            return (await _context.Atr
                .ByJenis(jenis)
                .AsNoTracking()
                .Select(a => a.Tahun)
                .Distinct()
                .OrderBy(a => a)
                .ToListAsync())
                .ConvertAll(x => (int)x);
        }

        public async Task<List<int>> TahunPerpresAsync(JenisRtrEnum jenis)
        {
            return (await _context.Atr
                .ByJenis(jenis)
                .AsNoTracking()
                .Select(a => a.Tahun)
                .Distinct()
                .OrderBy(a => a)
                .ToListAsync())
                .ConvertAll(x => (int)x);
        }


        public async Task<SelectList> TahunPerdaRdtrT51()
        {
            return await TahunPerda(JenisRtrEnum.RdtrT51);
        }

        public async Task<SelectList> TahunPerdaRdtrT52()
        {
            return await TahunPerda(JenisRtrEnum.RdtrT52);
        }

        public async Task<SelectList> TahunPerdaRtrwT50()
        {
            return await TahunPerda(JenisRtrEnum.RtrwT50);
        }

        public async Task<SelectList> TahunPerdaRtrwT51()
        {
            return await TahunPerda(JenisRtrEnum.RtrwT51);
        }

        public async Task<SelectList> TahunPerdaRtrwT52()
        {
            return await TahunPerda(JenisRtrEnum.RtrwT52);
        }

        public async Task<SelectList> TahunPerpresRtrPulauT51()
        {
            return await TahunPerpres(JenisRtrEnum.RtrPulauT51);
        }

        public async Task<SelectList> TahunPerpresRtrPulauT52()
        {
            return await TahunPerpres(JenisRtrEnum.RtrPulauT52);
        }

        public async Task<SelectList> TahunPerpresRtrKsnT51()
        {
            return await TahunPerpres(JenisRtrEnum.RtrKsnT51);
        }

        public async Task<SelectList> TahunPerpresRtrKsnT52()
        {
            return await TahunPerpres(JenisRtrEnum.RtrKsnT52);
        }

        public async Task<SelectList> TahunPerpresRtrwnT51()
        {
            return await TahunPerpres(JenisRtrEnum.RtrwnT51);
        }

        public async Task<SelectList> TahunPerpresRtrwnT52()
        {
            return await TahunPerpres(JenisRtrEnum.RtrwnT52);
        }

        public async Task<SelectList> TahunPerpresRtrKpnT51()
        {
            return await TahunPerpres(JenisRtrEnum.RtrKpnT51);
        }

        public async Task<SelectList> TahunPerpresRtrKpnT52()
        {
            return await TahunPerpres(JenisRtrEnum.RtrKpnT52);
        }

        public async Task<SelectList> TahunPerdaRtr()
        {
            return await TahunPerda(JenisRtrEnum.All);
        }

        public async Task<SelectList> KelompokDokumen()
        {
            IList<KelompokDokumen> list = await _context.KelompokDokumen
                .Include(k => k.JenisAtr)
                .OrderBy(k => k.KodeJenisAtr)
                .ThenBy(k => k.Nomor)
                .AsNoTracking()
                .ToListAsync();

            KelompokDokumen pilih = new KelompokDokumen
            {
                Kode = 0,
                Nama = "Pilih Kelompok Dokumen"
            };

            list.Insert(0, pilih);
            return new SelectList(list, "Kode", "DisplayNamaJenisAtr");
        }

        public async Task<SelectList> Dokumen()
        {
            List<Dokumen> list = await _context.Dokumen
                .Include(p => p.KelompokDokumen)
                .Include(p => p.KelompokDokumen.JenisAtr)
                .OrderBy(p => p.KelompokDokumen.KodeJenisAtr)
                .ThenBy(p => p.KelompokDokumen.Nomor)
                .ThenBy(p => p.Nomor)
                .AsNoTracking()
                .ToListAsync();

            list.ForEach(UpdateNamaDokumen);

            Dokumen pilih = new Dokumen
            {
                Kode = 0,
                Nama = "Pilih Dokumen"
            };

            list.Insert(0, pilih);
            return new SelectList(list, "Kode", "Nama");
        }

        public async Task<SelectList> JenisRtr()
        {
            IList<JenisAtr> list = await _context.JenisAtr
                .AsNoTracking()
                .ToListAsync();

            JenisAtr pilih = new JenisAtr
            {
                Kode = 0,
                Nama = "Pilih Jenis RTR"
            };

            list.Insert(0, pilih);
            return new SelectList(list, "Kode", "Nama");
        }

        public async Task<SelectList> ProgressRdtrT51()
        {
            return new SelectList(await InputProgressRdtrT51Async(), _valueProperty, _textProperty);
        }
        public async Task<IEnumerable<SelectListItem>> InputProgressRdtrT51Async()
        {
            IEnumerable<SelectListItem> temp = (await ProgressRtr(JenisRtrEnum.RdtrT51))
                .Select(c => new SelectListItem
                {
                    Value = c.Kode.ToString(),
                    Text = c.Nama
                });

            return temp.Prepend(_rdtrT51ProgressTitle);
        }

        public async Task<SelectList> ProgressRdtrT52()
        {
            return new SelectList(await InputProgressRdtrT52Async(), _valueProperty, _textProperty);
        }
        public async Task<IEnumerable<SelectListItem>> InputProgressRdtrT52Async()
        {
            IEnumerable<SelectListItem> temp = (await ProgressRtr(JenisRtrEnum.RdtrT52))
                .Select(c => new SelectListItem
                {
                    Value = c.Kode.ToString(),
                    Text = c.Nama
                });

            return temp.Prepend(_rdtrT52ProgressTitle);
        }

        public async Task<SelectList> ProgressRtrwT50()
        {
            IList<ProgressAtr> list = await ProgressRtr(
                JenisRtrEnum.RtrwT50);

            list.Insert(0, new ProgressAtr(0, "Pilih Progress RTRW T5-0"));
            return new SelectList(list, "Kode", "Nama");
        }

        public async Task<SelectList> ProgressRtrwT51()
        {
            IList<ProgressAtr> list = await ProgressRtr(
                JenisRtrEnum.RtrwT51);

            list.Insert(0, new ProgressAtr(0, "Pilih Progress RTRW T5-1"));
            return new SelectList(list, "Kode", "Nama");
        }

        public async Task<SelectList> ProgressRtrwT52()
        {
            IList<ProgressAtr> list = await ProgressRtr(
                JenisRtrEnum.RtrwT52);

            list.Insert(0, new ProgressAtr(0, "Pilih Progress RTRW T5-2"));
            return new SelectList(list, "Kode", "Nama");
        }

        public async Task<SelectList> ProgressRtrPulauT51()
        {
            IList<ProgressAtr> list = await ProgressRtr(
                JenisRtrEnum.RtrPulauT51);

            list.Insert(0, new ProgressAtr(0, "Pilih Progress RTR Pulau/Kepulauan T5-1"));
            return new SelectList(list, "Kode", "Nama");
        }

        public async Task<SelectList> ProgressRtrPulauT52()
        {
            IList<ProgressAtr> list = await ProgressRtr(
                JenisRtrEnum.RtrPulauT52);

            list.Insert(0, new ProgressAtr(0, "Pilih Progress RTR Pulau/Kepulauan T5-2"));
            return new SelectList(list, "Kode", "Nama");
        }

        public async Task<SelectList> ProgressRtrKsnT51()
        {
            IList<ProgressAtr> list = await ProgressRtr(
                JenisRtrEnum.RtrKsnT51);

            list.Insert(0, new ProgressAtr(0, "Pilih Progress RTR KSN T5-1"));
            return new SelectList(list, "Kode", "Nama");
        }

        public async Task<SelectList> ProgressRtrKsnT52()
        {
            IList<ProgressAtr> list = await ProgressRtr(
                JenisRtrEnum.RtrKsnT52);

            list.Insert(0, new ProgressAtr(0, "Pilih Progress RTR KSN T5-2"));
            return new SelectList(list, "Kode", "Nama");
        }

        public async Task<SelectList> ProgressRtrwnT51()
        {
            IList<ProgressAtr> list = await ProgressRtr(
                JenisRtrEnum.RtrwnT51);

            list.Insert(0, new ProgressAtr(0, "Pilih Progress RTRWN T5-1"));
            return new SelectList(list, "Kode", "Nama");
        }

        public async Task<SelectList> ProgressRtrwnT52()
        {
            IList<ProgressAtr> list = await ProgressRtr(
                JenisRtrEnum.RtrwnT52);

            list.Insert(0, new ProgressAtr(0, "Pilih Progress RTRWN T5-2"));
            return new SelectList(list, "Kode", "Nama");
        }

        public async Task<SelectList> ProgressRtrKpnT51()
        {
            IList<ProgressAtr> list = await ProgressRtr(
                JenisRtrEnum.RtrKpnT51);

            list.Insert(0, new ProgressAtr(0, "Pilih Progress RTR KPN T5-1"));
            return new SelectList(list, "Kode", "Nama");
        }

        public async Task<SelectList> ProgressRtrKpnT52()
        {
            IList<ProgressAtr> list = await ProgressRtr(
                JenisRtrEnum.RtrKpnT52);

            list.Insert(0, new ProgressAtr(0, "Pilih Progress RTR KPN T5-2"));
            return new SelectList(list, "Kode", "Nama");
        }

        private async Task<IList<ProgressAtr>> ProgressRtr(JenisRtrEnum jenisRtr)
        {
            return await _context.ProgressAtr
                .Where(p => p.KodeJenisAtr == (int)jenisRtr)
                .OrderBy(p => p.Nomor)
                .AsNoTracking()
                .ToListAsync();
        }

        private async Task<SelectList> TahunPerda(JenisRtrEnum jenis)
        {
            return Tahun(
                await TahunPerdaAsync(jenis),
                "Pilih Tahun Perda");
        }

        private async Task<SelectList> TahunPerpres(JenisRtrEnum jenis)
        {
            return Tahun(
                await TahunPerpresAsync(jenis),
                "Pilih Tahun Perpres");
        }

        private SelectList Tahun(List<int> listSumber, string pilih)
        {
            List<Tahun> list = new List<Tahun>
            {
                new Tahun(0, pilih)
            };

            AddTahunToListHasil(listSumber, list);
            return new SelectList(list, "Value", "Text");
        }

        private void AddTahunToListHasil(List<int> listSumber, List<Tahun> listHasil)
        {
            foreach (int tahun in listSumber)
            {
                listHasil.Add(new Tahun(tahun));
            }
        }

        private void InsertPilihKabupatenKota(IList<KabupatenKota> list)
        {
            KabupatenKota pilih = new KabupatenKota
            {
                Kode = 0,
                Nama = "Pilih Kabupaten/Kota"
            };

            list.Insert(0, pilih);
        }

        private void UpdateNamaDokumen(Dokumen dokumen)
        {
            dokumen.Nama = dokumen.Nama + " - " + dokumen.KelompokDokumen.JenisAtr.Nama;
        }

        private async Task UpdateNamaKawasan(List<Kawasan> list)
        {
            List<KawasanProvinsi> listProvinsi = await _context.KawasanProvinsi
                .Include(q => q.Provinsi)
                .OrderBy(q => q.KodeKawasan)
                .ThenBy(q => q.Provinsi.Nama)
                .AsNoTracking()
                .ToListAsync();

            StringBuilder builder = new StringBuilder();

            foreach (Kawasan kawasan in list)
            {
                builder.Clear();
                builder.Append(kawasan.Nama);
                builder.Append(" - ");

                List<KawasanProvinsi> match = listProvinsi.FindAll(q => q.KodeKawasan == kawasan.Kode);

                for (int index = 0; index < match.Count; index++)
                {
                    if (index > 0)
                    {
                        builder.Append(", ");
                    }

                    builder.Append(match[index].Provinsi.Nama);
                }

                kawasan.Nama = builder.ToString();
            }
        }

        private IEnumerable<SelectListItem> InputTahun(int value)
        {
            return Enumerable
                .Range(2000, DateTime.Today.Year + 2 - 2000)
                .Select(c => new SelectListItem
                {
                    Value = c.ToString(),
                    Text = c.ToString(),
                    Selected = c == value
                });
        }
        private static readonly List<StatusRevisi> _regular = new List<StatusRevisi>
        {
            StatusRevisi.RegularT51,
            StatusRevisi.RegularT52
        };
        private static readonly List<StatusRevisi> _revisi = new List<StatusRevisi>
        {
            StatusRevisi.RevisiT52,
            StatusRevisi.RevisiT53,
            StatusRevisi.RevisiT54
        };
        private static readonly IEnumerable<SelectListItem> _regularItems = _regular
            .Select(c => new SelectListItem
            {
                Value = c.Kode.ToString(),
                Text = c.Nama
            });
        private static readonly IEnumerable<SelectListItem> _revisiItems = _revisi
            .Select(c => new SelectListItem
            {
                Value = c.Kode.ToString(),
                Text = c.Nama
            });
        private static readonly SelectListItem _regularTitle =
            new SelectListItem("Pilih Status RTR T5-0/T5-1", string.Empty);
        private static readonly SelectListItem _revisiTitle =
            new SelectListItem("Pilih Status RTR T5-2", string.Empty);
        private static readonly SelectListItem _rdtrT51ProgressTitle =
            new SelectListItem("Pilih Progress RDTR T5-1", string.Empty);
        private static readonly SelectListItem _rdtrT52ProgressTitle =
            new SelectListItem("Pilih Progress RDTR T5-2", string.Empty);
        private static readonly SelectListItem _provinsiTitleRequired =
            new SelectListItem("Pilih Provinsi", string.Empty);
        private static readonly SelectListItem _provinsiTitleOptional =
            new SelectListItem("Pilih Provinsi", "0");
        private static readonly SelectListItem _kabupatenKotaTitleRequired =
            new SelectListItem("Pilih Kabupaten/Kota", string.Empty);
        private static readonly SelectListItem _kabupatenKotaTitleOptional =
            new SelectListItem("Pilih Kabupaten/Kota", "0");
        private static readonly SelectListItem _yearInputTitleRequired =
            new SelectListItem("Pilih Tahun", string.Empty);
        private static readonly SelectListItem _yearInputTitleOptional =
            new SelectListItem("Pilih Tahun", "0");

        private readonly PomeloDbContext _context;
        private const string _textProperty = "Text";
        private const string _valueProperty = "Value";
    }
}