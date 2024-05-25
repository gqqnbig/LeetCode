using Xunit;


namespace LeetCode.Tests
{
	public class StringToIntegerTests
	{
		[Theory]
		[InlineData("erulgfi", 0)]
		[InlineData("99999999999999999999999999999999999999999999999999999999999999999 sss", 2147483647)]
		[InlineData("3.14159", 3)]
		[InlineData("-0.1", 0)]
		[InlineData("-.1", 0)]
		[InlineData("+0.1", 0)]
		[InlineData("+.1", 0)]
		public void IsNumberTest(string s, int result)
		{
			//Assert.Equal(result, (new Program()).IsNumberLimitedRange(s));
			Assert.Equal(result, (new StringToInteger()).MyAtoi(s));

		}
	}

}