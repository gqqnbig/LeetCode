using Xunit;

namespace LeetCode.Tests
{
	public class MaxPointsOnALineTests
	{
		[Theory]
		[InlineData(1, new[] { 1, 1 })]
		[InlineData(2, new[] { 0, 0 }, new[] { 0, 0 })]
		[InlineData(2, new[] { 1, 1 }, new[] { 2, 2 })]
		[InlineData(3, new[] { 1, 1 }, new[] { 2, 2 }, new[] { 3, 3 })]
		[InlineData(4, new[] { 1, 1 }, new[] { 3, 2 }, new[] { 5, 3 }, new[] { 4, 1 }, new[] { 2, 3 }, new[] { 1, 4 })]
		[InlineData(3, new[] { 1, 1 }, new[] { 1, 1 }, new[] { 1, 1 })]
		[InlineData(3, new[] { 1, 1 }, new[] { 2, 1 }, new[] { 3, 1 }, new[] { 998, 889 })]
		[InlineData(3, new[] { 1, 1 }, new[] { 1, 2 }, new[] { 1, 3 }, new[] { 998, 889 })]
		[InlineData(3, new[] { 0, 0 }, new[] { 1, 1 }, new[] { 0, 0 })]
		[InlineData(3, new[] { 2, 3 }, new[] { 3, 3 }, new[] { -5, 3 })]

		public void MaxPointsTest(int max, params int[][] points)
		{
			Assert.Equal(max, new MaxPointsOnALine().MaxPoints(points));
		}


		[Fact]
		public void MaxPoints0LengthTest()
		{
			Assert.Equal(0, new MaxPointsOnALine().MaxPoints(new int[0][]));
		}

		[Fact]
		public void MaxPointsOverflowTest()
		{
			int[] p1 = { int.MinValue, 0 };
			int[] p2 = { int.MaxValue, 999999 };
			//int[] p3 = { int.MaxValue - 3, int.MaxValue - 2 };
			int[] p4 = { int.MinValue + 1, -999999 };
			int[][] points = new int[][] { p1, p2, p4 };

			Assert.Equal(2, new MaxPointsOnALine().MaxPoints(points));
		}


		[Fact]
		public void MaxPointsPrecisionTest()
		{
			int[] p1 = { int.MinValue, int.MinValue };
			int[] p2 = { int.MaxValue, int.MaxValue - 1 };
			int[] p3 = { int.MaxValue - 3, int.MaxValue - 2 };
			int[][] points = new int[][] { p1, p2, p3 };

			Assert.Equal(2, new MaxPointsOnALine().MaxPoints(points));
		}
	}
}
