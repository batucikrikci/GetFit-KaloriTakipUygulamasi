using GetFit.Classes;
using GetFit.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GetFit.Formlar
{
    public partial class Raporlar : Form
    {
        UygulamaDbContext db = new UygulamaDbContext();
        KullaniciYiyecekOgun kullaniciYiyecekOgun;
        Yiyecek secilenYiyecek;
        Ogun secilenOgun;
        Kullanici secilenKisi;
        public Raporlar()
        {
            InitializeComponent();
            YenenleriGetir();
            EncokYenenGetir();
            listBox1.SelectedIndex = -1;
            lstYenen.SelectedIndex = 0;
            OgunleriListele();
            KisileriGetir();
            OguneGoreKategoriListele();
        }

        private void KisileriGetir()
        {
            cmbKisiler.DataSource = db.Kullanicilar.ToList();
        }

        private void OgunleriListele()
        {
            cmbOgun.DataSource = db.Ogunler.ToList();

            cmbOgun2.DataSource = db.Ogunler.ToList();
        }

        private void OguneGoreYiyecekSayileriGetir()
        {


            secilenYiyecek = (Yiyecek)lstYenen.SelectedItem;
            secilenOgun = (Ogun)cmbOgun.SelectedItem;

            var sorgu = db.KullaniciYiyecekOgunler.Where(x => x.YiyecekId == secilenYiyecek.Id && x.OgunId == secilenOgun.Id).OrderBy(x => x.OgunId).GroupBy(x => x.OgunId).Select(x => new { OgunId = x.Key, YiyecekSayisi = x.Count() });
            listBox2.DataSource = sorgu.ToList();
        }

        private void YenenleriGetir()
        {
            var yiyecekler = db.Yiyecekler
             .Where(row => row.Id == db.KullaniciYiyecekOgunler
                                  .Where(kullaniciOgunYiyecekRow => kullaniciOgunYiyecekRow.YiyecekId == row.Id)
                                  .Select(kullaniciOgunYiyecekRow => kullaniciOgunYiyecekRow.YiyecekId)
                                  .FirstOrDefault());

            lstYenen.DataSource = yiyecekler.ToList();
        }

        private void lstYenen_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void EncokYenenGetir()
        {
            int enCok = 1;
            int enCokId = 1;
            var enTop = db.KullaniciYiyecekOgunler.OrderBy(x => x.YiyecekId)
                .GroupBy(x => x.YiyecekId)
                .Select(x => new { YiyecekId = x.Key,YiyecekSayisi = x.Count() });
            var sorgu=enTop.OrderByDescending(x=>x.YiyecekSayisi).Take(3).ToList();
            foreach (var item in enTop)
            {
                if (item.YiyecekSayisi > enCok)
                {
                    enCok = item.YiyecekSayisi;
                    enCokId = item.YiyecekId;
                }

            }
            var yiyecekler = db.Yiyecekler
             .Where(row => row.Id == enCokId).Select(row => row.Ad).ToList();
            
            listBox1.DataSource = sorgu.ToList();
            listBox1.DisplayMember = null;
            listBox1.DisplayMember = "Ad";
        }

        private void cmbOgun_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            OguneGoreYiyecekSayileriGetir();
        }

        
        private void cmbOgun2_SelectedIndexChanged(object sender, EventArgs e)
        {
            OguneGoreKategoriListele();
        }

        

        private void OguneGoreKategoriListele()
        {
            //if (cmbKisiler.SelectedIndex == -1 || cmbOgun2.SelectedIndex == -1)
            //{
            //    MessageBox.Show("Lütfen kişi ve öğün seçimi yapınız.");
            //    return;
            //}
            string currentDate = string.Format("{0}-{1}-{2}", dtpBaslangic.Value.Year, dtpBaslangic.Value.Month, dtpBaslangic.Value.Day);

            string currentDate2 = string.Format("{0}-{1}-{2}", dtpBitis.Value.Year, dtpBitis.Value.Month, dtpBitis.Value.Day);

            string bugun = string.Format("{0}-{1}-{2}", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

            //kullaniciYiyecekOgun.Tarih = DateTime.ParseExact(currentDate, "yyyy-M-d", CultureInfo.InvariantCulture);

            if (cmbKisiler.SelectedIndex == -1) return;
            secilenKisi = (Kullanici)cmbKisiler.SelectedItem;
            if (cmbOgun2.SelectedIndex == 0)
            {
                //var sorgu = db.KullaniciYiyecekOgunler.Where(x => x.KullaniciId == secilenKisi.Id && x.OgunId == 1).Select(x=>x.YiyecekId).FirstOrDefault();


                //var sorgu = db.KullaniciYiyecekOgunler
                //                 .Where(x => x.KullaniciId == secilenKisi.Id && x.OgunId == 1 && db.Yiyecekler
                //                 .Select(y => y.Id)
                //                 .where(y => y.)
                //                 .FirstOrDefault());

                //lstYenen.DataSource = yiyecekler.ToList();

                var sorgu = db.KullaniciYiyecekOgunler.Where(x => x.KullaniciId == secilenKisi.Id && x.OgunId == 1).Select(x => x.YiyecekId).ToList();
                


                //foreach (var item in sorgu)
                //{

                //    var kategoriIdler = db.Yiyecekler.Where(y => y.Id == item).Select(y => y.KategoriId);

                //    foreach (var item2 in kategoriIdler)
                //    {

                //        var kategoriAdlar = db.Kategoriler.Where(k => k.Id == item2).Select(k => k.KategoriAd);
                //        foreach (var item3 in kategoriAdlar)
                //        {
                //            List<string> list = new List<string>();
                //            list.Add(item3);
                //            lstKategori.DataSource = list;
                //        }
                //    }


                //}

            }
        }
    }
}
