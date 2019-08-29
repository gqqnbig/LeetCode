using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
//using  Microsoft.VisualStudio.TestTools.UnitTesting

namespace LeetCode.Tests
{
	//[TestClass]
	public class WordLadder2Tests
	{
		//[DataTestMethod]
		[Theory]
		[InlineData("dog", "cog", new[] { "cog" }, new[] {"dog", "cog" })]
		[InlineData("hit", "cog", new[] { "hot", "dot", "dog", "lot", "log", "cog" }, new[] { "hit", "hot", "dot", "dog", "cog" }, new[] { "hit", "hot", "lot", "log", "cog" })]
		public void FindLaddersTest(string beginWord, string endWord, string[] wordList, params string[][] expectedTransforms)
		{
			var actual = new WordLadder2().FindLadders(beginWord, endWord, new List<string>(wordList));
			foreach (var at in actual)
			{
				if (expectedTransforms.Any(et => Enumerable.SequenceEqual(et, at)) == false)
					Assert.True(false, string.Join("-->", at) + " is not in expected list.");
			}
		}

		[Fact]
		public void FindLaddersSpeedTest()
		{
			var actual = new WordLadder2().FindLadders("qa","sq" , new List<string>(new []{ "si", "go", "se", "cm", "so", "ph", "mt", "db", "mb", "sb", "kr", "ln", "tm", "le", "av", "sm", "ar", "ci", "ca", "br", "ti", "ba", "to", "ra", "fa", "yo", "ow", "sn", "ya", "cr", "po", "fe", "ho", "ma", "re", "or", "rn", "au", "ur", "rh", "sr", "tc", "lt", "lo", "as", "fr", "nb", "yb", "if", "pb", "ge", "th", "pm", "rb", "sh", "co", "ga", "li", "ha", "hz", "no", "bi", "di", "hi", "qa", "pi", "os", "uh", "wm", "an", "me", "mo", "na", "la", "st", "er", "sc", "ne", "mn", "mi", "am", "ex", "pt", "io", "be", "fm", "ta", "tb", "ni", "mr", "pa", "he", "lr", "sq", "ye" }));

		}

		//[Theory]
		//[InlineData("aaa", "aaa", new string[0], 0, new string[0])]
		//[InlineData("dog", "cog", new[] { "cog" }, 1, new[] {  "cog" })]
		//[InlineData("dog", "cog", new[] { "cog" }, 0, null)]
		//[InlineData("hit", "cog", new[] { "hot", "dot", "dog", "lot", "log", "cog" }, 6, new[] { "hit", "hot", "dot", "dog", "cog" }, new[] { "hit", "hot", "lot", "log", "cog" })]
		//public void FindLaddersBaseCaseTest(string beginWord, string endWord, string[] wordList, int limit, params string[][] expectedTransforms)
		//{
		//	var actual = new WordLadder2().FindLadders(beginWord, endWord, new LinkedList<string>(wordList), limit);
		//	if (expectedTransforms == null)
		//		Assert.Null(actual);
		//	foreach (var at in actual)
		//	{
		//		if (expectedTransforms.Any(et => Enumerable.SequenceEqual(et, at)) == false)
		//			Assert.True(false, string.Join("-->", at) + " is not in expected list.");
		//	}
		//}
	}
}
