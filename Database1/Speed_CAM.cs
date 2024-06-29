namespace Giatrican.Database1
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Speed_CAM
    {
        public int id { get; set; }

        [StringLength(100)]
        public string device { get; set; }

        public DateTime? time { get; set; }

        [StringLength(20)]
        public string plate { get; set; }

        [StringLength(20)]
        public string type { get; set; }

        public int? speed { get; set; }

        [StringLength(20)]
        public string direction { get; set; }

        public int? detection_region { get; set; }

        [StringLength(10)]
        public string region { get; set; }

        [StringLength(20)]
        public string resolution_width { get; set; }

        [StringLength(20)]
        public string resolution_height { get; set; }

        public int? coordinate_x1 { get; set; }

        public int? coordinate_y1 { get; set; }

        public int? coordinate_x2 { get; set; }

        public int? coordinate_y2 { get; set; }

        [StringLength(20)]
        public string confidence { get; set; }

        [StringLength(20)]
        public string plate_color { get; set; }

        [StringLength(20)]
        public string vehicle_type { get; set; }

        [StringLength(20)]
        public string vehicle_color { get; set; }

        [StringLength(20)]
        public string vehicle_brand { get; set; }

        public string plate_image { get; set; }

        public string full_image { get; set; }
    }
}
