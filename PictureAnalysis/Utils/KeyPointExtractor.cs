using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureAnalysis.Utils
{
    public class KeyPointExtractor : IFolderManager
    {
        private string _extracAppFolderPath;
        private string _extracAppPath;

        public KeyPointExtractor()
        {
            _extracAppFolderPath = Path.Combine(Const.projectDirectory, @"extract_features\", @"app\");
            _extracAppPath = Path.Combine(Const.projectDirectory, @"extract_features\", @"app\extract_features_32bit.exe");

        }

        


        public string ExtractKeyPoints(string imgPath)
        {
            string args01 = "-haraff -sift -i " + imgPath + " -DE";
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = _extracAppPath;
            startInfo.Arguments = args01;
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardOutput = true;

            string output;

            using (Process exeProcess = Process.Start(startInfo))
            {
                output = exeProcess.StandardOutput.ReadToEnd();
                exeProcess.WaitForExit();
            }

            return output;

        }

        public void GetFilesFromFolderNames()
        {
            string[] filesNames02 = Directory.GetFiles(_extracAppFolderPath);
            foreach (string s in filesNames02)
            {
                Console.WriteLine(s);
            }
        }

        public void DisplayFolderPath()
        {
            Console.WriteLine(_extracAppFolderPath);
        }

    }
}
