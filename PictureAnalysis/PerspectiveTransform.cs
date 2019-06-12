using System.Collections.Generic;
using MathNet.Numerics.LinearAlgebra;
using PictureAnalysis.Model;
using PictureAnalysis.Utils;

namespace PictureAnalysis
{
    public class PerspectiveTransform : ITransform
    {

        public KeyPoint Transform(KeyPoint point, Matrix<double> H)
        {
            double[,] h = new double[,] { { point.CoordX }, { point.CoordY }, { 1 } };
            double[,] aHelp = new double[,] { { H.At(0, 0), H.At(1, 0), H.At(2, 0) }, { H.At(3, 0), H.At(4, 0), H.At(5, 0) }, { H.At(6, 0), H.At(7, 0), 1 } };
            Matrix<double> matrixA = CreateMatrix.DenseOfArray(h);
            Matrix<double> resMatrix = CreateMatrix.DenseOfArray(aHelp).Multiply(matrixA);            
            double tu = resMatrix.At(0, 0);
            double tv = resMatrix.At(1, 0);
            double t = resMatrix.At(2, 0);
            return new KeyPoint( tu / t, tv / t);
        }


        public Matrix<double> ComputeTransform(List<KeyPoint> points)
        {
            KeyPoint a1 = points[0];
            KeyPoint a2 = points[1];
            KeyPoint a3 = points[2];
            KeyPoint a4 = points[3];

            KeyPoint b1 = points[4];
            KeyPoint b2 = points[5];
            KeyPoint b3 = points[6];
            KeyPoint b4 = points[7];


            double x1 = a1.CoordX;
            double y1 = a1.CoordY;

            double x2 = a2.CoordX;
            double y2 = a2.CoordY;

            double x3 = a3.CoordX;
            double y3 = a3.CoordY;

            double x4 = a4.CoordX;
            double y4 = a4.CoordY;

            double u1 = b1.CoordX;
            double v1 = b1.CoordY;

            double u2 = b2.CoordX;
            double v2 = b2.CoordY;

            double u3 = b3.CoordX;
            double v3 = b3.CoordY;

            double u4 = b4.CoordX;
            double v4 = b4.CoordY;
            

            double[,] a = {
                {x1, y1, 1,0,0,0, -1*u1*x1, -1*u1*y1},
                {x2,y2, 1, 0,0,0, -1*u2*x2, -1*u2*y2 },
                {x3,y3, 1, 0,0,0, -1*u3*x3, -1*u3*y3},
                {x4,y4, 1, 0,0,0, -1*u4*x4, -1*u4*y4},
                {0,0,0,x1,y1,1, -1*v1*x1, -1*v1*y1},
                {0,0,0,x2,y2,1, -1*v2*x2, -1*v2*y2},
                {0,0,0,x3,y3,1, -1*v3*x3, -1*v3*y3},
                {0,0,0,x4,y4,1, -1*v4*x4, -1*v4*y4}};
            double[,] b = { { u1 }, { u2 }, { u3 }, { u4 }, { v1 }, { v2 }, { v3 }, { v4 } };


            Matrix<double> matrixA = CreateMatrix.DenseOfArray(a);
            Matrix<double> matrixB = CreateMatrix.DenseOfArray(b);


            Matrix<double> ret = matrixA.Inverse();
            ret = ret.Multiply(matrixB);

            return ret;

        }

        public int GetNumSamples()
        {
            return 4;
        }
        

    }

}