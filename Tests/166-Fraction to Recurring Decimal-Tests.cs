using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LeetCode.Tests
{
	[TestClass]
	public class FractionToRecurringDecimalTests
	{
		[DataTestMethod]
		[DataRow("-6.25", -50, 8)]
		[DataRow("0", 0, 3)]
		[DataRow("2147483648", -2147483648, -1)]
		[DataRow("0.0000000004656612873077392578125", -1, -2147483648)]
		[DataRow("-0.58(3)", 7, -12)]
		[DataRow("2", 2, 1)]
		[DataRow("0.(9900)", 100, 101)]
		[DataRow("0.(857142)", 6, 7)]
		[DataRow("0.(999725802029064984919111598574170551137921579380312585686865917192212777625445571702769399506443652316972854400877433506992048258842884562654236358650945982)", 3646, 3647)]
		//[DataRow("", int.MaxValue - 1, int.MaxValue)]//Mathematica都算不出
		[DataRow("1", int.MaxValue, int.MaxValue)]
		[DataRow("2.(3)", 7, 3)]
		public void FractionToDecimalTest(string expected, int numerator, int denominator)
		{
			Assert.AreEqual(expected, new FractionToRecurringDecimal().FractionToDecimal(numerator, denominator));
		}


		[DataTestMethod]
		[DataRow(21, 1071, 462)]
		[DataRow(21, 1071, -462)]
		[DataRow(21, -1071, -462)]
		[DataRow(21, -1071, 462)]
		public void GetCommonDivisorTest(int expected, int a, int b)
		{
			Assert.AreEqual(expected, FractionToRecurringDecimal.GetCommonDivisor(a, b));
		}
	}
}
