using Giatrican.Database1;
using System;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace Giatrican
{
    public partial class Login : Form
    {
     
        public Login()
        {
            InitializeComponent();
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            try
            {
                string username = txt_username.Text.Trim();
                string password = txt_password.Text.Trim();
                using (var login = new database())
                {
                    var nv = login.tbl_NhanVien.FirstOrDefault(nvlogin => nvlogin.MSNV == username && nvlogin.MatMa == password);
                    if (nv != null)
                    {

                        User.nhanviendangnhap = nv.Hoten;
                        User.macv = nv.MSCViec;
                        var tramcan = login.tbl_Tramcan.FirstOrDefault();
                        if (tramcan != null)
                        {
                            User.tentramcan = tramcan.tentramcan;
                            User.tentinh = tramcan.tentinh;
                            User.lytrinh = tramcan.lytrinh;
                        }
                        User.savelogdata("Nhân viên đăng nhập");
                        this.Hide();
                        var frmHome = new Home();
                        frmHome.FormClosed += (s, args) => Application.Exit();
                        frmHome.Show();
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Tên đăng nhập hoặc mật khẩu k đúng");
                        txt_password.Clear();
                        return;
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
           
        }

        private void Login_Load(object sender, EventArgs e)
        {
        }
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this.Size = new System.Drawing.Size(414,278);
        }

        private void txt_password_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_login_Click(null,null);
                txt_password.Clear();
            }
        }
    }
}
