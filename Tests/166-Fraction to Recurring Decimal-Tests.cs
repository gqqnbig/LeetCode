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
		[DataRow("", int.MaxValue - 1, int.MaxValue)]
		[DataRow("1", int.MaxValue, int.MaxValue)]
		[DataRow("2.(3)", 7, 3)]
		public void FractionToDecimalTest(string expected, int numerator, int denominator)
		{
			Assert.AreEqual(expected, new FractionToRecurringDecimal().FractionToDecimal(numerator, denominator));
		}
	}
}
