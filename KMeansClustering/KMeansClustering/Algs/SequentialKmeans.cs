using KMeansClustering.Algs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMeansClustering
{
    public class SequentialKmeans : KMeans
    {
        public override void InitializeCentroids()
        {
            Random rnd = new Random(numberOfClusters);
            //for (int i = 0; i < numberOfClusters; ++i)
            //{
            //    normalizedDataToCluster[i].Cluster = defaultData[i].Cluster = i;
            //}
            for (int i = 0; i < normalizedDataToCluster.Count; i++)
            {
                normalizedDataToCluster[i].Cluster = defaultData[i].Cluster = rnd.Next(0, numberOfClusters);
            }
        }

        public override void Cluster(List<DataItem> data, int numberOfClusters)
        {
            bool changed = true;
            bool success = true;
            InitializeCentroids();
            int maxIteration = data.Count*10;
            int threshold = 0;
            while (success && changed && threshold<maxIteration)
            {
                ++threshold;
                success = UpdateMeans();
                changed = UpdateClusterMembership();
            }
        }

    }
}
