using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace MonevAtr.Models
{
    public class SelectListUtilities
    {
        public SelectListUtilities(MonevAtrDbContext context)
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

        public SelectList StatusRevisiRtrRegular
        {
            get
            {
                List<StatusRevisi> list = new List<StatusRevisi>
                {
                    new StatusRevisi(0, "Pilih Status RTR T5-0/T5-1"),
                    StatusRevisi.RegularT51,
                    StatusRevisi.RegularT52
                };

                return new SelectList(list, "Kode", "Nama");
            }
        }

        public SelectList StatusRevisiRtrRevisi
        {
            get
            {
                List<StatusRevisi> list = new List<StatusRevisi>
                {
                    new StatusRevisi(0, "Pilih Status RTR T5-2"),
                    StatusRevisi.RevisiT52,
                    StatusRevisi.RevisiT53,
                    StatusRevisi.RevisiT54
                };

                return new SelectList(list, "Kode", "Nama");
            }
        }

        public async Task<SelectList> Provinsi()
        {
            IList<Provinsi> list = await _context.Provinsi
                .Where(p => p.Kode > 0)
                .OrderBy(p => p.Nama)
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
                .ToListAsync();

            InsertPilihKabupatenKota(list);
            return new SelectList(list, "Kode", "Nama");
        }

        public async Task<SelectList> KabupatenKota(int kodeProvinsi)
        {
            IList<KabupatenKota> list = await _context.KabupatenKota
                .Where(k => k.KodeProvinsi == kodeProvinsi)
                .OrderBy(k => k.Nama)
                .ToListAsync();

            InsertPilihKabupatenKota(list);
            return new SelectList(list, "Kode", "Nama");
        }

        public async Task<SelectList> Pulau()
        {
            IList<Pulau> list = await _context.Pulau
                .Where(p => p.Kode > 0)
                .OrderBy(p => p.Nama)
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
            IList<Kawasan> list = await _context.Kawasan
                .Where(p => p.Kode > 0)
                .OrderBy(p => p.Nama)
                .ToListAsync();

            Kawasan pilih = new Kawasan
            {
                Kode = 0,
                Nama = "Pilih Kawasan"
            };

            list.Insert(0, pilih);
            return new SelectList(list, "Kode", "Nama");
        }

        public async Task<SelectList> TahunRekomendasiGubernur()
        {
            List<int> list = await _context.AtrDokumen
                .Where(a => a.KodeDokumen == 24 || a.KodeDokumen == 58 || a.KodeDokumen == 97)
                .OrderBy(a => a.Tanggal.Year)
                .Select(a => a.Tanggal.Year)
                .Distinct()
                .ToListAsync();

            return Tahun(list, "Pilih Tahun Surat/BA Rekomendasi Gubernur");
        }

        public async Task<SelectList> TahunPermohonanPersetujuanSubstansi()
        {
            List<int> list = await _context.AtrDokumen
                .Where(a => a.KodeDokumen == 25 || a.KodeDokumen == 59 || a.KodeDokumen == 98)
                .OrderBy(a => a.Tanggal.Year)
                .Select(a => a.Tanggal.Year)
                .Distinct()
                .ToListAsync();

            return Tahun(list, "Pilih Tahun Surat/BA Permohonan Persetujuan Substansi Prov/Kab/Kota");
        }

        public async Task<SelectList> TahunMasukLoket()
        {
            List<int> list = await _context.AtrDokumen
                .Where(a => a.KodeDokumen == 29 || a.KodeDokumen == 63 || a.KodeDokumen == 99)
                .OrderBy(a => a.Tanggal.Year)
                .Select(a => a.Tanggal.Year)
                .Distinct()
                .ToListAsync();

            return Tahun(list, "Pilih Tahun Tanggal Masuk Loket");
        }

        public async Task<SelectList> TahunRapatLintasSektor()
        {
            List<int> list = await _context.AtrDokumen
                .Where(a => a.KodeDokumen == 30 || a.KodeDokumen == 64 || a.KodeDokumen == 100)
                .OrderBy(a => a.Tanggal.Year)
                .Select(a => a.Tanggal.Year)
                .Distinct()
                .ToListAsync();

            return Tahun(list, "Pilih Tahun Rapat Lintas Sektor");
        }

        public async Task<SelectList> TahunPersetujuanSubstansi()
        {
            List<int> list = await _context.AtrDokumen
                .Where(a => a.KodeDokumen == 31 || a.KodeDokumen == 65 || a.KodeDokumen == 101)
                .OrderBy(a => a.Tanggal.Year)
                .Select(a => a.Tanggal.Year)
                .Distinct()
                .ToListAsync();

            return Tahun(list, "Pilih Tahun Persetujuan Substansi");
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
                .OrderBy(a => a.Tanggal.Year)
                .Select(a => a.Tanggal.Year)
                .Distinct()
                .ToListAsync();

            list.Remove(list.Find(q => q == 1));
            return Tahun(list, "Pilih Tahun");
        }

        public async Task<SelectList> TahunPerdaRdtrT51()
        {
            return await TahunPerda((int) JenisRtrEnum.RdtrT51);
        }

        public async Task<SelectList> TahunPerdaRdtrT52()
        {
            return await TahunPerda((int) JenisRtrEnum.RdtrT52);
        }

        public async Task<SelectList> TahunPerdaRtrwT50()
        {
            return await TahunPerda((int) JenisRtrEnum.RtrwT50);
        }

        public async Task<SelectList> TahunPerdaRtrwT51()
        {
            return await TahunPerda((int) JenisRtrEnum.RtrwT51);
        }

        public async Task<SelectList> TahunPerdaRtrwT52()
        {
            return await TahunPerda((int) JenisRtrEnum.RtrwT52);
        }

        public async Task<SelectList> TahunPerpresRtrPulauT51()
        {
            return await TahunPerpres((int) JenisRtrEnum.RtrPulauT51);
        }

        public async Task<SelectList> TahunPerpresRtrPulauT52()
        {
            return await TahunPerpres((int) JenisRtrEnum.RtrPulauT52);
        }

        public async Task<SelectList> TahunPerpresRtrKsnT51()
        {
            return await TahunPerpres((int) JenisRtrEnum.RtrKsnT51);
        }

        public async Task<SelectList> TahunPerpresRtrKsnT52()
        {
            return await TahunPerpres((int) JenisRtrEnum.RtrKsnT52);
        }

        public async Task<SelectList> TahunPerdaRtr()
        {
            return await TahunPerda(0);
        }

        public async Task<SelectList> KelompokDokumen()
        {
            IList<KelompokDokumen> list = await _context.KelompokDokumen
                .Include(k => k.JenisAtr)
                .OrderBy(k => k.KodeJenisAtr)
                .ThenBy(k => k.Nomor)
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
            IList<ProgressAtr> list = await ProgressRtr(
                JenisRtrEnum.RdtrT51);

            SelectList selectList = new SelectList(list, "Kode", "Nama");
            List<SelectListItem> listItems = new List<SelectListItem>
            {
                new SelectListItem("Pilih Progress RDTR T5-1", String.Empty)
            };

            foreach (SelectListItem item in selectList)
            {
                listItems.Add(item);
            }

            return new SelectList(listItems, "Value", "Text");
            // return new SelectList(list, "Kode", "Nama");
        }

        public async Task<SelectList> ProgressRdtrT52()
        {
            IList<ProgressAtr> list = await ProgressRtr(
                JenisRtrEnum.RdtrT52);

            list.Insert(0, new ProgressAtr(0, "Pilih Progress RDTR T5-2"));
            return new SelectList(list, "Kode", "Nama");
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

        private async Task<IList<ProgressAtr>> ProgressRtr(JenisRtrEnum jenisRtr)
        {
            return await _context.ProgressAtr
                .Where(p => p.KodeJenisAtr == (int) jenisRtr)
                .OrderBy(p => p.Nomor)
                .ToListAsync();
        }

        private async Task<SelectList> TahunPerda(int jenisRtr)
        {
            IQueryable<Atr> query = _context.Atr
                .OrderBy(a => a.Tahun);

            query = QueryByJenisRtr(query, jenisRtr);

            List<short> list = await query
                .Select(a => a.Tahun)
                .Distinct()
                .ToListAsync();

            return Tahun(list, "Pilih Tahun Perda");
        }

        private async Task<SelectList> TahunPerpres(int jenisRtr)
        {
            IQueryable<Atr> query = _context.Atr
                .OrderBy(a => a.Tahun);

            query = QueryByJenisRtr(query, jenisRtr);

            List<short> list = await query
                .Select(a => a.Tahun)
                .Distinct()
                .ToListAsync();

            return Tahun(list, "Pilih Tahun Perpres");
        }

        private SelectList Tahun(List<short> listSumber, string pilih)
        {
            List<Tahun> list = new List<Tahun>
            {
                new Tahun(0, pilih)
            };

            AddTahunToListHasil(listSumber, list);
            return new SelectList(list, "Value", "Text");
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

        private void AddTahunToListHasil(List<short> listSumber, List<Tahun> listHasil)
        {
            foreach (short tahun in listSumber)
            {
                listHasil.Add(new Tahun(tahun));
            }
        }
        private void AddTahunToListHasil(List<int> listSumber, List<Tahun> listHasil)
        {
            foreach (int tahun in listSumber)
            {
                listHasil.Add(new Tahun(tahun));
            }
        }

        private IQueryable<Atr> QueryByJenisRtr(IQueryable<Atr> query, int jenisRtr)
        {
            return jenisRtr == 0 ?
                query :
                query.Where(q => q.KodeJenisAtr == jenisRtr);
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

        private readonly MonevAtrDbContext _context;
    }
}