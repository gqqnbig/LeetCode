using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LeetCode.Tests
{
	[TestClass]
	public class RangeSumQueryMutableTests
	{
		[TestMethod]
		public void Test1()
		{
			var arr = new NumArray(new[] { 1, 3, 5 });
			Assert.AreEqual(9, arr.SumRange(0, 2));

			arr.Update(1, 2);

			Assert.AreEqual(8, arr.SumRange(0, 2));
		}

		[TestMethod]
		public void Test2()
		{
			var arr = new NumArray(new[] { 9, -8 });
			arr.Update(0, 3);

			Assert.AreEqual(-8, arr.SumRange(1, 1));

			Assert.AreEqual(-5, arr.SumRange(0, 1));

			arr.Update(1, -3);
			Assert.AreEqual(0, arr.SumRange(0, 1));
		}

	}
}
