using Xunit;


namespace LeetCode.Tests
{
	public class ValidNumberTests
	{
		[Theory]
		[InlineData("0", true)]
		[InlineData(" 0.1", true)]
		[InlineData("abc", false)]
		[InlineData("1 a", false)]
		[InlineData("2e10", true)]
		[InlineData(" -90e3 ", true)]
		[InlineData("1e", false)]
		[InlineData("e3", false)]
		[InlineData("6e-1", true)]
		[InlineData("99e2.5", false)]
		[InlineData("53.5e93", true)]
		[InlineData(" --6 ", false)]
		[InlineData("-+3",false)]
		[InlineData("95a54e53", false)]
		[InlineData("99e9999", true)]
		[InlineData(".1", true)]
		[InlineData("3.", true)]
		[InlineData("+.1", true)]
		public void IsNumberTest(string s, bool result)
		{
			//Assert.Equal(result, (new Program()).IsNumberLimitedRange(s));
			Assert.Equal(result, (new ValidNumber()).IsNumber(s));

		}
	}
}