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
    /// Interaction logic for TestDBSCAN.xaml
    /// </summary>
    public partial class TestDBSCAN : Window
    {
        public TestDBSCAN()
        {
            InitializeComponent();
        }

        private void Execute_Click(object sender, RoutedEventArgs e)
        {
            List<Point> points = new List<Point>();
            // sample data
            points.Add(new Point(0, 100));
            points.Add(new Point(0, 200));
            points.Add(new Point(0, 275));
            points.Add(new Point(100, 150));
            points.Add(new Point(200, 100));
            points.Add(new Point(250, 200));
            points.Add(new Point(0, 300));
            points.Add(new Point(100, 200));
            points.Add(new Point(600, 700));
            points.Add(new Point(650, 700));
            points.Add(new Point(675, 700));
            points.Add(new Point(675, 710));
            points.Add(new Point(675, 720));
            points.Add(new Point(50, 400));
            double eps = 100.0;
            int minPts = 3;
            List<List<Point>> clusters = GetClusters(points, eps, minPts);
            // print points to console
            result.Text+="The {0} points are :\n"+ points.Count;
            foreach (Point p in points) result.Text += " "+p;         
            // print clusters to console
            int total = 0;
            for (int i = 0; i < clusters.Count; i++)
            {
                int count = clusters[i].Count;
                total += count;
                string plural = (count != 1) ? "s" : "";
                result.Text+="\nCluster {0} consists of the following {1} point{2} :\n"+ i + 1+ " "+ count +" "+ plural;
                foreach (Point p in clusters[i]) result.Text += " " +p;
            }
            // print any points which are NOISE
            total = points.Count - total;
            if (total > 0)
            {
                string plural = (total != 1) ? "s" : "";
                string verb = (total != 1) ? "are" : "is";
                result.Text += "\nThe following {0} point{1} {2} NOISE :\n"+ total+" "+ plural+" "+ verb;
                foreach (Point p in points)
                {
                    if (p.ClusterId == Point.NOISE) result.Text+= " "+p;
                }
            }
            else
            {
                result.Text += "\nNo points are NOISE";
            }

        }


        private List<List<Point>> GetClusters(List<Point> points, double eps, int minPts)
        {
            if (points == null) return null;
            List<List<Point>> clusters = new List<List<Point>>();
            eps *= eps; // square eps
            int clusterId = 1;
            for (int i = 0; i < points.Count; i++)
            {
                Point p = points[i];
                if (p.ClusterId == Point.UNCLASSIFIED)
                {
                    if (ExpandCluster(points, p, clusterId, eps, minPts)) clusterId++;
                }
            }
            // sort out points into their clusters, if any
            int maxClusterId = points.OrderBy(p => p.ClusterId).Last().ClusterId;
            if (maxClusterId < 1) return clusters; // no clusters, so list is empty
            for (int i = 0; i < maxClusterId; i++) clusters.Add(new List<Point>());
            foreach (Point p in points)
            {
                if (p.ClusterId > 0) clusters[p.ClusterId - 1].Add(p);
            }
            return clusters;
        }

        private bool ExpandCluster(List<Point> points, Point p, int clusterId, double eps, int minPts)
        {
            List<Point> seeds = GetRegion(points, p, eps);
            if (seeds.Count < minPts) // no core point
            {
                p.ClusterId = Point.NOISE;
                return false;
            }
            else // all points in seeds are density reachable from point 'p'
            {
                for (int i = 0; i < seeds.Count; i++) seeds[i].ClusterId = clusterId;
                seeds.Remove(p);
                while (seeds.Count > 0)
                {
                    Point currentP = seeds[0];
                    List<Point> result = GetRegion(points, currentP, eps);
                    if (result.Count >= minPts)
                    {
                        for (int i = 0; i < result.Count; i++)
                        {
                            Point resultP = result[i];
                            if (resultP.ClusterId == Point.UNCLASSIFIED || resultP.ClusterId == Point.NOISE)
                            {
                                if (resultP.ClusterId == Point.UNCLASSIFIED) seeds.Add(resultP);
                                resultP.ClusterId = clusterId;
                            }
                        }
                    }
                    seeds.Remove(currentP);
                }
                return true;
            }
        }

        private List<Point> GetRegion(List<Point> points, Point p, double eps)
        {
            List<Point> region = new List<Point>();
            for (int i = 0; i < points.Count; i++)
            {
                int distSquared = Point.DistanceSquared(p, points[i]);
                if (distSquared <= eps) region.Add(points[i]);
            }
            return region;
        }
    }
      
}
