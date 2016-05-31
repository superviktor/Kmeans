using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMeansClustering
{
    static class DataController
    {
        private static List<DataItem> data = new List<DataItem>();
        public static List<DataItem> GenerateData1()
        {
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    data.Add(new DataItem(i, j));
                }
            }
            return data;
        }

        public static List<DataItem> GenerateData2()
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    data.Add(new DataItem(i, j));
                }
            }
            for (int i = 20; i < 30; i++)
            {
                for (int j = 20; j < 30; j++)
                {
                    data.Add(new DataItem(i, j));
                }
            }

            data.Add(new DataItem(0, 30));
            data.Add(new DataItem(30, 0));
            return data;
        }
    }
}
