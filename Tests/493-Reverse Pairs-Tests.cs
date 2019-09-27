using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LeetCode.Tests
{
	[TestClass]
	public class ImportantReversePairsTests
	{
		[DataTestMethod]
		[DataRow(0,new[]{2147483647, 2147483647, 2147483647, 2147483647, 2147483647, 2147483647})]
		[DataRow(2,new[]{1,3,2,3,1})]
		[DataRow(3,new[]{2,4,3,5,1})]
		public void Test(int expected, int[] nums)
		{
			Assert.AreEqual(expected,new ImportantReversePairs().ReversePairs(nums));
		}
	}
}
