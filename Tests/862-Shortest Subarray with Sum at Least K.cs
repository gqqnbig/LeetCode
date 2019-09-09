using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode.Tests
{
	class ShortestSubarrayWithSumAtLeastK
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="A"></param>
		/// <param name="K">sum</param>
		/// <returns></returns>
		public int ShortestSubarray(int[] A, int K)
		{
			int shortestLength = int.MaxValue;
			for (int i = 0; i < A.Length; i++)
			{
				int sum = 0;
				for (int j = i; j < A.Length; j++)
				{
					sum += A[j];

					if (sum >= K)
					{
						int l = j - i + 1;
						if (l < shortestLength)
							shortestLength = l;
						break;
					}
				}
			}

			if (shortestLength == int.MaxValue)
				return -1;
			else
				return shortestLength;
		}
	}
}
