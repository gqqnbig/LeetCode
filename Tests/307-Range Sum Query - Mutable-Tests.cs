using Xunit;

namespace LeetCode.Tests
{
	
	public class RangeSumQueryMutableTests
	{
		[Fact]
		public void Test1()
		{
			var arr = new NumArray(new[] { 1, 3, 5 });
			Assert.Equal(9, arr.SumRange(0, 2));

			arr.Update(1, 2);

			Assert.Equal(8, arr.SumRange(0, 2));
		}

		[Fact]
		public void Test2()
		{
			var arr = new NumArray(new[] { 9, -8 });
			arr.Update(0, 3);

			Assert.Equal(-8, arr.SumRange(1, 1));

			Assert.Equal(-5, arr.SumRange(0, 1));

			arr.Update(1, -3);
			Assert.Equal(0, arr.SumRange(0, 1));
		}

	}
}
