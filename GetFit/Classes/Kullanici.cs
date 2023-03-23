using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetFit.Classes
{
    public class Kullanici
    {
        public int Id { get; set; }
        public string KullaniciAdi { get; set; }
        public string Sifre { get; set; }
        public DateTime DogumTarihi { get; set; }
        public double Kilo { get; set; }
        public double Boy { get; set; }
        public bool Cinsiyet { get; set; }
        public int HareketSeviyesi { get; set; }
        public double BazalMetabolizmaHizi { get; set; }

        public ICollection<KullaniciYiyecekOgun> KullaniciYiyecekOgunler { get; set; }

    }
}
