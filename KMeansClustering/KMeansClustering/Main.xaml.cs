using KMeansClustering.Algs;
using KMeansClustering.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace KMeansClustering
{
    /// <summary>
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class Main : Window
    {
		MethodWrapper methodwrapper;
        ClusteringManager c = new ClusteringManager();
		ParallelKMeans pkmeans = new ParallelKMeans();
		ManualParallelKmeans manParKmeans = new ManualParallelKmeans();
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
            c.numberOfClusters = int.Parse(tbNumOfCls.Text);
            c.SetDataDefaultData(data);
			methodwrapper = new MethodWrapper(c.Execute);
			PerformanceEstimateManager.GetTimeSpan(methodwrapper);
            VisualizationController.DisplayResultData(resultCanvas, c.defaultData);

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
			pkmeans.numberOfClusters = int.Parse(tbNumOfCls.Text);
			pkmeans.SetDataDefaultData(data);
			methodwrapper = new MethodWrapper(pkmeans.ExecuteParallel);
			PerformanceEstimateManager.GetTimeSpan(methodwrapper);
			VisualizationController.DisplayResultData(resultCanvas, pkmeans.defaultData);
		}

		private void ManualParallelKMeneans_Click(object sender, RoutedEventArgs e)
		{
			VisualizationController.Clear(resultCanvas);
			manParKmeans.numberOfClusters = int.Parse(tbNumOfCls.Text);
			manParKmeans.SetDataDefaultData(data);
			methodwrapper = new MethodWrapper(manParKmeans.Execute);
			PerformanceEstimateManager.GetTimeSpan(methodwrapper);
			VisualizationController.DisplayResultData(resultCanvas, manParKmeans.defaultData);
		}
	}
}
