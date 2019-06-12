using PictureAnalysis.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureAnalysis.View
{
    public class ConsoleInterface
    {
        private ImageUtils _imgUtilities;
        private KeyPointExtractor _myExtractor;
        private bool _menuLoopExit;

        public ConsoleInterface()
        {
            _menuLoopExit = false;
            InitInterface();
        }

        private void InitInterface()
        {
            _imgUtilities = new ImageUtils();
            _myExtractor = new KeyPointExtractor();
            Console.WriteLine("PICTURE ANALYSIS USING RANSAC");

            while (!_menuLoopExit)
            {
                MenuPrompt();
                Console.Write(">");
                ComandReader(Console.ReadLine());
            }

        }

        private void MenuPrompt()
        {
            Console.WriteLine();
            Console.WriteLine("Press PI to print files in img folder");
            Console.WriteLine("Press PA to print files in app folder");
            Console.WriteLine("Press 0 to analyze images");
            Console.WriteLine("Press 1 to load key points from first image");
            Console.WriteLine("Press 2 to load key points from second image");
            Console.WriteLine();
            
        }

        private void ComandReader(string command)
        {
            switch( command.ToUpper() ){
                case "PI":
                    _imgUtilities.GetFilesFromFolderNames();
                    break;
                case "PA":
                    _myExtractor.GetFilesFromFolderNames();
                    break;
                case "DI":
                    DisplayImage();
                    break;
                case "0":
                    ExtractKeyPoints();
                    break;
                case "1":
                    
                    break;
                case "2":
                    break;

                case "END":
                    _menuLoopExit = true;
                    break;

            }
            
        }

        private void ExtractKeyPoints()
        {
            string output1 = _myExtractor.ExtractKeyPoints(_imgUtilities.GetImagePath(Const.imgName01));
            Console.WriteLine(output1);
            string output2 = _myExtractor.ExtractKeyPoints(_imgUtilities.GetImagePath(Const.imgName02));
            Console.WriteLine(output2);
            
        }

        private void DisplayImage()
        {
            Console.WriteLine("Enter image name: \n#");
            string imageName = Console.ReadLine();
            try
            {
                _imgUtilities.DisplayImageOnScreen(imageName);
            }
            catch(Exception e)
            {
                Console.WriteLine( e.ToString());
                Console.WriteLine("File not found");
            }
            
        }


    }
}
