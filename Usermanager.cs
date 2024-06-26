using Giatrican.Database;
using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Windows.Forms;

namespace Giatrican
{
    public partial class Usermanager : Form
    {
        private int Id { get; set; }
        public Usermanager()
        {
            InitializeComponent();
        }
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this.Size = new System.Drawing.Size(739, 561);
        }

        private void Usermanager_Load(object sender, EventArgs e)
        {
            using(var nhanvien =new database())
            {
                var data= nhanvien.tbl_NhanVien.ToList();
                if(data != null)
                {
                    foreach(var dt in data)
                    {
                        this.dataGridView1.Rows.Add(dt.MSNV,dt.Hoten,User.TypeUser(dt.MSCViec),Password.Encrypt(dt.MatMa,true),dt.GhiChu);
                    }
                }
                var tramcan=nhanvien.tbl_Tramcan.FirstOrDefault();
                if(tramcan != null)
                {
                    txt_tramcan.Text = tramcan.tentramcan;
                    txt_tinh.Text = tramcan.tentinh;
                    txt_lytrinh.Text=tramcan.lytrinh;
                    Id = tramcan.ID;
                }
            }
        }
        private void reloadgridview()
        {
            dataGridView1.Rows.Clear();
            using (var nhanvien = new database())
            {
                var data = nhanvien.tbl_NhanVien.ToList();
                if (data != null)
                {
                    foreach (var dt in data)
                    {
                        this.dataGridView1.Rows.Add(dt.MSNV, dt.Hoten, User.TypeUser(dt.MSCViec), Password.Encrypt(dt.MatMa, true), dt.GhiChu);
                    }
                }
                var tramcan = nhanvien.tbl_Tramcan.FirstOrDefault();
                if (tramcan != null)
                {
                    txt_tramcan.Text = tramcan.tentramcan;
                    txt_tinh.Text = tramcan.tentinh;
                    txt_lytrinh.Text = tramcan.lytrinh;
                }
            }
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0 && e.RowIndex< dataGridView1.Rows.Count-1)
            {
                txt_manv.Text = dataGridView1.Rows[e.RowIndex].Cells["manv"].Value.ToString();
                txt_tennv.Text = dataGridView1.Rows[e.RowIndex].Cells["tennv"].Value.ToString();
                txt_matkhau.Text = Password.Decrypt(dataGridView1.Rows[e.RowIndex].Cells["matma"].Value.ToString(),true);
                txt_note.Text = dataGridView1.Rows[e.RowIndex].Cells["note"].Value.ToString();
                comboBox1.SelectedIndex = dataGridView1.Rows[e.RowIndex].Cells["mscv"].Value.ToString()=="Admin"? 0:1;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string manv = txt_manv.Text;
            string matkhau = txt_matkhau.Text;
            string tennv = txt_tennv.Text;
            int phanquyen = comboBox1.SelectedItem.ToString() == "Admin" ? 1 : 0;
            string note = txt_note.Text;
            try
            {
                using (var addnhanvien = new database())
                {
                    var nhanvien = new tbl_NhanVien();
                    nhanvien.MSNV = manv;
                    nhanvien.Hoten = tennv;
                    nhanvien.MatMa = matkhau;
                    nhanvien.MSCViec = phanquyen;
                    nhanvien.GhiChu = note;
                    addnhanvien.tbl_NhanVien.Add(nhanvien);
                    addnhanvien.SaveChanges();
                }
            }
            catch { MessageBox.Show("Thêm không thành công"); return; }
            reloadgridview();
            MessageBox.Show("Thêm thành công");
        }

        private void txt_manv_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
            TextBox textBox = (TextBox)sender;
            if (textBox.Text.Length >= 4 && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string manv = txt_manv.Text;
            string matkhau = txt_matkhau.Text;
            string tennv = txt_tennv.Text;
            int phanquyen = comboBox1.SelectedItem.ToString() == "Admin" ? 1 : 0;
            string note = txt_note.Text;
            try
            {
                using (var updatenhanvien = new database())
                {
                    var nhanvien = updatenhanvien.tbl_NhanVien.FirstOrDefault(nv => nv.MSNV.Equals(manv));
                    if (nhanvien != null)
                    {
                        nhanvien.Hoten = tennv;
                        nhanvien.MatMa = matkhau;
                        nhanvien.MSCViec = phanquyen; 
                        nhanvien.GhiChu = note;
                        updatenhanvien.SaveChanges();
                    }
                }
            }
            catch { MessageBox.Show("Thêm không thành công"); return; }
            MessageBox.Show("Cập nhật thành công");
            reloadgridview();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (var tbltramcan = new database())
                {
                    var tramcan = tbltramcan.tbl_Tramcan.FirstOrDefault(nv => nv.ID==Id);
                    if (tramcan != null)
                    {
                        tramcan.tentramcan = txt_tramcan.Text.ToString();
                        tramcan.tentinh = txt_tinh.Text.ToString();
                        tramcan.lytrinh = txt_lytrinh.Text.ToString();
                        tbltramcan.SaveChanges();
                        MessageBox.Show("Cập nhật thành công");
                        reloadgridview();
                    }
                }
        }
    }
}
