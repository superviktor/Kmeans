using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace KMeansClustering.Algs
{
	public delegate void ExecuteParallel(int from, int to);
	public class ManualParallelKmeans:KMeans
	{
		//Gaussian Normalization 
		//public void NormalizeData(List<DataItem> defaultDataParam)
		//{

		//	double xSum = 0;
		//	double ySum = 0;

		//	Parallel.ForEach<DataItem>(defaultDataParam, (dataItem) =>
		//	{
		//		xSum += dataItem.X;
		//		ySum += dataItem.Y;
		//	});

		//	double xMean = xSum / defaultDataParam.Count;
		//	double yMean = ySum / defaultDataParam.Count;
		//	double xAndxMeanSubPow = 0;
		//	double yAndyMeanSubPow = 0;

		//	Parallel.ForEach<DataItem>(defaultDataParam, (dataItem) =>
		//	{
		//		xAndxMeanSubPow += Math.Pow(dataItem.X - xMean, 2);
		//		yAndyMeanSubPow += Math.Pow(dataItem.Y - yMean, 2);
		//	});

		//	double xSD = xAndxMeanSubPow / defaultDataParam.Count;
		//	double ySD = yAndyMeanSubPow / defaultDataParam.Count;

		//	Parallel.ForEach<DataItem>(defaultDataParam, (dataItem) =>
		//	{
		//		normalizedDataToCluster.Add(new DataItem()
		//		{
		//			X = (dataItem.X - xMean) / xSD,
		//			Y = (dataItem.Y - yMean) / ySD
		//		});
		//	});

		//}

		public override void InitializeCentroids()
		{
			Random rnd = new Random(numberOfClusters);
			Parallel.For(0, normalizedDataToCluster.Count, (i) =>
			{
				normalizedDataToCluster[i].Cluster = defaultData[i].Cluster = rnd.Next(0, numberOfClusters);
			});

		}

		private void ExecuteParallel(int threads, ExecuteParallel wrapper, int max)
		{
			int part = defaultData.Count / threads;
			Thread[] ts = new Thread[threads];
			for (int k = 0; k < ts.Length; k++)
			{
				int from = k * part;
				int to = k * part + part;
				ts[k] = new Thread(() => wrapper.Invoke(from, to));
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

		private void Update(int f, int t)
		{
			success = UpdateMeans();
			changed = UpdateClusterMembership();
		}

		public override void Cluster(List<DataItem> data, int numberOfClusters)
		{
			changed = true;
			success = true;
			InitializeCentroids();
			int maxIteration = data.Count * 10;
			if (success && changed)
			{
				executeParallelWrapper = new ExecuteParallel(Update);
				ExecuteParallel(4, executeParallelWrapper, maxIteration);
			}
		}

	}
}
