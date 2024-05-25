using Xunit;

namespace LeetCode.Tests
{
	
	public class WildcardMatchingTests
	{
		[Theory]
		[InlineData(true, "mississippi", "m*si*")]
		[InlineData(true, "a", "?*")]
		[InlineData(true, "a", "a*")]
		[InlineData(true, "", "*")]
		[InlineData(true, "", "")]
		[InlineData(true, "ab", "*?b")]
		[InlineData(true, "ab", "a*?")]
		[InlineData(true, "ab", "*?")]
		[InlineData(true, "ab", "?*")]
		[InlineData(true, "aab", "a?b")]
		[InlineData(true, "aa", "aa")]
		[InlineData(false, "aa", "a")]
		[InlineData(true, "ab", "a*b")]
		[InlineData(false, "acdcb", "a*c?b")]
		[InlineData(true, "adceb", "*a*b")]
		[InlineData(false, "cb", "?a")]
		[InlineData(true, "aa", "*")]
		[InlineData(false, "cab", "a*b")]
		[InlineData(true, "aaab", "a*b")]
		[InlineData(true, "aab", "a*b")]
		[InlineData(false, "aaa", "a?b")]
		[InlineData(false, "", "?")]
		[InlineData(true, "a", "?")]
		public void Test(bool expected, string s, string p)
		{
			Assert.Equal(expected, new WildcardMatching().IsMatch(s, p));
		}

		[Fact]
		public void SpeedTest()
		{
			//new WildcardMatching().IsMatch("aaaabaaaabbbbaabbbaabbaababbabbaaaababaaabbbbbbaabbbabababbaaabaabaaaaaabbaabbbbaababbababaabbbaababbbba", "*b*aba*babaa*bbaba*a*aaba*b*aa*a*b*ba*a*a*");
			new WildcardMatching().IsMatch("aaaabaaaabbbbaabbbaabbaababbabbaaaababaaabbbbbbaabbbabababbaaabaabaaaaaabbaabbbbaababbababaabbbaababbbba", "*****b*aba***babaa*bbaba***a*aaba*b*aa**a*b**ba***a*a*");
		}

		[Fact]
		public void SpeedTest2()
		{
			new WildcardMatching().IsMatch(
			"abbabaaabbabbaababbabbbbbabbbabbbabaaaaababababbbabababaabbababaabbbbbbaaaabababbbaabbbbaabbbbababababbaabbaababaabbbababababbbbaaabbbbbabaaaabbababbbbaababaabbababbbbbababbbabaaaaaaaabbbbbaabaaababaaaabb",
			"**aa*****ba*a*bb**aa*ab****a*aaaaaa***a*aaaa**bbabb*b*b**aaaaaaaaa*a********ba*bbb***a*ba*bb*bb**a*b*bb");
		}
	}
}
