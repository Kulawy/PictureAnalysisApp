using MathNet.Numerics.LinearAlgebra.Solvers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureAnalysis.Model
{
    public class NeighbourhoodCoherenceFilter
    {

        private Dictionary<KeyPoint, List<KeyPoint>> neighbourhoodA;
        private Dictionary<KeyPoint, List<KeyPoint>> neighbourhoodB;

        private Dictionary<KeyPoint, KeyValuePair<KeyPoint, KeyPoint>> pairsByPoint;

        private List<KeyPoint> pointsA;
        private List<KeyPoint> pointsB;

        private List<KeyValuePair<KeyPoint, KeyPoint>> _pairs;
        private int _coherenceCountToSearch;

        private int _coherenceCondition;

        public NeighbourhoodCoherenceFilter(List<KeyValuePair<KeyPoint, KeyPoint>> keyPointsPairs, int coherenceCountToSearch, int coherenceCondition)
        {
            neighbourhoodA = new Dictionary<KeyPoint, List<KeyPoint>>();
            neighbourhoodB = new Dictionary<KeyPoint, List<KeyPoint>>();
            _pairs = keyPointsPairs;
            _coherenceCountToSearch = coherenceCountToSearch;
            InitPoints();
            InitPairsByPoint();
            _coherenceCondition = coherenceCondition;

        }

        public List<KeyValuePair<KeyPoint, KeyPoint>> GetFilteredPairs()
        {
            List<KeyValuePair<KeyPoint, KeyPoint>> filteredPairs = new List<KeyValuePair<KeyPoint, KeyPoint>>();
            ComputerNeighbourhood();

            foreach (KeyValuePair<KeyPoint, KeyPoint> pair in _pairs)
            {

                int howManyPairs = 0;

                KeyPoint pointA = pair.Key;
                KeyPoint pointB = pair.Value;

                List<KeyPoint> neighboursA = neighbourhoodA[pointA];
                List<KeyPoint> neighboursB = neighbourhoodB[pointB];

                foreach (KeyPoint point in neighboursA)
                {
                    KeyPoint pointFromPair = pairsByPoint[point].Value;

                    if (neighboursB.Contains(pointFromPair))
                    {
                        howManyPairs++;
                    }

                }
                if (howManyPairs >= _coherenceCondition)
                {
                    filteredPairs.Add(pair);
                }

            }


            return filteredPairs;
        }

        private void InitPairsByPoint()
        {
            pairsByPoint = new Dictionary<KeyPoint, KeyValuePair<KeyPoint, KeyPoint>>();

            foreach (KeyValuePair<KeyPoint, KeyPoint> pair in _pairs)
            {
                pairsByPoint.Add(pair.Key, pair);
                pairsByPoint.Add(pair.Value, pair);
            }
        }

        private void InitPoints()
        {
            pointsA = new List<KeyPoint>();
            pointsB = new List<KeyPoint>();
            foreach (KeyValuePair<KeyPoint, KeyPoint> pair in _pairs)
            {
                pointsA.Add(pair.Key);
                pointsB.Add(pair.Value);
            }
        }

        private void ComputerNeighbourhood()
        {
            //IEnumerator<KeyPoint> itA = pointsA.GetEnumerator();

            foreach(KeyPoint pointA in pointsA)
            {
                List<KeyPoint> sorted = new List<KeyPoint>(pointsA);
                sorted.Sort(new NeighbourhoodComparator(pointA));
                neighbourhoodA.Add(pointA, sorted.GetRange(1, _coherenceCountToSearch + 1));
            }

            foreach(KeyPoint pointB in pointsB)
            {
                List<KeyPoint> sorted = new List<KeyPoint>(pointsB);
                sorted.Sort(new NeighbourhoodComparator(pointB));
                neighbourhoodB.Add(pointB, sorted.GetRange(1, _coherenceCountToSearch + 1));
            }
            
        }
    }

}
