using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;
using PictureAnalysis.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureAnalysis.PictureExtraction
{
    public class ExtractionManager
    {
        
        
        private int _keyPointsCount;
        private int _fromPictureNumber;
        //private Dictionary<KeyPoint, KeyPoint> _pointsPictureA;
        //private Dictionary<KeyPoint, KeyPoint> _pointsPictureB;

        public ExtractionManager()
        {
            
        }

        public string GetExtractedValuesString(string imgPath)
        {
            string output = "";

            var fileStream = new FileStream(imgPath + Const.afterKeyExtractionFileNameEnding, FileMode.Open, FileAccess.Read );
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                output = streamReader.ReadToEnd();
            }

            return output;

        }

        public List<KeyPoint> ExtractKeyPointsFromImages(string imgPath)
        {
            List<KeyPoint> keyPoints = new List<KeyPoint>();

            var fileStream = new FileStream(imgPath + ".png" + Const.afterKeyExtractionFileNameEnding, FileMode.Open, FileAccess.Read);
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                _fromPictureNumber = int.Parse(streamReader.ReadLine());
                _keyPointsCount = int.Parse(streamReader.ReadLine());

                for (int i = 0; i < _keyPointsCount; i++)
                {
                    keyPoints.Add(new KeyPoint(streamReader.ReadLine().Split(' ')));
                }

            }
            return keyPoints;
        }

        public Dictionary<KeyPoint, KeyPoint> FindNeighbers(List<KeyPoint> imgOneKeyPoints, List<KeyPoint> imgTwoKeyPoints)
        {
            Dictionary<KeyPoint, KeyPoint> pairsAB = new Dictionary<KeyPoint, KeyPoint>();
            Dictionary<KeyPoint, KeyPoint> pairsBA = new Dictionary<KeyPoint, KeyPoint>();

            foreach(KeyPoint kp in imgOneKeyPoints)
            {
                //pairsAB.Add(kp, FindPair(kp, imgTwoKeyPoints).Value);
                KeyPoint second = FindSimilarOnSecondPicture(kp, imgTwoKeyPoints);
                pairsAB.Add(kp, second);
            }
            foreach (KeyPoint kp in imgTwoKeyPoints)
            {
                //pairsBA.Add(kp, FindPair(kp, imgOneKeyPoints).Value);
                KeyPoint second = FindSimilarOnSecondPicture(kp, imgOneKeyPoints);
                pairsBA.Add(kp, second);
            }
           
            Dictionary<KeyPoint, KeyPoint> resultPairs = new Dictionary<KeyPoint, KeyPoint>();
            //Dictionary<KeyPoint, KeyPoint> smallerSet;
            //if ( pairsAB.Count > pairsBA.Count)
            //{
            //    smallerSet = pairsBA;
            //}
            //else
            //{
            //    smallerSet = pairsAB;
            //}

            foreach(KeyValuePair<KeyPoint, KeyPoint> kvp in pairsAB)
            {
                KeyPoint key = kvp.Key;
                KeyPoint value = kvp.Value;
                KeyPoint oppositValue = pairsBA[value];

                Console.WriteLine("\nKey from pairsAB: " + key);
                Console.WriteLine("Value from pairsAB: " + value);
                Console.WriteLine("Value  from pairBA: " + oppositValue + "\n");

                if( value.Equals(pairsBA[value]))
                {
                    resultPairs.Add(kvp.Key, kvp.Value);
                }
            }

            return pairsBA;
        }

        public KeyValuePair<KeyPoint, KeyPoint> FindPair(KeyPoint picOnePoint, List<KeyPoint> picTwoPointsList )
        {
            
            double newDistance;
            double smalestDist = picOnePoint.CalculateDistance(picTwoPointsList[0]);
            KeyPoint neighbor = picTwoPointsList[0];

            foreach (KeyPoint kp in picTwoPointsList)
            {
                newDistance = picOnePoint.CalculateDistance( kp);
                if ( newDistance < smalestDist)
                {
                    smalestDist = newDistance;
                    neighbor = kp;
                }
            }

            KeyValuePair<KeyPoint, KeyPoint> pair = new KeyValuePair<KeyPoint, KeyPoint>(picOnePoint, neighbor);

            return pair;
        }

        public KeyPoint FindSimilarOnSecondPicture(KeyPoint picOnePoint, List<KeyPoint> picTwoPointsList)
        {
            int newDistance;
            int smalestDist = picOnePoint.CalculateDistance(picTwoPointsList[0]);
            KeyPoint neighbor = picTwoPointsList[0];

            foreach (KeyPoint kp in picTwoPointsList)
            {
                newDistance = picOnePoint.CalculateDistance(kp);
                if (newDistance < smalestDist)
                {
                    picOnePoint.Distance = smalestDist;
                    kp.Distance = smalestDist;
                    smalestDist = newDistance;
                    neighbor = kp;
                }
            }

            return neighbor;
        }
        

       
    }
}
