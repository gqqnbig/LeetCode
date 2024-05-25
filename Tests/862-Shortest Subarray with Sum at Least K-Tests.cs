using System;
using System.Diagnostics;
using System.Linq;
using Xunit;

namespace LeetCode.Tests
{
	
	public class ShortestSubarrayWithSumAtLeastKTests
	{
		[Fact]
		public void CompactArrayTest()
		{
			var arr = new[] { 121, 2460, 2214, -1774, 1974, 2484, -2185, -1246, -204, -1954 };
			ShortestSubarrayWithSumAtLeastK.CompactArray(arr);
			CollectionAssert.Equal(new[] { 121, 2460, 2214, 0, 200, 2484, 0, 0, 0, -5589 }, arr);
		}


		[Theory]
		//[InlineData(2, 4471, new[] { 121, 2460, 2214, 0, 200, 2484, 0, 0, 0, -5589 })]
		[InlineData(2, 2258, new[] { 478, -2274, -1280, 1293, -844, 3, 1840, 2070, -1411, 2156 })]
		[InlineData(2, 4471, new[] { 121, 2460, 2214, -1774, 1974, 2484, -2185, -1246, -204, -1954 })]
		[InlineData(1, 5, new[] { 3, 5, -7, -6 })]
		[InlineData(2, 8, new[] { 1, 2, 3, 5, -7, -6 })]
		[InlineData(1, 1, new[] { 1 })]
		[InlineData(-1, 4, new[] { 1, 2 })]
		[InlineData(3, 3, new[] { 2, -1, 2 })]
		public void ShortestSubarrayTest(int expected, int K, int[] arr)
		{
			Assert.Equal(expected, new ShortestSubarrayWithSumAtLeastK().ShortestSubarray(arr, K));
		}

		[Fact]
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
				Assert.Equal(expected, actual);

			}
		}

		[Fact]
		public void SpeedTest()
		{
			var arr = (from c in Resource1.P862LongTestData.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
					   select Convert.ToInt32(c)).ToArray();

			var sw = Stopwatch.StartNew();
			var s1 = new ShortestSubarrayWithSumAtLeastK();
			var actual = s1.ShortestSubarray(arr, 663610288);
			sw.Stop();
			Console.WriteLine("DP {0}ms, Loop {1}", sw.ElapsedMilliseconds, s1.LoopCount);
			Assert.Equal(25813, actual);

			sw.Restart();
			var s2 = new ShortestSubarrayWithSumAtLeastK();
			s2.ShortestSubarrayBruteForce(arr, 663610288);
			sw.Stop();
			Console.WriteLine("BruteForce {0}ms, Loop {1}", sw.ElapsedMilliseconds, s2.LoopCount);
		}
	}
}
