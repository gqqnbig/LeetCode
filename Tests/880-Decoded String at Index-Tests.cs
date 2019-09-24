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
		[DataTestMethod]
		[DataRow('q', "l3mtm5weq7ki78c7hck4",165511)]
		[DataRow('b', "a2b3c4d5e6f7g8h9", 9)]
		[DataRow('a', "abc", 1)]
		[DataRow('a', "a2345678999999999999999", 5000)]
		[DataRow('h', "ha22", 5)]
		[DataRow('e', "leet2code3", 3)]
		[DataRow('l', "leet2code3", 5)]
		[DataRow('o', "leet2code3", 10)]
		[DataRow('t', "leet2code3", 20)]
		public void Test(char expected, string S, int K)
		{
			Assert.AreEqual(expected, new DecodedStringAtIndex().DecodeAtIndexCore(S, K));
		}
	}
}
