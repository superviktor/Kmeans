using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMeansClustering.Tests
{
	[TestFixture]
	class DistanceMeasureTests
	{
		DataItem di1 = new DataItem(5, 4);
		DataItem di2 = new DataItem(3, 2);
		ManhattanDistance md;

		private SquareEuclideanDistance CreateSquareEuclideanDistanceInstance()
		{
			return new SquareEuclideanDistance();
		}

		[SetUp]
		public void CreateManhattanDistanceInstance()
		{
			md = new ManhattanDistance();
		}

		[Test]
		public void EuclideanDistanceGetDistance_CorrectData_CorrectResult()
		{
			EuclideanDistance ed = new EuclideanDistance();
			Assert.AreEqual(Math.Sqrt(8), ed.GetDistance(di1, di2), "not correct result");
		}

		[Test]
		public void SquareEuclideanDistanceGetDistance_CorrectData_CorrectResult()
		{
			var sed = CreateSquareEuclideanDistanceInstance();
			Assert.AreEqual(8, sed.GetDistance(di1, di2), "not correct result");
		}

		[Test]
		public void ManhattanDistanceGetDistance_CorrectData_CorrectResult()
		{
			Assert.AreEqual(4, md.GetDistance(di1, di2), "not correct result");
		}

		[TearDown]
		public void Dispose()
		{
			md = null;
		}

	}
}