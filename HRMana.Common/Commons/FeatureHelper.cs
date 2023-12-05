using HRMana.Common.Events;
using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Win32;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HRMana.Common
{
    public static class FeatureHelper
    {
        public static string ChooseImage()
        {
            string pathImage = "";
            string fileName = "";
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files (*.jpg; *.png; *.bmp)|*.jpg; *.png; *.bmp";

            if (ofd.ShowDialog() == true)
            {
                fileName = ofd.FileName;

                try
                {
                    // Lưu vào bin/debug/
                    string currentDomain = AppDomain.CurrentDomain.BaseDirectory + "NhanVien_Image";
                    DirectoryInfo projectDirectory = new DirectoryInfo(currentDomain);

                    if (!projectDirectory.Exists)
                    {
                        projectDirectory.Create();
                    }

                    string relativePath = Path.Combine(projectDirectory.FullName, Path.GetFileName(ofd.FileName));

                    File.Copy(fileName, relativePath, true);

                    pathImage = AppDomain.CurrentDomain.BaseDirectory + "NhanVien_Image/" + Path.GetFileName(ofd.FileName);

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi: {ex.Message}");
                }
            }

            return pathImage;
        }


    }
}
