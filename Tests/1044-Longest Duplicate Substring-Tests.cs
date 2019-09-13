using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LeetCode.Tests
{
	[TestClass]
	public class LongestDuplicateSubstringTests
	{
		[DataTestMethod]
		[DataRow("ana", "banana")]
		[DataRow("", "abcd")]
		public void LongestDupSubstringTest(string expected, string input)
		{
			Assert.AreEqual(expected, new LongestDuplicateSubstring().LongestDupSubstring(input));
		}

		[TestMethod]
		public void LongestDupSubstringSpeedTest()
		{
			var actual = new LongestDuplicateSubstring().LongestDupSubstring(Resource1.P1044LongTestData);
			Assert.AreEqual("ddbcddbabee",actual);
		}
	}
}
