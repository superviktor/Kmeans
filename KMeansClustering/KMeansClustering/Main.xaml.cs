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
        int num=1;
        List<DataItem> data = new List<DataItem>();
        private void genetateBtb_Click(object sender, RoutedEventArgs e)
        {
            VisualizationController.Clear(defaultCanvas);
            data.Clear();
            switch (num)
            {
                case 1:
                    data = DataController.GenerateData1();
                    VisualizationController.DisplayDefaultData(defaultCanvas, data);
                    break;
                case 2:
                    data = DataController.GenerateData2();
                    VisualizationController.DisplayDefaultData(defaultCanvas, data);
                    break;
                case 3:
                    data = data = DataController.GenerateData3();
                    VisualizationController.DisplayDefaultData(defaultCanvas, data);
                    break;
                case 4:
                    data = data = DataController.GenerateData4();
                    VisualizationController.DisplayDefaultData(defaultCanvas, data);
                    break;
            }
            num++;
            if (num > 4)
            {
                num = 1;
            }
        }

   

        private void TestButton_Click_1(object sender, RoutedEventArgs e)
        {
            //1 display k means 
            //c.numberOfClusters = 4;
            //c.SetDataDefaultData(data);
            //c.Execute();
            //VisualizationController.DisplayResultData(resultCanvas, c.defaultData);


            //2 dbscan 
            DBSCAN.Init(data);
            DBSCAN.Execute();
            VisualizationController.DisplayResultData(resultCanvas, DBSCAN.clusters);

            //3 forel 
            //f.SetData(data);
            //f.Cluster();
            //VisualizationController.DisplayResultData(resultCanvas, f.result, f.centers);
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            
        }

 

        private void Kmeans_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void Dbscan_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
