using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing.Drawing2D;

namespace Giatrican
{
    public static class Utility
    {
        public static void SaveBitmapToFile(Bitmap bitmap, string filePath)
        {
            if (bitmap == null || string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentException("Invalid Bitmap or file path.");
            }
            bitmap.Save(filePath, ImageFormat.Jpeg);
        }

        public static Image fixBase64ForImage(string base64)
        {
            byte[] buffer = Convert.FromBase64String(base64);
            using (MemoryStream stream = new MemoryStream(buffer))
            return Image.FromStream(stream);
        }
        public static Bitmap fixBase64ForBitmap(string base64)
        {
            byte[] buffer = Convert.FromBase64String(base64);
            Image image;
            using (MemoryStream stream = new MemoryStream(buffer))
            {
                image = Image.FromStream(stream);
            }
            return image as Bitmap;
        }
        public static void saveImageByFormat(Image image, string imagePath, ImageFormat imageFormat)
        {
            checkExistingFolderFromFilePath(imagePath);
            Bitmap bitmap = new Bitmap(image.Width, image.Height, image.PixelFormat);
            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.DrawImage(image, new System.Drawing.Point(0, 0));
            graphics.Dispose();
            bitmap.Save(imagePath, imageFormat);
            bitmap.Dispose();
        }
        public static bool checkExistingFolderFromFilePath(string filePath)
        {
            string path = string.Empty;
            for (int num = filePath.Length - 1; num >= 0; num--)
            {
                if (filePath[num].Equals('\\'))
                {
                    path = filePath.Substring(0, num + 1);
                    break;
                }
            }
            bool check= Directory.Exists(path);
            if (!check)
            {
                Directory.CreateDirectory(path);
            }
            return true;
        }
        public static Bitmap drawBorderOnImage(Bitmap bitmap, int x, int y, int width, int height, System.Drawing.Color color, int thickness)
        {
            try
            {
                Graphics graphics = Graphics.FromImage(bitmap);
                System.Drawing.Pen pen = new System.Drawing.Pen(color, thickness);
                pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                graphics.DrawRectangle(pen, x, y, width, height);
                pen.Dispose();
                graphics.Dispose();
                return bitmap;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static Image drawBorderOnImage(Image image, int x, int y, int width, int height, System.Drawing.Color color, int thickness)
        {
            try
            {
                Graphics graphics = Graphics.FromImage(image);
                System.Drawing.Pen pen = new System.Drawing.Pen(color, thickness);
                pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                graphics.DrawRectangle(pen, x, y, width, height);
                pen.Dispose();
                graphics.Dispose();
                return image;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static Bitmap writeTextOnImage(Bitmap bitmap, Dictionary<string, string> content, System.Drawing.Color titleColor, System.Drawing.Color contentColor)
        {
            Graphics graphics = null;
            Font font = null;
            Font font2 = null;
            SolidBrush solidBrush = null;
            SolidBrush solidBrush2 = null;
            try
            {
                graphics = Graphics.FromImage(bitmap);
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                int num = ((bitmap.Size.Width < bitmap.Size.Height) ? (bitmap.Size.Width / 40) : (bitmap.Size.Height / 40));
                font = new Font("Arial", num, System.Drawing.FontStyle.Bold);
                font2 = new Font("Arial", num, System.Drawing.FontStyle.Regular);
                solidBrush = new SolidBrush(titleColor);
                solidBrush2 = new SolidBrush(contentColor);
                int x = 10;
                int num2 = 10;
                int num3 = 0;
                int num4 = 0;
                foreach (KeyValuePair<string, string> item in content)
                {
                    num4 = graphics.MeasureString(item.Key, font).ToSize().Width;
                    if (num4 > num3)
                    {
                        num3 = num4;
                    }
                }

                num3 += 10;
                foreach (KeyValuePair<string, string> item2 in content)
                {
                    graphics.DrawString(item2.Key, font, solidBrush, new System.Drawing.Point(x, num2));
                    graphics.DrawString(item2.Value, font2, solidBrush2, new System.Drawing.Point(num3, num2));
                    num2 += 40;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                graphics?.Dispose();
                font?.Dispose();
                font2?.Dispose();
                solidBrush?.Dispose();
                solidBrush2?.Dispose();
            }

            return bitmap;
        }
        public static Bitmap getBitmapFromFile(string imagePath)
        {
            if (!isValidImage(imagePath))
            {
                return null;
            }

            try
            {
                Bitmap bitmap = new Bitmap(imagePath);
                Image thumbnailImage = bitmap.GetThumbnailImage(bitmap.Width, bitmap.Height, null, (IntPtr)0);
                bitmap.Dispose();
                return thumbnailImage as Bitmap;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static Image getImageFromFile(string imagePath)
        {
            if (!isValidImage(imagePath))
            {
                return null;
            }

            try
            {
                Bitmap bitmap = new Bitmap(imagePath);
                Image thumbnailImage = bitmap.GetThumbnailImage(bitmap.Width, bitmap.Height, null, (IntPtr)0);
                bitmap.Dispose();
                return thumbnailImage;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static Bitmap drawImageOnImage(Bitmap backgroundImage, Bitmap foregroundImage, int left, int top)
        {
            try
            {
                using (Graphics graphics = Graphics.FromImage(backgroundImage))
                {
                    graphics.DrawImageUnscaled(foregroundImage, left, top);
                }

                return backgroundImage;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static Bitmap drawImageOnImage(Bitmap backgroundImage, Bitmap foregroundImage)
        {
            try
            {
                using (Graphics graphics = Graphics.FromImage(backgroundImage))
                {
                   
                    int newWidth = backgroundImage.Width / 4; 
                    int newHeight = (foregroundImage.Height * newWidth) / foregroundImage.Width;

                    graphics.DrawImage(foregroundImage, 0, 0, newWidth, newHeight);
                }

                return backgroundImage;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static Image drawImageOnImage(string backgroundImagePath, string foregroundImagePath, int left, int top)
        {
            try
            {
                if (!isValidImage(backgroundImagePath))
                {
                    throw new Exception("File " + backgroundImagePath + " is not valid!");
                }

                if (!isValidImage(foregroundImagePath))
                {
                    throw new Exception("File " + foregroundImagePath + " is not valid!");
                }

                Image image = Image.FromFile(backgroundImagePath);
                Image image2 = Image.FromFile(foregroundImagePath);
                Graphics graphics = Graphics.FromImage(image);
                graphics.DrawImageUnscaled(image2, left, top);
                image2.Dispose();
                return image;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static bool isValidImage(string fileName)
        {
            try
            {
                if (!File.Exists(fileName))
                {
                    return false;
                }

                using (new Bitmap(fileName))
                {
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static int gettrucxe(int check)
        {
            switch (check)
            {
                case 1:
                    return 2;
                case 2:
                case 3:
                    return 3;
                case 4:
                case 5:
                    return 4;
                case 6:
                    return 5;
                case 7:
                    return 3;
                case 8:
                case 9:
                    return 4;
                case 10:
                case 11:
                    return 5;
                case 12:
                    return 6;
                default:
                    return 0;
            }
        }
        public static string FormatPercen(string percentage)
        {
            if(percentage.Length > 0)
            {
                return percentage + "%";
            }
            return "0.00%";
           
        }
        public static string KilogramsToTons(int kilograms)
        {
            double result = (double)kilograms / 1000.0;
            return result.ToString("0.00");
        }

        public static int StringToInt(string inputString)
        {
            if (int.TryParse(inputString, out int result))
            {
               
                return result;
            }
            else
            {
                return 0;
            }
        }

    }
}
