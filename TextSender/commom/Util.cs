using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Common
{
    public static class Util
    {
        public static long TimeStamp
        {
            get
            {
                //Console.WriteLine((long)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds);
                return (long)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            }
        }

        public static string ByteArrayToString(byte[] ba)
        {
            string hex = BitConverter.ToString(ba);
            return hex.Replace("-", "");
        }

        public static void DirDelete(string dir)
        {
            try
            {
                Directory.Delete(dir, true);
            }
            catch (Exception)
            {
            }
        }

        public static void copy(string src, string des)
        {
            try
            {
                Microsoft.VisualBasic.FileIO.FileSystem.CopyFile(src, des, Microsoft.VisualBasic.FileIO.UIOption.AllDialogs);
            }
            //If the Copying Operation is Cancelled in
            // the midway
            //then it throws an Exception which is
            //carried by this catch block
            catch (OperationCanceledException)
            {
                Console.WriteLine("Copying Cancelled!!!");
            }
        }

        public static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            DirectoryInfo[] dirs = dir.GetDirectories();
            // If the destination directory doesn't exist, create it.
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destDirName, file.Name);
                file.CopyTo(temppath, false);
            }

            // If copying subdirectories, copy them and their contents to new location.
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, temppath, copySubDirs);
                }
            }
        }


        /// <summary>
        /// 按照指定的高和宽生成相应的规格的图片，采用此方法生成的缩略图片不会失真
        /// </summary>
        /// <param name="width">指定宽度</param>
        /// <param name="height">指定高度</param>
        /// <param name="imageFrom">原图片</param>
        /// <returns>返回新生成的图</returns>
        public static Image GetReducedImage(int width, int height, Image imageFrom)
        {
            // 源图宽度及高度
            int imageFromWidth = imageFrom.Width;
            int imageFromHeight = imageFrom.Height;

            // 生成的缩略图实际宽度及高度.如果指定的高和宽比原图大，则返回原图；否则按照指定高宽生成图片
            if (width >= imageFromWidth && height >= imageFromHeight)
            {
                return imageFrom;
            }
            else
            {
                // 生成的缩略图在上述"画布"上的位置
                int X = 0;
                int Y = 0;

                // 创建画布
                Bitmap bmp = new Bitmap(width, height, PixelFormat.Format24bppRgb);
                bmp.SetResolution(imageFrom.HorizontalResolution, imageFrom.VerticalResolution);
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    // 用白色清空
                    g.Clear(Color.White);

                    // 指定高质量的双三次插值法。执行预筛选以确保高质量的收缩。此模式可产生质量最高的转换图像。
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;

                    // 指定高质量、低速度呈现。
                    g.SmoothingMode = SmoothingMode.HighQuality;

                    // 在指定位置并且按指定大小绘制指定的 Image 的指定部分。
                    g.DrawImage(imageFrom, new Rectangle(X, Y, width, height),
                        new Rectangle(0, 0, imageFromWidth, imageFromHeight), GraphicsUnit.Pixel);

                    //将图片以指定的格式保存到到指定的位置
                    //bmp.Save(@"E:\640x480.png", ImageFormat.Png);
                    return bmp;
                }
            }
        }

        public static bool IsIPv4(string ipAddress)
        {
            return Regex.IsMatch(ipAddress, @"^\d{1,3}(\.\d{1,3}){3}$") &&
                ipAddress.Split('.').FirstOrDefault(s => int.Parse(s) > 255) == null;
        }

        public static string md5(string input)
        {
            // step 1, calculate MD5 hash from input
            System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.UTF8.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);
            // step 2, convert byte array to hex string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("x2"));
            }
            return sb.ToString();
        }
    }

    internal class TextFile
    {
        public static String fileToText(String fileName)
        {
            StreamReader reader = new StreamReader(fileName);
            String text = reader.ReadToEnd();
            reader.Close();
            return text;
        }

        public static IEnumerable<String> lines(String fileName)
        {
            StreamReader reader = new StreamReader(fileName);
            while (true)
            {
                String line = reader.ReadLine();
                if (line == null)
                    break;
                yield return line;
            }
            reader.Close();
        }
    }
}