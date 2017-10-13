using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMeansClustering
{
	public sealed class SquareEuclideanDistance : IDistanceMeasure
	{
		public double GetDistance(DataItem i1, DataItem i2)
		{
			double difference = 0;
			difference = Math.Pow(i1.X - i2.X, 2);
			difference += Math.Pow(i1.Y - i2.Y, 2);
			return difference;
		}
	}
}
