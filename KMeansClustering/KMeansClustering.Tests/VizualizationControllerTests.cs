using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMeansClustering.Tests
{
	[TestFixture]
	class VizualizationControllerTests
	{
		[Test]
		public void DisplayDefaultData_CanvasIsNull_ThrowException()
		{
			Assert.Throws(typeof(Exception), new TestDelegate(() => VisualizationController.DisplayDefaultData(null, null)));
		}
	}
}
	