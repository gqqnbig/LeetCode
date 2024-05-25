using Xunit;

namespace LeetCode.Tests
{
	
	public class DecodedStringAtIndexTests
	{
		[Theory]
		[InlineData('q', "l3mtm5weq7ki78c7hck4", 165511)]
		[InlineData('b', "a2b3c4d5e6f7g8h9", 9)]
		[InlineData('a', "abc", 1)]
		[InlineData('a', "a2345678999999999999999", 5000)]
		[InlineData('h', "ha22", 5)]
		[InlineData('e', "leet2code3", 3)]
		[InlineData('l', "leet2code3", 5)]
		[InlineData('o', "leet2code3", 10)]
		[InlineData('t', "leet2code3", 20)]
		public void Test(char expected, string S, int K)
		{
			Assert.Equal(expected, new DecodedStringAtIndex().DecodeAtIndexCore(S, K));
		}
	}
}
