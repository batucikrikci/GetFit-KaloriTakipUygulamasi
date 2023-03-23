using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetFit.Classes
{
    public class Ogun
    {
        public int Id { get; set; }
        public string Ad { get; set; }

        public ICollection<KullaniciYiyecekOgun> KullaniciYiyecekOgunler { get; set; }

    }
}
