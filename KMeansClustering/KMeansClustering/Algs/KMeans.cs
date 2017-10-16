using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMeansClustering.Algs
{
	public abstract class KMeans
	{
		protected List<DataItem> defaultData;
		protected List<DataItem> normalizedDataToCluster;
		protected List<DataItem> clusters;
		protected int numberOfClusters;
		protected ExecuteParallel executeParallelWrapper;
		protected bool changed;
		protected bool success;

		public KMeans()
		{
			defaultData = new List<DataItem>();
			normalizedDataToCluster = new List<DataItem>();
			clusters = new List<DataItem>();
		}

		public List<DataItem> Data
		{
			get { return defaultData; }
			set { defaultData = value; }
		}

		public int NumOfClusters
		{
			get { return numberOfClusters; }
			set { numberOfClusters = value; }
		}

		public abstract void InitializeCentroids();

		public abstract void Cluster(List<DataItem> data, int numberOfClusters);

		protected void NormalizeData(List<DataItem> defaultDataParam)
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

		protected bool UpdateMeans()
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

		protected bool EmptyCluster(List<DataItem> data)
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

		protected bool UpdateClusterMembership()
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

		protected int MinIndex(double[] distances)
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

		public virtual void Execute()
		{
			NormalizeData(defaultData);

			for (int i = 0; i < numberOfClusters; i++)
			{
				clusters.Add(new DataItem() { Cluster = i });
			}
			Cluster(normalizedDataToCluster, numberOfClusters);
		}
	}
}
