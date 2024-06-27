/*
 * Thân Văn Huy
 * 0944332653
 * https://www.facebook.com/huy02092001
 */
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using Giatrican.Database;
using Giatrican.Handle;
using Newtonsoft.Json;
using PlateMightsight;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace Giatrican
{
    public partial class frMain : Form
    {
        static SerialPort serialPort;
        public static int check { get; set; } = 0;
        aiCameraRtsp.aiCameraPlayer aiCameraPlayer;
        aiCameraRtsp.aiCameraPlayer aiCameraPlayer1;
        aiCameraRtsp.aiCameraPlayer aiCameraPlayer2;
        aiCameraRtsp.aiCameraPlayer aiCameraPlayer3;
        Bitmap hinhtruoc;
        Bitmap hinhsau;
       
        tbl_Data_Xe data_Xe_final = new tbl_Data_Xe();
        //tbl_Data_Dangkiem tbl_Data_Dangkiem=new tbl_Data_Dangkiem();
        public frMain()
        {
            InitializeComponent();
        }
        void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            Thread.Sleep(2000);
            SerialPort sp = (SerialPort)sender;
            string data = sp.ReadExisting();
            if (data.Length < 4) { return; }
            xuly(data);
            clickpicture(check);
        }
        private void xuly(string data)
        {
            try
            {
                data = Regex.Replace(data, "[^a-zA-Z0-9.:\\s-]+", "");
                data = Regex.Replace(data, @"\.1Vc|[cp]", "");
                data = Regex.Replace(data, @"E\s+KET\s+QUA\s+CAN", "KET QUA CAN");
                data = Regex.Replace(data, @"\bPNHA\b", "NHA");
                data = Regex.Replace(data, @"\bEaBIEN\b", "BIEN");
                string[] lines = data.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                data = string.Join(Environment.NewLine, lines);
                try
                {
                    User.savelogdata(data);
                }
                catch {  }
               
                tbl_Data_Xe dataxe = CanData.Getdata(data);
                dataxe.GetData(check);

                data_Xe_final.Sogplx = "";
                data_Xe_final.Thoigian = dataxe.Thoigian;
                data_Xe_final.Taitrongtruc1 = dataxe.Taitrongtruc1;
                data_Xe_final.Taitrongtruc2 = dataxe.Taitrongtruc2;
                data_Xe_final.Taitrongtruc3 = dataxe.Taitrongtruc3;
                data_Xe_final.Taitrongtruc4 = dataxe.Taitrongtruc4;
                data_Xe_final.Taitrongtruc5 = dataxe.Taitrongtruc5;
                data_Xe_final.Taitrongtruc6 = dataxe.Taitrongtruc6;
                data_Xe_final.Taitrongtruc7 = dataxe.Taitrongtruc7;
                data_Xe_final.Taitrongtruc8 = dataxe.Taitrongtruc8;
                data_Xe_final.TTLtruc = dataxe.TTLtruc;
                data_Xe_final.tocdo = (double)Math.Round(dataxe.tocdo, 2);
                data_Xe_final.Taitrongtruc1_1 = dataxe.Taitrongtruc1_1;
                data_Xe_final.Taitrongtruc2_1 = dataxe.Taitrongtruc2_1;
                data_Xe_final.Taitrongtruc3_1 = dataxe.Taitrongtruc3_1;
                data_Xe_final.TLtruc1 = dataxe.TLtruc1;
                data_Xe_final.TLtruc2 = dataxe.TLtruc2;
                data_Xe_final.TLtruc3 = dataxe.TLtruc3;
                data_Xe_final.Quataitruc1 = dataxe.Quataitruc1;
                data_Xe_final.Quataitruc2 = dataxe.Quataitruc2;
                data_Xe_final.Quataitruc3 = dataxe.Quataitruc3;
                data_Xe_final.Quataitong = dataxe.Quataitong;
                

                Setdata_UI(dataxe);
            }
            catch { }
        }
        private void Setdata_UI(tbl_Data_Xe data_Xe)
        {
            int x = (int)(data_Xe.TLtruc1 + data_Xe.TLtruc2 + data_Xe.TLtruc3);
            int y = (int)(data_Xe.Taitrongtruc1_1 + data_Xe.Taitrongtruc2_1 + data_Xe.Taitrongtruc3_1);
            if ((int)data_Xe.TTLtruc != x && (int)data_Xe.TTLtruc != y)
            {
                MessageBox.Show("Dữ liệu cân hoặc phân loại xe không hợp lệ");
                return;
            }
            if (check == 0)
            {
                MessageBox.Show("Vui lòng chọn loại xe");
                return;
            }
            clickpicture(check);
            
            if (checkBox2.Checked)
            {
                tbLicensePlate.Invoke(new Action(() =>
                {
                    tbLicensePlate.Text = data_Xe.Biensotruoc;
                }));
                tb_tai_trong_dau_keo.Invoke(new Action(() =>
                {
                    tb_tai_trong_dau_keo.Text = data_Xe.Taitrongdaukeo.ToString();
                }));
                tb_tai_trong_cho_phep.Invoke(new Action(() =>
                {
                    tb_tai_trong_cho_phep.Text = data_Xe.Taitrongchophep.ToString();
                }));
                tbDriver.Invoke(new Action(() =>
                {
                    tbDriver.Text = data_Xe.Laixe;
                }));
                string bssau = data_Xe.Biensosau;
                if (bssau.Contains("-"))
                {
                    string[] parts = bssau.Split('-');
                    tbLicensePlate1.Invoke(new Action(() =>
                    {
                        tbLicensePlate1.Text = parts[0];
                    }));
                    if (check > 6)
                    {
                        tb_tai_trong_ro_mooc.Invoke(new Action(() =>
                        {
                            tb_tai_trong_ro_mooc.Text = parts[1];
                        }));
                        tb_tai_trong_dau_keo.Invoke(new Action(() =>
                        {
                            tb_tai_trong_dau_keo.Text = (data_Xe.Taitrongdaukeo - Int32.Parse(parts[1])).ToString();
                        }));
                    }
                }
                else
                {
                    tbLicensePlate1.Invoke(new Action(() =>
                    {
                        tbLicensePlate1.Text = bssau;
                    }));
                }
            }
           
            tbAxleGroupWeight1.Invoke(new Action(() =>
            {
                tbAxleGroupWeight1.Text = (data_Xe.TLtruc1).ToString();
            }));
            tbAxleGroupWeight2.Invoke(new Action(() =>
            {
                tbAxleGroupWeight2.Text = (data_Xe.TLtruc2).ToString();
            }));
            tbAxleGroupWeight3.Invoke(new Action(() =>
            {
                tbAxleGroupWeight3.Text = (data_Xe.TLtruc3).ToString();
            }));
            tbAxleGroupOver1.Invoke(new Action(() =>
            {
                tbAxleGroupOver1.Text = Utility.FormatPercen(data_Xe.Quataitruc1);
            }));
            tbAxleGroupOver2.Invoke(new Action(() =>
            {
                tbAxleGroupOver2.Text = Utility.FormatPercen(data_Xe.Quataitruc2);
            }));
            tbAxleGroupOver3.Invoke(new Action(() =>
            {
                tbAxleGroupOver3.Text = Utility.FormatPercen(data_Xe.Quataitruc3);
            }));
            tbGrossWeight.Invoke(new Action(() =>
            {
                tbGrossWeight.Text = data_Xe.TTLtruc.ToString();
            }));
            tbGrossWeightOver.Invoke(new Action(() =>
            {
                tbGrossWeightOver.Text = Utility.FormatPercen(data_Xe.Quataitong);
            }));
            txt_speed.Invoke(new Action(() =>
            {
                double tocDo = Math.Round(data_Xe.tocdo, 2);
                txt_speed.Text = tocDo.ToString();
            }));
            if (tb_so_nguoi.Text.Length > 0)
            {
                tbLicenseGrossWeight.Invoke(new Action(() =>
                {
                    int a = (Int32.Parse(tb_so_nguoi.Text.Trim()) * Declare.Cannang) + Int32.Parse(tb_tai_trong_cho_phep.Text.Trim()) + Int32.Parse(tb_tai_trong_dau_keo.Text.Trim()) + Int32.Parse(tb_tai_trong_ro_mooc.Text.Trim());
                    tbLicenseGrossWeight.Text = a.ToString();
                }));
                tbLicenseGrossWeightOver.Invoke(new Action(() =>
                {
                    int a = ((int)(data_Xe.TTLtruc * 0.95) - Int32.Parse(tbLicenseGrossWeight.Text.Trim()));
                    int b = (Int32.Parse(tb_tai_trong_cho_phep.Text.Trim()));// 
                    double c;
                    if (b == 0)
                    {
                        c = 0;
                    }
                    else
                    {
                        c = ((double)a / b) * 100;
                        c = Math.Round(c, 2);
                    }

                    if (c < 0) c = 0;
                    tbLicenseGrossWeightOver.Text = Utility.FormatPercen(c.ToString());
                }));
            }
            
           
        }
        private void ClearText()
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(ClearText));
                return;
            }
            txtsgp.Text = "";
            txtsgplh.Text = "";
            tbDriver.Text = "";
            tbLicensePlate.Text = "";
            tbLicensePlate1.Text = "";
            tbstructure.Text = "";
            cbbAxleGroupLength1.Items.Clear();
            cbbAxleGroupLength2.Items.Clear();
            cbbAxleGroupLength3.Items.Clear();
            lbGrossWeightRe.Text = "";
            txtsgp.Text = "";
            txtsgplh.Text = "";
            txtmauxe.Text = "";
            txtchuxe.Text = "";
            checkBox1.Checked = false;
            pbAxle1.Image = null;
            pbAxle2.Image = null;
            pbAxle3.Image = null;
            pbAxle4.Image = null;
            pbRegulation.Image = null;
            tb_so_nguoi.Text = "0";
            tb_tai_trong_dau_keo.Text = "0";
            tb_tai_trong_ro_mooc.Text = "0";
            tb_tai_trong_cho_phep.Text = "0";
            txtdai.Text = "0";
            txtrong.Text = "0";
            txtcao.Text = "0";
            txtdai1.Text = "0";
            txtrong1.Text = "0";
            txtcao1.Text = "0";
            txtdai2.Text = "0";
            txtrong2.Text = "0";
            txtcao2.Text = "0";
            txtchieudai.Text = "0";
            Label29.Text = "TL cụm trục 1";
            Label30.Text = "TL cụm trục 2";
            Label31.Text = "TL cụm trục 3";
        }
        private void ClearTextFromAnotherThread1()
        {
            Invoke(new MethodInvoker(ClearText1));
        }
        private void ClearText1()
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(ClearText1));
                return;
            }
            tbAxleGroupWeight1.Text = "0";
            tbAxleGroupWeight2.Text = "0";
            tbAxleGroupWeight3.Text = "0";
            tbGrossWeight.Text = "0";
            tbLicenseGrossWeight.Text = "0";
            tbLicenseGrossWeightOver.Text = "0%";
            tbAxleGroupOver1.Text = "0%";
            tbAxleGroupOver2.Text = "0%";
            tbAxleGroupOver3.Text = "0%";
            tbGrossWeightOver.Text = "0%";
            txt_speed.Text = "0";

        }
        private void ClearTextFromAnotherThread()
        {
            Invoke(new MethodInvoker(ClearText));
        }
        protected override void OnResize(EventArgs e)
        {
            //base.OnResize(e);
            //this.Size = new System.Drawing.Size(1571, 904);
        }

        private void Timenow_Tick(object sender, EventArgs e)
        {
            label2.Text = DateTime.Now.ToString("dd/MM/yyyy  HH:mm:ss");
        }

        private void frMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn đóng chương trình?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                e.Cancel = true;
            }

            else
            {
                try
                {
                    if (serialPort.IsOpen)
                    {
                        serialPort.Close();
                        serialPort.Dispose();
                    }
                    if (aiCameraPlayer != null)
                    {
                        aiCameraPlayer.stopLiveview();
                        aiCameraPlayer = null;
                        aiCameraPlayer1.stopLiveview();
                        aiCameraPlayer1 = null;
                        aiCameraPlayer2.stopLiveview();
                        aiCameraPlayer2 = null;
                        aiCameraPlayer3.stopLiveview();
                        aiCameraPlayer3 = null;
                    }
                    if (hinhsau != null)
                    {
                        hinhsau.Dispose();
                    }
                    if (hinhtruoc != null)
                    {
                        hinhtruoc.Dispose();
                    }
                }
                catch { }
            }
        }
        private  void frMain_Load(object sender, EventArgs e)
        {
            
            if (Environment.MachineName.CompareTo("VVA-DC")==0)
            {
                textBox1.Visible = true;
                button2.Visible = true;
            }
            lb_nv.Text = User.nhanviendangnhap;
            if (aiCameraPlayer != null)
            {
                aiCameraPlayer.stopLiveview();
                aiCameraPlayer = null;
                aiCameraPlayer1.stopLiveview();
                aiCameraPlayer1 = null;
                aiCameraPlayer2.stopLiveview();
                aiCameraPlayer2 = null;
                aiCameraPlayer3.stopLiveview();
                aiCameraPlayer3 = null;
            }

            aiCameraPlayer = new aiCameraRtsp.aiCameraPlayer();
            aiCameraPlayer1 = new aiCameraRtsp.aiCameraPlayer();
            aiCameraPlayer2 = new aiCameraRtsp.aiCameraPlayer();
            aiCameraPlayer3 = new aiCameraRtsp.aiCameraPlayer();

            aiCameraPlayer.liveViewUrl((long)pbImage.Handle, Declare.rtsp, Declare.username, Declare.pass, Declare.key);
            aiCameraPlayer1.liveViewUrl((long)pbImage1.Handle, Declare.rtsp1, Declare.username, Declare.pass, Declare.key);
            aiCameraPlayer2.liveViewUrl((long)pbImage2.Handle, Declare.rtsp2, Declare.username, Declare.pass, Declare.key);
            aiCameraPlayer3.liveViewUrl((long)pbImage3.Handle, Declare.rtsp3, Declare.username, Declare.pass, Declare.key);
            aiCameraPlayer.display = true;
            aiCameraPlayer1.display = true;
            aiCameraPlayer2.display = true;
            aiCameraPlayer3.display = true;

            PlateMightsight.BuildSocketServer buildSocketServer = new PlateMightsight.BuildSocketServer(Declare.IP, Declare.Port);
            buildSocketServer.DataReceivednew += Datasocket;
            buildSocketServer.Start();
            if (buildSocketServer.error) MessageBox.Show("buildSocketServer err");


            cbbAxleGroupLength1.Enabled = false;
            cbbAxleGroupLength2.Enabled = false;
            cbbAxleGroupLength3.Enabled = false;
            serialPort = new SerialPort("COM3", 9600);
            serialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
            try
            {
                serialPort.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            //Socketserver server = new Socketserver("192.188.0.10", 10004);
            //server.DataReceivednew += DataReceivedHandlerSoket;
            //server.Start();
            //if (server.error) MessageBox.Show("buildSocketServer err");
        }

        private void DataReceivedHandlerSoket(string data, IPAddress endpointAddress, int endpointPort)
        {
            Thread.Sleep(1000);
            if (data.Length < 4) { return; }
            xuly(data);
            clickpicture(check);
        }
        private void Datasocket(string data, IPAddress endpointAddress, int endpointPort)
        {
            if (data.Length < 7) return;
            Datav1 objData = JsonConvert.DeserializeObject<Datav1>(data);
            if (objData.plate.Length < 6) return;
            if (objData.plate.Equals("No Plates")) return;

            string hinh = objData.evidence_image0;
            if (hinh.Length <= 0)
            {
                hinh = objData.full_image;
            }
            switch (endpointAddress.ToString())
            {

                case Declare.IPAddress:
                    tbLicensePlate.Invoke(new Action(() =>
                    {
                        tbLicensePlate.Text = objData.plate;
                    }));
                    txtmauxe.Invoke(new Action(() =>
                    {
                        txtmauxe.Text = objData.vehicle_color;
                    }));
                    string path = Declare.filepathsaveimage + DateTime.Now.ToString("ddMMyyyy") + "\\" + DateTime.Now.ToString("ddMMyyyy_HHmmss") + "_HT.jpeg";
                    Utility.checkExistingFolderFromFilePath(path);
                    data_Xe_final.hinhtruoc = path;
                    hinhtruoc = Utility.drawImageOnImage(Utility.fixBase64ForBitmap(hinh), Utility.fixBase64ForBitmap(objData.plate_image));
                    hinhtruoc.Save(path, ImageFormat.Jpeg);
                    UpdatePictureBoxImage(pbAxle1, Utility.drawImageOnImage(Utility.fixBase64ForBitmap(hinh), Utility.fixBase64ForBitmap(objData.plate_image)));
                    UpdatePictureBoxImage(pbAxle2, Utility.fixBase64ForBitmap(objData.plate_image));
                    break;
                case Declare.IPAddress2:
                    tbLicensePlate1.Invoke(new Action(() =>
                    {
                        tbLicensePlate1.Text = objData.plate;
                    }));
                    string path1 = Declare.filepathsaveimage + DateTime.Now.ToString("ddMMyyyy") + "\\" + DateTime.Now.ToString("ddMMyyyy_HHmmss") + "_HS.jpeg";
                    Utility.checkExistingFolderFromFilePath(path1);
                    data_Xe_final.hinhsau = path1;
                    hinhsau = Utility.fixBase64ForBitmap(hinh);
                    hinhsau = Utility.drawImageOnImage(Utility.fixBase64ForBitmap(hinh), Utility.fixBase64ForBitmap(objData.plate_image));
                    hinhsau.Save(path1, ImageFormat.Jpeg);
                    UpdatePictureBoxImage(pbAxle3, Utility.drawImageOnImage(Utility.fixBase64ForBitmap(hinh), Utility.fixBase64ForBitmap(objData.plate_image)));
                    UpdatePictureBoxImage(pbAxle4, Utility.fixBase64ForBitmap(objData.plate_image));
                    break;
                default:
                    break;
            }

        }
        private void Datasocket_datanew(string data, IPAddress endpointAddress, int endpointPort)
        {
            if (data.Length < 7) return;
            Data objData = JsonConvert.DeserializeObject<Data>(data);
            if (objData.license_plate.Length < 6) return;
            if (objData.license_plate.Equals("No Plates")) return;

            string hinh = objData.full_snapshot;
            if (hinh.Length <= 0)
            {
                hinh = objData.violation_snapshot;
            }
            switch (endpointAddress.ToString())
            {

                case Declare.IPAddress:
                    tbLicensePlate.Invoke(new Action(() =>
                    {
                        tbLicensePlate.Text = objData.license_plate;
                    }));
                    txtmauxe.Invoke(new Action(() =>
                    {
                        txtmauxe.Text = objData.vehicle_color;
                    }));
                    //evidence_image0
                    string path = Declare.filepathsaveimage + DateTime.Now.ToString("ddMMyyyy") + "\\" + DateTime.Now.ToString("ddMMyyyy_HHmmss") + "_HT.jpeg";
                    Utility.checkExistingFolderFromFilePath(path);
                    data_Xe_final.hinhtruoc = path;
                    hinhtruoc = Utility.drawImageOnImage(Utility.fixBase64ForBitmap(hinh), Utility.fixBase64ForBitmap(objData.license_plate_snapshot));
                    hinhtruoc.Save(path, ImageFormat.Jpeg);
                    UpdatePictureBoxImage(pbAxle1, Utility.drawImageOnImage(Utility.fixBase64ForBitmap(hinh), Utility.fixBase64ForBitmap(objData.license_plate_snapshot)));
                    UpdatePictureBoxImage(pbAxle2, Utility.fixBase64ForBitmap(objData.license_plate_snapshot));
                    break;
                case Declare.IPAddress2:
                    tbLicensePlate1.Invoke(new Action(() =>
                    {
                        tbLicensePlate1.Text = objData.license_plate;
                    }));
                    string path1 = Declare.filepathsaveimage + DateTime.Now.ToString("ddMMyyyy") + "\\" + DateTime.Now.ToString("ddMMyyyy_HHmmss") + "_HS.jpeg";
                    Utility.checkExistingFolderFromFilePath(path1);
                    data_Xe_final.hinhsau = path1;
                    //hinhsau = Utility.fixBase64ForBitmap(hinh);
                    hinhsau = Utility.drawImageOnImage(Utility.fixBase64ForBitmap(hinh), Utility.fixBase64ForBitmap(objData.license_plate_snapshot));
                    hinhsau.Save(path1, ImageFormat.Jpeg);
                    UpdatePictureBoxImage(pbAxle3, Utility.drawImageOnImage(Utility.fixBase64ForBitmap(hinh), Utility.fixBase64ForBitmap(objData.license_plate_snapshot)));
                    // UpdatePictureBoxImage(pbAxle3, Utility.fixBase64ForBitmap(hinh));
                    UpdatePictureBoxImage(pbAxle4, Utility.fixBase64ForBitmap(objData.license_plate_snapshot));
                    break;
                default:
                    break;
            }
        }
        private void pbReImg1_Click(object sender, EventArgs e)
        {
            lbGrossWeightRe.Text = "16 tấn";
            cbbAxleGroupLength1.Items.Clear();
            cbbAxleGroupLength2.Items.Clear();
            cbbAxleGroupLength3.Items.Clear();
            cbbAxleGroupLength1.Items.Add("Trục đơn 1");
            cbbAxleGroupLength2.Items.Add("Trục đơn 2");
            cbbAxleGroupLength1.SelectedIndex = 0;
            cbbAxleGroupLength2.SelectedIndex = 0;
            check = 1;
            pbRegulation.Image = pbReImg1.Image;
            tbstructure.Text = "Đơn-Đơn";
            data_Xe_final.Kieuxe = check.ToString();
            txtdai2.Enabled = false;
            txtrong2.Enabled = false;
            txtcao2.Enabled = false;
            Label29.Text = "TL trục đơn 1";
            Label30.Text = "TL trục đơn 2";
            Label4.Text = "Tự trọng  xe";
            tb_tai_trong_ro_mooc.Enabled=false;
        }

        private void pbReImg2_Click(object sender, EventArgs e)
        {
            lbGrossWeightRe.Text = "24 tấn";
            cbbAxleGroupLength1.Items.Clear();
            cbbAxleGroupLength2.Items.Clear();
            cbbAxleGroupLength3.Items.Clear();
            cbbAxleGroupLength1.Items.Add("Trục đơn 1");
            cbbAxleGroupLength2.Items.Add("Trục kép 2 d > 1,3");
            cbbAxleGroupLength1.SelectedIndex = 0;
            cbbAxleGroupLength2.SelectedIndex = 0;
            check = 2;
            pbRegulation.Image = pbReImg2.Image;
            tbstructure.Text = "Đơn-Kép";
            data_Xe_final.Kieuxe = check.ToString();
            txtdai2.Enabled = false;
            txtrong2.Enabled = false;
            txtcao2.Enabled = false;
            Label29.Text = "TL trục đơn 1";
            Label30.Text = "TL trục kép 1";
            Label4.Text = "Tự trọng của xe"; tb_tai_trong_ro_mooc.Enabled = false;
        }

        private void pbReImg3_Click(object sender, EventArgs e)
        {
            lbGrossWeightRe.Text = "24 tấn";
            cbbAxleGroupLength1.Items.Clear();
            cbbAxleGroupLength2.Items.Clear();
            cbbAxleGroupLength3.Items.Clear();
            cbbAxleGroupLength1.Items.Add("Trục đơn 1");
            cbbAxleGroupLength2.Items.Add("Trục đơn 2");
            cbbAxleGroupLength3.Items.Add("Trục đơn 3");
            cbbAxleGroupLength1.SelectedIndex = 0;
            cbbAxleGroupLength2.SelectedIndex = 0;
            cbbAxleGroupLength3.SelectedIndex = 0;
            check = 3;
            pbRegulation.Image = pbReImg3.Image;
            tbstructure.Text = "Đơn-Đơn-Đơn";
            data_Xe_final.Kieuxe = check.ToString();
            txtdai2.Enabled = false;
            txtrong2.Enabled = false;
            txtcao2.Enabled = false;
            Label29.Text = "TL trục đơn 1";
            Label30.Text = "TL trục đơn 2";
            Label31.Text = "TL trục đơn 3";
            Label4.Text = "Tự trọng của xe"; tb_tai_trong_ro_mooc.Enabled = false;
        }

        private void pbReImg4_Click(object sender, EventArgs e)
        {
            lbGrossWeightRe.Text = "30 tấn";
            cbbAxleGroupLength1.Items.Clear();
            cbbAxleGroupLength2.Items.Clear();
            cbbAxleGroupLength3.Items.Clear();
            cbbAxleGroupLength1.Items.Add("Trục đơn 1");
            cbbAxleGroupLength2.Items.Add("Cụm trục 3");

            cbbAxleGroupLength1.SelectedIndex = 0;
            cbbAxleGroupLength2.SelectedIndex = 0;
            check = 4;
            pbRegulation.Image = pbReImg4.Image;
            tbstructure.Text = "Đơn-Ba";
            data_Xe_final.Kieuxe = check.ToString();
            txtdai2.Enabled = false;
            txtrong2.Enabled = false;
            txtcao2.Enabled = false;
            Label29.Text = "TL trục đơn 1";
            Label30.Text = "TL cụm trục 1";
            Label4.Text = "Tự trọng của xe"; tb_tai_trong_ro_mooc.Enabled = false;
        }

        private void pbReImg5_Click(object sender, EventArgs e)
        {
            lbGrossWeightRe.Text = "30 tấn";
            cbbAxleGroupLength1.Items.Clear();
            cbbAxleGroupLength2.Items.Clear();
            cbbAxleGroupLength3.Items.Clear();
            cbbAxleGroupLength1.Items.Add("Trục đơn 1");
            cbbAxleGroupLength2.Items.Add("Trục đơn 2");
            cbbAxleGroupLength3.Items.Add("Cụm trục 3 d > 1.3m");

            cbbAxleGroupLength1.SelectedIndex = 0;
            cbbAxleGroupLength2.SelectedIndex = 0;
            cbbAxleGroupLength3.SelectedIndex = 0;
            check = 5;

            pbRegulation.Image = pbReImg5.Image;
            tbstructure.Text = "Đơn-Đơn-Kép";
            data_Xe_final.Kieuxe = check.ToString();
            txtdai2.Enabled = false;
            txtrong2.Enabled = false;
            txtcao2.Enabled = false;
            Label29.Text = "TL trục đơn 1";
            Label30.Text = "TL trục đơn 2";
            Label31.Text = "TL trục kép 1";
            Label4.Text = "Tự trọng của xe"; tb_tai_trong_ro_mooc.Enabled = false;
        }

        private void pbReImg6_Click(object sender, EventArgs e)
        {
            lbGrossWeightRe.Text = "34 tấn";
            cbbAxleGroupLength1.Items.Clear();
            cbbAxleGroupLength2.Items.Clear();
            cbbAxleGroupLength3.Items.Clear();
            cbbAxleGroupLength1.Items.Add("Trục đơn 1");
            cbbAxleGroupLength2.Items.Add("Trục đơn 2");
            cbbAxleGroupLength3.Items.Add("Cụm trục 3 d > 1.3m");

            cbbAxleGroupLength1.SelectedIndex = 0;
            cbbAxleGroupLength2.SelectedIndex = 0;
            cbbAxleGroupLength3.SelectedIndex = 0;
            check = 6;

            pbRegulation.Image = pbReImg6.Image;
            tbstructure.Text = "Đơn-Đơn-Ba";
            data_Xe_final.Kieuxe = check.ToString();
            checkBox1.Text = "Chiều dài nhỏ hơn 7m";
            txtdai2.Enabled = false;
            txtrong2.Enabled = false;
            txtcao2.Enabled = false;
            Label29.Text = "TL trục đơn 1";
            Label30.Text = "TL trục đơn 2";
            Label31.Text = "TL cụm trục 3";
            Label4.Text = "Tự trọng của xe"; tb_tai_trong_ro_mooc.Enabled = false;
        }

        private void pbReImg7_Click(object sender, EventArgs e)
        {
            lbGrossWeightRe.Text = "26 tấn";
            cbbAxleGroupLength1.Items.Clear();
            cbbAxleGroupLength2.Items.Clear();
            cbbAxleGroupLength3.Items.Clear();
            cbbAxleGroupLength1.Items.Add("Trục đơn 1");
            cbbAxleGroupLength2.Items.Add("Trục đơn 2");
            cbbAxleGroupLength3.Items.Add("Trục đơn 3");

            cbbAxleGroupLength1.SelectedIndex = 0;
            cbbAxleGroupLength2.SelectedIndex = 0;
            cbbAxleGroupLength3.SelectedIndex = 0;
            check = 7;
            pbRegulation.Image = pbReImg7.Image;
            tbstructure.Text = "Đơn-Đơn-Đơn";
            data_Xe_final.Kieuxe = check.ToString();
            txtdai2.Enabled = true;
            txtrong2.Enabled = true;
            txtcao2.Enabled = true;

            Label29.Text = "TL trục đơn 1";
            Label30.Text = "TL trục đơn 2";
            Label31.Text = "TL trục đơn 3";
            Label4.Text = "Tự trọng đầu kéo"; tb_tai_trong_ro_mooc.Enabled = true;
        }

        private void pbReImg8_Click(object sender, EventArgs e)
        {
            lbGrossWeightRe.Text = "34 tấn";
            cbbAxleGroupLength1.Items.Clear();
            cbbAxleGroupLength2.Items.Clear();
            cbbAxleGroupLength3.Items.Clear();
            cbbAxleGroupLength1.Items.Add("Trục đơn 1");
            cbbAxleGroupLength2.Items.Add("Trục đơn 2");
            cbbAxleGroupLength3.Items.Add("Trục kép 3");

            cbbAxleGroupLength1.SelectedIndex = 0;
            cbbAxleGroupLength2.SelectedIndex = 0;
            cbbAxleGroupLength3.SelectedIndex = 0;
            check = 8;
            pbRegulation.Image = pbReImg8.Image;
            tbstructure.Text = "Đơn-Đơn-Kép";
            data_Xe_final.Kieuxe = check.ToString();
            txtdai2.Enabled = true;
            txtrong2.Enabled = true;
            txtcao2.Enabled = true;

            Label29.Text = "TL trục đơn 1";
            Label30.Text = "TL trục đơn 2";
            Label31.Text = "TL trục kép 1";
            Label4.Text = "Tự trọng đầu kéo"; tb_tai_trong_ro_mooc.Enabled = true;
        }

        private void pbReImg9_Click(object sender, EventArgs e)
        {
            lbGrossWeightRe.Text = "34 tấn";
            cbbAxleGroupLength1.Items.Clear();
            cbbAxleGroupLength2.Items.Clear();
            cbbAxleGroupLength3.Items.Clear();
            cbbAxleGroupLength1.Items.Add("Trục đơn 1");
            cbbAxleGroupLength2.Items.Add("Trục kép 2");
            cbbAxleGroupLength3.Items.Add("Trục đơn 3");

            cbbAxleGroupLength1.SelectedIndex = 0;
            cbbAxleGroupLength2.SelectedIndex = 0;
            cbbAxleGroupLength3.SelectedIndex = 0;
            check = 9;
            pbRegulation.Image = pbReImg9.Image;
            tbstructure.Text = "Đơn-Kép-Đơn";
            data_Xe_final.Kieuxe = check.ToString();
            txtdai2.Enabled = true;
            txtrong2.Enabled = true;
            txtcao2.Enabled = true;
            Label29.Text = "TL trục đơn 1";
            Label30.Text = "TL trục kép 1";
            Label31.Text = "TL trục đơn 3";
            Label4.Text = "Tự trọng đầu kéo"; tb_tai_trong_ro_mooc.Enabled = true;
        }

        private void pbReImg10_Click(object sender, EventArgs e)
        {
            lbGrossWeightRe.Text = "42 tấn";
            cbbAxleGroupLength1.Items.Clear();
            cbbAxleGroupLength2.Items.Clear();
            cbbAxleGroupLength3.Items.Clear();
            cbbAxleGroupLength1.Items.Add("Trục đơn 1");
            cbbAxleGroupLength2.Items.Add("Trục kép 2");
            cbbAxleGroupLength3.Items.Add("Trục kép 3");

            cbbAxleGroupLength1.SelectedIndex = 0;
            cbbAxleGroupLength2.SelectedIndex = 0;
            cbbAxleGroupLength3.SelectedIndex = 0;
            check = 10;
            pbRegulation.Image = pbReImg10.Image;
            tbstructure.Text = "Đơn-Kép-Kép";
            data_Xe_final.Kieuxe = check.ToString();
            txtdai2.Enabled = true;
            txtrong2.Enabled = true;
            txtcao2.Enabled = true;

            Label29.Text = "TL trục đơn 1";
            Label30.Text = "TL trục kép 1";
            Label31.Text = "TL trục kép 2";
            Label4.Text = "Tự trọng đầu kéo"; tb_tai_trong_ro_mooc.Enabled = true;
        }

        private void pbReImg11_Click(object sender, EventArgs e)
        {
            lbGrossWeightRe.Text = "42 tấn";
            cbbAxleGroupLength1.Items.Clear();
            cbbAxleGroupLength2.Items.Clear();
            cbbAxleGroupLength3.Items.Clear();
            cbbAxleGroupLength1.Items.Add("Trục đơn 1");
            cbbAxleGroupLength2.Items.Add("Trục đơn 2");
            cbbAxleGroupLength3.Items.Add("Cụm trục 3");

            cbbAxleGroupLength1.SelectedIndex = 0;
            cbbAxleGroupLength2.SelectedIndex = 0;
            cbbAxleGroupLength3.SelectedIndex = 0;
            check = 11;
            pbRegulation.Image = pbReImg11.Image;
            tbstructure.Text = "Đơn-Đơn-Ba";
            data_Xe_final.Kieuxe = check.ToString();
            checkBox1.Text = "Chiều dài nhỏ hơn 4.5m";
            txtdai2.Enabled = true;
            txtrong2.Enabled = true;
            txtcao2.Enabled = true;

            Label29.Text = "TL trục đơn 1";
            Label30.Text = "TL trục đơn 2";
            Label31.Text = "TL cụm trục 3";
            Label4.Text = "Tự trọng đầu kéo"; tb_tai_trong_ro_mooc.Enabled = true;
        }

        private void pbReImg12_Click(object sender, EventArgs e)
        {
            lbGrossWeightRe.Text = "48 tấn";
            cbbAxleGroupLength1.Items.Clear();
            cbbAxleGroupLength2.Items.Clear();
            cbbAxleGroupLength3.Items.Clear();
            cbbAxleGroupLength1.Items.Add("Trục đơn 1");
            cbbAxleGroupLength2.Items.Add("Cụm trục 2");
            cbbAxleGroupLength3.Items.Add("Cụm trục 3");

            cbbAxleGroupLength1.SelectedIndex = 0;
            cbbAxleGroupLength2.SelectedIndex = 0;
            cbbAxleGroupLength3.SelectedIndex = 0;
            check = 12;
            pbRegulation.Image = pbReImg12.Image;
            tbstructure.Text = "Đơn-Kép-Ba";
            data_Xe_final.Kieuxe = check.ToString();
            checkBox1.Text = "Chiều dài nhỏ hơn 6.5m";
            txtdai2.Enabled = true;
            txtrong2.Enabled = true;
            txtcao2.Enabled = true;

            Label29.Text = "TL trục đơn 1";
            Label30.Text = "TL trục kép 1";
            Label31.Text = "TL cụm trục 3";
            Label4.Text = "Tự trọng đầu kéo"; tb_tai_trong_ro_mooc.Enabled = true;
        }

        private void btPrinter_Click(object sender, EventArgs e)
        {
            Thread thread2 = new Thread(new ThreadStart(inphieu));
            thread2.Start();
            try
            {
                User.savelogdata("In phiếu cân");
            }
            catch { }
            
        }
        private void inphieu()
        {
            string filepath = @"Exel/Phieucan1A.xlsx";
            if (!File.Exists(filepath))
            {
                MessageBox.Show("Phiếu in không tồn tại");
                return;
            }
            // xuất exel
            var workbook = new XLWorkbook(filepath);
            var ws1 = workbook.Worksheet(1);
            ws1.Cell($"A{1}").Value = User.tentinh;
            ws1.Cell($"F{1}").Value = User.tentramcan;
            ws1.Cell($"I{2}").Value = User.lytrinh;
            ws1.Cell($"I{1}").Value = "STT:"+Declare.STT;
            ws1.Cell($"F{2}").Value = "Số phiếu cân:"+ data_Xe_final.Thoigian?.ToString("ddMMyyy_hhmmss");

            ws1.Cell($"A{6}").Value = "BKS xe ô tô: "+ tbLicensePlate.Text.ToString();
            //ws1.Cell($"D{6}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws1.Cell($"F{6}").Value = "BKS SMRM/RM: "+ tbLicensePlate1.Text.ToString();
           // ws1.Cell($"I{6}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws1.Cell($"A{7}").Value =check < 7 ? "Loại xe: Xe thân liền" : "Loại xe: Tổ hợp xe";
           // ws1.Cell($"B{7}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            ws1.Cell($"D{7}").Value = "Tổng số trục: " +Utility.gettrucxe(check).ToString();
            //ws1.Cell($"E{7}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            ws1.Cell($"F{7}").Value = "Mầu xe phía trước: "+ txtmauxe.Text.ToString();
            //ws1.Cell($"G{7}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            ws1.Cell($"A{8}").Value = "Tên chủ xe, địa chỉ: "+ txtchuxe.Text.ToString();
            //ws1.Cell($"C{8}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            ws1.Cell($"A{9}").Value = "Họ tên lái xe: "+tbDriver.Text.ToString();
            //ws1.Cell($"C{9}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            ws1.Cell($"E{9}").Value = "Số GPLX: "+txtsgp.Text.ToString();
           // ws1.Cell($"F{9}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            ws1.Cell($"H{9}").Value = "Số GPLHX: "+txtsgplh.Text.ToString();
            //ws1.Cell($"I{9}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            ws1.Cell($"A{10}").Value = "Tốc độ xe qua cân[km/h]: "+ data_Xe_final.tocdo;
            //ws1.Cell($"C{10}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            ws1.Cell($"D{10}").Value = "Thời gian vào: " + data_Xe_final.Thoigian?.ToString("HH:mm:ss");

            //ws1.Cell($"E{10}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            ws1.Cell($"F{10}").Value = "Ngày cân: " + data_Xe_final.Thoigian?.ToString("dd/MM/yyyy");

            //ws1.Cell($"G{10}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            ws1.Cell($"A{12}").Value = "Khối lượng bản thân của ô tô [tấn]: "+ Utility.KilogramsToTons(Int32.Parse(tb_tai_trong_dau_keo.Text.ToString()));
           // ws1.Cell($"E{12}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            ws1.Cell($"F{12}").Value = "Khối lượng bản thân của SMRM/RM [tấn]: " + Utility.KilogramsToTons(Int32.Parse(tb_tai_trong_ro_mooc.Text.ToString()));
            //ws1.Cell($"J{12}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            ws1.Cell($"H{13}").Value = tb_so_nguoi.Text.ToString();
            //ws1.Cell($"H{13}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            ws1.Cell($"H{14}").Value = Utility.KilogramsToTons(Int32.Parse(tb_tai_trong_cho_phep.Text.ToString()));
            // ws1.Cell($"H{14}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            ws1.Cell($"H{10}").Value = "Chế độ cân: "+Declare.Chedocan;
            if (check < 7)
            {
                ws1.Cell($"B{16}").Value = txtdai.Text.ToString() + "x" + txtrong.Text.ToString() + "x" + txtcao.Text.ToString();
                ws1.Cell($"B{16}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                ws1.Cell($"E{16}").Value = txtdai1.Text.ToString() + "x" + txtrong1.Text.ToString() + "x" + txtcao1.Text.ToString();
                ws1.Cell($"E{16}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                ws1.Cell($"H{16}").Value = txtchieudai.Text.ToString();
                ws1.Cell($"H{16}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            }
            else
            {
                ws1.Cell($"B{16}").Value = txtdai.Text.ToString() + "x" + txtrong.Text.ToString() + "x" + txtcao.Text.ToString();
                ws1.Cell($"B{16}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                ws1.Cell($"E{16}").Value = txtdai1.Text.ToString() + "x" + txtrong1.Text.ToString() + "x" + txtcao1.Text.ToString();
                ws1.Cell($"E{16}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                ws1.Cell($"H{16}").Value = txtchieudai.Text.ToString();
                ws1.Cell($"H{16}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;



                ws1.Cell($"B{17}").Value = txtdai2.Text.ToString() + "x" + txtrong2.Text.ToString() + "x" + txtcao2.Text.ToString();
                ws1.Cell($"B{17}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                ws1.Cell($"E{17}").Value = txtdai1.Text.ToString() + "x" + txtrong1.Text.ToString() + "x" + txtcao1.Text.ToString();
                ws1.Cell($"E{17}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                ws1.Cell($"H{17}").Value = txtchieudai.Text.ToString();
                ws1.Cell($"H{17}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            }
            if (check == 1)
            {

                ws1.Cell($"E{21}").Value = Utility.KilogramsToTons(Int32.Parse(tbAxleGroupWeight1.Text.ToString())).ToString();
                ws1.Cell($"E{21}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws1.Cell($"E{22}").Value = Utility.KilogramsToTons(Int32.Parse(tbAxleGroupWeight2.Text.ToString())).ToString();
                ws1.Cell($"E{22}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws1.Cell($"H{21}").Value = "10";
                ws1.Cell($"H{21}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws1.Cell($"H{22}").Value = "10";
                ws1.Cell($"H{22}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws1.Cell($"H{26}").Value = "16";
                ws1.Cell($"H{26}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            }
            if (check == 2)
            {
                ws1.Cell($"E{21}").Value = Utility.KilogramsToTons(Int32.Parse(tbAxleGroupWeight1.Text.ToString())).ToString();
                ws1.Cell($"E{21}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws1.Cell($"E{23}").Value = Utility.KilogramsToTons(Int32.Parse(tbAxleGroupWeight2.Text.ToString())).ToString();
                ws1.Cell($"E{23}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws1.Cell($"H{21}").Value = "10";
                ws1.Cell($"H{21}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws1.Cell($"H{23}").Value = "18";
                ws1.Cell($"H{23}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws1.Cell($"H{26}").Value = "24";
                ws1.Cell($"H{26}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            }
            if (check == 3)
            {
                ws1.Cell($"E{21}").Value = Utility.KilogramsToTons(Int32.Parse(tbAxleGroupWeight1.Text.ToString())).ToString();
                ws1.Cell($"E{21}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws1.Cell($"E{22}").Value = Utility.KilogramsToTons(Int32.Parse(tbAxleGroupWeight2.Text.ToString())).ToString();
                ws1.Cell($"E{22}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws1.Cell($"E{25}").Value = Utility.KilogramsToTons(Int32.Parse(tbAxleGroupWeight3.Text.ToString())).ToString();
                ws1.Cell($"E{25}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                ws1.Cell($"H{21}").Value = "10";
                ws1.Cell($"H{21}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws1.Cell($"H{22}").Value = "10";
                ws1.Cell($"H{22}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws1.Cell($"H{25}").Value = "10";
                ws1.Cell($"H{25}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws1.Cell($"H{26}").Value = "24";
                ws1.Cell($"H{26}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            }
            if (check == 4)
            {
                ws1.Cell($"E{21}").Value = Utility.KilogramsToTons(Int32.Parse(tbAxleGroupWeight1.Text.ToString())).ToString();
                ws1.Cell($"E{21}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws1.Cell($"E{25}").Value = Utility.KilogramsToTons(Int32.Parse(tbAxleGroupWeight2.Text.ToString())).ToString();
                ws1.Cell($"E{25}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;


                ws1.Cell($"H{21}").Value = "10";
                ws1.Cell($"H{21}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws1.Cell($"H{25}").Value = "24";
                ws1.Cell($"H{25}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                ws1.Cell($"H{26}").Value = "30";
                ws1.Cell($"H{26}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            }
            if (check == 5)
            {
                ws1.Cell($"E{21}").Value = Utility.KilogramsToTons(Int32.Parse(tbAxleGroupWeight1.Text.ToString())).ToString();
                ws1.Cell($"E{21}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws1.Cell($"E{22}").Value = Utility.KilogramsToTons(Int32.Parse(tbAxleGroupWeight2.Text.ToString())).ToString();
                ws1.Cell($"E{22}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws1.Cell($"E{23}").Value = Utility.KilogramsToTons(Int32.Parse(tbAxleGroupWeight3.Text.ToString())).ToString();
                ws1.Cell($"E{23}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                ws1.Cell($"H{21}").Value = "10";
                ws1.Cell($"H{21}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws1.Cell($"H{22}").Value = "10";
                ws1.Cell($"H{22}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws1.Cell($"H{23}").Value = "18";
                ws1.Cell($"H{23}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws1.Cell($"H{26}").Value = "30";
                ws1.Cell($"H{26}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            }
            if (check == 6)
            {
                ws1.Cell($"E{21}").Value = Utility.KilogramsToTons(Int32.Parse(tbAxleGroupWeight1.Text.ToString())).ToString();
                ws1.Cell($"E{21}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws1.Cell($"E{22}").Value = Utility.KilogramsToTons(Int32.Parse(tbAxleGroupWeight2.Text.ToString())).ToString();
                ws1.Cell($"E{22}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws1.Cell($"E{25}").Value = Utility.KilogramsToTons(Int32.Parse(tbAxleGroupWeight3.Text.ToString())).ToString();
                ws1.Cell($"E{25}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                ws1.Cell($"H{21}").Value = "10";
                ws1.Cell($"H{21}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws1.Cell($"H{22}").Value = "10";
                ws1.Cell($"H{22}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws1.Cell($"H{25}").Value = "10";
                ws1.Cell($"H{25}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                ws1.Cell($"H{26}").Value = "34";
                ws1.Cell($"H{26}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            }
            if (check == 7)
            {
                ws1.Cell($"E{21}").Value = Utility.KilogramsToTons(Int32.Parse(tbAxleGroupWeight1.Text.ToString())).ToString();
                ws1.Cell($"E{21}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws1.Cell($"E{22}").Value = Utility.KilogramsToTons(Int32.Parse(tbAxleGroupWeight2.Text.ToString())).ToString();
                ws1.Cell($"E{22}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws1.Cell($"E{25}").Value = Utility.KilogramsToTons(Int32.Parse(tbAxleGroupWeight3.Text.ToString())).ToString();
                ws1.Cell($"E{25}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                ws1.Cell($"H{21}").Value = "10";
                ws1.Cell($"H{21}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws1.Cell($"H{22}").Value = "10";
                ws1.Cell($"H{22}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws1.Cell($"H{25}").Value = "10";
                ws1.Cell($"H{25}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                ws1.Cell($"H{26}").Value = "26";
                ws1.Cell($"H{26}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            }
            if (check == 8)
            {
                ws1.Cell($"E{21}").Value = Utility.KilogramsToTons(Int32.Parse(tbAxleGroupWeight1.Text.ToString())).ToString();
                ws1.Cell($"E{21}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws1.Cell($"E{22}").Value = Utility.KilogramsToTons(Int32.Parse(tbAxleGroupWeight2.Text.ToString())).ToString();
                ws1.Cell($"E{22}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws1.Cell($"E{23}").Value = Utility.KilogramsToTons(Int32.Parse(tbAxleGroupWeight3.Text.ToString())).ToString();
                ws1.Cell($"E{23}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                ws1.Cell($"H{21}").Value = "10";
                ws1.Cell($"H{21}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws1.Cell($"H{22}").Value = "10";
                ws1.Cell($"H{22}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws1.Cell($"H{23}").Value = "18";
                ws1.Cell($"H{23}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                ws1.Cell($"H{26}").Value = "34";
                ws1.Cell($"H{26}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            }
            if (check == 9)
            {
                ws1.Cell($"E{21}").Value = Utility.KilogramsToTons(Int32.Parse(tbAxleGroupWeight1.Text.ToString())).ToString();
                ws1.Cell($"E{21}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws1.Cell($"E{23}").Value = Utility.KilogramsToTons(Int32.Parse(tbAxleGroupWeight2.Text.ToString())).ToString();
                ws1.Cell($"E{23}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws1.Cell($"E{25}").Value = Utility.KilogramsToTons(Int32.Parse(tbAxleGroupWeight3.Text.ToString())).ToString();
                ws1.Cell($"E{25}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                ws1.Cell($"H{21}").Value = "10";
                ws1.Cell($"H{21}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws1.Cell($"H{23}").Value = "18";
                ws1.Cell($"H{23}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws1.Cell($"H{25}").Value = "10";
                ws1.Cell($"H{25}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                ws1.Cell($"H{26}").Value = "34";
                ws1.Cell($"H{26}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            }
            if (check == 10)
            {
                ws1.Cell($"E{21}").Value = Utility.KilogramsToTons(Int32.Parse(tbAxleGroupWeight1.Text.ToString())).ToString();
                ws1.Cell($"E{21}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws1.Cell($"E{23}").Value = Utility.KilogramsToTons(Int32.Parse(tbAxleGroupWeight2.Text.ToString())).ToString();
                ws1.Cell($"E{23}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws1.Cell($"E{24}").Value = Utility.KilogramsToTons(Int32.Parse(tbAxleGroupWeight3.Text.ToString())).ToString();
                ws1.Cell($"E{24}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                ws1.Cell($"H{21}").Value = "10";
                ws1.Cell($"H{21}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws1.Cell($"H{23}").Value = "18";
                ws1.Cell($"H{23}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws1.Cell($"H{24}").Value = "18";
                ws1.Cell($"H{24}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                ws1.Cell($"H{26}").Value = "42";
                ws1.Cell($"H{26}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            }
            if (check == 11)
            {
                ws1.Cell($"E{21}").Value = Utility.KilogramsToTons(Int32.Parse(tbAxleGroupWeight1.Text.ToString())).ToString();
                ws1.Cell($"E{21}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws1.Cell($"E{22}").Value = Utility.KilogramsToTons(Int32.Parse(tbAxleGroupWeight2.Text.ToString())).ToString();
                ws1.Cell($"E{22}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws1.Cell($"E{25}").Value = Utility.KilogramsToTons(Int32.Parse(tbAxleGroupWeight3.Text.ToString())).ToString();
                ws1.Cell($"E{25}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                ws1.Cell($"H{21}").Value = "10";
                ws1.Cell($"H{21}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws1.Cell($"H{22}").Value = "10";
                ws1.Cell($"H{22}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                string text = lbGrossWeightRe.Text.ToString();
                if (text.StartsWith("42"))
                {
                    ws1.Cell($"H{25}").Value = "24";
                    ws1.Cell($"H{25}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    ws1.Cell($"H{26}").Value = "42";
                    ws1.Cell($"H{26}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                }
                else
                {
                    ws1.Cell($"H{25}").Value = "21";
                    ws1.Cell($"H{25}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    ws1.Cell($"H{26}").Value = "38";
                    ws1.Cell($"H{26}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                }
            }
            if (check == 12)
            {
                ws1.Cell($"E{21}").Value = Utility.KilogramsToTons(Int32.Parse(tbAxleGroupWeight1.Text.ToString())).ToString();
                ws1.Cell($"E{21}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws1.Cell($"E{23}").Value = Utility.KilogramsToTons(Int32.Parse(tbAxleGroupWeight2.Text.ToString())).ToString();
                ws1.Cell($"E{23}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws1.Cell($"E{25}").Value = Utility.KilogramsToTons(Int32.Parse(tbAxleGroupWeight3.Text.ToString())).ToString();
                ws1.Cell($"E{25}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                ws1.Cell($"H{21}").Value = "10";
                ws1.Cell($"H{21}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws1.Cell($"H{23}").Value = "18";
                ws1.Cell($"H{23}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                string text = lbGrossWeightRe.Text.ToString();
                if (text.StartsWith("48"))
                {
                    ws1.Cell($"H{25}").Value = "24";
                    ws1.Cell($"H{25}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    ws1.Cell($"H{26}").Value = "48";
                    ws1.Cell($"H{26}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                }
                else
                {
                    ws1.Cell($"H{25}").Value = "21";
                    ws1.Cell($"H{25}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    ws1.Cell($"H{26}").Value = "44";
                    ws1.Cell($"H{26}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                }
            }
            int a = (Int32.Parse(tb_so_nguoi.Text.Trim()) * Declare.Cannang)  + Int32.Parse(tb_tai_trong_dau_keo.Text.Trim()) + Int32.Parse(tb_tai_trong_ro_mooc.Text.Trim());
            int b = (int)(data_Xe_final.TTLtruc * 0.95);
            ws1.Cell($"A{30}").Value = Utility.KilogramsToTons(b - a).ToString();
            ws1.Cell($"A{30}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws1.Cell($"G{30}").Value = tbLicenseGrossWeightOver.Text.ToString();
            ws1.Cell($"G{30}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            string tbAxleGroupOve1 = tbAxleGroupOver1.Text.ToString();
            string tbAxleGroupOve2 = tbAxleGroupOver2.Text.ToString();
            string tbAxleGroupOve3 = tbAxleGroupOver3.Text.ToString();
            string tbAxleGroupOve4 = tbGrossWeightOver.Text.ToString();
            string tbAxleGroupOve5 = tbLicenseGrossWeightOver.Text.ToString();

            if (tbAxleGroupOve1.StartsWith("0") && tbAxleGroupOve2.StartsWith("0") && tbAxleGroupOve3.StartsWith("0") && tbAxleGroupOve4.StartsWith("0") && tbAxleGroupOve5.StartsWith("0"))
            {
                ws1.Cell($"A{35}").Value = "Xe không vi phạm";
            }
            else
            {
                if (!tbAxleGroupOve4.StartsWith("0"))
                {
                    ws1.Cell($"A{35}").Value = "Xe vượt tổng trọng lượng cho phép của cầu, đường: " + tbAxleGroupOve4;
                }
                if (!tbAxleGroupOve1.StartsWith("0") || !tbAxleGroupOve2.StartsWith("0") || !tbAxleGroupOve3.StartsWith("0"))
                {
                    string largestValue = tbAxleGroupOve1;
                    if (tbAxleGroupOve2.CompareTo(largestValue) > 0)
                    {
                        largestValue = tbAxleGroupOve2;
                    }

                    if (tbAxleGroupOve3.CompareTo(largestValue) > 0)
                    {
                        largestValue = tbAxleGroupOve3;
                    }
                    if (largestValue != "")
                    {
                        ws1.Cell($"A{36}").Value = "Xe vượt tải trọng trục cho phép, đường: " + largestValue;
                    }
                }
                if (!tbAxleGroupOve5.StartsWith("0"))
                {
                    ws1.Cell($"A{37}").Value = "Xe vượt khối lượng hàng CC CPTGGT: " + tbAxleGroupOve5 ;
                }
            }
            if (hinhtruoc != null)
            {
                var imageStream = new MemoryStream();
                hinhtruoc.Save(imageStream, ImageFormat.Png);
                var cellA5 = ws1.Cell("A5");
                var picture = cellA5.Worksheet.AddPicture(imageStream).MoveTo(cellA5);
                picture.WithSize(350, 240);
            }
            if (hinhsau != null)
            {
                var imageStream1 = new MemoryStream();
                hinhsau.Save(imageStream1, ImageFormat.Png);
                var cellA51 = ws1.Cell("F5");
                var picture1 = cellA51.Worksheet.AddPicture(imageStream1).MoveTo(cellA51);
                picture1.WithSize(350, 240);
            }
            User.pathexel = "D:\\Phieucan\\" + "phieucan_" + DateTime.Now.ToString("ddMMyyy_hhmmss") + ".xlsx";
            workbook.SaveAs(User.pathexel);
            workbook.Dispose();
            lb_exel.Invoke((MethodInvoker)delegate {
                lb_exel.Visible = true;
            });
            try
            {
                Savedata_sql();
            }
            catch { }
            MessageBox.Show("IN HOÀN THÀNH");
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            string text = lbGrossWeightRe.Text.ToString();
            if (check == 12 && text.StartsWith("48"))
            {
                lbGrossWeightRe.Text = "44 tấn";
            }
            if (check == 12 && text.StartsWith("44"))
            {
                lbGrossWeightRe.Text = "48 tấn";
            }
            if (check == 11 && text.StartsWith("42"))
            {
                lbGrossWeightRe.Text = "38 tấn";
            }
            if (check == 11 && text.StartsWith("38"))
            {
                lbGrossWeightRe.Text = "42 tấn";
            }
            if (check == 6 && text.StartsWith("34"))
            {
                lbGrossWeightRe.Text = "32 tấn";
            }
            if (check == 6 && text.StartsWith("32"))
            {
                lbGrossWeightRe.Text = "34 tấn";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            UpdatePictureBoxImage(pbAxle1, Utility.drawImageOnImage(aiCameraPlayer1.CurrentImage(), aiCameraPlayer.CurrentImage()));
            UpdatePictureBoxImage(pbAxle2, aiCameraPlayer1.CurrentImage());
            hinhtruoc = new Bitmap(Utility.drawImageOnImage(aiCameraPlayer1.CurrentImage(), aiCameraPlayer.CurrentImage()));

            string path = Declare.filepathsaveimage + DateTime.Now.ToString("ddMMyyyy") + "\\" + DateTime.Now.ToString("ddMMyyyy_HHmmss")+ "_HT.jpeg";
            Utility.checkExistingFolderFromFilePath(path);
            data_Xe_final.hinhtruoc = path;
            hinhtruoc.Save(path, ImageFormat.Jpeg);
        }
        private void button4_Click(object sender, EventArgs e)
        {
            UpdatePictureBoxImage(pbAxle3, Utility.drawImageOnImage(aiCameraPlayer3.CurrentImage(), aiCameraPlayer2.CurrentImage()));
           // UpdatePictureBoxImage(pbAxle3,aiCameraPlayer2.CurrentImage());

            UpdatePictureBoxImage(pbAxle4, aiCameraPlayer3.CurrentImage());
            hinhsau = new Bitmap(Utility.drawImageOnImage(aiCameraPlayer3.CurrentImage(), aiCameraPlayer2.CurrentImage()));
            //hinhsau = new Bitmap(aiCameraPlayer2.CurrentImage());
            string path = Declare.filepathsaveimage + DateTime.Now.ToString("ddMMyyyy") + "\\" + DateTime.Now.ToString("ddMMyyyy_HHmmss") + "_HS.jpeg";
            Utility.checkExistingFolderFromFilePath(path);
            data_Xe_final.hinhsau = path;
            hinhsau.Save(path, ImageFormat.Jpeg);
        }
        private void UpdatePictureBoxImage(PictureBox pictureBox, Bitmap image)
        {
            if (pictureBox.InvokeRequired)
            {
                pictureBox.Invoke(new Action<PictureBox, Bitmap>(UpdatePictureBoxImage), pictureBox, image);
            }
            else
            {
                pictureBox.Image = image;
            }
        }
        private void btCancel_Click(object sender, EventArgs e)
        {
            ClearTextFromAnotherThread1();
            try
            {
                User.savelogdata("Hủy kết quả cân xe:" + data_Xe_final.Biensotruoc);
            }
            catch {  }
          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            ClearTextFromAnotherThread();
            try
            {
                User.savelogdata("Hủy thông tin xe:" + data_Xe_final.Biensotruoc);
            }
            catch { }
        }
        private void tbLicensePlate_TextChanged(object sender, EventArgs e)
        {
            data_Xe_final.Biensotruoc=tbLicensePlate.Text;
        }
       
        private void tbLicensePlate1_TextChanged(object sender, EventArgs e)
        {
            data_Xe_final.Biensosau = tbLicensePlate1.Text;
        }

        private void txtmauxe_TextChanged(object sender, EventArgs e)
        {
            data_Xe_final.Mauxe = txtmauxe.Text;
        }

        private void tbDriver_TextChanged(object sender, EventArgs e)
        {
            data_Xe_final.Laixe = tbDriver.Text;
        }

        private void txtchuxe_TextChanged(object sender, EventArgs e)
        {
            data_Xe_final.Chuxe = txtchuxe.Text;
        }

        private void txtsgp_TextChanged(object sender, EventArgs e)
        {
            data_Xe_final.Sogplx = txtsgp.Text;
        }

        private void txtsgplh_TextChanged(object sender, EventArgs e)
        {
            data_Xe_final.Sogplhx = txtsgplh.Text;
        }

        private void tb_so_nguoi_TextChanged(object sender, EventArgs e)
        {
            data_Xe_final.Songuoitrenxe = Utility.StringToInt(tb_so_nguoi.Text);
        }

        private void tb_tai_trong_dau_keo_TextChanged(object sender, EventArgs e)
        {
            data_Xe_final.Taitrongdaukeo = Utility.StringToInt(tb_tai_trong_dau_keo.Text);
        }

        private void tb_tai_trong_ro_mooc_TextChanged(object sender, EventArgs e)
        {
            data_Xe_final.Taitrongromoc = Utility.StringToInt(tb_tai_trong_ro_mooc.Text);
        }

        private void tb_tai_trong_cho_phep_TextChanged(object sender, EventArgs e)
        {
            data_Xe_final.Taitrongchophep = Utility.StringToInt(tb_tai_trong_cho_phep.Text);
        }

        private void txtdai_TextChanged(object sender, EventArgs e)
        {
            data_Xe_final.KT_oto_dai = Utility.StringToInt(txtdai.Text);
        }

        private void txtrong_TextChanged(object sender, EventArgs e)
        {
            data_Xe_final.KT_oto_rong = Utility.StringToInt(txtrong.Text);
        }

        private void txtcao_TextChanged(object sender, EventArgs e)
        {
            data_Xe_final.KT_oto_cao = Utility.StringToInt(txtcao.Text);
        }

        private void txtdai2_TextChanged(object sender, EventArgs e)
        {
            data_Xe_final.KT_rm_dai = Utility.StringToInt(txtdai2.Text);
        }

        private void txtrong2_TextChanged(object sender, EventArgs e)
        {
            data_Xe_final.KT_rm_rong = Utility.StringToInt(txtrong2.Text);
        }

        private void txtcao2_TextChanged(object sender, EventArgs e)
        {
            data_Xe_final.KT_rm_cao = Utility.StringToInt(txtcao2.Text);
        }

        private void txtdai1_TextChanged(object sender, EventArgs e)
        {
            data_Xe_final.KTthunghang_dai = Utility.StringToInt(txtdai1.Text);
        }

        private void txtrong1_TextChanged(object sender, EventArgs e)
        {
            data_Xe_final.KTthunghang_rong = Utility.StringToInt(txtrong1.Text);
        }

        private void txtcao1_TextChanged(object sender, EventArgs e)
        {
            data_Xe_final.KTthunghang_cao = Utility.StringToInt(txtcao1.Text);
        }

        private void tbLicenseGrossWeight_TextChanged(object sender, EventArgs e)
        {
            data_Xe_final.TLgiayphep = Utility.StringToInt(tbLicenseGrossWeight.Text);
        }

        private void tbLicenseGrossWeightOver_TextChanged(object sender, EventArgs e)
        {
            data_Xe_final.Quataitheogp = tbLicenseGrossWeightOver.Text;
        }
        private void txtchieudai_TextChanged(object sender, EventArgs e)
        {
            data_Xe_final.Chieudaicoso = txtchieudai.Text;
        }
        private void button6_Click(object sender, EventArgs e)
        {
            return;// đang dùng ko sql nên ko tìm
            string bienso = tbLicensePlate.Text.Trim();
            using (var dtxe=new database())
            {
                var data = dtxe.tbl_Data_Xe.Where(n => n.Biensotruoc.Contains(bienso)).ToList();
                if(data.Count > 0)
                {
                    if(data.Count == 1)
                    {
                        tbLicensePlate.Text = data[0].Biensotruoc;
                        tbLicensePlate1.Text = data[0].Biensosau;
                        txtmauxe.Text = data[0].Mauxe;
                        tbDriver.Text = data[0].Laixe;
                        txtchuxe.Text = data[0].Chuxe;
                        txtsgplh.Text = data[0].Sogplhx;
                        tb_so_nguoi.Text = data[0].Songuoitrenxe.ToString();
                        tb_tai_trong_dau_keo.Text = data[0].Taitrongdaukeo.ToString();
                        tb_tai_trong_ro_mooc.Text = data[0].Taitrongromoc.ToString();
                        tb_tai_trong_cho_phep.Text = data[0].Taitrongchophep.ToString();
                        txtdai.Text = data[0].KT_oto_dai.ToString();
                        txtrong.Text = data[0].KT_oto_rong.ToString();
                        txtcao.Text = data[0].KT_oto_cao.ToString();
                        txtdai1.Text = data[0].KT_rm_dai.ToString();
                        txtrong1.Text = data[0].KT_rm_rong.ToString();
                        txtcao1.Text = data[0].KT_rm_cao.ToString();
                        txtdai2.Text = data[0].KTthunghang_dai.ToString();
                        txtrong2.Text = data[0].KTthunghang_rong.ToString();
                        txtcao2.Text = data[0].KTthunghang_cao.ToString();
                        txtchieudai.Text = data[0].Chieudaicoso.ToString();
                        tbAxleGroupWeight1.Text = data[0].TLtruc1.ToString();
                        tbAxleGroupWeight2.Text = data[0].TLtruc2.ToString();
                        tbAxleGroupWeight3.Text = data[0].TLtruc3.ToString();
                        tbGrossWeight.Text = data[0].TTLtruc.ToString();
                        tbLicenseGrossWeight.Text = data[0].TLgiayphep.ToString();
                        tbAxleGroupOver1.Text = data[0].Quataitruc1.ToString();
                        tbAxleGroupOver2.Text = data[0].Quataitruc2.ToString();
                        tbAxleGroupOver3.Text = data[0].Quataitruc3.ToString();
                        tbGrossWeightOver.Text = data[0].Quataitong.ToString();
                        tbLicenseGrossWeightOver.Text = data[0].Quataitheogp.ToString();
                        txt_speed.Text = data[0].tocdo.ToString();
                        string Sogplx = data[0].Sogplx;
                        if (Sogplx.Contains("#"))
                        {
                            string[] parts = Sogplx.Split('#');
                            txtsgp.Text = parts[0];
                            Declare.STT= parts[1];
                        }
                        else
                        {
                            txtsgp.Text = data[0].Sogplx;
                        }
                        check = int.Parse(data[0].Kieuxe.ToString());
                        clickpicture(check);
                        //hình chưa có
                        pbAxle1.Image = Utility.getImageFromFile(data[0].hinhtruoc.ToString());
                        pbAxle3.Image = Utility.getImageFromFile(data[0].hinhsau.ToString());

                        hinhtruoc = Utility.getBitmapFromFile(data[0].hinhtruoc.ToString());
                        hinhsau = Utility.getBitmapFromFile(data[0].hinhsau.ToString());

                        data_Xe_final.tocdo = data[0].tocdo;
                        data_Xe_final.Thoigian = data[0].Thoigian;
                        return;
                    }
                    else
                    {
                        dataGridView1.Rows.Clear();
                        foreach (var dt in data)
                        {
                            this.dataGridView1.Rows.Add(
                                 dt.Id,
                                dt.Biensotruoc,
                                dt.Thoigian
                            );
                        }
                        return;
                    }
                }
                MessageBox.Show("Không tìm thấy thông tin xe");
                return;
            }
        }

        private void Label28_Click(object sender, EventArgs e)
        {
            button6_Click(null,null);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int id;
                if (dataGridView1.Rows[e.RowIndex].Cells["ID"].Value != null &&
                    int.TryParse(dataGridView1.Rows[e.RowIndex].Cells["ID"].Value.ToString(), out id))
                {
                    using (var dtxe = new database())
                    {
                        var data = dtxe.tbl_Data_Xe.Where(n => n.Id == id).FirstOrDefault();
                        tbLicensePlate.Text = data.Biensotruoc;
                        tbLicensePlate1.Text = data.Biensosau;
                        txtmauxe.Text = data.Mauxe;
                        tbDriver.Text = data.Laixe;
                        txtchuxe.Text = data.Chuxe;
                        txtsgplh.Text = data.Sogplhx;
                        tb_so_nguoi.Text = data.Songuoitrenxe.ToString();
                        tb_tai_trong_dau_keo.Text = data.Taitrongdaukeo.ToString();
                        tb_tai_trong_ro_mooc.Text = data.Taitrongromoc.ToString();
                        tb_tai_trong_cho_phep.Text = data.Taitrongchophep.ToString();
                        txtdai.Text = data.KT_oto_dai.ToString();
                        txtrong.Text = data.KT_oto_rong.ToString();
                        txtcao.Text = data.KT_oto_cao.ToString();
                        txtdai1.Text = data.KT_rm_dai.ToString();
                        txtrong1.Text = data.KT_rm_rong.ToString();
                        txtcao1.Text = data.KT_rm_cao.ToString();
                        txtdai2.Text = data.KTthunghang_dai.ToString();
                        txtrong2.Text = data.KTthunghang_rong.ToString();
                        txtcao2.Text = data.KTthunghang_cao.ToString();
                        txtchieudai.Text = data.Chieudaicoso.ToString();
                        tbAxleGroupWeight1.Text = data.TLtruc1.ToString();
                        tbAxleGroupWeight2.Text = data.TLtruc2.ToString();
                        tbAxleGroupWeight3.Text = data.TLtruc3.ToString();
                        tbGrossWeight.Text = data.TTLtruc.ToString();
                        tbLicenseGrossWeight.Text = data.TLgiayphep.ToString();
                        tbAxleGroupOver1.Text = data.Quataitruc1.ToString();
                        tbAxleGroupOver2.Text = data.Quataitruc2.ToString();
                        tbAxleGroupOver3.Text = data.Quataitruc3.ToString();
                        tbGrossWeightOver.Text = data.Quataitong.ToString();
                        tbLicenseGrossWeightOver.Text = data.Quataitheogp.ToString();
                        txt_speed.Text = data.tocdo.ToString();
                        string Sogplx = data.Sogplx;
                        if (Sogplx.Contains("#"))
                        {
                            string[] parts = Sogplx.Split('#');
                            txtsgp.Text = parts[0];
                            Declare.STT = parts[1];
                        }
                        else
                        {
                            txtsgp.Text = data.Sogplx;
                        }
                        check = int.Parse(data.Kieuxe.ToString());
                        clickpicture(check);
                        //hình chưa có
                        pbAxle1.Image = Utility.getImageFromFile(data.hinhtruoc.ToString());
                        pbAxle3.Image = Utility.getImageFromFile(data.hinhsau.ToString());

                        hinhtruoc = Utility.getBitmapFromFile(data.hinhtruoc.ToString());
                        hinhsau = Utility.getBitmapFromFile(data.hinhsau.ToString());

                        data_Xe_final.tocdo = data.tocdo;
                        data_Xe_final.Thoigian = data.Thoigian;
                        return;
                    }
                }
                return;
            }
        }

        private void lb_exel_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(User.pathexel))
            {
                Process.Start(User.pathexel);
            }
        }
        private void clickpicture(int check)
        {
            switch (check)
            {
                case 1:
                    pbReImg1_Click(null,null);
                    return;
                    case 2:
                    pbReImg2_Click(null, null);
                    return;
                    case 3:
                    pbReImg3_Click(null, null);
                    return;
                    case 4:
                    pbReImg4_Click(null, null);
                    return;
                    case 5:
                    pbReImg5_Click(null, null);
                    return;
                    case 6: 
                    pbReImg6_Click(null, null);
                    return;
                    case 7: 
                    pbReImg7_Click(null, null);
                    return;
                    case 8:
                    pbReImg8_Click(null, null);
                    return;
                    case 9:
                    pbReImg9_Click(null, null);
                    return;
                    case 10:
                    pbReImg10_Click(null, null);
                    return;
                    case 11:
                    pbReImg11_Click(null, null);
                    return;
                    case 12:
                    pbReImg12_Click(null, null);
                    return;
                default: return;
            }
        }
        private void Savedata_sql()
        {
            Thread thread = new Thread(savedata);
            thread.Start();
        }
        private async void savedata()
        {
            try
            {
                if (File.Exists(data_Xe_final.hinhtruoc) && File.Exists(data_Xe_final.hinhtruoc))
                {
                    List<string> strings = new List<string>();
                    strings.Add(data_Xe_final.hinhtruoc);
                    strings.Add(data_Xe_final.hinhsau);
                    List<string> uploadedPaths = await UploadImage.UploadAsync(strings);
                    if (uploadedPaths.Count == 2)
                    {
                        data_Xe_final.hinhtruoc = uploadedPaths[0];
                        data_Xe_final.hinhsau = uploadedPaths[1];
                    }
                }
                try
                {
                    using (var tbldataxe = new database())
                    {
                        data_Xe_final.Sogplx = data_Xe_final.Sogplx + "#" + Declare.STT + "#" + Declare.Chedocan;
                        tbldataxe.tbl_Data_Xe.Add(data_Xe_final);
                        tbldataxe.SaveChanges();
                        //data_Xe_final = new tbl_Data_Xe();
                        //check = 0;
                    }
                }
                catch
                {
                    try
                    {
                        using (var tbldataxe = new databaselocal())
                        {
                            data_Xe_final.Sogplx = data_Xe_final.Sogplx + "#" + Declare.STT + "#" + Declare.Chedocan;
                            tbldataxe.tbl_Data_Xe.Add(data_Xe_final);
                            tbldataxe.SaveChanges();
                            //data_Xe_final = new tbl_Data_Xe();
                            //check = 0;
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            catch (DbEntityValidationException)
            {
                //foreach (var validationResult in ex.EntityValidationErrors)
                //{
                //    var entityType = validationResult.Entry.Entity.GetType().Name;
                //    foreach (var error in validationResult.ValidationErrors)
                //    {

                //        Console.WriteLine($"Validation error for entity '{entityType}': {error.ErrorMessage}");
                //    }
                //}
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            xuly(textBox1.Text);
        }
    }
}
