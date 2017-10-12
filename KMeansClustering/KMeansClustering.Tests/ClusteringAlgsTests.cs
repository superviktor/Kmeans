using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMeansClustering.Tests
{
	[TestFixture]
	class ClusteringAlgsTests
	{
		ClusteringManager cm;

		[SetUp]
		public void Init()
		{
			 cm= new ClusteringManager();
		}

		[Test]
		public void MinIndex_EmptyDistancesArray_ReturnMinusOne()
		{
			double[] d = new double[] { };
			Assert.AreEqual(cm.MinIndex(d),-1);
		}
		[Test]
		public void MinIndex_NormalDistancesArray_ReturnMin()
		{
			double[] d = new double[] {2,2,3,6,5,7,4,8,9,4,3,7 ,1};
			Assert.Positive(cm.MinIndex(d));
		}


		[TearDown]
		public void Dispose()
		{
			cm = null;
		}
	}
}
