using System;
using System.Threading;
using System.Windows.Forms;

namespace Giatrican
{
    public partial class Home : Form
    {
        private frMain frmHome;
        private Exportexel frmExportexel;
        private Usermanager frmUsermanager;
        public Home()
        {
            InitializeComponent();
        }
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this.Size = new System.Drawing.Size(730, 454);
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if (frmHome == null || frmHome.IsDisposed)
            {
                frmHome = new frMain();
                frmHome.FormClosed += (s, args) => this.Show();
                frmHome.Show();
            }
            else
            {
                frmHome.Show();
            }
            this.Hide();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (frmExportexel == null || frmExportexel.IsDisposed)
            {
                frmExportexel = new Exportexel();
                frmExportexel.FormClosed += (s, args) => this.Show();
                frmExportexel.Show();
            }
            else
            {
                frmExportexel.Show();
            }
            this.Hide();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (User.macv == 0)
            {
                MessageBox.Show("Không có quyền truy cập");
                return;
            }
            if (frmUsermanager == null || frmUsermanager.IsDisposed)
            {
                frmUsermanager = new Usermanager();
                frmUsermanager.FormClosed += (s, args) => this.Show();
                frmUsermanager.Show();
            }
            else
            {
                frmUsermanager.Show();
            }
            this.Hide();
        }

        private void Home_Load(object sender, EventArgs e)
        {

        }

        private void Home_FormClosing(object sender, FormClosingEventArgs e)
        {
             User.savelogdata("Nhân viên đăng xuất");
        }
    }
}
