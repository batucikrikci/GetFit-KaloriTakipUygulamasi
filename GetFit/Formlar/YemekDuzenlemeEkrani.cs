using GetFit.Classes;
using GetFit.Context;
using Microsoft.EntityFrameworkCore;
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
    public partial class YemekDuzenlemeEkrani : Form
    {
        UygulamaDbContext db=new UygulamaDbContext();
        Yiyecek secilenyiyecek;
        public YemekDuzenlemeEkrani()
        {
            InitializeComponent();
            cmbKategori.DataSource = db.Kategoriler.ToList();
        }

        private void cmbKategori_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvYiyecekler.DataSource = db.Yiyecekler.Where(x => x.KategoriId==cmbKategori.SelectedIndex+1).ToList();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtYiyecekAd.Text=="" || txtMiktar.Text=="" || txtKalori.Text=="")
                {
                    MessageBox.Show("Boşlukları Doldurunuz.");
                    return;
                }
                var secilenKategori = cmbKategori.SelectedItem;
                Yiyecek yiyecek = new Yiyecek();
                yiyecek.Ad = txtYiyecekAd.Text;
                yiyecek.Miktar = Convert.ToDouble(txtMiktar.Text);
                yiyecek.Kalori = Convert.ToDouble(txtKalori.Text);
                if (cmbKategori.SelectedIndex < 0)
                {
                    MessageBox.Show("Lütfen Kategori Seçiniz.");
                    return;
                }
                else if (cmbKategori.SelectedIndex == 0)
                {
                    yiyecek.KategoriId = 1;
                }
                else if (cmbKategori.SelectedIndex == 1)
                {
                    yiyecek.KategoriId = 2;
                }
                else if (cmbKategori.SelectedIndex == 2)
                {
                    yiyecek.KategoriId = 3;
                }
                else if (cmbKategori.SelectedIndex == 3)
                {
                    yiyecek.KategoriId = 4;
                }
                else if (cmbKategori.SelectedIndex == 4)
                {
                    yiyecek.KategoriId = 5;
                }
                else if (cmbKategori.SelectedIndex == 5)
                {
                    yiyecek.KategoriId = 6;
                }
                else if (cmbKategori.SelectedIndex == 6)
                {
                    yiyecek.KategoriId = 7;
                }
                else if (cmbKategori.SelectedIndex == 7)
                {
                    yiyecek.KategoriId = 8;
                }
                else if (cmbKategori.SelectedIndex == 8)
                {
                    yiyecek.KategoriId = 9;
                }
                else if (cmbKategori.SelectedIndex == 9)
                {
                    yiyecek.KategoriId = 10;
                }
                db.Yiyecekler.Add(yiyecek);
                db.SaveChanges();
                dgvYiyecekler.DataSource = db.Yiyecekler.Where(x => x.KategoriId == cmbKategori.SelectedIndex + 1).ToList();
                MessageBox.Show("Yiyecek eklenmiştir.");
            }
            catch (Exception ex)
            {

                MessageBox.Show("Hata oluştu!!"+ex.Message);
            }
        }

      
        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtYiyecekAd.Text == "" || txtMiktar.Text == "" || txtKalori.Text == "")
                {
                    MessageBox.Show("Boşlukları doldurunuz.");
                    return;
                }
                if (secilenyiyecek==null)
                {
                    MessageBox.Show("Lütfen güncellenecek yiyeceği seçiniz");
                    return;
                }
                secilenyiyecek.KategoriId = cmbKategori.SelectedIndex + 1;
                secilenyiyecek.Ad = txtYiyecekAd.Text;
                secilenyiyecek.Miktar = Convert.ToDouble(txtMiktar.Text);
                secilenyiyecek.Kalori = Convert.ToDouble(txtKalori.Text);
                db.SaveChanges();
                dgvYiyecekler.DataSource = db.Yiyecekler.Where(x => x.KategoriId == cmbKategori.SelectedIndex + 1).ToList();
                secilenyiyecek = null;
                txtKalori.Text = "";
                txtMiktar.Text = "";
                txtYiyecekAd.Text = "";
                MessageBox.Show("Yiyecek güncellenmiştir.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata oluştu!!" + ex.Message);
            }
        }

        private void YemekDuzenlemeEkrani_FormClosed(object sender, FormClosedEventArgs e)
        {
            
            Application.Exit();
        }

        private void dgvYiyecekler_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            secilenyiyecek = db.Yiyecekler.Find(Convert.ToInt32(dgvYiyecekler.Rows[e.RowIndex].Cells[0].Value));
            txtMiktar.Text = secilenyiyecek.Miktar.ToString();
            txtYiyecekAd.Text = secilenyiyecek.Ad;
            txtKalori.Text = secilenyiyecek.Kalori.ToString();
            
            
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            try
            {
                if (secilenyiyecek == null)
                {
                    MessageBox.Show("Lütfen silinecek yiyeceği seçiniz");
                    return;
                }
                db.Yiyecekler.Remove(secilenyiyecek);
                db.SaveChanges();
                dgvYiyecekler.DataSource = db.Yiyecekler.Where(x => x.KategoriId == cmbKategori.SelectedIndex + 1).ToList();
                txtKalori.Text = "";
                txtMiktar.Text = "";
                txtYiyecekAd.Text = "";
                MessageBox.Show("Yiyecek silinmiştir.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata oluştu!!" + ex.Message);
            }

        }

        private void btnRaporlar_Click(object sender, EventArgs e)
        {
            Raporlar raporlar = new Raporlar();
            raporlar.Show();
            this.Hide();
        }
    }
}
