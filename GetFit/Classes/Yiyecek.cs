using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetFit.Classes
{
    public class Yiyecek
    {
        public int Id { get; set; }
        public string Ad { get; set; }
        public double Kalori { get; set; }
        public double Miktar { get; set; }

        public Kategori Kategori { get; set; }
        public int KategoriId { get; set; }

        public ICollection<KullaniciYiyecekOgun> KullaniciYiyecekOgunler { get; set; }


    }
}
