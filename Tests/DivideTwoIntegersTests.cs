using System;
using Xunit;

namespace LeetCode.Tests
{
	public class DivideTwoIntegersTests
	{
		[Fact]
		public void DivideTests()
		{
			for (int i = 0; i < 10000; i++)
			{
				Random random = new Random();
				int dividend = random.Next(int.MinValue, int.MaxValue);
				int divisor = random.Next(int.MinValue, int.MaxValue);
				if (divisor == 0)
					continue;

				int actual = new DivideTwoIntegers().Divide(dividend, divisor);
				int expected = dividend / divisor;
				Assert.True(actual == expected, $"{dividend}/{divisor} should be {expected}, but we get {actual}.");
			}
		}

		[Theory]
		[InlineData(-2147483648, -1, int.MaxValue)]
		[InlineData(4, -1, -4)]
		[InlineData(2147483647, -1, -2147483647)]
		[InlineData(int.MinValue + 1, int.MinValue, 0)]
		[InlineData(1623447558, 1893170224, 0)]
		[InlineData(-485247160 , 398425835,-1)]
		public void DivideLimitTests(int dividend, int divisor, int expected)
		{
			Assert.Equal(expected, new DivideTwoIntegers().Divide(dividend, divisor));

		}
	}
}
