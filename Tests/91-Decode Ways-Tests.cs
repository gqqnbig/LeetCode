using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LeetCode.Tests
{
	[TestClass]
	public class DecodeWaysTests
	{
		[DataTestMethod]
		[DataRow(1, "")]
		[DataRow(1, "110")]
		[DataRow(1, "10")]
		[DataRow(3, "111")]
		public void NumDecodingsTest(int expected, string input)
		{
			Assert.AreEqual(expected, new DecodeWays().NumDecodings(input));
		}
	}
}
