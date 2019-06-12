using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureAnalysis.Model
{
    public class KeyPoint
    {
        public double CoordX { get; }
        public double CoordY { get; }
        public double ParamA { get;  }
        public double ParamB { get;  }
        public double ParamC { get;  }
        public double Distance { get; set; }

        public List<double> Traits { get; }

        public KeyPoint(double coordX, double coordY)
        {
            CoordX = coordX;
            CoordY = coordY;
        }

        public KeyPoint(double coordX, double coordY, List<double> traits)
        {
            CoordX = coordX;
            CoordY = coordY;
            Traits = traits;
        }

        public KeyPoint(string[] inputLine)
        {
            CoordX = double.Parse(inputLine[0].Replace('.', ','));
            CoordY = double.Parse(inputLine[1].Replace('.', ','));
            ParamA = double.Parse(inputLine[2].Replace('.', ','));
            ParamB = double.Parse(inputLine[3].Replace('.', ','));
            ParamC = double.Parse(inputLine[4].Replace('.', ','));

            Traits = new List<double>();
            for (int i = 5; i < inputLine.Count(); i++){
                Traits.Add(double.Parse(inputLine[i]));
            }

            Distance = -1;
        }

        public int CalculateDistance(KeyPoint other)
        {
            int distance = 0;
            //if ( p1.Traits.Count == p2.Traits.Count)
            //{
            //    Console.WriteLine("Same count of Traits");
            //}

            //Vector<double> v1 = CreateVector.SparseOfEnumerable<double>(p1.Traits);
            //Vector<double> v2 = CreateVector.SparseOfEnumerable<double>(p2.Traits);
            //distance = Distance.Euclidean(v1, v2);

            for (int i = 0; i < Traits.Count; i++)
            {
                distance += Math.Abs( ( int) Traits[i] - (int) other.Traits[i]);
            }

            return distance;
        }

        public override string ToString()
        {
            string result = "";
            result += " CoordX: " + CoordX.ToString();
            result += " CoordY: " + CoordY.ToString();
            result += " ParamA: " + ParamA.ToString();
            result += " ParamB: " + ParamB.ToString();
            result += " ParamC: " + ParamC.ToString();

            result += "\nTraits: ";
            for (int i=0; i < Traits.Count; i++)
            {
                result += Traits[i].ToString() + " | ";
            }
            result += "\nDistance to neighbour: " + Distance.ToString();

            return result;

        }

        public override bool Equals(object obj)
        {
            var point = obj as KeyPoint;
            return point != null &&
                   CoordX == point.CoordX &&
                   CoordY == point.CoordY &&
                   ParamA == point.ParamA &&
                   ParamB == point.ParamB &&
                   ParamC == point.ParamC;
                   //Distance == point.Distance &&
                   //EqualityComparer<List<int>>.Default.Equals(Traits, point.Traits);
        }

        public override int GetHashCode()
        {
            var hashCode = 892106565;
            hashCode = hashCode * -1521134295 + CoordX.GetHashCode();
            hashCode = hashCode * -1521134295 + CoordY.GetHashCode();
            hashCode = hashCode * -1521134295 + ParamA.GetHashCode();
            hashCode = hashCode * -1521134295 + ParamB.GetHashCode();
            hashCode = hashCode * -1521134295 + ParamC.GetHashCode();
            hashCode = hashCode * -1521134295 + Distance.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<List<double>>.Default.GetHashCode(Traits);
            return hashCode;
        }

        //public static bool operator ==(KeyPoint kp1, KeyPoint kp2)
        //{
        //    bool flag = true;
        //    if (kp1 == null || kp2 == null)
        //    {
        //        return false;
        //    }
        //    if ( kp1.CoordX != kp2.CoordX )
        //    {
        //        flag = false;
        //    }
        //    if (kp1.CoordY != kp2.CoordY)
        //    {
        //        flag = false;
        //    }
        //    if (kp1.ParamA != kp2.ParamA)
        //    {
        //        flag = false;
        //    }
        //    if (kp1.ParamB != kp2.ParamB)
        //    {
        //        flag = false;
        //    }
        //    if (kp1.ParamC != kp2.ParamC)
        //    {
        //        flag = false;
        //    }
            
        //    return flag;

        //}

        //public static bool operator !=(KeyPoint kp1, KeyPoint kp2)
        //{
        //    bool flag = false;
        //    if (kp1 == null || kp2 == null)
        //    {
        //        return true;
        //    }
        //    if (kp1.CoordX != kp2.CoordX)
        //    {
        //        flag = true;
        //    }
        //    if (kp1.CoordY != kp2.CoordY)
        //    {
        //        flag = true;
        //    }
        //    if (kp1.ParamA != kp2.ParamA)
        //    {
        //        flag = true;
        //    }
        //    if (kp1.ParamB != kp2.ParamB)
        //    {
        //        flag = true;
        //    }
        //    if (kp1.ParamC != kp2.ParamC)
        //    {
        //        flag = true;
        //    }

        //    return flag;
        //}

    }
}
