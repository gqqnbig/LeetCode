using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LeetCode.Tests
{
	[TestClass]
	public class WildcardMatchingTests
	{
		[DataTestMethod]
		[DataRow(true,"aab", "a?b")]
		[DataRow(true,"aa", "aa")]
		[DataRow(false,"aa", "a")]
		[DataRow(true,"ab", "a*b")]
		[DataRow(false,"acdcb", "a*c?b")]
		[DataRow(true,"adceb", "*a*b")]
		[DataRow(false,"cb", "?a")]
		[DataRow(true,"aa", "*")]
		[DataRow(false,"cab", "a*b")]
		[DataRow(true,"aaab", "a*b")]
		[DataRow(true,"aab", "a*b")]
		[DataRow(false,"aaa", "a?b")]
		[DataRow(false,"", "?")]
		[DataRow(true,"a", "?")]
		public void Test(bool expected, string s, string p)
		{
			Assert.AreEqual(expected, new WildcardMatching().IsMatch(s,p));
		}

		[TestMethod]
		public void SpeedTest()
		{
			//new WildcardMatching().IsMatch("aaaabaaaabbbbaabbbaabbaababbabbaaaababaaabbbbbbaabbbabababbaaabaabaaaaaabbaabbbbaababbababaabbbaababbbba", "*b*aba*babaa*bbaba*a*aaba*b*aa*a*b*ba*a*a*");
			new WildcardMatching().IsMatch("aaaabaaaabbbbaabbbaabbaababbabbaaaababaaabbbbbbaabbbabababbaaabaabaaaaaabbaabbbbaababbababaabbbaababbbba", "*****b*aba***babaa*bbaba***a*aaba*b*aa**a*b**ba***a*a*");
		}

		[TestMethod]
		public void SpeedTest2()
		{
			new WildcardMatching().IsMatch(
			"abbabaaabbabbaababbabbbbbabbbabbbabaaaaababababbbabababaabbababaabbbbbbaaaabababbbaabbbbaabbbbababababbaabbaababaabbbababababbbbaaabbbbbabaaaabbababbbbaababaabbababbbbbababbbabaaaaaaaabbbbbaabaaababaaaabb",
			"**aa*****ba*a*bb**aa*ab****a*aaaaaa***a*aaaa**bbabb*b*b**aaaaaaaaa*a********ba*bbb***a*ba*bb*bb**a*b*bb");
		}
	}
}
