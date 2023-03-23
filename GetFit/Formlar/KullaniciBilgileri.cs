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

namespace GetFit.Formlar
{
    public partial class KullaniciBilgileri : Form
    {
        UygulamaDbContext db = new UygulamaDbContext();
        Kullanici kullanici;
        public static int id;
        public static string kullaniciAdi;
        public KullaniciBilgileri()
        {
            InitializeComponent();
            cmbHareket.SelectedIndex = 0;
        }

        private void pcbGeri_Click(object sender, EventArgs e)
        {
            KullaniciGirisi frm2 = new KullaniciGirisi();
            frm2.Show();
            this.Close();
        }

        private void btnIptal_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnKaydol_Click(object sender, EventArgs e)
        {

            try
            {
                if (txtKullaniciAd.Text == "" || txtSifre.Text == "" || txtKilo.Text == "" || txtBoy.Text == "" || cmbHareket.SelectedIndex == 0 || (rdoErkek.Checked == false && rdoKadın.Checked == false) || dtpDogum.Text == DateTime.Now.ToString())
                {
                    MessageBox.Show("Boşlukları doldurun.");
                    return;
                }
                else
                {
                    if (KullaniciAdiKayitliMi(txtKullaniciAd.Text) == true)
                    {
                        MessageBox.Show("Bu kullanıcı adı ile zaten bir hesap bulunmaktadır.");
                        return;
                    }
                    else
                    {
                        if (lblDerece.Text != "Yüksek")
                        {
                            MessageBox.Show("Lütfen güçlü bir şifre giriniz.");
                            return ;
                        }
                        kullanici = new Kullanici();
                        kullanici.KullaniciAdi = txtKullaniciAd.Text;
                        kullanici.Sifre = txtSifre.Text;
                        kullanici.DogumTarihi = dtpDogum.Value;
                        kullanici.Kilo = Convert.ToDouble(txtKilo.Text);
                        kullanici.Boy = Convert.ToDouble(txtBoy.Text);
                        if (rdoErkek.Checked)
                        {
                            kullanici.Cinsiyet = true;
                        }
                        else if (rdoKadın.Checked)
                        {
                            kullanici.Cinsiyet = false;
                        }
                        kullanici.HareketSeviyesi = cmbHareket.SelectedIndex;
                        db.Kullanicilar.Add(kullanici);
                        db.SaveChanges();
                        MessageBox.Show("Kayıt Başarılı");
                        KullaniciGirisi frm2 = new KullaniciGirisi();
                        frm2.Show();
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata oluştu."+ex.Message);
            }
            
        }

        private void txtSifre_TextChanged(object sender, EventArgs e)
        {
            string sifre;
            sifre = txtSifre.Text;
            if (sifre.Length <= 6 || sifre.All(s => char.IsDigit(s)) || sifre.All(s => char.IsLetter(s)))
            {
                lblDerece.Text = "Düşük";
                lblDerece.ForeColor = Color.Red;
            }
            else if (sifre.Length == 7 && sifre.Any(s => char.IsDigit(s)) && sifre.Any(s => char.IsLetter(s)))
            {

                lblDerece.Text = "Orta";
                lblDerece.ForeColor = Color.Orange;
            }
            else if (sifre.Length >= 8 && sifre.Any(s => char.IsDigit(s)) && sifre.Any(s => char.IsLetter(s)))
            {

                lblDerece.Text = "Yüksek";
                lblDerece.ForeColor = Color.Green;
            }


        }
        public bool KullaniciAdiKayitliMi(string kullaniciAdi)
        {
            var kullaniciAdiKontrol = db.Kullanicilar.Where(x => x.KullaniciAdi == kullaniciAdi).FirstOrDefault();
            if (kullaniciAdiKontrol != null)
            {
                return true;
            }
            else return false;
        }
    }
}
