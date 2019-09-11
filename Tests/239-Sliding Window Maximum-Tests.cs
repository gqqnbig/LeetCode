using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xunit.Sdk;

namespace LeetCode.Tests
{
	[TestClass]
	public class SlidingWindowMaximumTests
	{
		[DataTestMethod]
		[DataRow(new[] { 3, 3, 5, 5, 6, 7 }, new[] { 1, 3, -1, -3, 5, 3, 6, 7 }, 3)]

		public void MaxSlidingWindowTest(int[] expected, int[] input, int windowLength)
		{
			CollectionAssert.That.AreEqual(() => expected, () => new SlidingWindowMaximum().MaxSlidingWindow(input, windowLength));
		}
	}
}
