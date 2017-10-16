using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KMeansClustering.Algs
{
	public delegate void NewMethodWrapper(int from, int to);
	public delegate void NewMethodWrapper11(List<DataItem> items,int from, int to);
	public class ManualParallelKmeans
	{
		public List<DataItem> defaultData = new List<DataItem>();
		public List<DataItem> normalizedDataToCluster = new List<DataItem>();
		public List<DataItem> clusters = new List<DataItem>();
		public int numberOfClusters = 0;
		NewMethodWrapper methodWrapper;
		NewMethodWrapper11 methodWrapper11;
		public void SetDataDefaultData(List<DataItem> data)
		{
			defaultData = data;
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

		double xSum;
		double ySum;
		double xMean;
		double yMean;
		double xAndxMeanSubPow;
		double yAndyMeanSubPow;
		double xSD;
		double ySD;

		private void GetXSum( int from, int to)
		{
			for (int i = from; i < to; i++)
			{
				xSum += defaultData[i].X;
			}

		}

		private void GetYSum( int from, int to)
		{
			for (int i = from; i < to; i++)
			{
				ySum += defaultData[i].Y;
			}
		}

		private void GetXAndxMeanSubPow( int from, int to)
		{
			for (int i = from; i < to; i++)
			{
				xAndxMeanSubPow += Math.Pow(defaultData[i].X - xMean, 2);
			}
		}

		private void GetYAndYMeanSubPow(int from, int to)
		{
			for (int i = from; i < to; i++)
			{
				yAndyMeanSubPow += Math.Pow(defaultData[i].Y - yMean, 2);
			}

		}

		private void SetNormalizedDataArray(int from, int to)
		{
			for (int i = from; i < to; i++)
			{
				normalizedDataToCluster.Add(new DataItem()
				{
					X = (defaultData[i].X - xMean) / xSD,
					Y = (defaultData[i].Y - yMean) / ySD
				});
			}	
		}

		private void SetRandomDataItemCluster(int from, int to)
		{
			Random rnd = new Random(numberOfClusters);
			for (int i = from; i < to; i++)
			{
				normalizedDataToCluster[i].Cluster = defaultData[i].Cluster = rnd.Next(0, numberOfClusters);
			}
		}

		private void GetXYSum(List<DataItem> items,int from, int to)
		{
			for (int i = from; i < to; i++)
			{
				x += items[i].X;
				y += items[i].Y;
			}
			
		}

		private void ExecuteParallel(int threads, NewMethodWrapper method)
		{

			int part = defaultData.Count / threads;
			Thread[] ts = new Thread[threads];
			for (int k = 0; k < ts.Length; k++)
			{
				int from = k * part;
				int to = k * part + part;
				ts[k] = new Thread(() => method.Invoke(from, to));
			}
			for (int k = 0; k < ts.Length; k++)
			{
				ts[k].Start();
			}
			for (int k = 0; k < ts.Length; k++)
			{
				ts[k].Join();
			}
		}

		private void ExecuteParallel2(List<DataItem> list,int threads, NewMethodWrapper11 method)
		{

			int part = list.Count / threads;
			Thread[] ts = new Thread[threads];
			for (int k = 0; k < ts.Length; k++)
			{
				int from = k * part;
				int to = k * part + part;
				ts[k] = new Thread(() => method.Invoke(list,from, to));
			}
			for (int k = 0; k < ts.Length; k++)
			{
				ts[k].Start();
			}
			for (int k = 0; k < ts.Length; k++)
			{
				ts[k].Join();
			}
		}

		//Gaussian Normalization 
		private void NormalizeData(List<DataItem> defaultDataParam)
		{
			methodWrapper = new NewMethodWrapper(GetXSum);
			ExecuteParallel(4, methodWrapper);
			methodWrapper = new NewMethodWrapper(GetYSum);
			ExecuteParallel(4, methodWrapper);
			xMean = xSum / defaultData.Count;
			yMean = ySum / defaultData.Count;
			methodWrapper = new NewMethodWrapper(GetXAndxMeanSubPow);
			ExecuteParallel(4, methodWrapper);
			methodWrapper = new NewMethodWrapper(GetYAndYMeanSubPow);
			ExecuteParallel(4, methodWrapper);
			Thread.Sleep(100);
			xSD = xAndxMeanSubPow / defaultData.Count;
			ySD = yAndyMeanSubPow / defaultData.Count;
			methodWrapper = new NewMethodWrapper(SetNormalizedDataArray);
			ExecuteParallel(4, methodWrapper);
		}

		private void InitializeCentroids()
		{		
			methodWrapper = new NewMethodWrapper(SetRandomDataItemCluster);
			ExecuteParallel(4, methodWrapper);		
		}

		double x;
		double y;
		public bool UpdateMeans()
		{
			if (EmptyCluster(normalizedDataToCluster))
				return false;
			var groupsToComputeMeans = normalizedDataToCluster.GroupBy(s => s.Cluster).OrderBy(s => s.Key);
			int clusterIndex = 0;
			x = 0;
			y = 0;
			foreach (var group in groupsToComputeMeans)
			{
				var groupList = group.ToList();
				methodWrapper11 = new NewMethodWrapper11(GetXYSum);
				ExecuteParallel2(groupList, 4, methodWrapper11);

				clusters[clusterIndex].X = x / group.Count();
				clusters[clusterIndex].Y = y / group.Count();
				clusterIndex++;
				x = 0;
				y = 0;
			}
			return true;

			//if (EmptyCluster(normalizedDataToCluster))
			//	return false;
			//var groupsToComputeMeans = normalizedDataToCluster.GroupBy(s => s.Cluster).OrderBy(s => s.Key);
			//int clusterIndex = 0;
			//double x = 0;
			//double y = 0;
			//foreach (var group in groupsToComputeMeans)
			//{
			//	foreach (var item in group)
			//	{
			//		x += item.X;
			//		y += item.Y;
			//	}
			//	clusters[clusterIndex].X = x / group.Count();
			//	clusters[clusterIndex].Y = y / group.Count();
			//	clusterIndex++;
			//	x = 0;
			//	y = 0;
			//}
			//return true;

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

		public void Execute()
		{
			NormalizeData(defaultData);
			Thread.Sleep(100);
			for (int i = 0; i < numberOfClusters; i++)
			{
				clusters.Add(new DataItem() { Cluster = i });
			}

			Cluster(normalizedDataToCluster, numberOfClusters);
		}
	}
}
