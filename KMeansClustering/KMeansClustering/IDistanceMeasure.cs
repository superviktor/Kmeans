using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMeansClustering
{
	interface IDistanceMeasure
	{
		double GetDistance(DataItem i1, DataItem d2);	
	}
}
