using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpressionToCodeLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LeetCode.Tests
{
	[TestClass]
	public class KConcatenationMaximumSumTests
	{
		[DataTestMethod]
		[DataRow(29, new[] { 12, -1, -3, -45, 17 }, 4)]
		[DataRow(20, new[] { -5, -2, 0, 0, 3, 9, -2, -5, 4 }, 5)]
		[DataRow(40, new[] { 1, 0, 4, 1, 4 }, 4)]
		[DataRow(38, new[] { 20, -49, -43, -14, -32, -46, -21, 38, -9, -25 }, 1)]
		public void KConcatenationMaxSumTest(int expected, int[] arr, int k)
		{
			var actual = new KConcatenationMaximumSum().KConcatenationMaxSum(arr, k);
			if (expected != actual)
			{
				var newArray = Enumerable.Repeat(arr, k).SelectMany(n => n);
				Console.WriteLine(KConcatenationMaximumSum.MaxSubArray(newArray));
			}

			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void KConcatenationMaxSumGenerationTest()
		{
			Random rand = new Random();

			for (int c = 0; c < 50; c++)
			{
				int length = rand.Next(10);
				int[] arr = new int[length];
				for (int i = 0; i < arr.Length; i++)
				{
					arr[i] = rand.Next(100) - 50;
				}

				int k = rand.Next(10);
				int expected = KConcatenationMaximumSum.MaxSubArray(Enumerable.Repeat(arr, k).SelectMany(n => n));
				var solution = new KConcatenationMaximumSum();


				Assert.AreEqual(expected, solution.KConcatenationMaxSum(arr, k), "arr=" + ObjectToCode.ComplexObjectToPseudoCode(arr) + "; k=" + k);
			}
		}

		[TestMethod]
		public void LargeInputTest()
		{
			var arr = Resource1.P1191Input1.Split(',').Select(s=>Convert.ToInt32(s)).ToArray();
			Assert.AreEqual(664859242, new KConcatenationMaximumSum().KConcatenationMaxSum(arr, 36263));

		}
	}
}
