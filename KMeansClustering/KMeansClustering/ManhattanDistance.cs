using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMeansClustering
{
	public sealed class ManhattanDistance : IDistanceMeasure
	{
		public double GetDistance(DataItem i1, DataItem i2)
		{
			sbyte difference = 0;
			sbyte n0, n1 = 0;
			n0 = (sbyte)i1.X;
			n1 = (sbyte)i2.X;
			difference = Math.Abs((sbyte)(n0 - n1));
			n0 = (sbyte)i1.Y;
			n1 = (sbyte)i2.Y;
			difference += Math.Abs((sbyte)(n0 - n1));
			return difference;
		}
	}
}
