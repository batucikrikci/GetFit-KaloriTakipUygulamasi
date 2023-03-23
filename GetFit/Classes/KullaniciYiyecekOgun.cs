using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetFit.Classes
{
    public class KullaniciYiyecekOgun
    {
        public int Id { get; set; }
        public Kullanici Kullanici { get; set; }
        public int KullaniciId { get; set; }

        public Yiyecek Yiyecek { get; set; }
        public int YiyecekId { get; set; }

        public Ogun Ogun { get; set; }
        public int OgunId { get; set; }

        public DateTime Tarih { get; set; } = DateTime.Now;
    }
}
