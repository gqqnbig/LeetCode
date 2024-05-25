using Xunit;
using Xunit.Sdk;

namespace LeetCode.Tests
{
	public class SlidingWindowMaximumTests
	{
		[Theory]
		[InlineData(new[] { 6, 5, 4, 3 }, new[] { 6, 5, 4, 3, 2, 1 }, 3)]
		[InlineData(new int[0], new int[0], 0)]
		[InlineData(new[] { 3, 3, 5, 5, 6, 7 }, new[] { 1, 3, -1, -3, 5, 3, 6, 7 }, 3)]
		[InlineData(new[] { 5, 5, 6, 8, 8 }, new[] { 1, 5, -1, 3, 6, 8, 2 }, 3)]
		[InlineData(new[] { 3, 4, 5, 6 }, new[] { 1, 2, 3, 4, 5, 6 }, 3)]

		public void MaxSlidingWindowTest(int[] expected, int[] input, int windowLength)
		{
			CollectionAssert.That.AreEqual(expected, () => new SlidingWindowMaximum().MaxSlidingWindowQueue(input, windowLength));
		}
	}
}
