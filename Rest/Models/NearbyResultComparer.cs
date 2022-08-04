using System;
using System.Collections.Generic;

namespace Rest.Models
{
    public class NearbyResultComparer : IComparer<NearbyResult>
    {
        public int Compare(NearbyResult x, NearbyResult y)
        {
            if (object.ReferenceEquals(x, y))
            {
                return 0;
            }

            if (x == null)
            {
                return -1;
            }

            if (y == null)
            {
                return 1;
            }

            return x.Distance.CompareTo(y.Distance);
        }
    }
}
