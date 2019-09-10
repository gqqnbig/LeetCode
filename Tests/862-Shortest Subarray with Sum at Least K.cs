using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Principal;
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
			var r = ShortestSubarray(A, 0, K, new Dictionary<int, int>[A.Length], new Dictionary<int, int>[A.Length],
									 Enumerable.Repeat(int.MaxValue, A.Length).ToArray(), false);
			if (r == int.MaxValue)
				return -1;
			else
				return r;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="A"></param>
		/// <param name="startIndex"></param>
		/// <param name="K"></param>
		/// <param name="stickyMap"></param>
		/// <param name="minimalImpossible">记录最小返回-1的K。如果在相同位置传入了更大的K，则更不可能满足条件了。</param>
		/// <returns></returns>
		int ShortestSubarray(int[] A, int startIndex, int K, Dictionary<int, int>[] stickyMap, Dictionary<int, int>[] freeMap, int[] minimalImpossible, bool stickToStart)
		{
			if (startIndex >= A.Length)
				return int.MaxValue;
			if (K >= minimalImpossible[startIndex])
				return int.MaxValue;
			if (A[startIndex] >= K)
				return 1;

			var map = stickToStart ? stickyMap : freeMap;

			int shortestLength = 0;
			if (map[startIndex]?.TryGetValue(K, out shortestLength) ?? false)
				return shortestLength;

			shortestLength = ShortestSubarray(A, startIndex + 1, K - A[startIndex], stickyMap, freeMap, minimalImpossible, true);
			if (shortestLength < int.MaxValue)
				shortestLength++; //加上A[startIndex]这个元素

			if (stickToStart == false)
			{
				for (int i = startIndex + 1; i < A.Length; i++)
				{
					var l = ShortestSubarray(A, i, K, stickyMap, freeMap, minimalImpossible, false);
					if (l < shortestLength)
					{
						shortestLength = l;
						if (shortestLength == 1)
							break;
					}
				}
			}


			if (shortestLength < int.MaxValue)
			{
				if (map[startIndex] == null)
					map[startIndex] = new Dictionary<int, int>();
				map[startIndex].Add(K, shortestLength);
			}
			else
			{
				Debug.Assert(minimalImpossible[startIndex] > K);
				minimalImpossible[startIndex] = K;
			}
			return shortestLength;
		}


		public int ShortestSubarrayBruteForce(int[] A, int K)
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
