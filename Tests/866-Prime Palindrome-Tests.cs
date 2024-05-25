using System;
using System.Diagnostics;
using Xunit;

namespace LeetCode.Tests
{
	
	public class PrimePalindromeTests
	{
		[Theory]
		[InlineData(6, 7)]
		[InlineData(8, 11)]
		[InlineData(13, 101)]
		[InlineData(1, 2)]
		public void PrimePalindromeTest(int input, int expected)
		{
			Assert.Equal(expected, new CPrimePalindrome().PrimePalindrome(input));
		}

		[Fact]
		public void PrimePalindromeSpeedTest()
		{
			Stopwatch sw = Stopwatch.StartNew();
			var solution = new CPrimePalindrome();
			var result = solution.PrimePalindrome(31880255);
			sw.Stop();
			Console.WriteLine("运行时间{0}ms, NearestPalindromicUpEntry={1}, IsPrimeEntry={2}", sw.ElapsedMilliseconds, solution.NearestPalindromicUpEntry, solution.IsPrimeEntry);
			Assert.Equal(100030001, result);
		}
	}
}
