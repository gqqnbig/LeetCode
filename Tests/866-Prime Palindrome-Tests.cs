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
			Stopwatch sw=Stopwatch.StartNew();
			var result = new CPrimePalindrome().PrimePalindrome(31880255);
			sw.Stop();
			Console.WriteLine(sw.ElapsedMilliseconds);
			Assert.AreEqual(100030001, result);
		}
	}
}
