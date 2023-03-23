using GetFit.Classes;
using GetFit.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace GetFit.Formlar
{
    public partial class KullaniciGirisi : Form
    {
        UygulamaDbContext db = new UygulamaDbContext();
        Kullanici kullanici;
        public static int id;
        public static string kullaniciAdi;
        public KullaniciGirisi()
        {
            InitializeComponent();
        }

        private void btnYeniOlustur_Click(object sender, EventArgs e)
        {
            KullaniciBilgileri frm3 = new KullaniciBilgileri();
            frm3.Show();
            this.Hide();
        }

        private void btnGiris_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtKullaniciAdi.Text == "admin" && txtSifre.Text == "admin")
                {
                    YoneticiGirisi();
                }
                else
                {
                    var sorgulama = db.Kullanicilar.Where(x => x.KullaniciAdi == txtKullaniciAdi.Text).FirstOrDefault();
                    kullaniciAdi = sorgulama.KullaniciAdi;
                    id = sorgulama.Id;
                    GirisYapKontrol();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bu kullanıcı Kayıtlı değildir. Lütfen yeni hesap oluşturunuz.");
                txtKullaniciAdi.Text = "";
                txtSifre.Text = "";
            }

        }

        private void YoneticiGirisi()
        {
            YemekDuzenlemeEkrani yemekDuzenlemeEkrani = new YemekDuzenlemeEkrani();
            yemekDuzenlemeEkrani.Show();
            this.Hide();
        }

        private void GirisYapKontrol()
        {
            var kullaniciAdiKontrol = db.Kullanicilar.Where(x => x.KullaniciAdi == txtKullaniciAdi.Text).FirstOrDefault();
            var sifreKontrol = db.Kullanicilar.Where(x => x.Sifre == txtSifre.Text).FirstOrDefault();

            if (kullaniciAdiKontrol != null)
            {
                if (kullaniciAdiKontrol.Sifre == txtSifre.Text)
                {
                    MessageBox.Show("Giriş Başarılı");

                    frmTakipEkrani frmTakipEkrani = new frmTakipEkrani();
                    frmTakipEkrani.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Şifre ya da kullanıcı adı hatalı!\n"
                        + "Lütfen tekrar deneyiniz.");
                }
            }
            else
                MessageBox.Show("Kayıtlı kullanıcı bulunamadı");
        }


    }
}
