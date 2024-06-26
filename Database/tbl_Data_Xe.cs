namespace Giatrican.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_Data_Xe
    {
        public int Id { get; set; }

        [StringLength(10)]
        public string Biensotruoc { get; set; }

        [StringLength(10)]
        public string Biensosau { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? Thoigian { get; set; }

        [StringLength(10)]
        public string Mauxe { get; set; }

        [StringLength(50)]
        public string Laixe { get; set; }

        [StringLength(50)]
        public string Chuxe { get; set; }

        [StringLength(50)]
        public string Sogplx { get; set; }

        [StringLength(50)]
        public string Sogplhx { get; set; }

        [StringLength(20)]
        public string Kieuxe { get; set; } = "0";

        public int? Songuoitrenxe { get; set; }

        private int? _taitrongdaukeo;

        public int? Taitrongdaukeo
        {
            get { return _taitrongdaukeo; }
            set
            {
                if (value < 0)
                {
                    _taitrongdaukeo = 0;
                }
                else
                {
                    _taitrongdaukeo = value;
                }
            }
        }

        public int? Taitrongromoc { get; set; }

        public int? Taitrongchophep { get; set; }

        public int? KT_oto_dai { get; set; }

        public int? KT_oto_rong { get; set; }

        public int? KT_oto_cao { get; set; }

        public int? KT_rm_dai { get; set; }

        public int? KT_rm_rong { get; set; }

        public int? KT_rm_cao { get; set; }

        public int? KTthunghang_dai { get; set; }

        public int? KTthunghang_rong { get; set; }

        public int? KTthunghang_cao { get; set; }

        [StringLength(20)]
        public string Chieudaicoso { get; set; }

        public int? Taitrongtruc1 { get; set; }

        public int? Taitrongtruc2 { get; set; }

        public int? Taitrongtruc3 { get; set; }

        public int? Taitrongtruc4 { get; set; }

        public int? Taitrongtruc5 { get; set; }

        public int? Taitrongtruc6 { get; set; }

        public int? Taitrongtruc7 { get; set; }

        public int? Taitrongtruc8 { get; set; }

        public int? Taitrongtruc1_1 { get; set; }

        public int? Taitrongtruc2_1 { get; set; }

        public int? Taitrongtruc3_1 { get; set; }

        public int? TLtruc1 { get; set; }

        public int? TLtruc2 { get; set; }

        public int? TLtruc3 { get; set; }

        public int? TTLtruc { get; set; }

        public int? TLgiayphep { get; set; }

        [StringLength(10)]
        public string Quataitruc1 { get; set; } = "0.00";

        [StringLength(10)]
        public string Quataitruc2 { get; set; } = "0.00";

        [StringLength(10)]
        public string Quataitruc3 { get; set; } = "0.00";

        [StringLength(10)]
        public string Quataitong { get; set; } = "0.00";

        [StringLength(10)]
        public string Quataitheogp { get; set; } = "0.00%";

        [StringLength(50)]
        public string hinhtruoc { get; set; }

        [StringLength(50)]
        public string hinhsau { get; set; }

        public double tocdo { get; set; }
        public tbl_Data_Xe()
        {
           
            Biensotruoc = string.Empty;
            Biensosau = string.Empty;
            Thoigian = DateTime.Now;
            Mauxe = string.Empty;
            Laixe = string.Empty;
            Chuxe = string.Empty;
            Sogplx = string.Empty;
            Sogplhx = string.Empty;
            Kieuxe = string.Empty;
            Songuoitrenxe = 0;
            Taitrongdaukeo = 0;
            Taitrongromoc = 0;
            Taitrongchophep = 0;
            KT_oto_dai = 0;
            KT_oto_rong = 0;
            KT_oto_cao = 0;
            KT_rm_dai = 0;
            KT_rm_rong = 0;
            KT_rm_cao = 0;
            KTthunghang_dai = 0;
            KTthunghang_rong = 0;
            KTthunghang_cao = 0;
            Chieudaicoso = string.Empty;
            Taitrongtruc1 = 0;
            Taitrongtruc2 = 0;
            Taitrongtruc3 = 0;
            Taitrongtruc4 = 0;
            Taitrongtruc5 = 0;
            Taitrongtruc6 = 0;
            Taitrongtruc7 = 0;
            Taitrongtruc8 = 0;
            Taitrongtruc1_1 = 0;
            Taitrongtruc2_1 = 0;
            Taitrongtruc3_1 = 0;
            TLtruc1 = 0;
            TLtruc2 = 0;
            TLtruc3 = 0;
            TTLtruc = 0;
            TLgiayphep = 0;
            Quataitruc1 = "0.00";
            Quataitruc2 = "0.00";
            Quataitruc3 = "0.00";
            Quataitong = "0.00";
            Quataitheogp = "0.00%";
            hinhtruoc = string.Empty;
            hinhsau = string.Empty;
            tocdo = 0.0;
        }
        public void GetData(int type)
        {
            // phân giải theo loại xe
            bool checktype = false;
            if (this.Taitrongtruc1_1 > 0 || this.Taitrongtruc2_1 > 0 || this.Taitrongtruc3_1 > 0)
            {
                if (this.Taitrongtruc1_1 == 0)
                {
                    return;
                }
                checktype = true;
            }
            else
            {
                if (this.Taitrongtruc1 == 0)
                {
                    return;
                }
            }
            if (checktype)
            {
                switch (type)
                {
                    case 1:
                        this.TLtruc1 = this.Taitrongtruc1_1;
                        this.TLtruc2 = this.Taitrongtruc2_1;
                        this.Quataitruc1 = Math.Max(0, (((double)(this.Taitrongtruc1_1 - Declare.xe10tan) / Declare.xe10tan) * 100)).ToString("0.00");
                        this.Quataitruc2 = Math.Max(0, (((double)(this.Taitrongtruc2_1 - Declare.xe10tan) / Declare.xe10tan) * 100)).ToString("0.00");
                        this.Quataitong = Math.Max(0, (((double)(this.TTLtruc - Declare.xe16tan) / Declare.xe16tan) * 100)).ToString("0.00");

                        return;
                    case 2:
                        this.TLtruc1 = this.Taitrongtruc1_1;
                        this.TLtruc2 = this.Taitrongtruc2_1;
                        this.Quataitruc1 = Math.Max(0, (((double)(this.Taitrongtruc1_1 - Declare.xe10tan) / Declare.xe10tan) * 100)).ToString("0.00");
                        this.Quataitruc2 = Math.Max(0, (((double)(this.Taitrongtruc2_1 - Declare.xe16tan) / Declare.xe16tan) * 100)).ToString("0.00");
                        this.Quataitong = Math.Max(0, (((double)(this.TTLtruc - Declare.xe24tan) / Declare.xe24tan) * 100)).ToString("0.00");

                        return;
                    case 3:
                        this.TLtruc1 = this.Taitrongtruc1_1;
                        this.TLtruc2 = this.Taitrongtruc2_1;
                        this.TLtruc3 = this.Taitrongtruc3_1;
                        this.Quataitruc1 = Math.Max(0, (((double)(this.Taitrongtruc1_1 - Declare.xe10tan) / Declare.xe10tan) * 100)).ToString("0.00");
                        this.Quataitruc2 = Math.Max(0, (((double)(this.Taitrongtruc2_1 - Declare.xe10tan) / Declare.xe10tan) * 100)).ToString("0.00");
                        this.Quataitruc3 = Math.Max(0, (((double)(this.Taitrongtruc3_1 - Declare.xe10tan) / Declare.xe10tan) * 100)).ToString("0.00");
                        this.Quataitong = Math.Max(0, (((double)(this.TTLtruc - Declare.xe24tan) / Declare.xe24tan) * 100)).ToString("0.00");

                        return;
                    case 4:
                        this.TLtruc1 = this.Taitrongtruc1_1;
                        this.TLtruc2 = this.Taitrongtruc2_1;
                        this.Quataitruc1 = Math.Max(0, (((double)(this.Taitrongtruc1_1 - Declare.xe10tan) / Declare.xe10tan) * 100)).ToString("0.00");
                        this.Quataitruc2 = Math.Max(0, (((double)(this.Taitrongtruc2_1 - Declare.xe24tan) / Declare.xe24tan) * 100)).ToString("0.00");
                        this.Quataitong = Math.Max(0, (((double)(this.TTLtruc - Declare.xe30tan) / Declare.xe30tan) * 100)).ToString("0.00");

                        return;
                    case 5:
                        this.TLtruc1 = this.Taitrongtruc1_1;
                        this.TLtruc2 = this.Taitrongtruc2_1;
                        this.TLtruc3 = this.Taitrongtruc3_1;
                        this.Quataitruc1 = Math.Max(0, (((double)(this.Taitrongtruc1_1 - Declare.xe10tan) / Declare.xe10tan) * 100)).ToString("0.00");
                        this.Quataitruc2 = Math.Max(0, (((double)(this.Taitrongtruc2_1 - Declare.xe10tan) / Declare.xe10tan) * 100)).ToString("0.00");
                        this.Quataitruc3 = Math.Max(0, (((double)(this.Taitrongtruc3_1 - Declare.xe16tan) / Declare.xe16tan) * 100)).ToString("0.00");
                        this.Quataitong = Math.Max(0, (((double)(this.TTLtruc - Declare.xe30tan) / Declare.xe30tan) * 100)).ToString("0.00");

                        return;
                    case 6:
                        this.TLtruc1 = this.Taitrongtruc1_1;
                        this.TLtruc2 = this.Taitrongtruc2_1;
                        this.TLtruc3 = this.Taitrongtruc3_1;
                        this.Quataitruc1 = Math.Max(0, (((double)(this.Taitrongtruc1_1 - Declare.xe10tan) / Declare.xe10tan) * 100)).ToString("0.00");
                        this.Quataitruc2 = Math.Max(0, (((double)(this.Taitrongtruc2_1 - Declare.xe10tan) / Declare.xe10tan) * 100)).ToString("0.00");
                        this.Quataitruc3 = Math.Max(0, (((double)(this.Taitrongtruc3_1 - Declare.xe24tan) / Declare.xe24tan) * 100)).ToString("0.00");
                        this.Quataitong = Math.Max(0, (((double)(this.TTLtruc - Declare.xe34tan) / Declare.xe34tan) * 100)).ToString("0.00");

                        return;
                    case 7:
                        this.TLtruc1 = this.Taitrongtruc1_1;
                        this.TLtruc2 = this.Taitrongtruc2_1;
                        this.TLtruc3 = this.Taitrongtruc3_1;
                        this.Quataitruc1 = Math.Max(0, (((double)(this.Taitrongtruc1_1 - Declare.xe10tan) / Declare.xe10tan) * 100)).ToString("0.00");
                        this.Quataitruc2 = Math.Max(0, (((double)(this.Taitrongtruc2_1 - Declare.xe10tan) / Declare.xe10tan) * 100)).ToString("0.00");
                        this.Quataitruc3 = Math.Max(0, (((double)(this.Taitrongtruc3_1 - Declare.xe10tan) / Declare.xe10tan) * 100)).ToString("0.00");
                        this.Quataitong = Math.Max(0, (((double)(this.TTLtruc - Declare.xe26tan) / Declare.xe26tan) * 100)).ToString("0.00");

                        return;
                    case 8:
                        this.TLtruc1 = this.Taitrongtruc1_1;
                        this.TLtruc2 = this.Taitrongtruc2_1;
                        this.TLtruc3 = this.Taitrongtruc3_1;
                        this.Quataitruc1 = Math.Max(0, (((double)(this.Taitrongtruc1_1 - Declare.xe10tan) / Declare.xe10tan) * 100)).ToString("0.00");
                        this.Quataitruc2 = Math.Max(0, (((double)(this.Taitrongtruc2_1 - Declare.xe10tan) / Declare.xe10tan) * 100)).ToString("0.00");
                        this.Quataitruc3 = Math.Max(0, (((double)(this.Taitrongtruc3_1 - Declare.xe16tan) / Declare.xe16tan) * 100)).ToString("0.00");
                        this.Quataitong = Math.Max(0, (((double)(this.TTLtruc - Declare.xe26tan) / Declare.xe26tan) * 100)).ToString("0.00");

                        return;
                    case 9:
                        this.TLtruc1 = this.Taitrongtruc1_1;
                        this.TLtruc2 = this.Taitrongtruc2_1;
                        this.TLtruc3 = this.Taitrongtruc3_1;
                        this.Quataitruc1 = Math.Max(0, (((double)(this.Taitrongtruc1_1 - Declare.xe10tan) / Declare.xe10tan) * 100)).ToString("0.00");
                        this.Quataitruc2 = Math.Max(0, (((double)(this.Taitrongtruc2_1 - Declare.xe10tan) / Declare.xe10tan) * 100)).ToString("0.00");
                        this.Quataitruc3 = Math.Max(0, (((double)(this.Taitrongtruc3_1 - Declare.xe24tan) / Declare.xe24tan) * 100)).ToString("0.00");
                        this.Quataitong = Math.Max(0, (((double)(this.TTLtruc - Declare.xe34tan) / Declare.xe34tan) * 100)).ToString("0.00");

                        return;
                    case 10:
                        this.TLtruc1 = this.Taitrongtruc1_1;
                        this.TLtruc2 = this.Taitrongtruc2_1;
                        this.TLtruc3 = this.Taitrongtruc3_1;
                        this.Quataitruc1 = Math.Max(0, (((double)(this.Taitrongtruc1_1 - Declare.xe10tan) / Declare.xe10tan) * 100)).ToString("0.00");
                        this.Quataitruc2 = Math.Max(0, (((double)(this.Taitrongtruc2_1 - Declare.xe10tan) / Declare.xe10tan) * 100)).ToString("0.00");
                        this.Quataitruc3 = Math.Max(0, (((double)(this.Taitrongtruc3_1 - Declare.xe24tan) / Declare.xe24tan) * 100)).ToString("0.00");
                        this.Quataitong = Math.Max(0, (((double)(this.TTLtruc - Declare.xe34tan) / Declare.xe34tan) * 100)).ToString("0.00");

                        return;
                    case 11:
                        this.TLtruc1 = this.Taitrongtruc1_1;
                        this.TLtruc2 = this.Taitrongtruc2_1;
                        this.TLtruc3 = this.Taitrongtruc3_1;
                        this.Quataitruc1 = Math.Max(0, (((double)(this.Taitrongtruc1_1 - Declare.xe10tan) / Declare.xe10tan) * 100)).ToString("0.00");
                        this.Quataitruc2 = Math.Max(0, (((double)(this.Taitrongtruc2_1 - Declare.xe16tan) / Declare.xe16tan) * 100)).ToString("0.00");
                        this.Quataitruc3 = Math.Max(0, (((double)(this.Taitrongtruc3_1 - Declare.xe24tan) / Declare.xe24tan) * 100)).ToString("0.00");
                        this.Quataitong = Math.Max(0, (((double)(this.TTLtruc - Declare.xe34tan) / Declare.xe34tan) * 100)).ToString("0.00");

                        return;
                    case 12:
                        this.TLtruc1 = this.Taitrongtruc1_1;
                        this.TLtruc2 = this.Taitrongtruc2_1;
                        this.TLtruc3 = this.Taitrongtruc3_1;
                        this.Quataitruc1 = Math.Max(0, (((double)(this.Taitrongtruc1_1 - Declare.xe10tan) / Declare.xe10tan) * 100)).ToString("0.00");
                        this.Quataitruc2 = Math.Max(0, (((double)(this.Taitrongtruc2_1 - Declare.xe18tan) / Declare.xe18tan) * 100)).ToString("0.00");
                        this.Quataitruc3 = Math.Max(0, (((double)(this.Taitrongtruc3_1 - Declare.xe24tan) / Declare.xe24tan) * 100)).ToString("0.00");
                        this.Quataitong = Math.Max(0, (((double)(this.TTLtruc - Declare.xe48tan) / Declare.xe48tan) * 100)).ToString("0.00");

                        return;

                    default: return;
                }
            }
            else
            {
                switch (type)
                {
                    case 1:
                        this.TLtruc1 = this.Taitrongtruc1;
                        this.TLtruc2 = this.Taitrongtruc2;
                        this.Quataitruc1 = Math.Max(0, (((double)(this.Taitrongtruc1 - Declare.xe10tan) / Declare.xe10tan) * 100)).ToString("0.00");
                        this.Quataitruc2 = Math.Max(0, (((double)(this.Taitrongtruc2 - Declare.xe10tan) / Declare.xe10tan) * 100)).ToString("0.00");
                        this.Quataitong = Math.Max(0, (((double)(this.TTLtruc - Declare.xe16tan) / Declare.xe16tan) * 100)).ToString("0.00");
                        return;
                    case 2:
                        this.TLtruc1 = this.Taitrongtruc1;
                        this.TLtruc2 = this.Taitrongtruc1 + this.Taitrongtruc2;
                        this.Quataitruc1 = Math.Max(0, (((double)(this.Taitrongtruc1 - Declare.xe10tan) / Declare.xe10tan) * 100)).ToString("0.00");
                        this.Quataitruc2 = Math.Max(0, (((double)(this.Taitrongtruc1 + this.Taitrongtruc2 - Declare.xe16tan) / Declare.xe16tan) * 100)).ToString("0.00");
                        this.Quataitong = Math.Max(0, (((double)(this.TTLtruc - Declare.xe24tan) / Declare.xe24tan) * 100)).ToString("0.00");
                        return;
                    case 3:
                        this.TLtruc1 = this.Taitrongtruc1;
                        this.TLtruc2 = this.Taitrongtruc2;
                        this.TLtruc3 = this.Taitrongtruc3;
                        this.Quataitruc1 = Math.Max(0, (((double)(this.Taitrongtruc1 - Declare.xe10tan) / Declare.xe10tan) * 100)).ToString("0.00");
                        this.Quataitruc2 = Math.Max(0, (((double)(this.Taitrongtruc2 - Declare.xe10tan) / Declare.xe10tan) * 100)).ToString("0.00");
                        this.Quataitruc3 = Math.Max(0, (((double)(this.Taitrongtruc3 - Declare.xe10tan) / Declare.xe10tan) * 100)).ToString("0.00");
                        this.Quataitong = Math.Max(0, (((double)(this.TTLtruc - Declare.xe24tan) / Declare.xe24tan) * 100)).ToString("0.00");
                        return;
                    case 4:
                        this.TLtruc1 = this.Taitrongtruc1;
                        this.TLtruc2 = this.Taitrongtruc2 + this.Taitrongtruc3 + this.Taitrongtruc4;
                        this.Quataitruc1 = Math.Max(0, (((double)(this.Taitrongtruc1 - Declare.xe10tan) / Declare.xe10tan) * 100)).ToString("0.00");
                        this.Quataitruc2 = Math.Max(0, (((double)(this.Taitrongtruc2 + this.Taitrongtruc3 + this.Taitrongtruc4 - Declare.xe21tan) / Declare.xe21tan) * 100)).ToString("0.00");
                        this.Quataitong = Math.Max(0, (((double)(this.TTLtruc - Declare.xe30tan) / Declare.xe30tan) * 100)).ToString("0.00");
                        return;
                    case 5:
                        this.TLtruc1 = this.Taitrongtruc1;
                        this.TLtruc2 = this.Taitrongtruc2;
                        this.TLtruc3 = this.Taitrongtruc3 + this.Taitrongtruc4;
                        this.Quataitruc1 = Math.Max(0, (((double)(this.Taitrongtruc1 - Declare.xe10tan) / Declare.xe10tan) * 100)).ToString("0.00");
                        this.Quataitruc2 = Math.Max(0, (((double)(this.Taitrongtruc2 - Declare.xe10tan) / Declare.xe10tan) * 100)).ToString("0.00");
                        this.Quataitruc3 = Math.Max(0, (((double)(this.Taitrongtruc3 + this.Taitrongtruc4 - Declare.xe16tan) / Declare.xe16tan) * 100)).ToString("0.00");
                        this.Quataitong = Math.Max(0, (((double)(this.TTLtruc - Declare.xe30tan) / Declare.xe30tan) * 100)).ToString("0.00");
                        return;
                    case 6:
                        this.TLtruc1 = this.Taitrongtruc1;
                        this.TLtruc2 = this.Taitrongtruc2;
                        this.TLtruc3 = this.Taitrongtruc3 + this.Taitrongtruc4 + this.Taitrongtruc5;
                        this.Quataitruc1 = Math.Max(0, (((double)(this.Taitrongtruc1 - Declare.xe10tan) / Declare.xe10tan) * 100)).ToString("0.00");
                        this.Quataitruc2 = Math.Max(0, (((double)(this.Taitrongtruc2 - Declare.xe10tan) / Declare.xe10tan) * 100)).ToString("0.00");
                        this.Quataitruc3 = Math.Max(0, (((double)(this.Taitrongtruc3 + this.Taitrongtruc4 + this.Taitrongtruc5 - Declare.xe21tan) / Declare.xe21tan) * 100)).ToString("0.00");
                        this.Quataitong = Math.Max(0, (((double)(this.TTLtruc - Declare.xe34tan) / Declare.xe34tan) * 100)).ToString("0.00");
                        return;
                    case 7:
                        this.TLtruc1 = this.Taitrongtruc1;
                        this.TLtruc2 = this.Taitrongtruc2;
                        this.TLtruc3 = this.Taitrongtruc3;
                        this.Quataitruc1 = Math.Max(0, (((double)(this.Taitrongtruc1 - Declare.xe10tan) / Declare.xe10tan) * 100)).ToString("0.00");
                        this.Quataitruc2 = Math.Max(0, (((double)(this.Taitrongtruc2 - Declare.xe10tan) / Declare.xe10tan) * 100)).ToString("0.00");
                        this.Quataitruc3 = Math.Max(0, (((double)(this.Taitrongtruc3 - Declare.xe10tan) / Declare.xe10tan) * 100)).ToString("0.00");
                        this.Quataitong = Math.Max(0, (((double)(this.TTLtruc - Declare.xe26tan) / Declare.xe26tan) * 100)).ToString("0.00");
                        return;
                    case 8:
                        this.TLtruc1 = this.Taitrongtruc1;
                        this.TLtruc2 = this.Taitrongtruc2;
                        this.TLtruc3 = this.Taitrongtruc3;
                        this.Quataitruc1 = Math.Max(0, (((double)(this.Taitrongtruc1 - Declare.xe10tan) / Declare.xe10tan) * 100)).ToString("0.00");
                        this.Quataitruc2 = Math.Max(0, (((double)(this.Taitrongtruc2 - Declare.xe10tan) / Declare.xe10tan) * 100)).ToString("0.00");
                        this.Quataitruc3 = Math.Max(0, (((double)(this.Taitrongtruc3 - Declare.xe10tan) / Declare.xe10tan) * 100)).ToString("0.00");
                        this.Quataitong = Math.Max(0, (((double)(this.TTLtruc - Declare.xe26tan) / Declare.xe26tan) * 100)).ToString("0.00");
                        return;
                    case 9:
                        this.TLtruc1 = this.Taitrongtruc1;
                        this.TLtruc2 = this.Taitrongtruc2 + this.Taitrongtruc3;
                        this.TLtruc3 = this.Taitrongtruc4;
                        this.Quataitruc1 = Math.Max(0, (((double)(this.Taitrongtruc1 - Declare.xe10tan) / Declare.xe10tan) * 100)).ToString("0.00");
                        this.Quataitruc2 = Math.Max(0, (((double)(this.Taitrongtruc2 + this.Taitrongtruc3 - Declare.xe16tan) / Declare.xe16tan) * 100)).ToString("0.00");
                        this.Quataitruc3 = Math.Max(0, (((double)(this.Taitrongtruc4 - Declare.xe10tan) / Declare.xe10tan) * 100)).ToString("0.00");
                        this.Quataitong = Math.Max(0, (((double)(this.TTLtruc - Declare.xe34tan) / Declare.xe34tan) * 100)).ToString("0.00");
                        return;
                    case 10:
                        this.TLtruc1 = this.Taitrongtruc1;
                        this.TLtruc2 = this.Taitrongtruc2 + this.Taitrongtruc3;
                        this.TLtruc3 = this.Taitrongtruc4 + this.Taitrongtruc5;
                        this.Quataitruc1 = Math.Max(0, (((double)(this.Taitrongtruc1 - Declare.xe10tan) / Declare.xe10tan) * 100)).ToString("0.00");
                        this.Quataitruc2 = Math.Max(0, (((double)(this.Taitrongtruc2 + this.Taitrongtruc3 - Declare.xe16tan) / Declare.xe16tan) * 100)).ToString("0.00");
                        this.Quataitruc3 = Math.Max(0, (((double)(this.Taitrongtruc4 + this.Taitrongtruc5 - Declare.xe10tan) / Declare.xe10tan) * 100)).ToString("0.00");
                        this.Quataitong = Math.Max(0, (((double)(this.TTLtruc - Declare.xe42tan) / Declare.xe42tan) * 100)).ToString("0.00");
                        return;
                    case 11:
                        this.TLtruc1 = this.Taitrongtruc1;
                        this.TLtruc2 = this.Taitrongtruc2;
                        this.TLtruc3 = this.Taitrongtruc3 + this.Taitrongtruc4 + this.Taitrongtruc5;
                        this.Quataitruc1 = Math.Max(0, (((double)(this.Taitrongtruc1 - Declare.xe10tan) / Declare.xe10tan) * 100)).ToString("0.00");
                        this.Quataitruc2 = Math.Max(0, (((double)(this.Taitrongtruc2 - Declare.xe10tan) / Declare.xe10tan) * 100)).ToString("0.00");
                        this.Quataitruc3 = Math.Max(0, (((double)(this.Taitrongtruc3 + this.Taitrongtruc4 + this.Taitrongtruc5 - Declare.xe24tan) / Declare.xe24tan) * 100)).ToString("0.00");
                        this.Quataitong = Math.Max(0, (((double)(this.TTLtruc - Declare.xe44tan) / Declare.xe44tan) * 100)).ToString("0.00");
                        return;
                    case 12:
                        this.TLtruc1 = this.Taitrongtruc1;
                        this.TLtruc2 = this.Taitrongtruc2 + this.Taitrongtruc3;
                        this.TLtruc3 = this.Taitrongtruc4 + this.Taitrongtruc5 + this.Taitrongtruc6 + this.Taitrongtruc7 + this.Taitrongtruc8;
                        this.Quataitruc1 = Math.Max(0, (((double)(this.Taitrongtruc1 - Declare.xe10tan) / Declare.xe10tan) * 100)).ToString("0.00");
                        this.Quataitruc2 = Math.Max(0, (((double)(this.Taitrongtruc2 + this.Taitrongtruc3 - Declare.xe16tan) / Declare.xe16tan) * 100)).ToString("0.00");
                        this.Quataitruc3 = Math.Max(0, (((double)(this.Taitrongtruc4 + this.Taitrongtruc5 + this.Taitrongtruc6 + this.Taitrongtruc7 + this.Taitrongtruc8 - Declare.xe24tan) / Declare.xe24tan) * 100)).ToString("0.00");
                        this.Quataitong = Math.Max(0, (((double)(this.TTLtruc - Declare.xe48tan) / Declare.xe48tan) * 100)).ToString("0.00");
                        return;

                    default: return;
                }
            }

        }
    }
}
