namespace Giatrican.Database1
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_NhanVien
    {
        [Key]
        [StringLength(4)]
        public string MSNV { get; set; }

        [Required]
        [StringLength(50)]
        public string Hoten { get; set; }

        [Required]
        [StringLength(20)]
        public string MatMa { get; set; }

        public int MSCViec { get; set; }

        [StringLength(50)]
        public string GhiChu { get; set; }

        public bool? Admin { get; set; }
    }
}
