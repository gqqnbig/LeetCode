using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LeetCode.Tests
{
	public class ReverseWordsInAStringTests
	{
		[Theory]
		[InlineData("blue is sky the", "the sky is blue")]
		[InlineData("world! hello", "  hello world!  ")]
		[InlineData("", "    ")]
		//[InlineData("world! hello", "  hello world!  ")]
		public void ReverseWordsTest(string expected, string input)
		{
			Assert.Equal(expected, new ReverseWordsInAString().ReverseWords(input));
		}
	}
}
