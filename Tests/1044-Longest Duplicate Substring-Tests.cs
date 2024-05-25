using System;
using System.Diagnostics;
using Xunit;

namespace LeetCode.Tests
{
	public class LongestDuplicateSubstringTests
	{
		[Fact]
		[InlineData("ana", "banana")]
		[InlineData("", "abcd")]
		public void LongestDupSubstringTest(string expected, string input)
		{
			Assert.Equal(expected, new LongestDuplicateSubstring().LongestDupSubstring(input));
		}

		[Fact]
		public void LongestDupSubstringSpeedTest()
		{
			var input = Resource1.P1044LongTestData;
			Stopwatch sw = Stopwatch.StartNew();
			var actual = new LongestDuplicateSubstring().LongestDupSubstring(input);
			sw.Stop();
			Console.WriteLine("{0}ms", sw.ElapsedMilliseconds);
			Assert.Equal("ddbcddbabee", actual);
		}

		[Fact]
		public void LongestDupSubstringSpeedTest2()
		{
			var input = Resource1.P1044Input2;
			Stopwatch sw = Stopwatch.StartNew();
			var actual = new LongestDuplicateSubstring().LongestDupSubstring(input);
			sw.Stop();
			Console.WriteLine("{0}ms", sw.ElapsedMilliseconds);
			//Assert.Equal("ddbcddbabee", actual);
		}

		[Fact]
		public void FindDupWithLengthTest()
		{
			Assert.Equal(new string('b', 30), LongestDuplicateSubstring.FindDupWithLength(new string('b', 100), 30));
		}
	}
}
