using MathNet.Numerics.LinearAlgebra;
using PictureAnalysis.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureAnalysis.Utils
{
    public interface ITransform
    {
        KeyPoint Transform(KeyPoint point, Matrix<double> transform);
        Matrix<double> ComputeTransform(List<KeyPoint> pointList);
        int GetNumSamples();
    }

}
