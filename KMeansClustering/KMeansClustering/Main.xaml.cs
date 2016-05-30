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
        List<Point> testPoints = new List<Point>();
        ClusteringManager c = new ClusteringManager();
        public Main()
        {
            InitializeComponent();
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    testPoints.Add(new Point(i, j));
                }
            }
        }    

        private void genetateBtb_Click(object sender, RoutedEventArgs e)
        {
            //1 kmeans display default data
            //VisualizationController.DisplayPoints(defaultCanvas, testPoints, 15, 15, 15, 1, Color.FromRgb(0, 0, 0));

            //2 display  dbscan  default data    
            DBSCAN.Execute();
            VisualizationController.DisplayPoints(defaultCanvas, DBSCAN.points,10,10,10,1,Color.FromRgb(0,0,0));  
        }


        private void TestButton_Click_1(object sender, RoutedEventArgs e)
        {
            //1 display k means 
            //c.numberOfClusters = 5;
            //c.GetDefaultData();
            //c.Execute();
            //VisualizationController.DisplayKMeansResult(c, resultCanvas, 15, 15, 15, 1);


            //2 dbscan 
            VisualizationController.DisplayDBSCANResult(resultCanvas,DBSCAN.clusters,10,10,10,1);

        }
    }
}
