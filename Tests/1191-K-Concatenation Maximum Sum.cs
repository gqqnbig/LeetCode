using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode.Tests
{
	class KConcatenationMaximumSum
	{
		private static int modulo = (int) (Math.Pow(10, 9) + 7);


		public int KConcatenationMaxSum(int[] arr, int k)
		{
			if (k == 0)
				return 0;
			else if (k == 1)
			{
				//退化成最大子序列问题
				return MaxSubArray(arr) % modulo;
			}
			else
			{
				int sum =Math.Max(0, arr.Sum());
				int compact = (int) (sum * (k - 2L) % modulo);
				int intraArraySum = MaxSubArray(arr,compact);
				return intraArraySum;
			}
		}

		/// <summary>
		/// 允许长度为0。
		/// </summary>
		/// <param name="arr"></param>
		/// <returns></returns>
		public static int MaxSubArray(IEnumerable<int> arr)
		{
			long maxEndingThis = 0;
			long max = 0;
			int i = 0;
			foreach (var n in arr)
			{
				maxEndingThis = Math.Max(0, maxEndingThis + n) % modulo;
				max = Math.Max(max, maxEndingThis);
				i++;
			}

			return (int) max;
		}

		/// <summary>
		/// 允许长度为0。返回值的最大值为modulo-1
		/// </summary>
		/// <param name="arr"></param>
		/// <returns></returns>
		static int MaxSubArray(int[] arr, int next)
		{
			long maxEndingThis = 0;
			long max = 0;
			foreach (var n in arr)
			{
				maxEndingThis = Math.Max(0, maxEndingThis + n) % modulo;
				max = Math.Max(max, maxEndingThis);
			}

			maxEndingThis = Math.Max(0, maxEndingThis + next) % modulo;
			max = Math.Max(max, maxEndingThis);

			foreach (var n in arr)
			{
				maxEndingThis = Math.Max(0, maxEndingThis + n) % modulo;
				max = Math.Max(max, maxEndingThis);
			}
			return (int)max;
		}
	}
}
