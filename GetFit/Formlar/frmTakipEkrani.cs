using GetFit.Classes;
using GetFit.Context;
using Microsoft.EntityFrameworkCore;
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
    public partial class frmTakipEkrani : Form
    {

        UygulamaDbContext db = new UygulamaDbContext();
        Yiyecek secilenYiyecek;
        KullaniciYiyecekOgun kullaniciYiyecekOgun=new KullaniciYiyecekOgun();
        Kullanici kullanici = new Kullanici();
        double toplamKalori = 0;
        public frmTakipEkrani()
        {
            InitializeComponent();
            
            VerileriYukle();
            OgunleriListele();
            KategorileriListele();
            HedefKaloriHesabi();
            ProfileGetir();
        }

        private void GunlukKaloriHesapla()
        {
            double gunlukKal=0;

            string currentDate = string.Format("{0}-{1}-{2}", dtpTarih2.Value.Year, dtpTarih2.Value.Month, dtpTarih2.Value.Day);
            DateTime selectedDate = DateTime.ParseExact(currentDate, "yyyy-M-d", CultureInfo.InvariantCulture);
            var xx = db.KullaniciYiyecekOgunler.Where(x => x.KullaniciId == kullanici.Id && x.Tarih == selectedDate);
            foreach (KullaniciYiyecekOgun item in xx.ToList<KullaniciYiyecekOgun>())
            {
                if (item.Yiyecek != null)
                {
                    gunlukKal += int.Parse(item.Yiyecek.Kalori.ToString());
                }
            }
            lblGunlukKalori.Text = gunlukKal.ToString();
        }

        private void ProfileGetir()
        {
            kullanici.Id = KullaniciGirisi.id;
            lblKullaniciAdi.Text = kullanici.KullaniciAdi;
            lblDogum.Text = kullanici.DogumTarihi.ToString();
            lblKilo.Text = kullanici.Kilo.ToString();
            lblBoy.Text = kullanici.Boy.ToString();
            if (kullanici.HareketSeviyesi==1)
            {
                lblHareket.Text = "Aktivite Yok";
            }
            else if (kullanici.HareketSeviyesi==2)
            {
                lblHareket.Text = "Az Aktif";
            }
            else if (kullanici.HareketSeviyesi == 3)
            {
                lblHareket.Text = "Aktif";
            }
            else if (kullanici.HareketSeviyesi == 2)
            {
                lblHareket.Text = "Çok Aktif";
            }
            
            lblBazal.Text = kullanici.BazalMetabolizmaHizi.ToString();
            if (kullanici.Cinsiyet==true)
            {
                lblCinsiyet.Text = "Erkek";
            }
            else if (kullanici.Cinsiyet == false)
            {
                lblCinsiyet.Text = "Kadın";
            }
            GunlukKaloriHesapla();
            OguneGoreListele();
        }

        private void HedefKaloriHesabi()
        {
            double hedefKalori = 0;
            kullanici = db.Kullanicilar.Where(x => x.Id == KullaniciGirisi.id).FirstOrDefault();
            if (kullanici.Cinsiyet == true)
            {
                kullanici.BazalMetabolizmaHizi = 66.5 + (13.7 * kullanici.Kilo) + (5 * kullanici.Boy) - (6.7 * (DateTime.Now.Year - kullanici.DogumTarihi.Year));
            }
            else if (kullanici.Cinsiyet == false)
            {
                kullanici.BazalMetabolizmaHizi = 65.5 + (9.6 * kullanici.Kilo) + (1.8 * kullanici.Boy) - (4.7 * (DateTime.Now.Year - kullanici.DogumTarihi.Year));
            }

            if (kullanici.HareketSeviyesi == 1)
                hedefKalori = kullanici.BazalMetabolizmaHizi * 1;
            else if (kullanici.HareketSeviyesi == 2)
                hedefKalori = kullanici.BazalMetabolizmaHizi * 1.55;
            else if (kullanici.HareketSeviyesi == 3)
                hedefKalori = kullanici.BazalMetabolizmaHizi * 1.7;
            else if (kullanici.HareketSeviyesi == 4)
                hedefKalori = kullanici.BazalMetabolizmaHizi * 2;
            lblHedefKalori.Text = hedefKalori.ToString();
            lblHedef2.Text = hedefKalori.ToString();
        }

        private void YiyecekleriListele()
        {

            var seciliKategori = (Kategori)cmbKategoriler.SelectedItem;
            if (cmbKategoriler.SelectedIndex == 0)
            {
                dgvYiyecekler.DataSource = seciliKategori.Yiyecekler.Select(x => new {Id=x.Id,YemekAd=x.Ad, Kalorisi=x.Kalori, Gram=x.Miktar}).ToList();
            }
            if (cmbKategoriler.SelectedIndex == 1)
            {
                dgvYiyecekler.DataSource = seciliKategori.Yiyecekler.Select(x => new { Id = x.Id, YemekAd = x.Ad, Kalorisi = x.Kalori, Gram = x.Miktar }).ToList();
            }
            if (cmbKategoriler.SelectedIndex == 2)
            {
                dgvYiyecekler.DataSource = seciliKategori.Yiyecekler.Select(x => new { Id = x.Id, YemekAd = x.Ad, Kalorisi = x.Kalori, Gram = x.Miktar }).ToList();
            }
            if (cmbKategoriler.SelectedIndex == 3)
            {
                dgvYiyecekler.DataSource = seciliKategori.Yiyecekler.Select(x => new { Id = x.Id, YemekAd = x.Ad, Kalorisi = x.Kalori, Gram = x.Miktar }).ToList();
            }
            if (cmbKategoriler.SelectedIndex == 4)
            {
                dgvYiyecekler.DataSource = seciliKategori.Yiyecekler.Select(x => new { Id = x.Id, YemekAd = x.Ad, Kalorisi = x.Kalori, Gram = x.Miktar }).ToList();
            }
            if (cmbKategoriler.SelectedIndex == 5)
            {
                dgvYiyecekler.DataSource = seciliKategori.Yiyecekler.Select(x => new { Id = x.Id, YemekAd = x.Ad, Kalorisi = x.Kalori, Gram = x.Miktar }).ToList();
            }
            if (cmbKategoriler.SelectedIndex == 6)
            {
                dgvYiyecekler.DataSource = seciliKategori.Yiyecekler.Select(x => new { Id = x.Id, YemekAd = x.Ad, Kalorisi = x.Kalori, Gram = x.Miktar }).ToList();
            }
            if (cmbKategoriler.SelectedIndex == 7)
            {
                dgvYiyecekler.DataSource = seciliKategori.Yiyecekler.Select(x => new { Id = x.Id, YemekAd = x.Ad, Kalorisi = x.Kalori, Gram = x.Miktar }).ToList();
            }
            if (cmbKategoriler.SelectedIndex == 8)
            {
                dgvYiyecekler.DataSource = seciliKategori.Yiyecekler.Select(x => new { Id = x.Id, YemekAd = x.Ad, Kalorisi = x.Kalori, Gram = x.Miktar }).ToList();
            }
            if (cmbKategoriler.SelectedIndex == 9)
            {
                dgvYiyecekler.DataSource = seciliKategori.Yiyecekler.Select(x => new { Id = x.Id, YemekAd = x.Ad, Kalorisi = x.Kalori, Gram = x.Miktar }).ToList();
            }
        }

        private void KategorileriListele()
        {
            cmbKategoriler.DataSource = db.Kategoriler.Include(x => x.Yiyecekler).ToList();
        }

        private void OgunleriListele()
        {
            cmbOgunler.DataSource = db.Ogunler.ToList();
            cmbOgun.DataSource = db.Ogunler.ToList();
            cmbOgun.SelectedIndex = 0;
        }

        private void VerileriYukle()
        {
            if (db.Ogunler.Any() || db.Yiyecekler.Any() || db.Kategoriler.Any()) return;

            var k1 = new Kategori() { KategoriAd = "Yemekler" };
            var k2 = new Kategori() { KategoriAd = "Deniz Mahsulleri" };
            var k3 = new Kategori() { KategoriAd = "Sebzeler" };
            var k4 = new Kategori() { KategoriAd = "Meyveler" };
            var k5 = new Kategori() { KategoriAd = "Et ürünleri" };
            var k6 = new Kategori() { KategoriAd = "Kahvaltılık" };
            var k7 = new Kategori() { KategoriAd = "Ekmek Grubu Besinler" };
            var k8 = new Kategori() { KategoriAd = "Kuruyemişler" };
            var k9 = new Kategori() { KategoriAd = "İçecekler" };
            var k10 = new Kategori() { KategoriAd = "Çorbalar" };



            var y1 = new Yiyecek() { Ad = "Fırında Kuru Fasulye", Kalori = 94, Miktar = 100, KategoriId = 1 };
            var y2 = new Yiyecek() { Ad = "Havuçlu Bezelye", Kalori = 48, Miktar = 100, KategoriId = 1 };
            var y3 = new Yiyecek() { Ad = "Humus", Kalori = 177, Miktar = 100, KategoriId = 1 };
            var y4 = new Yiyecek() { Ad = "Pişmiş Enginar", Kalori = 53, Miktar = 100, KategoriId = 1 };
            var y5 = new Yiyecek() { Ad = "Hünkar Beğendi", Kalori = 174, Miktar = 100, KategoriId = 1 };
            var y6 = new Yiyecek() { Ad = "Patates Püresi", Kalori = 83, Miktar = 100, KategoriId = 1 };
            var y7 = new Yiyecek() { Ad = "Pişmiş Kereviz", Kalori = 26, Miktar = 100, KategoriId = 1 };
            var y8 = new Yiyecek() { Ad = "Patates Salatası", Kalori = 143, Miktar = 100, KategoriId = 1 };
            var y9 = new Yiyecek() { Ad = "Bulgur Pilavı", Kalori = 215, Miktar = 100, KategoriId = 1 };
            var y10 = new Yiyecek() { Ad = "Pirinç Pilavı", Kalori = 185, Miktar = 100, KategoriId = 1 };
            var y11 = new Yiyecek() { Ad = "Zeytinyağlı Yaprak Sarma", Kalori = 125, Miktar = 100, KategoriId = 1 };
            var y12 = new Yiyecek() { Ad = "Tavuklu Salata", Kalori = 48, Miktar = 100, KategoriId = 1 };
            var y13 = new Yiyecek() { Ad = "Kıymalı Dolma", Kalori = 80, Miktar = 100, KategoriId = 1 };
            var y14 = new Yiyecek() { Ad = "Zeytinyağlı Taze Fasulye", Kalori = 50, Miktar = 100, KategoriId = 1 };
            var y15 = new Yiyecek() { Ad = "Kabak Yemeği", Kalori = 140, Miktar = 100, KategoriId = 1 };
            var y16 = new Yiyecek() { Ad = "Bamya Yemeği", Kalori = 142, Miktar = 100, KategoriId = 1 };
            var y17 = new Yiyecek() { Ad = "Karnabahar Yemeği", Kalori = 115, Miktar = 100, KategoriId = 1 };
            var y18 = new Yiyecek() { Ad = "Karnıyarık", Kalori = 134, Miktar = 100, KategoriId = 1 };

            var y19 = new Yiyecek() { Ad = "Hamsi Tava", Kalori = 150, Miktar = 100, KategoriId = 2 };
            var y20 = new Yiyecek() { Ad = "Somon", Kalori = 180, Miktar = 100, KategoriId = 2 };
            var y21 = new Yiyecek() { Ad = "Palamut", Kalori = 135, Miktar = 100, KategoriId = 2 };
            var y22 = new Yiyecek() { Ad = "Levrek", Kalori = 95, Miktar = 100, KategoriId = 2 };
            var y23 = new Yiyecek() { Ad = "Çupra", Kalori = 110, Miktar = 100, KategoriId = 2 };
            var y24 = new Yiyecek() { Ad = "İstavrit", Kalori = 200, Miktar = 100, KategoriId = 2 };
            var y25 = new Yiyecek() { Ad = "Barbun", Kalori = 125, Miktar = 100, KategoriId = 2 };

            var y26 = new Yiyecek() { Ad = "Taze Mısır", Kalori = 125, Miktar = 100, KategoriId = 3 };
            var y27 = new Yiyecek() { Ad = "Haşlanmış Patates", Kalori = 76, Miktar = 100, KategoriId = 3 };
            var y28 = new Yiyecek() { Ad = "Haşlanmış Pancar", Kalori = 43, Miktar = 100, KategoriId = 3 };
            var y29 = new Yiyecek() { Ad = "Havuç", Kalori = 42, Miktar = 100, KategoriId = 3 };
            var y30 = new Yiyecek() { Ad = "Kuru Soğan", Kalori = 38, Miktar = 100, KategoriId = 3 };
            var y31 = new Yiyecek() { Ad = "Haşlanmış Ispanak", Kalori = 26, Miktar = 100, KategoriId = 3 };
            var y32 = new Yiyecek() { Ad = "Lahana", Kalori = 24, Miktar = 100, KategoriId = 3 };
            var y33 = new Yiyecek() { Ad = "Biber", Kalori = 22, Miktar = 100, KategoriId = 3 };
            var y34 = new Yiyecek() { Ad = "Domates", Kalori = 22, Miktar = 100, KategoriId = 3 };
            var y35 = new Yiyecek() { Ad = "Turp", Kalori = 19, Miktar = 100, KategoriId = 3 };

            var y36 = new Yiyecek() { Ad = "Avokado", Kalori = 167, Miktar = 100, KategoriId = 4 };
            var y37 = new Yiyecek() { Ad = "Muz", Kalori = 85, Miktar = 100, KategoriId = 4 };
            var y38 = new Yiyecek() { Ad = "İncir", Kalori = 80, Miktar = 100, KategoriId = 4 };
            var y39 = new Yiyecek() { Ad = "Erik", Kalori = 75, Miktar = 100, KategoriId = 4 };
            var y40 = new Yiyecek() { Ad = "Kiraz", Kalori = 70, Miktar = 100, KategoriId = 4 };
            var y41 = new Yiyecek() { Ad = "Elma", Kalori = 58, Miktar = 100, KategoriId = 4 };
            var y42 = new Yiyecek() { Ad = "Kayısı", Kalori = 51, Miktar = 100, KategoriId = 4 };
            var y43 = new Yiyecek() { Ad = "Hurma", Kalori = 281, Miktar = 100, KategoriId = 4 };
            var y44 = new Yiyecek() { Ad = "Portakal", Kalori = 49, Miktar = 100, KategoriId = 4 };
            var y45 = new Yiyecek() { Ad = "Şeftali", Kalori = 38, Miktar = 100, KategoriId = 4 };
            var y46 = new Yiyecek() { Ad = "Armut", Kalori = 61, Miktar = 100, KategoriId = 4 };
            var y47 = new Yiyecek() { Ad = "Çilek", Kalori = 37, Miktar = 100, KategoriId = 4 };
            var y48 = new Yiyecek() { Ad = "Limon", Kalori = 27, Miktar = 100, KategoriId = 4 };
            var y49 = new Yiyecek() { Ad = "Karpuz", Kalori = 26, Miktar = 100, KategoriId = 4 };

            var y50 = new Yiyecek() { Ad = "Dana Eti", Kalori = 223, Miktar = 100, KategoriId = 5 };
            var y51 = new Yiyecek() { Ad = "Tavuk Eti", Kalori = 215, Miktar = 100, KategoriId = 5 };
            var y52 = new Yiyecek() { Ad = "Hindi Eti", Kalori = 160, Miktar = 100, KategoriId = 5 };
            var y53 = new Yiyecek() { Ad = "Köfte", Kalori = 168, Miktar = 100, KategoriId = 5 };
            var y54 = new Yiyecek() { Ad = "Biftek", Kalori = 156, Miktar = 100, KategoriId = 5 };

            var y55 = new Yiyecek() { Ad = "Kaşar Peyniri", Kalori = 202, Miktar = 50, KategoriId = 6 };
            var y56 = new Yiyecek() { Ad = "Krem Peynir", Kalori = 175, Miktar = 50, KategoriId = 6 };
            var y57 = new Yiyecek() { Ad = "Beyaz Peynir", Kalori = 90, Miktar = 50, KategoriId = 6 };
            var y58 = new Yiyecek() { Ad = "Lor Peynir", Kalori = 43, Miktar = 50, KategoriId = 6 };
            var y59 = new Yiyecek() { Ad = "Tulum Peyniri", Kalori = 130, Miktar = 50, KategoriId = 6 };
            var y60 = new Yiyecek() { Ad = "Yoğurt", Kalori = 62, Miktar = 100, KategoriId = 6 };

            var y61 = new Yiyecek() { Ad = "Makarna", Kalori = 307, Miktar = 100, KategoriId = 1 };

            var y62 = new Yiyecek() { Ad = "Tam Buğday Ekmeği", Kalori = 63, Miktar = 25, KategoriId = 7 };
            var y63 = new Yiyecek() { Ad = "Çavdar Ekmeği", Kalori = 60, Miktar = 25, KategoriId = 7 };
            var y64 = new Yiyecek() { Ad = "Kepek Ekmeği", Kalori = 53, Miktar = 25, KategoriId = 7 };
            var y65 = new Yiyecek() { Ad = "Yulaf Ezmesi", Kalori = 73, Miktar = 25, KategoriId = 7 };

            var y66 = new Yiyecek() { Ad = "Fındık", Kalori = 97, Miktar = 25, KategoriId = 8 };
            var y67 = new Yiyecek() { Ad = "Badem", Kalori = 97, Miktar = 25, KategoriId = 8 };
            var y68 = new Yiyecek() { Ad = "Ceviz", Kalori = 52, Miktar = 25, KategoriId = 8 };
            var y69 = new Yiyecek() { Ad = "Çiğ Kaju", Kalori = 111, Miktar = 25, KategoriId = 8 };
            var y70 = new Yiyecek() { Ad = "Kabak Çekirdeği", Kalori = 104, Miktar = 25, KategoriId = 8 };
            var y71 = new Yiyecek() { Ad = "Leblebi", Kalori = 71, Miktar = 25, KategoriId = 8 };

            var y72 = new Yiyecek() { Ad = "Şekersiz Çay", Kalori = 3, Miktar = 100, KategoriId = 9 };
            var y73 = new Yiyecek() { Ad = "Şekersiz Kahve", Kalori = 9, Miktar = 100, KategoriId = 9 };
            var y74 = new Yiyecek() { Ad = "Soda", Kalori = 0, Miktar = 100, KategoriId = 9 };
            var y75 = new Yiyecek() { Ad = "Ayran", Kalori = 38, Miktar = 100, KategoriId = 9 };
            var y76 = new Yiyecek() { Ad = "Süt", Kalori = 61, Miktar = 100, KategoriId = 9 };
            var y77 = new Yiyecek() { Ad = "Su", Kalori = 0, Miktar = 100, KategoriId = 9 };
            var y78 = new Yiyecek() { Ad = "Meyve Suyu", Kalori = 47, Miktar = 100, KategoriId = 9 };

            var y79 = new Yiyecek() { Ad = "Mercimek Çorbası", Kalori = 99, Miktar = 180, KategoriId = 10 };
            var y80 = new Yiyecek() { Ad = "Ezogelin Çorbası", Kalori = 93, Miktar = 180, KategoriId = 10 };
            var y81 = new Yiyecek() { Ad = "Tarhana Çorbası", Kalori = 147, Miktar = 180, KategoriId = 10 };
            var y82 = new Yiyecek() { Ad = "Yayla Çorbası", Kalori = 117, Miktar = 180, KategoriId = 10 };
            var y83 = new Yiyecek() { Ad = "Domates Çorbası", Kalori = 68, Miktar = 180, KategoriId = 10 };
            var y84 = new Yiyecek() { Ad = "Brokoli Çorbası", Kalori = 156, Miktar = 180, KategoriId = 10 };
            var y85 = new Yiyecek() { Ad = "Sebze Çorbası", Kalori = 59, Miktar = 180, KategoriId = 10 };
            var y86 = new Yiyecek() { Ad = "Mantar Çorbası", Kalori = 123, Miktar = 180, KategoriId = 10 };

            var y87 = new Yiyecek() { Ad = "Salatalık", Kalori = 15, Miktar = 100, KategoriId = 3 };

            var y88 = new Yiyecek() { Ad = "Haşlanmış Yumurta", Kalori = 75, Miktar = 50, KategoriId = 6 };
            var y89 = new Yiyecek() { Ad = "Omlet", Kalori = 190, Miktar = 100, KategoriId = 6 };
            var y90 = new Yiyecek() { Ad = "Menemen", Kalori = 180, Miktar = 100, KategoriId = 6 };
            var y91 = new Yiyecek() { Ad = "Zeytin", Kalori = 115, Miktar = 100, KategoriId = 6 };
            var y92 = new Yiyecek() { Ad = "Bal", Kalori = 30, Miktar = 10, KategoriId = 6 };


            //var o1 = new Ogun() { Ad = "Kahvaltı", Yiyecekler = new HashSet<Yiyecek> { y55, y56, y57, y58, y59, y60, y65, y88, y87, y89, y90, y91, y92, y72, y73, y74, y75, y76, y77, y78 } };
            //var o2 = new Ogun() { Ad = "Öğle Yemeği", Yiyecekler = new HashSet<Yiyecek> { y1, y2, y3, y4, y5, y6, y7, y8, y9, y10, y11, y12, y13, y14, y15, y16, y17, y18, y19, y20, y21, y22, y23, y24, y25, y26, y27, y28, y29, y30, y31, y32, y33, y34, y35, y50, y51, y52, y53, y54, y61, y60, y62, y63, y64, y72, y73, y74, y75, y76, y77, y78, y79, y80, y81, y82, y83, y84, y85, y86 } };
            //var o3 = new Ogun() { Ad = "Akşam Yemeği", Yiyecekler = new HashSet<Yiyecek> { y1, y2, y3, y4, y5, y6, y7, y8, y9, y10, y11, y12, y13, y14, y15, y16, y17, y18, y19, y20, y21, y22, y23, y24, y25, y26, y27, y28, y29, y30, y31, y32, y33, y34, y35, y50, y51, y52, y53, y54, y61, y60, y62, y63, y64, y72, y73, y74, y75, y76, y77, y78, y79, y80, y81, y82, y83, y84, y85, y86 } };
            //var o4 = new Ogun() { Ad = "Ara Öğün", Yiyecekler = new HashSet<Yiyecek> { y36, y37, y38, y39, y40, y41, y42, y43, y44, y45, y46, y47, y48, y49, y66, y67, y68, y69, y70, y71, y29, y32, y33, y34, y35, y60, y65, y72, y73, y74, y75, y76, y77, y78, y87 } };

            var o1 = new Ogun() { Ad = "Kahvaltı" };
            var o2 = new Ogun() { Ad = "Öğle Yemeği" };
            var o3 = new Ogun() { Ad = "Akşam Yemeği" };
            var o4 = new Ogun() { Ad = "Ara Öğün" };




            db.AddRange(k1, k2, k3, k4, k5, k6, k7, k8, k9, k10, o1, o2, o3, o4, y1, y2, y3, y4, y5, y6, y7, y8, y9, y10, y11, y12, y13, y14, y15, y16, y17, y18, y19, y20, y21, y22, y23, y24, y25, y26, y27, y28, y29, y30, y31, y32, y33, y34, y35, y36, y37, y38, y39, y40, y41, y42, y43, y44, y45, y46, y47, y48, y49, y50, y51, y52, y53, y54, y55, y56, y57, y58, y59, y60, y61, y62, y63, y64, y65, y66, y67, y68, y69, y70, y71, y72, y73, y74, y75, y76, y77, y78, y79, y80, y81, y82, y83, y84, y85, y86, y87, y88, y89, y90, y91, y92);
            db.SaveChanges();
        }

        private void cmbKategoriler_SelectedIndexChanged(object sender, EventArgs e)
        {
            YiyecekleriListele();
        }

        private void dgvYiyecekler_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
            //secilenYiyecek = db.Yiyecekler.Find(Convert.ToInt32(dgvYiyecekler.Rows[e.RowIndex].Cells[0].Value));
        }

        private void pboListeyeEkle_Click(object sender, EventArgs e)
        {
            var kullaniciYiyecekOgun = dgvYiyecekler.SelectedRows[0].DataBoundItem.ToString().Split(",");
            var id = kullaniciYiyecekOgun[0].Split("=")[1];
            Yiyecek secilenYiyecek = db.Yiyecekler.Find(Convert.ToInt32(id));

            if (cmbOgunler.SelectedIndex < 0) return;
            
            if (cmbOgunler.SelectedIndex == 0)
                lstKahvalti.Items.Add(secilenYiyecek);

            if (cmbOgunler.SelectedIndex == 1)
                lstOgleYemegi.Items.Add(secilenYiyecek);

            if (cmbOgunler.SelectedIndex == 2)
                lstAksamYemegi.Items.Add(secilenYiyecek);

            if (cmbOgunler.SelectedIndex == 3)
                lstAraOgun.Items.Add(secilenYiyecek);

            toplamKalori += secilenYiyecek.Kalori;
            lblAlinanKalori.Text = toplamKalori.ToString();

            KullaniciyaEkleme(secilenYiyecek);
            
        }

        private void KullaniciyaEkleme(Yiyecek secilenYiyecek)
        {
            if (secilenYiyecek == null)
            {
                return;
            }

            var secilenOgun = (Ogun)cmbOgunler.SelectedItem;

            var sorgu = db.Yiyecekler.Where(x => x.Id == secilenYiyecek.Id);
            kullaniciYiyecekOgun = new KullaniciYiyecekOgun();
            kullaniciYiyecekOgun.KullaniciId = KullaniciGirisi.id;
            kullaniciYiyecekOgun.YiyecekId = sorgu.FirstOrDefault().Id;

            kullaniciYiyecekOgun.OgunId = secilenOgun.Id;

            

            string currentDate = string.Format("{0}-{1}-{2}", dtpTarih.Value.Year, dtpTarih.Value.Month, dtpTarih.Value.Day);
           
            kullaniciYiyecekOgun.Tarih = DateTime.ParseExact(currentDate,"yyyy-M-d",CultureInfo.InvariantCulture);
            kullanici.Id = KullaniciGirisi.id;

            db.KullaniciYiyecekOgunler.Add(kullaniciYiyecekOgun);
            db.SaveChanges();
            lblGunlukKalori.Text = toplamKalori.ToString();
        }
        private void lstKahvalti_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbOgunler.SelectedIndex = 0;       
        }
        private void OguneGoreListele()
        {
            double toplamOgunKalori=0;
            
            string currentDate = string.Format("{0}-{1}-{2}", dtpTarih2.Value.Year, dtpTarih2.Value.Month, dtpTarih2.Value.Day);
            DateTime selectedDate= DateTime.ParseExact(currentDate, "yyyy-M-d", CultureInfo.InvariantCulture);
            if (cmbOgun.SelectedIndex == 0)
            {
                kullanici.Id = KullaniciGirisi.id;
                dgvBilgiler.DataSource = db.KullaniciYiyecekOgunler.Where(x => x.KullaniciId == kullanici.Id && x.OgunId == 1&&x.Tarih== selectedDate)
                    .Select(a => new
                    {
                        Id = a.Id,
                        YemekId = a.YiyecekId,
                        YemekAdı = a.Yiyecek.Ad,
                        Kalorisi = a.Yiyecek.Kalori
                    }).ToList();
                double totalCal=0;
                var xx = db.KullaniciYiyecekOgunler.Where(x => x.KullaniciId == kullanici.Id && x.OgunId==1&& x.Tarih==selectedDate);
                foreach (KullaniciYiyecekOgun item in xx.ToList<KullaniciYiyecekOgun>())
                {
                    if (item.Yiyecek!=null)
                    {
                        totalCal += int.Parse(item.Yiyecek.Kalori.ToString());
                        
                    }
                }
                lblOgunTopKal.Text=totalCal.ToString();
                
            }
            else if (cmbOgun.SelectedIndex == 1)
            {
                kullanici.Id = KullaniciGirisi.id;
                dgvBilgiler.DataSource = db.KullaniciYiyecekOgunler.Where(x => x.KullaniciId == kullanici.Id && x.OgunId == 2 && x.Tarih == selectedDate)
                    .Select(a => new
                    {
                        Id = a.Id,
                        YemekId = a.YiyecekId,

                        YemekAdı = a.Yiyecek.Ad,
                        Kalorisi = a.Yiyecek.Kalori
                    }).ToList();
                double totalCal = 0;
                var xx = db.KullaniciYiyecekOgunler.Where(x => x.KullaniciId == kullanici.Id && x.OgunId == 2 && x.Tarih == selectedDate);
                foreach (KullaniciYiyecekOgun item in xx.ToList<KullaniciYiyecekOgun>())
                {
                    if (item.Yiyecek != null)
                    {
                        totalCal += int.Parse(item.Yiyecek.Kalori.ToString());
                        
                    }
                }
                lblOgunTopKal.Text = totalCal.ToString();
               
            }
            else if (cmbOgun.SelectedIndex == 2)
            {
                kullanici.Id = KullaniciGirisi.id;
                dgvBilgiler.DataSource = db.KullaniciYiyecekOgunler.Where(x => x.KullaniciId == kullanici.Id && x.OgunId == 3 && x.Tarih == selectedDate)
                    .Select(a => new
                    {
                        Id = a.Id,
                        YemekId = a.YiyecekId,

                        YemekAdı = a.Yiyecek.Ad,
                        Kalorisi = a.Yiyecek.Kalori
                    }).ToList();
                double totalCal = 0;
                var xx = db.KullaniciYiyecekOgunler.Where(x => x.KullaniciId == kullanici.Id && x.OgunId == 3 && x.Tarih == selectedDate);
                foreach (KullaniciYiyecekOgun item in xx.ToList<KullaniciYiyecekOgun>())
                {
                    if (item.Yiyecek != null)
                    {
                        totalCal += int.Parse(item.Yiyecek.Kalori.ToString());
                        
                    }
                }
                lblOgunTopKal.Text = totalCal.ToString();
               
            }
            else if (cmbOgun.SelectedIndex == 3)
            {
                kullanici.Id = KullaniciGirisi.id;
                dgvBilgiler.DataSource = db.KullaniciYiyecekOgunler.Where(x => x.KullaniciId == kullanici.Id && x.OgunId == 4 && x.Tarih == selectedDate)
                    .Select(a => new
                    {
                        Id = a.Id,
                        YemekId = a.YiyecekId,

                        YemekAdı = a.Yiyecek.Ad,
                        Kalorisi = a.Yiyecek.Kalori
                    }).ToList();
                double totalCal = 0;
                var xx = db.KullaniciYiyecekOgunler.Where(x => x.KullaniciId == kullanici.Id && x.OgunId == 4 && x.Tarih == selectedDate);
                foreach (KullaniciYiyecekOgun item in xx.ToList<KullaniciYiyecekOgun>())
                {
                    if (item.Yiyecek != null)
                    {
                        totalCal += int.Parse(item.Yiyecek.Kalori.ToString());
                        
                    }
                }
                lblOgunTopKal.Text = totalCal.ToString();
                
            }
            

        }
        private void btnKaldır_Click(object sender, EventArgs e)
        {
            var kullaniciYiyecekOgun = dgvBilgiler.SelectedRows[0].DataBoundItem.ToString().Split(",");

            var id = kullaniciYiyecekOgun[0].Split("=")[1];
            var silinecekYiyecek = db.KullaniciYiyecekOgunler.Find(Convert.ToInt32(id));
            db.KullaniciYiyecekOgunler.Remove(silinecekYiyecek);
            db.SaveChanges();
            OguneGoreListele();
            ProfileGetir();
        }

        private void cmbOgun_SelectedIndexChanged(object sender, EventArgs e)
        {
            OguneGoreListele();
        }

       

        private void dtpTarih2_ValueChanged(object sender, EventArgs e)
        {
            GunlukKaloriHesapla();
        }
    }
}