using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetFit.Classes
{
    public class Kategori
    {
        public int Id { get; set; }
        public string KategoriAd { get; set; }

        public ICollection<Yiyecek> Yiyecekler { get; set; }
    }
}
