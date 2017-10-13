using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMeansClustering
{
    public class DataItem
    {
        public double X { get; set; }
        public double Y { get; set; }
        public int Cluster { get; set; }
        public const int NOISE = -1;
        public const int UNCLASSIFIED = 0;
        public DataItem(double x , double y)
        {
            X = x;
            Y = y;
            Cluster = 0;
        }

        public DataItem()
        {
            
        }

        public static int DistanceSquared(DataItem p1, DataItem p2)
        {
            double diffX = p2.X - p1.X;
            double diffY = p2.Y - p1.Y;
            return (int) (diffX * diffX + diffY * diffY);
        }

    }
}
