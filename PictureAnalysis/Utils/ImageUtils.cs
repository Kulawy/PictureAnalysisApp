using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureAnalysis.Utils
{
    public class ImageUtils : IFolderManager
    {
        private string _imgFolderPath;

        public ImageUtils()
        {
            _imgFolderPath = Const.imagesFolderDirectory;
        }
        
        public void DisplayImageOnScreen(string imageName)
        {
            string imgDisplayPath = _imgFolderPath + imageName + ".png" + Const.afterKeyExtractionFileNameEnding + ".png";
            ProcessStartInfo imgDisplayInfo = new ProcessStartInfo();
            imgDisplayInfo.FileName = imgDisplayPath;
            imgDisplayInfo.UseShellExecute = true;
            imgDisplayInfo.CreateNoWindow = false;

            Process imgDisplay = Process.Start(imgDisplayInfo);
        }

        public string GetImagePath(string imageName)
        {
            return _imgFolderPath + imageName + ".png";
        }

        public void GetFilesFromFolderNames()
        {
            string[] filesNames = Directory.GetFiles(_imgFolderPath);
            foreach (string s in filesNames)
            {
                Console.WriteLine(s);
            }
        }

        public void DisplayFolderPath()
        {
            Console.WriteLine(_imgFolderPath);
        }

    }
}
