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
    static class DBSCAN
    {
        public static List<DataItem> points = new List<DataItem>();
        public static double eps;
        public static int minPts;
        public static List<List<DataItem>> clusters = new List<List<DataItem>>();
        public static void Init(List<DataItem> data)
        {
            points = data;
        }

        public static void Execute()
        {
            clusters = GetClusters(points, eps, minPts);
        }

        private static List<List<DataItem>> GetClusters(List<DataItem> points, double eps, int minPts)
        {
            if (points == null) return null;
            List<List<DataItem>> clusters = new List<List<DataItem>>();
            eps *= eps; // square eps
            int clusterId = 1;
            for (int i = 0; i < points.Count; i++)
            {
                DataItem p = points[i];
                if (p.Cluster == DataItem.UNCLASSIFIED)
                {
                    if (ExpandCluster(points, p, clusterId, eps, minPts)) clusterId++;
                }
            }
            // sort out points into their clusters, if any
            int maxClusterId = points.OrderBy(p => p.Cluster).Last().Cluster;
            if (maxClusterId < 1) return clusters; // no clusters, so list is empty
            for (int i = 0; i < maxClusterId; i++) clusters.Add(new List<DataItem>());
            foreach (DataItem p in points)
            {
                if (p.Cluster > 0) clusters[p.Cluster - 1].Add(p);
            }
            return clusters;
        }

        static bool ExpandCluster(List<DataItem> points, DataItem p, int clusterId, double eps, int minPts)
        {
            List<DataItem> seeds = GetRegion(points, p, eps);
            if (seeds.Count < minPts) // no core point
            {
                p.Cluster = Point.NOISE;
                return false;
            }
            else // all points in seeds are density reachable from point 'p'
            {
                for (int i = 0; i < seeds.Count; i++) seeds[i].Cluster = clusterId;
                seeds.Remove(p);
                while (seeds.Count > 0)
                {
                    DataItem currentP = seeds[0];
                    List<DataItem> result = GetRegion(points, currentP, eps);
                    if (result.Count >= minPts)
                    {
                        for (int i = 0; i < result.Count; i++)
                        {
                            DataItem resultP = result[i];
                            if (resultP.Cluster == DataItem.UNCLASSIFIED || resultP.Cluster == DataItem.NOISE)
                            {
                                if (resultP.Cluster == Point.UNCLASSIFIED) seeds.Add(resultP);
                                resultP.Cluster = clusterId;
                            }
                        }
                    }
                    seeds.Remove(currentP);
                }
                return true;
            }
        }

        static List<DataItem> GetRegion(List<DataItem> points, DataItem p, double eps)
        {
            List<DataItem> region = new List<DataItem>();
            for (int i = 0; i < points.Count; i++)
            {
                int distSquared = DataItem.DistanceSquared(p, points[i]);
                if (distSquared <= eps) region.Add(points[i]);
            }
            return region;
        }
     
    }
}
