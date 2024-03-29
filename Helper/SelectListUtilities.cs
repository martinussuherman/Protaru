using System;
using System.Collections.Generic;
using System.Linq;
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

        public IEnumerable<SelectListItem> StatusRegularItems(int? value = 0)
        {
            int val = value == null ? 0 : value.Value;
            IEnumerable<SelectListItem> temp = _regular
                .Select(c => new SelectListItem
                {
                    Value = c.Kode.ToString(),
                    Text = c.Nama,
                    Selected = c.Kode == val
                });

            return temp.Prepend(_regularTitle);
        }

        public IEnumerable<SelectListItem> StatusRevisiItems(int? value = 0)
        {
            int val = value == null ? 0 : value.Value;
            IEnumerable<SelectListItem> temp = _revisi
                .Select(c => new SelectListItem
                {
                    Value = c.Kode.ToString(),
                    Text = c.Nama,
                    Selected = c.Kode == val
                });

            return temp.Prepend(_revisiTitle);
        }

        public IEnumerable<SelectListItem> InputTahunRequired(int value = 0)
        {
            return InputTahun(value).Prepend(_yearInputTitleRequired);
        }

        public IEnumerable<SelectListItem> InputTahunOptional(int value = 0)
        {
            return InputTahun(value).Prepend(_yearInputTitleOptional);
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

        public async Task<IEnumerable<SelectListItem>> ProvinsiAsync()
        {
            List<SelectListItem> list = await ProvinsiAsyncInternal();

            return list.Prepend(_provinsiTitle);
        }
        public async Task<IEnumerable<SelectListItem>> ProvinsiOptionalAsync()
        {
            List<SelectListItem> list = await ProvinsiAsyncInternal();

            return list.Prepend(_provinsiTitleOptional);
        }
        private async Task<List<SelectListItem>> ProvinsiAsyncInternal()
        {
            return await _context.Provinsi
                .Where(p => p.Kode > 0)
                .OrderBy(p => p.Nama)
                .AsNoTracking()
                .Select(c => new SelectListItem
                {
                    Value = c.Kode.ToString(),
                    Text = c.Nama
                })
                .ToListAsync();
        }
        public async Task<SelectList> Provinsi()
        {
            List<SelectListItem> list = await ProvinsiAsyncInternal();
            return new SelectList(list.Prepend(_provinsiTitleOptional), _valueProperty, _textProperty);
        }

        public async Task<IEnumerable<SelectListItem>> KabupatenKotaAsync(int kodeProvinsi = -1)
        {
            if (kodeProvinsi == -1)
            {
                return new List<SelectListItem>
                {
                    _kabupatenKotaTitleOptional
                };
            }

            List<SelectListItem> list = await _context.KabupatenKota
                .Where(k => k.KodeProvinsi == kodeProvinsi)
                .OrderBy(k => k.Nama)
                .AsNoTracking()
                .Select(c => new SelectListItem
                {
                    Value = c.Kode.ToString(),
                    Text = c.Nama
                })
                .ToListAsync();

            return list.Prepend(_kabupatenKotaTitleOptional);

        }
        public async Task<SelectList> KabupatenKota(int kodeProvinsi)
        {
            return new SelectList(await KabupatenKotaAsync(kodeProvinsi), _valueProperty, _textProperty);
        }

        public async Task<IEnumerable<SelectListItem>> PulauAsync()
        {
            List<SelectListItem> list = await _context.Pulau
                .Where(p => p.Kode > 0)
                .OrderBy(p => p.Nama)
                .AsNoTracking()
                .Select(c => new SelectListItem
                {
                    Value = c.Kode.ToString(),
                    Text = c.Nama
                })
                .ToListAsync();

            return list.Prepend(_pulauTitle);
        }

        public async Task<IEnumerable<SelectListItem>> KawasanAsync()
        {
            List<SelectListItem> list = await _context.Kawasan
                .Where(p => p.Kode > 0)
                .OrderBy(p => p.Nama)
                .AsNoTracking()
                .Select(c => new SelectListItem
                {
                    Value = c.Kode.ToString(),
                    Text = c.Nama
                })
                .ToListAsync();

            return list.Prepend(_kawasanTitle);
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

        public async Task<IEnumerable<SelectListItem>> TahunRapatLinsekPersubAsync()
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

            return list
                .Select(c => new SelectListItem
                {
                    Value = c.ToString(),
                    Text = c.ToString()
                });
        }

        public async Task<IEnumerable<SelectListItem>> TahunAsyncOptional(JenisRtrEnum jenis)
        {
            IEnumerable<SelectListItem> temp = (await TahunAsync(jenis))
                .Select(c => new SelectListItem
                {
                    Value = c.ToString(),
                    Text = c.ToString()
                });

            return temp.Prepend(_yearInputTitleOptional);
        }
        public async Task<List<int>> TahunAsync(JenisRtrEnum jenis)
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

        public async Task<IEnumerable<SelectListItem>> ProgressRtrAsync(
            int jenisRtr,
            int? value)
        {
            int val = value == null ? 0 : value.Value;
            IEnumerable<SelectListItem> temp = (await ProgressRtrAsync(jenisRtr))
                .Select(c => new SelectListItem
                {
                    Value = c.Kode.ToString(),
                    Text = c.Nama,
                    Selected = c.Kode == val
                });

            return temp.Prepend(_rtrProgressTitle);
        }
        public async Task<IList<ProgressAtr>> ProgressRtrAsync(int jenisRtr)
        {
            IQueryable<ProgressAtr> query = _context.ProgressAtr.AsNoTracking();

            if (jenisRtr != (int)JenisRtrEnum.None)
            {
                query = query.Where(p => p.KodeJenisAtr == jenisRtr);
            }
            return await query
                .OrderBy(p => p.Nomor)
                .ToListAsync();
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
        private void UpdateNamaDokumen(Dokumen dokumen)
        {
            dokumen.Nama = dokumen.Nama + " - " + dokumen.KelompokDokumen.JenisAtr.Nama;
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
        private static readonly SelectListItem _regularTitle =
            new SelectListItem("Pilih Status RTR Lima tahun pertama", string.Empty);
        private static readonly SelectListItem _revisiTitle =
            new SelectListItem("Pilih Status RTR Lima tahun kedua", string.Empty);
        private static readonly SelectListItem _rtrProgressTitle =
            new SelectListItem("Pilih Progress RTR", string.Empty);
        private static readonly SelectListItem _provinsiTitle =
            new SelectListItem("Pilih Provinsi", string.Empty);
        private static readonly SelectListItem _provinsiTitleOptional =
            new SelectListItem("Pilih Provinsi", "0");
        private static readonly SelectListItem _kabupatenKotaTitle =
            new SelectListItem("Pilih Kabupaten/Kota", string.Empty);
        private static readonly SelectListItem _kabupatenKotaTitleOptional =
            new SelectListItem("Pilih Kabupaten/Kota", "0");
        private static readonly SelectListItem _kawasanTitle =
            new SelectListItem("Pilih Kawasan", string.Empty);
        private static readonly SelectListItem _pulauTitle =
            new SelectListItem("Pilih Pulau", string.Empty);
        private static readonly SelectListItem _yearInputTitleRequired =
            new SelectListItem("Pilih Tahun", string.Empty);
        private static readonly SelectListItem _yearInputTitleOptional =
            new SelectListItem("Pilih Tahun", "-1");

        private readonly PomeloDbContext _context;
        private const string _textProperty = "Text";
        private const string _valueProperty = "Value";
    }
}