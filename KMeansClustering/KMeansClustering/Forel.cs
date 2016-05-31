using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMeansClustering
{
    public class Forel
    {
        public List<DataItem> points = new List<DataItem>();
        public List<List<DataItem>> result = new List<List<DataItem>>();
        public List<DataItem> centers = new List<DataItem>();
        public double R = 1.65;

        public void GetData()
        {
            String input = File.ReadAllText("inputFile.txt");
            int i = 0;
            int j = 0;
            double[,] resultArray = new double[17, 2];
            foreach (string row in input.Split('\n'))
            {
                j = 0;

                foreach (string col in row.Trim().Split(','))
                {
                    resultArray[i, j] = Double.Parse(col, CultureInfo.InvariantCulture);
                    j++;
                }
                i++;
            }

            for (int k = 0; k < resultArray.GetLength(0); k++)
            {
                for (int l = 0; l < resultArray.GetLength(1); l++)
                {
                    points.Add(new DataItem(resultArray[k, l], resultArray[k, l + 1]));
                    break;
                }
            }
        }

        public void Cluster()
        {

            while (points.Count > 0)
            {

                Random rnd = new Random();
                var index = rnd.Next(0, points.Count);
                DataItem center = points[index];
                DataItem newCenter = center;
                //inside points 
                List<DataItem> lst = new List<DataItem>();
                while (center == newCenter)
                {
                    foreach (var p in points)
                    {
                        if (Math.Sqrt(Math.Pow((p.X - center.X), 2) + Math.Pow((p.Y - center.Y), 2)) < R)
                        {
                            lst.Add(p);
                        }
                    }
                    //power center
                    double powerX = 0;
                    double powerY = 0;
                    foreach (var p in lst)
                    {
                        powerX += p.X;
                        powerY += p.Y;
                    }
                    double powerCenterX = powerX / lst.Count;
                    double powerCenterY = powerY / lst.Count;
                    newCenter = new DataItem(powerCenterX, powerCenterY);
                }
                result.Add(lst);
                centers.Add(newCenter);
                foreach (var p in lst)
                {
                    points.Remove(p);
                }
            }
        }
    }
}
