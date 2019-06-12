using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureAnalysis.Model
{
    public class PictureKeyPoints
    {
        private string _imgName;
        private string _imgFolderPath;
        public List<KeyPoint> KeyPoints { get; set; }

        public PictureKeyPoints(string imgName)
        {
            _imgName = imgName;
            _imgFolderPath = Const.imagesFolderDirectory;
        }

        public string GetImagePath()
        {
            return _imgFolderPath + _imgName;
        }

        public KeyPoint GetClosestNeighbour(KeyPoint other)
        {
            KeyPoint closestPoint = KeyPoints[0];
            int minDiff = other.CalculateDistance(closestPoint);

            foreach (KeyPoint point in KeyPoints)
            {
                int diff = other.CalculateDistance(point);
                if (diff < minDiff)
                {
                    minDiff = diff;
                    closestPoint = point;
                }
            }

            return closestPoint;

        }

    }
}
