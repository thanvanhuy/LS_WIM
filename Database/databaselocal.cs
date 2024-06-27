using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Giatrican.Database
{
    public partial class databaselocal : DbContext
    {
        public databaselocal()
            : base("name=databaselocal")
        {
            try
            {
                this.Database.Connection.Open();
            }
            catch
            {
                throw new Exception("không có kết nối");
            }
        }

        public virtual DbSet<tbl_Data_Dangkiem> tbl_Data_Dangkiem { get; set; }
        public virtual DbSet<tbl_Data_Xe> tbl_Data_Xe { get; set; }
        public virtual DbSet<tbl_Logdata> tbl_Logdata { get; set; }
        public virtual DbSet<tbl_NhanVien> tbl_NhanVien { get; set; }
        public virtual DbSet<tbl_Tramcan> tbl_Tramcan { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<tbl_Data_Dangkiem>()
                .Property(e => e.Bienso)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Data_Xe>()
                .Property(e => e.Chieudaicoso)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Data_Xe>()
                .Property(e => e.Quataitruc1)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Data_Xe>()
                .Property(e => e.Quataitruc2)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Data_Xe>()
                .Property(e => e.Quataitruc3)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Data_Xe>()
                .Property(e => e.Quataitong)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Data_Xe>()
                .Property(e => e.Quataitheogp)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Data_Xe>()
                .Property(e => e.hinhtruoc)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Data_Xe>()
                .Property(e => e.hinhsau)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_NhanVien>()
                .Property(e => e.MSNV)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<tbl_NhanVien>()
                .Property(e => e.MatMa)
                .IsUnicode(false);
        }
    }
}
