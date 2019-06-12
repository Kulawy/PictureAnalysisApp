using Emgu;
using Emgu.CV;
using Emgu.CV.Stitching;
using Emgu.CV.Structure;
using Emgu.CV.UI;
using Emgu.CV.Util;
using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureAnalysis.Model
{
    public class LetDraw
    {
        private Random _rnd;

        public LetDraw()
        {
            _rnd = new Random();
        }

        public void MergeImages()
        {
            
            Mat img1 = CvInvoke.Imread(Const.imagesFolderDirectory + Const.imgName01 + ".png");
            Mat img2 = CvInvoke.Imread(Const.imagesFolderDirectory + Const.imgName02 + ".png");
            int width = Math.Max(img1.Width, img2.Width);
            Mat combine = Mat.Zeros(img1.Height+img2.Height, width, Emgu.CV.CvEnum.DepthType.Cv16S, 3);
            Stitcher sticher = new Stitcher(Stitcher.Mode.Scans);
            //VectorOfMat vom = new VectorOfMat();
            //vom.Push(img1);
            //vom.Push(img2);
            //sticher.Stitch(vom, combine);
            img1.PushBack(img2);
            

            string window1 = "TestWindow";
            CvInvoke.NamedWindow(window1, Emgu.CV.CvEnum.NamedWindowType.Normal);
            

            CvInvoke.Imshow(window1, img1);
            CvInvoke.WaitKey(0);
        }

        public void MergeImagesAndDrawLines(Dictionary<KeyPoint, KeyPoint> points)
        {

            Mat img1 = CvInvoke.Imread(Const.imagesFolderDirectory + Const.imgName01 + ".png");
            Mat img2 = CvInvoke.Imread(Const.imagesFolderDirectory + Const.imgName02 + ".png");
            int width = Math.Max(img1.Width, img2.Width);
            Mat combine = Mat.Zeros(img1.Height + img2.Height, width, Emgu.CV.CvEnum.DepthType.Cv16S, 3);
            Stitcher sticher = new Stitcher(Stitcher.Mode.Scans);
            img1.PushBack(img2);

            DrawLines(points, img1);

            string window1 = "TestWindow";
            CvInvoke.NamedWindow(window1, Emgu.CV.CvEnum.NamedWindowType.Normal);
            
            CvInvoke.Imshow(window1, img1);
            CvInvoke.WaitKey(0);
        }

        public void MergeImagesAndDrawLines(List<KeyValuePair<KeyPoint, KeyPoint>> points)
        {

            Mat img1 = CvInvoke.Imread(Const.imagesFolderDirectory + Const.imgName01 + ".png");
            Mat img2 = CvInvoke.Imread(Const.imagesFolderDirectory + Const.imgName02 + ".png");
            int width = Math.Max(img1.Width, img2.Width);
            Mat combine = Mat.Zeros(img1.Height + img2.Height, width, Emgu.CV.CvEnum.DepthType.Cv16S, 3);
            Stitcher sticher = new Stitcher(Stitcher.Mode.Scans);
            img1.PushBack(img2);

            DrawLines(points, img1);

            string window1 = "TestWindow";
            CvInvoke.NamedWindow(window1, Emgu.CV.CvEnum.NamedWindowType.Normal);

            CvInvoke.Imshow(window1, img1);
            CvInvoke.WaitKey(0);
        }



        public void DrawLines(Dictionary<KeyPoint, KeyPoint> points, Mat img)
        {
            foreach(KeyValuePair<KeyPoint, KeyPoint> kps in points)
            {
                Point p1 = new Point((int) kps.Key.CoordX, (int) kps.Key.CoordY);
                Point p2 = new Point( (int) kps.Value.CoordX, (int) kps.Value.CoordY + img.Height/2);
                DrawLineOnImage(p1, p2, img);
            }
        }

        public void DrawLines(List<KeyValuePair<KeyPoint, KeyPoint>> points, Mat img)
        {
            foreach (KeyValuePair<KeyPoint, KeyPoint> kps in points)
            {
                Point p1 = new Point((int)kps.Key.CoordX, (int)kps.Key.CoordY);
                Point p2 = new Point((int)kps.Value.CoordX, (int)kps.Value.CoordY + img.Height / 2);
                DrawLineOnImage(p1, p2, img);
            }
        }

        public void DrawLineOnImage(Point p1, Point p2, Mat img)
        {
            //LineSegment2D line = new LineSegment2D(p1, p2);
            int thickness = 2;
            CvInvoke.Line(img, p1, p2, new MCvScalar( _rnd.NextDouble() * 256, _rnd.NextDouble() * 256, _rnd.NextDouble() * 256), thickness, Emgu.CV.CvEnum.LineType.Filled );


        }

    }
}
