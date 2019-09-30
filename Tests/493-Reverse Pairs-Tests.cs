using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LeetCode.Tests
{
	[TestClass]
	public class ImportantReversePairsTests
	{
		[TestMethod]
		public void IndexTest()
		{

			Console.WriteLine(Array.BinarySearch(new[]{1,1,1,1,1},1));
		}


		[DataTestMethod]
		[DataRow(2, new[] { 1, 3, 2, 3, 1 })]
		[DataRow(19, new[] { 68, 45, 28, 49, 13, 60, 72, 61, 20, 9 })]
		[DataRow(0, new[] { 2147483647, 2147483647, 2147483647, 2147483647, 2147483647, 2147483647 })]
		[DataRow(3, new[] { 2, 4, 3, 5, 1 })]
		public void Test(int expected, int[] nums)
		{
			Assert.AreEqual(expected, new ImportantReversePairs().ReversePairs(nums));
		}

		[TestMethod]
		public void SpeedTest()
		{
			var nums = (from m in Resource1.P493Input1.Split(',')
						select Convert.ToInt32(m)).ToArray();

			var sw = Stopwatch.StartNew();
			new ImportantReversePairs().ReversePairs(nums);
			sw.Stop();
			Console.WriteLine(sw.ElapsedMilliseconds);
		}

		[TestMethod]
		public void GenerationTest()
		{
			Random rand = new Random();
			var solution = new ImportantReversePairs();
			for (int i = 0; i < 100; i++)
			{
				int length = 10;
				int[] arr = new int[length];
				for (int j = 0; j < length; j++)
					arr[j] = rand.Next(100);


				Assert.AreEqual(solution.ReversePairsBruteForce(arr), solution.ReversePairs(arr), "nums=" + ExpressionToCodeLib.ObjectToCode.ComplexObjectToPseudoCode(arr));
			}
		}
	}
}
