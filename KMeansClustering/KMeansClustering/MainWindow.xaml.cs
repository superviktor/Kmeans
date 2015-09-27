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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KMeansClustering
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        ClusteringManager c = new ClusteringManager();
        
        private void DisplayDefaultData_Click(object sender, RoutedEventArgs e)
        {
            c.numberOfClusters = Convert.ToInt32(nuOfClusters.Text);
            c.GetDefaultData();

            foreach (var p in c.defaultData)
            {
                Ellipse elipse = new Ellipse();
                elipse.Fill = new SolidColorBrush(Colors.LightSlateGray);    
                elipse.StrokeThickness = 1;
                elipse.Stroke = Brushes.Black;
                elipse.Width = 15;
                elipse.Height = 15;
                Canvas.SetTop(elipse, p.Y * 15);
                Canvas.SetLeft(elipse, p.X * 15);
                defaultcanvas.Children.Add(elipse);
            }
        }

        private void Clustering_Click(object sender, RoutedEventArgs e)
        {
            resultcanvas.Children.Clear();
            c.Execute();    
            Color[] clrs = new Color[]{Colors.Red,Colors.SkyBlue,Colors.Orange,Colors.ForestGreen,Colors.Blue,Colors.DeepPink,Colors.Lime,Colors.BlueViolet,Colors.Aqua};
            var groupsToDisplay = c.defaultData.GroupBy(s => s.Cluster).OrderBy(s => s.Key);
            foreach (var group in groupsToDisplay)
            {                                         
                foreach (var item in group)
                {
                    Ellipse elipse = new Ellipse();
                    elipse.Fill = new SolidColorBrush(clrs[group.Key]);   
                    elipse.StrokeThickness = 1;
                    elipse.Stroke = Brushes.Black;
                    elipse.Width = 15;
                    elipse.Height = 15;
                    Canvas.SetTop(elipse, item.Y * 15);
                    Canvas.SetLeft(elipse, item.X * 15);
                    resultcanvas.Children.Add(elipse);
                }
            }
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            c = new ClusteringManager();
            defaultcanvas.Children.Clear();
            resultcanvas.Children.Clear();
        }
    }
}
