using DocumentFormat.OpenXml.Spreadsheet;
using Giatrican.Database;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Giatrican
{
    public class CanData
    {
       
        public static tbl_Data_Xe Getdata(string data)
        {
            tbl_Data_Xe tbl_Data_Xe = new tbl_Data_Xe();
            string[] lines = data.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            int i = 0;
            int s1 = 0, s2 = 0, s3 = 0;
            foreach (string line in lines)
            {
                string[] parts = line.Split(new char[] { ':' }, 2);
                string key = parts[0].Trim();
                string value = parts.Length > 1 ? parts[1].Trim() : string.Empty;
                switch (key)
                {
                    case "STT":
                        Declare.STT = "";
                        Declare.STT = value;
                        break;
                    case "THOI GIAN CAN":
                        tbl_Data_Xe.Thoigian = getDatetime(value);
                        break;
                    case "KET QUA CHE DO CAN TINH":
                        Declare.Chedocan = "";
                        Declare.Chedocan = "Tĩnh";
                        break;
                    case "KET QUA CHE DO CAN DONG":
                        Declare.Chedocan = "";
                        Declare.Chedocan = "Động";
                        break;
                    case "BIEN SO XE 1":
                        tbl_Data_Xe.Biensotruoc = value;
                        break;
                    case "BIEN SO XE 2":
                        tbl_Data_Xe.Biensosau = value;//tải trọng romoc  24R012345-9800 //CAN BO KIEM TRA:8900
                        break;
                    case "LAI XE":
                        tbl_Data_Xe.Laixe = value;
                        break;
                    case "TR. LUONG CUA TRUC1":
                        tbl_Data_Xe.Taitrongtruc1 = Int32.Parse(value.Replace("kg", ""));
                        break;
                    case "TR. LUONG CUA TRUC2":
                        tbl_Data_Xe.Taitrongtruc2 = Int32.Parse(value.Replace("kg", ""));
                        break;
                    case "TR. LUONG CUA TRUC3":
                        tbl_Data_Xe.Taitrongtruc3 = Int32.Parse(value.Replace("kg", ""));
                        break;
                    case "TR. LUONG CUA TRUC4":
                        tbl_Data_Xe.Taitrongtruc4 = Int32.Parse(value.Replace("kg", ""));
                        break;
                    case "TR. LUONG CUA TRUC5":
                        tbl_Data_Xe.Taitrongtruc5 = Int32.Parse(value.Replace("kg", ""));
                        break;
                    case "TR. LUONG CUA TRUC6":
                        tbl_Data_Xe.Taitrongtruc6 = Int32.Parse(value.Replace("kg", ""));
                        break;
                    case "TR. LUONG CUA TRUC7":
                        tbl_Data_Xe.Taitrongtruc7 = Int32.Parse(value.Replace("kg", ""));
                        break;
                    case "TR. LUONG CUA TRUC8":
                        tbl_Data_Xe.Taitrongtruc8 = Int32.Parse(value.Replace("kg", ""));
                        break;
                    case "TONG TR.LUONG":
                        tbl_Data_Xe.TTLtruc = Int32.Parse(value.Replace("kg", ""));
                        break;
                    case "TRONG LUONG XE":
                        tbl_Data_Xe.Taitrongdaukeo = Int32.Parse(value.Replace("kg", ""));
                        break;
                    case "TR.LUONG HANG HOA CC DUOC CHO   PHEP":
                        tbl_Data_Xe.Taitrongchophep = Int32.Parse(value.Replace("kg", ""));
                        break;
                    //loại dữ liệu 2 
                    case "TOC DO":
                        tbl_Data_Xe.tocdo = float.Parse(value.Replace("kmh", ""));
                        break;
                    case "SO 1 KIEU TRUC":
                        s1 = int.Parse(value.Trim());
                        break;
                    case "SO 2 KIEU TRUC":
                       s2 = int.Parse(value.Trim());
                        break;
                    case "SO 3 KIEU TRUC":
                       s3 = int.Parse(value.Trim());
                        break;
                    case "TR.LUONG TRUC":
                        if(i== 0)
                        {
                            tbl_Data_Xe.Taitrongtruc1_1 = Int32.Parse(value.Replace("kg", ""));
                        }
                        if (i == 1)
                        {
                            tbl_Data_Xe.Taitrongtruc2_1 = Int32.Parse(value.Replace("kg", ""));
                        }
                        if (i == 2)
                        {
                            tbl_Data_Xe.Taitrongtruc3_1 = Int32.Parse(value.Replace("kg", ""));
                        }
                        i++;
                        break;
                    default:
                        break;
                }
            }
            int chek = getloaixe(s1, s2, s3);
            if (chek > 0)
            {
                frMain.check = chek;
            }
            return tbl_Data_Xe;
        }

        static int getloaixe(int s1,int s2,int s3)
        {
            if (s1 == 1 && s2==1 && s3 == 0)
            {
                return 1;
            }
            if (s1 == 1 && s2 == 2 && s3 == 0)
            {
                return 1;
            }
            if (s1 == 1 && s2 == 5 && s3 == 0)
            {
                return 3;
            }
            if (s1 == 1 && s2 == 7 && s3 == 0)
            {
                return 5;
            }
            if (s1 == 1 && s2 == 1 && s3 == 5)
            {
                return 5;
            }
            if (s1 == 1 && s2 == 2 && s3 == 2)
            {
                return 2;
            }
            if (s1 == 1 && s2 == 2 && s3 == 5)
            {
                return 4;
            }
            if (s1 == 1 && s2 == 5 && s3 == 5)
            {
                return 6;
            }
            if (s1 == 1 && s2 == 5 && s3 == 7)
            {
                return 12;
            }
            return 0; //ddang chay
        } 
        static DateTime getDatetime(string dateString)
        {
            try
            {
                DateTime myDate = DateTime.ParseExact(dateString, "HH:mm dd-MM-yyyy",
                                       System.Globalization.CultureInfo.InvariantCulture);
                return myDate;
            }
            catch { }
            return DateTime.Now;
        }
    }
}
