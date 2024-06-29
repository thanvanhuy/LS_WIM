namespace Giatrican.Database1
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_Logdata
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string Tennv { get; set; }

        public DateTime? thoigian { get; set; }

        public string ghichu { get; set; }
    }
}
