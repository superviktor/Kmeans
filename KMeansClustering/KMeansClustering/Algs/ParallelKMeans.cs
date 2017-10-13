using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KMeansClustering.Algs
{
	public class ParallelKMeans
	{
		public List<DataItem> defaultData = new List<DataItem>();
		public List<DataItem> normalizedDataToCluster = new List<DataItem>();
		public List<DataItem> clusters = new List<DataItem>();
		public int numberOfClusters = 0;
		public void SetDataDefaultData(List<DataItem> data)
		{
			defaultData = data;
		}

		//Gaussian Normalization 
		public void NormalizeData(List<DataItem> defaultDataParam)
		{
			double xSum = 0;
			double ySum = 0;
			foreach (var dataItem in defaultDataParam)
			{
				xSum += dataItem.X;
				ySum += dataItem.Y;
			}
			double xMean = xSum / defaultDataParam.Count;
			double yMean = ySum / defaultDataParam.Count;
			double xAndxMeanSubPow = 0;
			double yAndyMeanSubPow = 0;
			foreach (var dataItem in defaultDataParam)
			{
				xAndxMeanSubPow += Math.Pow(dataItem.X - xMean, 2);
				yAndyMeanSubPow += Math.Pow(dataItem.Y - yMean, 2);
			}
			double xSD = xAndxMeanSubPow / defaultDataParam.Count;
			double ySD = yAndyMeanSubPow / defaultDataParam.Count;
			foreach (var dataItem in defaultDataParam)
			{
				normalizedDataToCluster.Add(new DataItem()
				{
					X = (dataItem.X - xMean) / xSD,
					Y = (dataItem.Y - yMean) / ySD
				});
			}
		}

		public void InitializeCentroids()
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

		public bool UpdateMeans()
		{
			if (EmptyCluster(normalizedDataToCluster))
				return false;
			var groupsToComputeMeans = normalizedDataToCluster.GroupBy(s => s.Cluster).OrderBy(s => s.Key);
			int clusterIndex = 0;
			double x = 0;
			double y = 0;
			foreach (var group in groupsToComputeMeans)
			{
				foreach (var item in group)
				{
					x += item.X;
					y += item.Y;
				}
				clusters[clusterIndex].X = x / group.Count();
				clusters[clusterIndex].Y = y / group.Count();
				clusterIndex++;
				x = 0;
				y = 0;
			}
			return true;
		}

		private bool EmptyCluster(List<DataItem> data)
		{
			var emptyCluster =
			data.GroupBy(s => s.Cluster).OrderBy(s => s.Key).Select(g => new { Cluster = g.Key, Count = g.Count() });

			foreach (var item in emptyCluster)
			{
				if (item.Count == 0)
				{
					return true;
				}
			}
			return false;
		}

		public bool UpdateClusterMembership()
		{
			EuclideanDistance ed = new EuclideanDistance();
			bool changed = false;
			double[] distances = new double[numberOfClusters];
			for (int i = 0; i < normalizedDataToCluster.Count; ++i)
			{
				for (int j = 0; j < numberOfClusters; ++j)
				{
					distances[j] = ed.GetDistance(normalizedDataToCluster[i], clusters[j]);
					int newClusterIndex = MinIndex(distances);
					if (newClusterIndex != normalizedDataToCluster[i].Cluster)
					{
						changed = true;
						normalizedDataToCluster[i].Cluster = defaultData[i].Cluster = newClusterIndex;
					}
				}
			}
			if (changed == false)
				return false;
			if (EmptyCluster(normalizedDataToCluster))
				return false;
			return true;
		}

		public int MinIndex(double[] distances)
		{
			if (distances.Length <= 0)
			{
				return -1;
			}
			int minIndex = 0;
			double minDistance = distances[0];
			for (int i = 0; i < distances.Length; ++i)
			{
				if (distances[i] < minDistance)
				{
					minDistance = distances[i];
					minIndex = i;
				}
			}
			return minIndex;
		}

		public void Cluster(List<DataItem> data, int numberOfClusters)
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

		public void ExecuteParallel()
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
