using PictureAnalysis.Model;
using PictureAnalysis.PictureExtraction;
using PictureAnalysis.Utils;
using PictureAnalysis.View;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureAnalysis
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch st = new Stopwatch();

            #region Develop
            ImageUtils imgUtils = new ImageUtils();
            KeyPointExtractor myExtrac = new KeyPointExtractor();
            ExtractionManager exManager = new ExtractionManager();
            myExtrac.ExtractKeyPoints(Const.imagesFolderDirectory + Const.imgName01 + ".png");
            myExtrac.ExtractKeyPoints(Const.imagesFolderDirectory + Const.imgName02 + ".png");
            PictureKeyPoints pkp01 = new PictureKeyPoints(Const.imgName01);
            PictureKeyPoints pkp02 = new PictureKeyPoints(Const.imgName02);
            LetDraw ld = new LetDraw();

            pkp01.KeyPoints = exManager.ExtractKeyPointsFromImages(pkp01.GetImagePath());
            pkp02.KeyPoints = exManager.ExtractKeyPointsFromImages(pkp02.GetImagePath());

            KeyPointPairsFinder kppf = new KeyPointPairsFinder(pkp01, pkp02);

            //foreach (KeyPoint kp in pkp01.KeyPoints)
            //{
            //    Console.WriteLine("Picture One key points: \n");
            //    Console.WriteLine(kp);
            //}

            Console.WriteLine("Start searching neighbours");
            st.Start();
            //Dictionary<KeyPoint, KeyPoint> neighbors = exManager.FindNeighbers(pkp01.KeyPoints, pkp02.KeyPoints); //WHY DICTIONARY NOT WORK ? 
            List<KeyValuePair<KeyPoint, KeyPoint>> neighbors = kppf.FindKeyPointsPairs();
            st.Stop();
            Console.WriteLine("Neighbors Count: " + neighbors.Count + "\nin time: " + st.ElapsedMilliseconds.ToString());
            //ld.MergeImagesAndDrawLines(neighbors);

            

            st.Reset();
            st.Start();
            NeighbourhoodCoherenceFilter neighbourhoodCoherenceFilter = new NeighbourhoodCoherenceFilter(neighbors, Const.neighborsToCheck, Const.neighborsCondition);
            List<KeyValuePair<KeyPoint, KeyPoint>> filteredPairs = neighbourhoodCoherenceFilter.GetFilteredPairs();
            st.Stop();
            Console.WriteLine("AFTER FILTER");
            Console.WriteLine("pairs Count: " + filteredPairs.Count + "\nin time: " + st.ElapsedMilliseconds.ToString());
            //ld.MergeImagesAndDrawLines(filteredPairs);

            
            Console.WriteLine("RANSAC IN PROGRESS");
            st.Reset();
            st.Start();
            //ITransform transform = new AffineTransform();
            ITransform transform = new PerspectiveTransform();
            Ransac ransac = new Ransac(Const.iterationNumber, transform, Const.maxError);
            TransformFilter transformFilter = new TransformFilter(ransac);
            List<KeyValuePair<KeyPoint, KeyPoint>> ransacPairs = transformFilter.Filter(filteredPairs);
            st.Stop();
            Console.WriteLine("AFTER RANSCAC");
            Console.WriteLine("pairs Count: " + ransacPairs.Count + "\nin time: " + st.ElapsedMilliseconds.ToString());

            ld.MergeImagesAndDrawLines(ransacPairs);

    

            
            Console.ReadKey();

            #endregion

            //ConsoleInterface cI = new ConsoleInterface();






        }
    }
}
