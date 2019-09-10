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
	public class ShortestSubarrayWithSumAtLeastKTests
	{
		[TestMethod]
		public void CompactArrayTest()
		{
			var arr = new[] { 121, 2460, 2214, -1774, 1974, 2484, -2185, -1246, -204, -1954 };
			ShortestSubarrayWithSumAtLeastK.CompactArray(arr);
			CollectionAssert.AreEqual(new[] { 121, 2460, 2214, 0, 200, 2484, 0, 0, 0, -5589 }, arr);
		}


		[DataTestMethod]
		//[DataRow(2, 4471, new[] { 121, 2460, 2214, 0, 200, 2484, 0, 0, 0, -5589 })]
		[DataRow(2, 2258, new[] { 478, -2274, -1280, 1293, -844, 3, 1840, 2070, -1411, 2156 })]
		[DataRow(2, 4471, new[] { 121, 2460, 2214, -1774, 1974, 2484, -2185, -1246, -204, -1954 })]
		[DataRow(1, 5, new[] { 3, 5, -7, -6 })]
		[DataRow(2, 8, new[] { 1, 2, 3, 5, -7, -6 })]
		[DataRow(1, 1, new[] { 1 })]
		[DataRow(-1, 4, new[] { 1, 2 })]
		[DataRow(3, 3, new[] { 2, -1, 2 })]
		public void ShortestSubarrayTest(int expected, int K, int[] arr)
		{
			Assert.AreEqual(expected, new ShortestSubarrayWithSumAtLeastK().ShortestSubarray(arr, K));
		}

		[TestMethod]
		public void ShortestSubarrayGenerationTest()
		{
			var sw = new Stopwatch();
			Random rand = new Random();
			for (int c = 0; c < 50; c++)
			{

				int length = 20;
				int[] arr = new int[length];
				for (int i = 0; i < arr.Length; i++)
				{
					arr[i] = rand.Next(5000) - 2500;
				}

				int k = rand.Next(10000);

				Console.WriteLine("{{ {0} }}, K={1}", string.Join(", ", arr), k);
				var solution = new ShortestSubarrayWithSumAtLeastK();
				sw.Restart();
				var expected = solution.ShortestSubarrayBruteForce(arr, k);
				sw.Stop();
				var t1 = sw.ElapsedMilliseconds;
				sw.Restart();
				var actual = solution.ShortestSubarray(arr, k);
				sw.Stop();
				var t2 = sw.ElapsedMilliseconds;
				Console.WriteLine("BruteForce: {0}ms; mine: {1}ms", t1, t2);
				Assert.AreEqual(expected, actual);

			}
		}

		[TestMethod]
		public void SpeedTest()
		{
			var arr = (from c in Resource1.P862LongTestData.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
					   select Convert.ToInt32(c)).ToArray();
			Console.WriteLine(new ShortestSubarrayWithSumAtLeastK().ShortestSubarray(arr, 663610288));
		}
	}
}
