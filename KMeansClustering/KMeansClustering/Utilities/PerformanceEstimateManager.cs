using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace KMeansClustering.Utilities
{
	public delegate void MethodWrapper();
	public static class PerformanceEstimateManager
	{
		public static void GetTimeSpan(MethodWrapper method)
		{
			DateTime dateTimeStart, dateTimeFinish;
			dateTimeStart = DateTime.Now;
			method.Invoke();
			dateTimeFinish = DateTime.Now;
			Thread.Sleep(1000);//not to write until finish method
			//Console.WriteLine(String.Format("{0} - {1}", dateTimeStart.ToString("HH:mm:ss.ffffff"), dateTimeFinish.ToString("HH:mm:ss.ffffff")));
			TimeSpan timeSpan = dateTimeFinish - dateTimeStart;
			MessageBox.Show(timeSpan.TotalMilliseconds.ToString());
		}
	}
}
