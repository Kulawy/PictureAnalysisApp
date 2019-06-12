using System;
using System.Collections.Generic;

namespace PictureAnalysis.Model
{
    internal class NeighbourhoodComparator : IComparer<KeyPoint>
    {
        private KeyPoint _other;

        public NeighbourhoodComparator(KeyPoint other)
        {
            _other = other;
        }

        public int Compare(KeyPoint p1, KeyPoint p2)
        {
            return p1.CalculateDistance(_other).CompareTo(p2.CalculateDistance(_other));
        }
    }
}