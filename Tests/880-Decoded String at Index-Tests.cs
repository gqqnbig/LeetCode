using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LeetCode.Tests
{
	[TestClass]
	public class DecodedStringAtIndexTests
	{

		[TestMethod]
		public void TotalLengthTest()
		{
			DecodedStringAtIndex.DecodeAtIndex("leet2code3");
		}

		[DataTestMethod]
		[DataRow("o","leet2code3",10)]
		[DataRow("t","leet2code3",20)]
		public void Test(string expected, string S, int K)
		{
			Assert.AreEqual(expected,new DecodedStringAtIndex().DecodeAtIndex(S,K));
		}
	}
}
