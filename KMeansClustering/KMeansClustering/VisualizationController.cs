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
    public static class VisualizationController
    {
        public static void DisplayDefaultData(Canvas c, List<DataItem> points)
        {
			if (c == null)
			{
				throw new Exception("Canvas is null");
			}
            foreach (var p in points)
            {
                Ellipse elipse = new Ellipse();
                elipse.Fill = new SolidColorBrush(Color.FromRgb(0,0,0));
                elipse.StrokeThickness = 1;
                elipse.Stroke = Brushes.Black;
                elipse.Width = 15;
                elipse.Height = 15;
                c.Children.Add(elipse);
                Canvas.SetTop(elipse, p.Y * 15);
                Canvas.SetLeft(elipse, p.X * 15);
            }
        }

        public static void DisplayResultData(Canvas canvas, List<DataItem> data)
        {
            Random rnd = new Random();
            List<Color> clrs = new List<Color>();           
            for (int i = 0; i < 30; i++)
            {
                clrs.Add(Color.FromRgb((byte) rnd.Next(0,255), (byte)rnd.Next(0, 255), (byte)rnd.Next(0, 255)));
            }          
            var groupsToDisplay = data.GroupBy(s => s.Cluster).OrderBy(s => s.Key);
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
                    canvas.Children.Add(elipse);
                }
            }
        }

        public static void Clear(Canvas c)
        {
            c.Children.Clear();
        }

        public static void DisplayResultData(Canvas canvas, List<List<DataItem>> clusters)
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
                    elipse.StrokeThickness = 1;
                    elipse.Stroke = Brushes.Black;
                    elipse.Width = 15;
                    elipse.Height = 15;
                    canvas.Children.Add(elipse);
                    Canvas.SetTop(elipse, p.Y * 15);
                    Canvas.SetLeft(elipse, p.X * 15);
                }
                temp++;
            }
        }

        public static void DisplayResultData(Canvas canvas, List<List<DataItem>> clusters, List<DataItem> centers)
        {
            Random rnd = new Random();
            List<Color> clrs = new List<Color>();
            for (int i = 0; i < 30; i++)
            {
                clrs.Add(Color.FromRgb((byte)rnd.Next(0, 255), (byte)rnd.Next(0, 255), (byte)rnd.Next(0, 255)));
            }
            foreach (var cluster in clusters)
            {

                var color = new SolidColorBrush(clrs[clusters.IndexOf(cluster)]);
                foreach (var p in cluster)
                {
                    Ellipse elipse = new Ellipse();
                    elipse.Fill = color;
                    elipse.StrokeThickness = 1;
                    elipse.Stroke = Brushes.Black;
                    elipse.Width = 15;
                    elipse.Height = 15;
                    Canvas.SetTop(elipse, p.Y * 15);
                    Canvas.SetLeft(elipse, p.X * 15);
                    canvas.Children.Add(elipse);
                }
            }

            foreach (var c in centers)
            {
                var color = new SolidColorBrush(clrs[centers.IndexOf(c)]);
                Ellipse elps = new Ellipse();
                elps.Fill = color;
                elps.Opacity = 0.4;
                elps.StrokeThickness = 2;
                elps.Stroke = Brushes.Black;
                Canvas.SetTop(elps, c.Y * 25);
                Canvas.SetLeft(elps, c.X * 25);
                elps.Width = 5;
                elps.Height = 5;
                canvas.Children.Add(elps);
            }
        }
    }
}
