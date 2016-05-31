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
        ClusteringManager c = new ClusteringManager();
        Forel f = new Forel();        
        private void genetateBtb_Click(object sender, RoutedEventArgs e)
        {
            VisualizationController.DisplayDefaultData(defaultCanvas,DataController.GenerateData1());
        }


        private void TestButton_Click_1(object sender, RoutedEventArgs e)
        {
            //1 display k means 
            //c.numberOfClusters = 2;
            //c.SetDataDefaultData(DataController.GenerateData1());
            //c.Execute();
            //VisualizationController.DisplayResultData(resultCanvas, c.defaultData);


            //2 dbscan 
            DBSCAN.Init(DataController.GenerateData1());
            DBSCAN.Execute();
            VisualizationController.DisplayResultData(resultCanvas, DBSCAN.clusters);

            //3 forel 
            //f.SetData(DataController.GenerateData1());
            //f.Cluster();
            //VisualizationController.DisplayResultData(resultCanvas,f.result,f.centers);
        }
    }
}
