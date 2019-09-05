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
	public class PrimePalindromeTests
	{
		[DataTestMethod]
		[DataRow(6, 7)]
		[DataRow(8, 11)]
		[DataRow(13, 101)]
		[DataRow(1, 2)]
		public void PrimePalindromeTest(int input, int expected)
		{
			Assert.AreEqual(expected, new CPrimePalindrome().PrimePalindrome(input));
		}

		[TestMethod]
		public void PrimePalindromeSpeedTest()
		{
			Stopwatch sw = Stopwatch.StartNew();
			var solution = new CPrimePalindrome();
			var result = solution.PrimePalindrome(31880255);
			sw.Stop();
			Console.WriteLine("运行时间{0}ms, NearestPalindromicUpEntry={1}, IsPrimeEntry={2}", sw.ElapsedMilliseconds, solution.NearestPalindromicUpEntry, solution.IsPrimeEntry);
			Assert.AreEqual(100030001, result);
		}
	}
}
