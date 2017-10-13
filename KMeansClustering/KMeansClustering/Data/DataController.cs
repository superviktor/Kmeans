using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMeansClustering
{
    public static class DataController
    {
        private static List<DataItem> data = new List<DataItem>();

        public static List<DataItem> GenerateData1()
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    data.Add(new DataItem(i, j));
                }
            }
            for (int i = 11; i < 21; i++)
            {
                for (int j = 11; j < 21; j++)
                {
                    data.Add(new DataItem(i, j));
                }
            }
            return data;
        }

        public static List<DataItem> GenerateData2()
        {
            for (int i = 0; i < 50; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    data.Add(new DataItem(i, j));
                }
            }

            for (int i = 0; i < 3; i++)
            {
                for (int j = 7; j < 18; j++)
                {
                    data.Add(new DataItem(i, j));
                }
            }

            for (int i = 10; i < 13; i++)
            {
                for (int j = 7; j < 27; j++)
                {
                    data.Add(new DataItem(i, j));
                }
            }

            for (int i = 18; i < 22; i++)
            {
                for (int j = 7; j < 15; j++)
                {
                    data.Add(new DataItem(i, j));
                }
            }

            for (int i = 26; i < 30; i++)
            {
                for (int j = 7; j < 15; j++)
                {
                    data.Add(new DataItem(i, j));
                }
            }

            for (int i = 18; i < 36; i++)
            {
                for (int j = 18; j < 21; j++)
                {
                    data.Add(new DataItem(i, j));
                }
            }

            for (int i = 40; i < 42; i++)
            {
                for (int j = 7; j < 25; j++)
                {
                    data.Add(new DataItem(i, j));
                }
            }


            return data;
        }

        public static List<DataItem> GenerateData3()
        {
            data.Add(new DataItem(1, 1));
            data.Add(new DataItem(2, 1));
            data.Add(new DataItem(1, 2));
            data.Add(new DataItem(2, 2));
            data.Add(new DataItem(1, 3));
            data.Add(new DataItem(2, 3));
            data.Add(new DataItem(1, 4));
            data.Add(new DataItem(2, 4));
            data.Add(new DataItem(1, 5));
            data.Add(new DataItem(2, 5));
            data.Add(new DataItem(1, 6));
            data.Add(new DataItem(2, 6));
            data.Add(new DataItem(1, 7));
            data.Add(new DataItem(2, 7));
            data.Add(new DataItem(1, 8));
            data.Add(new DataItem(2, 8));
            data.Add(new DataItem(1.2, 9));
            data.Add(new DataItem(2.2, 9));
            data.Add(new DataItem(1.5, 10));
            data.Add(new DataItem(2.5, 10));
            data.Add(new DataItem(1.7, 11));
            data.Add(new DataItem(2.7, 11));
            data.Add(new DataItem(2, 12));
            data.Add(new DataItem(3, 12));
            data.Add(new DataItem(3, 13));
            data.Add(new DataItem(4, 13));
            data.Add(new DataItem(4, 14));
            data.Add(new DataItem(5, 14));
            data.Add(new DataItem(5, 15));
            data.Add(new DataItem(6, 15));
            data.Add(new DataItem(6, 16));
            data.Add(new DataItem(7, 16));
            data.Add(new DataItem(8, 16));
            data.Add(new DataItem(8, 17));
            data.Add(new DataItem(9, 16));
            data.Add(new DataItem(9, 17));
            data.Add(new DataItem(10, 16));
            data.Add(new DataItem(10, 17));
            data.Add(new DataItem(11, 16));
            data.Add(new DataItem(11, 17));
            data.Add(new DataItem(12, 16));
            data.Add(new DataItem(12, 17));
            data.Add(new DataItem(13, 16));
            data.Add(new DataItem(13, 17));
            data.Add(new DataItem(14, 16));
            data.Add(new DataItem(14, 17));
            data.Add(new DataItem(15, 16));
            data.Add(new DataItem(16, 16));
            data.Add(new DataItem(16, 15));
            data.Add(new DataItem(17, 15));
            data.Add(new DataItem(18, 15));
            data.Add(new DataItem(17, 14));
            data.Add(new DataItem(18, 14));
            data.Add(new DataItem(17, 13));
            data.Add(new DataItem(18, 13));
            data.Add(new DataItem(17, 12));
            data.Add(new DataItem(18, 12));
            data.Add(new DataItem(17, 11));
            data.Add(new DataItem(18, 11));
            data.Add(new DataItem(17, 10));
            data.Add(new DataItem(18, 10));
            data.Add(new DataItem(17, 9));
            data.Add(new DataItem(18, 9));
            data.Add(new DataItem(17, 8));
            data.Add(new DataItem(16, 8));
            data.Add(new DataItem(16, 7));
            data.Add(new DataItem(15, 7));
            data.Add(new DataItem(15, 6));
            data.Add(new DataItem(14, 6));
            data.Add(new DataItem(14, 5));
            data.Add(new DataItem(13, 5));

            data.Add(new DataItem(27, 26));
            data.Add(new DataItem(27, 27));
            data.Add(new DataItem(26, 26));
            data.Add(new DataItem(26, 27));
            data.Add(new DataItem(25, 26));
            data.Add(new DataItem(25, 27));
            data.Add(new DataItem(24, 26));
            data.Add(new DataItem(24, 27));
            data.Add(new DataItem(24, 25));
            data.Add(new DataItem(24, 24));
            data.Add(new DataItem(23, 25));
            data.Add(new DataItem(23, 24));
            data.Add(new DataItem(23, 26));
            data.Add(new DataItem(22, 25));
            data.Add(new DataItem(22, 24));
            data.Add(new DataItem(23, 23));
            data.Add(new DataItem(22, 23));
            data.Add(new DataItem(21, 23));
            data.Add(new DataItem(22, 22));
            data.Add(new DataItem(21, 22));
            data.Add(new DataItem(22, 21));
            data.Add(new DataItem(21, 21));
            data.Add(new DataItem(22, 20));
            data.Add(new DataItem(21, 20));
            data.Add(new DataItem(22, 19));
            data.Add(new DataItem(21, 19));
            data.Add(new DataItem(28, 26));
            data.Add(new DataItem(28, 27));
            data.Add(new DataItem(29, 26));
            data.Add(new DataItem(29, 27));
            data.Add(new DataItem(30, 26));
            data.Add(new DataItem(30, 27));
            data.Add(new DataItem(31, 26));
            data.Add(new DataItem(31, 27));
            data.Add(new DataItem(32, 26));
            data.Add(new DataItem(32, 27));
            data.Add(new DataItem(32, 25));
            data.Add(new DataItem(31, 25));
            data.Add(new DataItem(33, 25));
            data.Add(new DataItem(33, 26));
            data.Add(new DataItem(34, 25));
            data.Add(new DataItem(34, 26));
            data.Add(new DataItem(35, 25));
            data.Add(new DataItem(35, 26));
            data.Add(new DataItem(36, 25));
            data.Add(new DataItem(36, 26));

            for (int i = 30; i < 50; i++)
            {
                for (int j = 15; j < 17; j++)
                {
                    data.Add(new DataItem(i, j));
                }
            }

            for (int i = 25; i < 35; i++)
            {
                for (int j = 6; j < 9; j++)
                {
                    data.Add(new DataItem(i, j));
                }
            }

            data.Add(new DataItem(10, 0));
            data.Add(new DataItem(10, 10));
            data.Add(new DataItem(10, 19));
            data.Add(new DataItem(10, 30));


            data.Add(new DataItem(24, 0));
            data.Add(new DataItem(24, 14));
            data.Add(new DataItem(24, 14));

            data.Add(new DataItem(40, 0));
            data.Add(new DataItem(40, 10));

            data.Add(new DataItem(50, 20));
            data.Add(new DataItem(50, 31));
            data.Add(new DataItem(50, 32));
            data.Add(new DataItem(50, 33));
            return data;
        }

        public static List<DataItem> GenerateData4()
        {
            data.Add(new DataItem(0.8, 2.5));
            data.Add(new DataItem(1, 3));
            data.Add(new DataItem(1.4, 1.3));
            data.Add(new DataItem(1.4, 2));
            data.Add(new DataItem(1.5, 1.7));
            data.Add(new DataItem(2, 2.5));
            data.Add(new DataItem(3.5, 4));
            data.Add(new DataItem(3.5, 4.8));
            data.Add(new DataItem(4, 4.5));
            data.Add(new DataItem(3.7, 3.8));
            data.Add(new DataItem(4.7, 3.5));
            data.Add(new DataItem(4, 1.5));
            data.Add(new DataItem(3.5, 1));
            data.Add(new DataItem(5, 1.5));
            data.Add(new DataItem(5, 1));
            data.Add(new DataItem(4.5, 0.1));
            return data;
        }

		public static bool IsFileExist(string path)
		{
			if (File.Exists(path))
			{
				return true;
			}
			return false;
		}

		public static bool IsFileEmpty(string path)
		{
			if (new FileInfo(path).Length == 0)
			{
				return true;
			}
			return false;
		}

		public static bool IsFileDataValid(string path)
		{
			try
			{
				//logic
				return false;
			}
			catch (Exception e) 
			{
				return false;
			}
			
		}

		public static List<DataItem> GetDataFromFile(string path)
		{
			//logic
			DataItem d = new DataItem(1, 1);
			var list = new List<DataItem>();
			list.Add(d);
			return list;
		}

	}
}
