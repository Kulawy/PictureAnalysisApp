using MathNet.Numerics.LinearAlgebra;
using PictureAnalysis.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureAnalysis.Model
{
    public class Ransac
    {
        private Random _rnd;
        
        private int _numIters;

        private int _numSamples;

        private ITransform _transform;

        public double MaxError { get; set; }

        public Ransac(int numIters, ITransform
                transform, double maxError)
        {
            _rnd = new Random();
            _numIters = numIters;
            _numSamples = transform.GetNumSamples();
            _transform = transform;
            MaxError = maxError;
        }

        public Matrix<double> FindBestModel(List<KeyValuePair<KeyPoint, KeyPoint>> pairs)
        {
            Matrix<double> bestModel = null;
            int bestScore = 0;

            for (int i = 0; i < _numIters; i++)
            {
                Matrix<double> model = null;
                ISet<KeyValuePair<KeyPoint, KeyPoint>> samples = new HashSet<KeyValuePair<KeyPoint, KeyPoint>>();
                while (model == null)
                {


                    samples.Add(GetRandomPair(pairs));

                    if (samples.Count == _numSamples)
                    {
                        List<KeyPoint> pointsA = new List<KeyPoint>();
                        List<KeyPoint> pointsB = new List<KeyPoint>();
                        foreach(KeyValuePair<KeyPoint, KeyPoint> pair in samples)
                        {
                            pointsA.Add(pair.Key);
                            pointsB.Add(pair.Value);
                        }
                        List<KeyPoint> points = new List<KeyPoint>();
                        points.AddRange(pointsA);
                        points.AddRange(pointsB);
                        try
                        {
                            model = _transform.ComputeTransform(points);
                        }
                        catch (Exception e)
                        {
                            model = null;
                            samples = new HashSet<KeyValuePair<KeyPoint, KeyPoint>>(new List<KeyValuePair<KeyPoint, KeyPoint>>(_numSamples));
                        }
                    }
                }

                int score = 0;

                foreach ( KeyValuePair<KeyPoint, KeyPoint> pair in pairs)
                {
                    double error = ModelError(pair, model);
                    if (error < MaxError)
                    {
                        score++;
                    }
                }
                if (score > bestScore)
                {
                    bestScore = score;
                    bestModel = model;
                }

            }

            return bestModel;
        }

        public KeyValuePair<KeyPoint, KeyPoint> GetRandomPair(List<KeyValuePair<KeyPoint, KeyPoint>> pairs)
        {
            int rand =  _rnd.Next(pairs.Count);
            return pairs[rand];
        }

        public double ModelError(KeyValuePair<KeyPoint, KeyPoint> pair, Matrix<double> model)
        {
            KeyPoint pointA = pair.Key;
            KeyPoint pointB = pair.Value;
            KeyPoint transformedPoint = _transform.Transform(pointA, model);


            return Math.Sqrt((transformedPoint.CoordY - pointB.CoordY) * (transformedPoint.CoordY - pointB.CoordY)
                    + (transformedPoint.CoordX - pointB.CoordX) * (transformedPoint.CoordX - pointB.CoordX));

        }
    }

}
