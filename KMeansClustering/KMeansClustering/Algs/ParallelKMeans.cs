using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KMeansClustering.Algs
{
	public class ParallelKMeans:KMeans
	{
		public override void InitializeCentroids()
		{
			Random rnd = new Random(numberOfClusters);
			for (int i = 0; i < numberOfClusters; ++i)
			{
				normalizedDataToCluster[i].Cluster = defaultData[i].Cluster = i;
			}
			for (int i = numberOfClusters; i < normalizedDataToCluster.Count; i++)
			{
				normalizedDataToCluster[i].Cluster = defaultData[i].Cluster = rnd.Next(0, numberOfClusters);
			}
		}

		public override void Cluster(List<DataItem> data, int numberOfClusters)
		{
			bool changed = true;
			bool success = true;
			InitializeCentroids();
			int maxIteration = data.Count * 10;
			int threshold = 0;
			while (success && changed && threshold < maxIteration)
			{
				++threshold;
				success = UpdateMeans();
				changed = UpdateClusterMembership();
			}
		}

		public override void Execute()
		{
			NormalizeData(defaultData);
			for (int i = 0; i < numberOfClusters; i++)
			{
				clusters.Add(new DataItem() { Cluster = i });
			}
			ThreadPool.QueueUserWorkItem(new WaitCallback(delegate (object state) { Cluster(normalizedDataToCluster, numberOfClusters); ; }), null);
		}
	}
}
