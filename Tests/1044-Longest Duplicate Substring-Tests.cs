﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
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
			var input = Resource1.P1044LongTestData;
			Stopwatch sw = Stopwatch.StartNew();
			var actual = new LongestDuplicateSubstring().LongestDupSubstring(input);
			sw.Stop();
			Console.WriteLine("{0}ms", sw.ElapsedMilliseconds);
			Assert.AreEqual("ddbcddbabee", actual);
		}
	}
}
