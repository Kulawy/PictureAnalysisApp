using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureAnalysis.Model
{
    public class TransformFilter
    {

        private Ransac _ransac;

        public TransformFilter(Ransac ransac)
        {
            this._ransac = ransac;
        }


        public List<KeyValuePair<KeyPoint, KeyPoint>> Filter(List<KeyValuePair<KeyPoint, KeyPoint>> pairs)
        {
            Matrix<double> model = _ransac.FindBestModel(pairs);

            return pairs.Where(pair => _ransac.ModelError(pair, model) <= _ransac.MaxError).ToList();

            //return pairs.stream().filter(
            //         pair->ransac.modelError(pair, model) <= _ransac.maxError)
            //         .collect(Collectors.toList());
        }
    }

}
