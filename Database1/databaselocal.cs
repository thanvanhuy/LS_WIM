using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Giatrican.Database1
{
    public partial class databaselocal : DbContext
    {
        public databaselocal()
            : base("name=database1")
        {
        }

        public virtual DbSet<C__EFMigrationsHistory> C__EFMigrationsHistory { get; set; }
        public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }
        public virtual DbSet<tbl_Data_Dangkiem> tbl_Data_Dangkiem { get; set; }
        public virtual DbSet<tbl_Logdata> tbl_Logdata { get; set; }
        public virtual DbSet<tbl_NhanVien> tbl_NhanVien { get; set; }
        public virtual DbSet<tbl_Tramcan> tbl_Tramcan { get; set; }
        public virtual DbSet<Speed_CAM> Speed_CAM { get; set; }
        public virtual DbSet<tbl_Data_Xe> tbl_Data_Xe { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRole>()
                .HasMany(e => e.AspNetRoleClaims)
                .WithRequired(e => e.AspNetRole)
                .HasForeignKey(e => e.RoleId);

            modelBuilder.Entity<AspNetRole>()
                .HasMany(e => e.AspNetUsers)
                .WithMany(e => e.AspNetRoles1)
                .Map(m => m.ToTable("AspNetUserRoles").MapLeftKey("RoleId").MapRightKey("UserId"));

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.AspNetRoles)
                .WithOptional(e => e.AspNetUser)
                .HasForeignKey(e => e.AppUserId);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.AspNetUserClaims)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.AspNetUserLogins)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.AspNetUserTokens)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<tbl_Data_Dangkiem>()
                .Property(e => e.Bienso)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_NhanVien>()
                .Property(e => e.MSNV)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<tbl_NhanVien>()
                .Property(e => e.MatMa)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Data_Xe>()
                .Property(e => e.Chieudaicoso)
                .IsUnicode(false);
        }
    }
}
