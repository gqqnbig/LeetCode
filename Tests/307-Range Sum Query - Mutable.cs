using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode.Tests
{
	public class NumArray
	{
		private BinaryIndexedArray arr;


		public NumArray(int[] nums)
		{
			arr = new BinaryIndexedArray(nums);
		}

		public void Update(int i, int val)
		{
			arr[i] = val;
		}

		/// <summary>
		/// 获取[i,j]区间的和
		/// </summary>
		/// <param name="i"></param>
		/// <param name="j"></param>
		/// <returns></returns>
		public int SumRange(int i, int j)
		{
			return arr.GetSum(i, j - i + 1);
		}
	}

	/**
	 * Your NumArray object will be instantiated and called as such:
	 * NumArray obj = new NumArray(nums);
	 * obj.Update(i,val);
	 * int param_2 = obj.SumRange(i,j);
	 */
}
