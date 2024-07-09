using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Giatrican.Database1;
using System.Threading;

namespace Giatrican
{
    public static class User
    {
       public enum Usercan
        {
            DatViet=1,
            Ctykhac=2
        }
        public static string tentramcan { get; set; } = string.Empty;
        public static string tentinh { get; set; } = string.Empty;
        public static string lytrinh { get; set; } = string.Empty;
        public static string nhanviendangnhap { get; set; } = string.Empty;
        public static int macv { get; set; } = 0;
        public static string pathexel { get; set; } = string.Empty;
       
        public enum UserType
        {
            User = 0,
            Admin = 1
        }
        public static void savelogdata(string data)
        {
            Thread thread = new Thread(() => savelog(data));
            thread.Start();
        }
        private static bool savelog(string data)
        {
            try
            {
                using (var log = new database())
                {
                    var std = new tbl_Logdata()
                    {
                        Tennv = User.nhanviendangnhap,
                        thoigian=DateTime.Now,
                        ghichu = data
                };
                    log.tbl_Logdata.Add(std);

                    log.SaveChanges();
                }
                return true;
            }
            catch (Exception) { }
            return false;
        }
        public static string TypeUser(int type)
        {
            UserType userType = (UserType)type;
            if (userType == UserType.Admin)
            {
                return "Admin";
            }
            return "User";
        }
        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }
    }
}
