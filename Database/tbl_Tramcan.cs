namespace Giatrican.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_Tramcan
    {
        public int ID { get; set; }

        [StringLength(100)]
        public string tentramcan { get; set; }

        [StringLength(100)]
        public string tentinh { get; set; }

        [StringLength(100)]
        public string lytrinh { get; set; }
    }
}
