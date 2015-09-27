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

        public DataItem(double x , double y)
        {
            X = x;
            Y = y;
            Cluster = 0;
        }

        public DataItem()
        {
            
        }

    }
}
