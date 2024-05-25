using Xunit;

namespace LeetCode.Tests
{
	
	public class DecodeWaysTests
	{
		[Theory]
		[InlineData(1, "")]
		[InlineData(1, "110")]
		[InlineData(1, "10")]
		[InlineData(3, "111")]
		public void NumDecodingsTest(int expected, string input)
		{
			Assert.Equal(expected, new DecodeWays().NumDecodings(input));
		}
	}
}
