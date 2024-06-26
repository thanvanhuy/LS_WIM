using ClosedXML.Excel;
using Giatrican.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Giatrican
{
    public partial class Exportexel : Form
    {
        public Exportexel()
        {
            InitializeComponent();
            checkBox1.CheckedChanged += checkBox1_CheckedChanged;
            checkBox2.CheckedChanged += checkBox1_CheckedChanged;
            checkBox3.CheckedChanged += checkBox1_CheckedChanged;
        }

        // Biến để lưu trữ giá trị của lựa chọn
        private int selectedOption = 0;

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            System.Windows.Forms.CheckBox checkBox = sender as System.Windows.Forms.CheckBox;

            if (checkBox != null && checkBox.Checked)
            {
                selectedOption = 0;

                if (checkBox == checkBox1)
                {
                    selectedOption = 1;
                    checkBox2.Checked = false;
                    checkBox3.Checked = false;
                }
                else if (checkBox == checkBox2)
                {
                    selectedOption = 2;
                    checkBox1.Checked = false;
                    checkBox3.Checked = false;
                }
                else if (checkBox == checkBox3)
                {
                    selectedOption = 3;
                    checkBox1.Checked = false;
                    checkBox2.Checked = false;
                }
            }
        }

        private void Exportexel_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Value = DateTime.Now.AddDays(-1);
            dateTimePicker2.Value = DateTime.Now;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sql=string.Empty;
            string timein = dateTimePicker1.Text;
            string timeout = dateTimePicker2.Text;
            using (var exel =new database())
            {
                List<tbl_Data_Xe> tbl_Data_Xe = null;
                switch (selectedOption)
                {
                    case 1:
                        tbl_Data_Xe = exel.tbl_Data_Xe.Where(n => n.Thoigian > dateTimePicker1.Value && n.Thoigian < dateTimePicker2.Value).ToList();
                        break;
                    case 2:
                        tbl_Data_Xe = exel.tbl_Data_Xe.Where(n => n.Thoigian > dateTimePicker1.Value && n.Thoigian < dateTimePicker2.Value && n.Quataitong !="0").ToList();
                        break;
                    case 3:
                        tbl_Data_Xe = exel.tbl_Data_Xe.Where(n => n.Thoigian > dateTimePicker1.Value && n.Thoigian < dateTimePicker2.Value && n.Quataitheogp != "0").ToList();
                        break;
                    default:
                        MessageBox.Show("Vui lòng chọn loại báo cáo cần xuất");
                        return;
                }
                if (tbl_Data_Xe != null)
                {
                    string filepath = @"Exel/BAOCAOCANXE.xlsx";
                    if (!File.Exists(filepath))
                    {
                        MessageBox.Show("Phiếu in không tồn tại");
                        return;
                    }
                    var workbook = new XLWorkbook(filepath);
                    var ws1 = workbook.Worksheet(1);
                    ws1.Cell($"A{1}").Value = User.tentinh;
                    ws1.Cell($"A{2}").Value = User.tentramcan;
                    ws1.Cell($"I{2}").Value = User.lytrinh;

                    int row = 5;
                    foreach ( var cell in tbl_Data_Xe)
                    {
                        row++;
                        ws1.Cell($"A{row}").Value = (row -5).ToString();
                        ws1.Cell($"B{row}").Value = cell.Thoigian;
                        ws1.Cell($"C{row}").Value = getsotruc(int.Parse(cell.Kieuxe));
                        ws1.Cell($"D{row}").Value = getsocautruc(int.Parse(cell.Kieuxe));
                        ws1.Cell($"E{row}").Value = cell.Biensotruoc;
                        ws1.Cell($"F{row}").Value = cell.Biensosau;
                        ws1.Cell($"G{row}").Value = cell.tocdo;
                        ws1.Cell($"H{row}").Value = cell.TTLtruc;
                        ws1.Cell($"I{row}").Value = cell.TLgiayphep;
                        ws1.Cell($"J{row}").Value = cell.Taitrongtruc1;
                        ws1.Cell($"K{row}").Value = cell.Quataitruc1;
                        ws1.Cell($"L{row}").Value = cell.Quataitruc2;
                        ws1.Cell($"M{row}").Value = cell.Quataitruc3;
                        ws1.Cell($"N{row}").Value = cell.TLtruc2;
                        ws1.Cell($"O{row}").Value = cell.Quataitruc2;
                        ws1.Cell($"P{row}").Value = cell.Taitrongtruc4;
                        ws1.Cell($"Q{row}").Value = cell.Taitrongtruc5;
                        ws1.Cell($"R{row}").Value = cell.Taitrongtruc6;
                        ws1.Cell($"S{row}").Value = cell.TLtruc3;
                        ws1.Cell($"T{row}").Value = cell.Quataitruc3;
                        ws1.Cell($"U{row}").Value = cell.Quataitong;
                    }
                    
                    string newfilepate = "D:\\Phieucan\\" + "baocao_" + DateTime.Now.ToString("ddMMyyy_hhmmss") + ".xlsx";
                    User.pathexel = newfilepate;
                    workbook.SaveAs(newfilepate);
                    workbook.Dispose();
                    User.savelogdata("Nhân viên xuất báo cáo xe qua cân");
                    label3.Invoke((MethodInvoker)delegate {
                        label3.Visible = true;
                    });
                    MessageBox.Show("TẠO BÁO CÁO HOÀN THÀNH");
                }
            }
        }
        
       private string getsocautruc(int check)
       {
            switch (check)
            {
                case 1:
                    return "1-1";
                case 2:
                    return "1-2";
                case 3:
                    return "1-1-1";
                case 4:
                    return "1-1-2";
                case 5:
                    return "1-3";
                case 6:
                    return "1-1-3";
                case 7:
                    return "1-1-1";
                case 8:
                    return "1-1-2";
                case 9:
                    return "1-2-1";
                case 10:
                    return "1-1-3";
                case 11:
                    return "1-2-2";
                case 12:
                    return "1-2-3";
                default: return "Không xác định";
            }
       }
        private string getsotruc(int check)
        {
            switch (check)
            {
                case 1:
                    return "2 trục";
                case 2:
                    return "3 trục";
                case 3:
                    return "3 trục";
                case 4:
                    return "4 trục";
                case 5:
                    return "4 trục";
                case 6:
                    return "5 trục";
                case 7:
                    return "3 trục";
                case 8:
                    return "4 trục";
                case 9:
                    return "4 trục";
                case 10:
                    return "5 trục";
                case 11:
                    return "5 trục";
                case 12:
                    return "6 trục";
                default: return "Không xác định";
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(User.pathexel))
            {
                Process.Start(User.pathexel);
            }
        }
    }
}
