using MathNet.Numerics.LinearAlgebra;
using PictureAnalysis.Model;
using PictureAnalysis.Utils;
using System.Collections.Generic;

namespace PictureAnalysis
{
    public class AffineTransform : ITransform
    {


        //    public AffineTransform(Point a1, Point a2, Point a3, Point b1, Point b2, Point b3)
        //    {
        //        A = computeTransform(a1,a2,a3,b1,b2,b3);
        //    }


        public KeyPoint Transform(KeyPoint point, Matrix<double> A)
        {
            double[,] h = { { point.CoordX }, { point.CoordY }, { 1 } };
            double[,] aHelp = { { A.At(0, 0), A.At(1, 0), A.At(2, 0) }, { A.At(3, 0), A.At(4, 0), A.At(5, 0) }, { 0, 0, 1 } };
            Matrix<double> matrixA = CreateMatrix.DenseOfArray(h);
            Matrix<double> resMatrix = CreateMatrix.DenseOfArray(aHelp).Multiply(matrixA);
            double u = resMatrix.At(0, 0);
            double v = resMatrix.At(1, 0);
            return new KeyPoint(u, v);
        }

        public Matrix<double> ComputeTransform(List<KeyPoint> points
        )
        {
            KeyPoint a1 = points[0];
            KeyPoint a2 = points[1];
            KeyPoint a3 = points[2];

            KeyPoint b1 = points[3];
            KeyPoint b2 = points[4];
            KeyPoint b3 = points[5];


            double x1 = a1.CoordX;
            double y1 = a1.CoordY;

            double x2 = a2.CoordX;
            double y2 = a2.CoordY;

            double x3 = a3.CoordX;
            double y3 = a3.CoordY;

            double u1 = b1.CoordX;
            double v1 = b1.CoordY;

            double u2 = b2.CoordX;
            double v2 = b2.CoordY;

            double u3 = b3.CoordX;
            double v3 = b3.CoordY;


            double[,] a = { { x1, y1, 1, 0, 0, 0 }, { x2, y2, 1, 0, 0, 0 }, { x3, y3, 1, 0, 0, 0 }, { 0, 0, 0, x1, y1, 1 }, { 0, 0, 0, x2, y2, 1 }, { 0, 0, 0, x3, y3, 1 } };
            double[,] b = { { u1 }, { u2 }, { u3 }, { v1 }, { v2 }, { v3 } };


            Matrix<double> matrixA = CreateMatrix.DenseOfArray(a); 
            Matrix<double> matrixB = CreateMatrix.DenseOfArray(b); 


            Matrix<double> ret = matrixA.Inverse().Multiply(matrixB);

            return ret;

        }

        
    public int GetNumSamples()
        {
            return 3;
        }
        
    }


}