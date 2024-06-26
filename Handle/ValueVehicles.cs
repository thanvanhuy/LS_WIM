using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giatrican
{
    public class ValueVehicles
    {
        // có thể thay đổi từ bên ngoài
        public static double Percent { get; set; } = 0.95;
        public int Tltruc1 { get; set; } = 0;
        public int Tltruc2 { get; set; } = 0;
        public int Tltruc3 { get; set; } = 0;
        public int TongTLT { get; set; } = 0;

        public double Quataitruc1 { get; set; } = 0.00;
        public double Quataitruc2 { get;set; } = 0.00;
        public double Quataitruc3 { get;set; } = 0.00;
        public double Quataitong { get; set; } = 0.00;


        public string Hinhtruoc { get; set; } = string.Empty;
        public string Hinhsau { get; set; } = string.Empty;
        public string Biensotruoc { get; set; } = string.Empty;
        public string Biensosau { get; set; } = string.Empty;
        public string Mauxe { get; set; } = string.Empty;
        public string Laixe { get; set; } = string.Empty;
        public string Chuxe { get; set; } = string.Empty;
        public string Sogplx { get; set; } = string.Empty;
        public string Sogplhx { get; set; } = string.Empty;
        public string Kieuxe { get; set; } = string.Empty;
        public int Songuoitrenxe { get; set; } = 0;
        public int Taitrongdaukeo { get; set; } = 0;
        public int Taitrongromoc { get; set; } = 0;
        public int Taitrongchophep { get; set; } = 0;
        public int KT_oto_dai { get; set; } = 0;
        public int KT_oto_rong { get; set; } = 0;
        public int KT_oto_cao { get; set; } = 0;
        public int KT_rm_dai { get; set; } = 0;
        public int KT_rm_rong { get; set; } = 0;
        public int KT_rm_cao { get; set; } = 0;
        public int KTthunghang_dai { get; set; } = 0;
        public int KTthunghang_rong { get; set; } = 0;
        public int KTthunghang_cao { get; set; } = 0;
        public int TLgiayphep { get; set; } = 0;
        public string Chieudaicoso { get; set; } = string.Empty;
        public string Quataitheogp { get; set; } = string.Empty;


        public static ValueVehicles DecreaseValuesBy5Percent(ValueVehicles vehicles)
        {
            ValueVehicles result = new ValueVehicles();

            result.Tltruc1 = (int)(vehicles.Tltruc1 * Percent);
            result.Tltruc2 = (int)(vehicles.Tltruc2 * Percent);
            result.Tltruc3 = (int)(vehicles.Tltruc3 * Percent);
            result.TongTLT = (int)(vehicles.TongTLT * Percent);

            result.Quataitruc1 = vehicles.Quataitruc1 * Percent > 0 ? vehicles.Quataitruc1 * Percent : 0;
            result.Quataitruc2 = vehicles.Quataitruc2 * Percent > 0 ? vehicles.Quataitruc2 * Percent : 0;
            result.Quataitruc3 = vehicles.Quataitruc3 * Percent > 0 ? vehicles.Quataitruc3 * Percent : 0;
            result.Quataitong = vehicles.Quataitong * Percent > 0 ? vehicles.Quataitong * Percent : 0;

            return result;
        }
    }
}
