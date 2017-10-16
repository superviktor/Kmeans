using KMeansClustering.Algs;
using KMeansClustering.Utilities;
using System.Collections.Generic;
using System.Windows;

namespace KMeansClustering
{
	/// <summary>
	/// Interaction logic for Main.xaml
	/// </summary>
	public partial class Main : Window
    {
		MethodWrapper methodwrapper;
        SequentialKmeans sequentialKmeans = new SequentialKmeans();
		ParallelKMeans pkmeans = new ParallelKMeans();
		ManualParallelKmeans mpkm = new ManualParallelKmeans();

        Forel f = new Forel();
        List<DataItem> data = new List<DataItem>();
        private void genetateBtb_Click(object sender, RoutedEventArgs e)
        {
            Clear();
            data = DataController.GenerateData1();
            VisualizationController.DisplayDefaultData(defaultCanvas, data);
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Kmeans_Click(object sender, RoutedEventArgs e)
        {
            VisualizationController.Clear(resultCanvas);
			sequentialKmeans.NumOfClusters = int.Parse(tbNumOfCls.Text);
			sequentialKmeans.Data=data;
			methodwrapper = new MethodWrapper(sequentialKmeans.Execute);
			PerformanceEstimateManager.GetTimeSpan(methodwrapper);
            VisualizationController.DisplayResultData(resultCanvas, sequentialKmeans.Data);

        }

        private void Dbscan_Click(object sender, RoutedEventArgs e)
        {
            VisualizationController.Clear(resultCanvas);
            DBSCAN.eps = int.Parse(tbEps.Text);
            DBSCAN.minPts = int.Parse(tbMinPts.Text);
            DBSCAN.Init(data);
            DBSCAN.Execute();
            VisualizationController.DisplayResultData(resultCanvas, DBSCAN.clusters);

        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            VisualizationController.Clear(defaultCanvas);
            data.Clear();
            VisualizationController.Clear(resultCanvas);
        }

        private void genetateBtb_Click2(object sender, RoutedEventArgs e)
        {
            Clear();
            data = DataController.GenerateData2();
            VisualizationController.DisplayDefaultData(defaultCanvas, data);

        }

        private void Clear()
        {
            VisualizationController.Clear(defaultCanvas);
            data.Clear();
        }

        private void genetateBtb_Click3(object sender, RoutedEventArgs e)
        {
            Clear();
            data = DataController.GenerateData3();
            VisualizationController.DisplayDefaultData(defaultCanvas, data);

        }

        private void genetateBtb_Click4(object sender, RoutedEventArgs e)
        {
            Clear();
            data = data = DataController.GenerateData4();
            VisualizationController.DisplayDefaultData(defaultCanvas, data);
        }

        private void Forel_Click(object sender, RoutedEventArgs e)
        {
            f.R = double.Parse(tbRadius.Text);
            f.SetData(data);
            f.Cluster();
            VisualizationController.DisplayResultData(resultCanvas, f.result, f.centers);
        }

		private void ParallelKMeneans_Click(object sender, RoutedEventArgs e)
		{
			VisualizationController.Clear(resultCanvas);
			pkmeans.NumOfClusters = int.Parse(tbNumOfCls.Text);
			pkmeans.Data = data;
			methodwrapper = new MethodWrapper(pkmeans.Execute);
			PerformanceEstimateManager.GetTimeSpan(methodwrapper);
			VisualizationController.DisplayResultData(resultCanvas, pkmeans.Data);
		}

		private void ManualParallelKMeneans_Click(object sender, RoutedEventArgs e)
		{
			VisualizationController.Clear(resultCanvas);
			mpkm.Data = data;
			mpkm.NumOfClusters = int.Parse(tbNumOfCls.Text);
			methodwrapper = new MethodWrapper(mpkm.Execute);
			PerformanceEstimateManager.GetTimeSpan(methodwrapper);
			VisualizationController.DisplayResultData(resultCanvas, mpkm.Data);
		}
	}
}
