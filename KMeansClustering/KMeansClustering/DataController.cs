using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMeansClustering
{
    static class DataController
    {
        private static List<DataItem> kmeansData = new List<DataItem>();

        public static List<DataItem> GenerateDataForKMeans()
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    kmeansData.Add(new DataItem(i, j));
                }
            }
            return kmeansData;
        }
    }

    //need to generate data for dbscan
}
