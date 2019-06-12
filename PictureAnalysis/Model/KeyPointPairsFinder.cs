using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureAnalysis.Model
{
    public class KeyPointPairsFinder
    {
        PictureKeyPoints _pictureA;
        PictureKeyPoints _pictureB;

        private Dictionary<KeyPoint, KeyPoint> _pointsPictureA;
        private Dictionary<KeyPoint, KeyPoint> _pointsPictureB;

        public KeyPointPairsFinder(PictureKeyPoints pictureA, PictureKeyPoints pictureB)
        {
            _pictureA = pictureA;
            _pictureB = pictureB;
            _pointsPictureA = new Dictionary<KeyPoint, KeyPoint>();
            _pointsPictureB = new Dictionary<KeyPoint, KeyPoint>();
        }

        public List<KeyValuePair<KeyPoint, KeyPoint>> FindKeyPointsPairs()
        {
            ComputeClosestNeighbours();

            List<KeyValuePair<KeyPoint, KeyPoint>> keyPointsPair = new List<KeyValuePair<KeyPoint, KeyPoint>>();

            foreach(KeyValuePair<KeyPoint, KeyPoint> entryPointA in _pointsPictureA)
            {
                KeyPoint pointA = entryPointA.Key;
                KeyPoint pointFromB = entryPointA.Value;
                if (_pointsPictureB[pointFromB] == pointA)
                {
                    keyPointsPair.Add(new KeyValuePair<KeyPoint, KeyPoint>(pointA, pointFromB));
                }
            }


            return keyPointsPair;

        }

        private void ComputeClosestNeighbours()
        {
            foreach (KeyPoint point in _pictureA.KeyPoints)
            {
                KeyPoint closestNeighbour = _pictureB.GetClosestNeighbour(point);
                _pointsPictureA.Add(point, closestNeighbour);

            }

            foreach (KeyPoint point in _pictureB.KeyPoints)
            {
                KeyPoint closestNeighbour = _pictureA.GetClosestNeighbour(point);
                _pointsPictureB.Add(point, closestNeighbour);

            }

        }

    }
}
