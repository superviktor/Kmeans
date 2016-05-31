using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace KMeansClustering
{
    static class VisualizationController
    {
        public static void DisplayPoints(Canvas c, List<Point> points,int width, int height,int sizeKoef, int thickeness, Color defaultColor)
        {
            foreach (var p in points)
            {
                Ellipse elipse = new Ellipse();
                elipse.Fill = new SolidColorBrush(defaultColor);
                elipse.StrokeThickness = thickeness;
                elipse.Stroke = Brushes.Black;
                elipse.Width = width;
                elipse.Height = height;
                c.Children.Add(elipse);
                Canvas.SetTop(elipse, p.Y * sizeKoef);
                Canvas.SetLeft(elipse, p.X * sizeKoef);
            }
        }
        public static void DisplayPoints(Canvas c, List<DataItem> points, int width, int height, int sizeKoef, int thickeness, Color defaultColor)
        {
            foreach (var p in points)
            {
                Ellipse elipse = new Ellipse();
                elipse.Fill = new SolidColorBrush(defaultColor);
                elipse.StrokeThickness = thickeness;
                elipse.Stroke = Brushes.Black;
                elipse.Width = width;
                elipse.Height = height;
                c.Children.Add(elipse);
                Canvas.SetTop(elipse, p.Y * sizeKoef);
                Canvas.SetLeft(elipse, p.X * sizeKoef);
            }
        }

        public static void DisplayKMeansResult(ClusteringManager clustMan,Canvas canvas, int width, int height, int sizeKoef, int thickeness)
        {
            Random rnd = new Random();
            List<Color> clrs = new List<Color>();           
            for (int i = 0; i < 30; i++)
            {
                clrs.Add(Color.FromRgb((byte) rnd.Next(0,255), (byte)rnd.Next(0, 255), (byte)rnd.Next(0, 255)));
            }          
            var groupsToDisplay = clustMan.defaultData.GroupBy(s => s.Cluster).OrderBy(s => s.Key);
            foreach (var group in groupsToDisplay)
            {
                foreach (var item in group)
                {
                    Ellipse elipse = new Ellipse();
                    elipse.Fill = new SolidColorBrush(clrs[group.Key]);
                    elipse.StrokeThickness = thickeness;
                    elipse.Stroke = Brushes.Black;
                    elipse.Width = width;
                    elipse.Height = height;
                    Canvas.SetTop(elipse, item.Y * sizeKoef);
                    Canvas.SetLeft(elipse, item.X * sizeKoef);
                    canvas.Children.Add(elipse);
                }
            }
        }

        static void Clear(Canvas c)
        {
            c.Children.Clear();
        }

        public static void DisplayDBSCANResult(Canvas c, List<List<Point>> clusters, int width, int height, int sizeKoef, int thickeness)
        {
            Random rnd = new Random();
            List<Color> clrs = new List<Color>();
            for (int i = 0; i < 30; i++)
            {
                clrs.Add(Color.FromRgb((byte)rnd.Next(0, 255), (byte)rnd.Next(0, 255), (byte)rnd.Next(0, 255)));
            }
            int temp = 0;
            foreach (var clust in clusters)
            {               
                foreach (var p in clust)
                {
                    Ellipse elipse = new Ellipse();
                    elipse.Fill = new SolidColorBrush(clrs[temp]);
                    elipse.StrokeThickness = thickeness;
                    elipse.Stroke = Brushes.Black;
                    elipse.Width = width;
                    elipse.Height = height;
                    c.Children.Add(elipse);
                    Canvas.SetTop(elipse, p.Y * sizeKoef);
                    Canvas.SetLeft(elipse, p.X * sizeKoef);
                }
                temp++;
            }
        }

        public static void DisplayForel(Forel f, Canvas resultCanvas,int width, int height, int sizeKoef, int thickeness)
        {
            Random rnd = new Random();
            List<Color> clrs = new List<Color>();
            for (int i = 0; i < 30; i++)
            {
                clrs.Add(Color.FromRgb((byte)rnd.Next(0, 255), (byte)rnd.Next(0, 255), (byte)rnd.Next(0, 255)));
            }
            f.Cluster();
            foreach (var cluster in f.result)
            {
              
                var color = new SolidColorBrush(clrs[f.result.IndexOf(cluster)]);
                foreach (var p in cluster)
                {
                    Ellipse elipse = new Ellipse();
                    elipse.Fill = color;
                    elipse.StrokeThickness = thickeness;
                    elipse.Stroke = Brushes.Black;
                    elipse.Width = width;
                    elipse.Height = height;
                    Canvas.SetTop(elipse, p.Y * sizeKoef);
                    Canvas.SetLeft(elipse, p.X * sizeKoef);
                    resultCanvas.Children.Add(elipse);
                }
            }
        
            foreach (var c in f.centers)
            {
                var color = new SolidColorBrush(clrs[f.centers.IndexOf(c)]);
                Ellipse elps = new Ellipse();
                elps.Fill = color;
                elps.Opacity = 0.4;
                elps.StrokeThickness = 2;
                elps.Stroke = Brushes.Black;
                Canvas.SetTop(elps, c.Y * 25);
                Canvas.SetLeft(elps, c.X * 25);
                elps.Width = 5;
                elps.Height = 5;
                resultCanvas.Children.Add(elps);
            }
        }
    }
}
