using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KMeansClustering;

namespace KMeansClustering.Tests
{
	[TestFixture]
	class DataControllerTests
	{
		[Test]
		public void GenerateData1_PassNothing_ReturnNotEmptyList()
		{
			Assert.Greater(DataController.GenerateData1().Count(), 0, "Return empty collection");
		}
		
		[Test]
		public void	IsFileExist_CorrectPath_ReturnTrue()
		{
			Assert.IsTrue(DataController.IsFileExist(@"D:\Clustering\KMeansClustering\KMeansClustering.sln"));
		}

		[Test]
		public void IsFileEmpty_NotEmptyFile_ReturnFalse()
		{
			Assert.IsFalse(DataController.IsFileEmpty(@"C:\Users\Viktor\tmp\file1.txt"));
		}

		[Test]
		public void IsFileDataValid_InvalidDataInFile_ReturnFalse()
		{
			Assert.IsFalse(DataController.IsFileDataValid(@"C:\Users\Viktor\tmp\file1.txt"));
		}

		public void GetDataFromFile_CorrectDataInFilePasses_ReturnNotEmptyList()
		{
			Assert.Greater(DataController.GetDataFromFile(@"C:\Users\Viktor\tmp\file1.txt").Count(),0,"Read collection is empty");
		}
	}
}
