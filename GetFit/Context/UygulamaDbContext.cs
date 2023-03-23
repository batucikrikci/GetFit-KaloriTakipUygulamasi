using GetFit.Classes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetFit.Context
{
    public class UygulamaDbContext : DbContext
    {
        public DbSet<Kullanici> Kullanicilar { get; set; }
        public DbSet<Kategori> Kategoriler { get; set; }
        public DbSet<Ogun> Ogunler { get; set; }
        public DbSet<Yiyecek> Yiyecekler { get; set; }
        public DbSet<KullaniciYiyecekOgun> KullaniciYiyecekOgunler { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=.;database=GetFitDb;trusted_connection=true");
        }
    }
}
