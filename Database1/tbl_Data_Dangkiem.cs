namespace Giatrican.Database1
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_Data_Dangkiem
    {
        public int ID { get; set; }

        [Required]
        [StringLength(20)]
        public string Bienso { get; set; }

        public byte? LoaiBS { get; set; }

        [StringLength(50)]
        public string NhanHieu { get; set; }

        public byte? LoaiPT { get; set; }

        [StringLength(50)]
        public string LoaiPT_String { get; set; }

        [StringLength(100)]
        public string ChieudaiCoso { get; set; }

        [StringLength(50)]
        public string SoMay { get; set; }

        [StringLength(50)]
        public string SoKhung { get; set; }

        [StringLength(50)]
        public string KichThuocBao { get; set; }

        [StringLength(50)]
        public string KichThuocThung { get; set; }

        public int? SoCho { get; set; }

        [StringLength(50)]
        public string SoCho_String { get; set; }

        public byte? SoTruc { get; set; }

        public int? TuTrongTK { get; set; }

        public int? TaiTrongGT { get; set; }

        public int? TrLgToanBoGT { get; set; }

        public int? TrLgMoocCP { get; set; }

        [StringLength(50)]
        public string DoViDK { get; set; }

        [StringLength(50)]
        public string TemDK { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? NgayDK { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? HanDK { get; set; }
    }
}
