using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace LeetCode.Tests
{
	class ShortestSubarrayWithSumAtLeastK
	{
		public int LoopCount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="A"></param>
		/// <param name="K">sum</param>
		/// <returns></returns>
		public int ShortestSubarray(int[] A, int K)
		{
			//stickyData[i]表示从i开始的子数组，最大的和为stickyData[i].Sum，由stickyData[i].Length个元素构成。
			SumLength[] stickyData = new SumLength[A.Length];
			stickyData[A.Length - 1] = new SumLength { Sum = A[A.Length - 1], Length = 1 };

			for (int i = A.Length - 2; i >= 0; i--)
			{
				if (stickyData[i + 1].Sum < 0)
					stickyData[i] = new SumLength { Sum = A[i], Length = 1 };
				else
				{
					var data = new SumLength { Sum = A[i] + stickyData[i + 1].Sum, Length = stickyData[i + 1].Length + 1 };
					int t = i + data.Length - 1;
					var s = data.Sum;
					while (t > i) //当compactA[t]==0时，t可能减到小于i。
					{
						s -= A[t];
						t--;
						if (s >= K)
						{
							data.Sum = s;
							data.Length = t - i + 1;
						}

						LoopCount++;
					}
					stickyData[i] = data;
				}
			}

			int length = stickyData.Where(d => d.Sum >= K).Select(d => d.Length).DefaultIfEmpty(-1).Min();
			return length;
		}

		/// <summary>
		/// 除了最后一个元素可能是负数外，其他元素都非负。
		/// </summary>
		/// <param name="A"></param>
		public static void CompactArray(int[] A)
		{
			for (int i = 0; i < A.Length - 1; i++)
			{
				if (A[i] < 0)
				{
					A[i + 1] += A[i];
					A[i] = 0;

					//for (int j = i + 1; j >= 0; j--)
					//{
					//	if (A[j] < 0)
					//	{
					//		A[i] += A[j];
					//		A[j] = 0;
					//	}
					//	else
					//	{
					//		i = j + 1;
					//		break;
					//	}
					//}
				}
			}


		}


		[DebuggerDisplay("Sum={Sum}, Length={Length}")]
		struct SumLength
		{
			public int Sum { get; set; }
			public int Length { get; set; }
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="A"></param>
		/// <param name="K">sum</param>
		/// <returns></returns>
		public int ShortestSubarray2(int[] A, int K)
		{
			int[] maximumPossible = new int[A.Length];
			maximumPossible[A.Length - 1] = A[A.Length - 1];
			for (int i = A.Length - 2; i >= 0; i--)
				maximumPossible[i] = Math.Max(A[i], A[i] + maximumPossible[i + 1]);


			var r = ShortestSubarray(A, 0, K, new Dictionary<int, int>[A.Length], new Dictionary<int, int>[A.Length],
									 maximumPossible, false);
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
		/// <param name="maximumPossible">在子数组包含index的情况下，记录最小返回-1的K。如果在相同位置传入了更大的K，则更不可能满足条件了。</param>
		/// <returns></returns>
		int ShortestSubarray(int[] A, int startIndex, int K, Dictionary<int, int>[] stickyMap, Dictionary<int, int>[] freeMap, int[] maximumPossible, bool stickToStart)
		{
			if (startIndex >= A.Length)
				return int.MaxValue;
			if (stickToStart && K > maximumPossible[startIndex])
				return int.MaxValue;
			if (A[startIndex] >= K)
				return 1;

			var map = stickToStart ? stickyMap : freeMap;

			int shortestLength = 0;
			if (map[startIndex]?.TryGetValue(K, out shortestLength) ?? false)
				return shortestLength;

			shortestLength = ShortestSubarray(A, startIndex + 1, K - A[startIndex], stickyMap, freeMap, maximumPossible, true);
			if (shortestLength < int.MaxValue)
				shortestLength++; //加上A[startIndex]这个元素

			if (stickToStart == false)
			{
				var l = ShortestSubarray(A, startIndex + 1, K, stickyMap, freeMap, maximumPossible, false);
				if (l < shortestLength)
					shortestLength = l;
			}


			//if (shortestLength < int.MaxValue)
			//{

			//}
			//else
			//{
			//	Debug.Assert(maximumPossible[startIndex] > K);
			//	maximumPossible[startIndex] = K;
			//}
			if (map[startIndex] == null)
				map[startIndex] = new Dictionary<int, int>();
			map[startIndex].Add(K, shortestLength);
			return shortestLength;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="A"></param>
		/// <param name="index"></param>
		/// <param name="K"></param>
		/// <param name="stickyMap"></param>
		/// <param name="minimalImpossible">记录最小返回-1的K。如果在相同位置传入了更大的K，则更不可能满足条件了。</param>
		/// <returns></returns>
		int ShortestSubarraySticky(int[] A, int startIndex, int K, Dictionary<int, int>[] stickyMap, int[] minimalImpossible)
		{
			int index = startIndex;
			int l;
			while (true)
			{
				if (index >= A.Length)
					return int.MaxValue;
				if (K >= minimalImpossible[index])
					return int.MaxValue;
				if (A[index] >= K)
				{
					l = index - startIndex;
					break;
				}


				int shortestLength = 0;
				if (stickyMap[index]?.TryGetValue(K, out shortestLength) ?? false)
				{
					l = index - startIndex + shortestLength;
					break;
				}

				shortestLength = ShortestSubarraySticky(A, index + 1, K - A[index], stickyMap, minimalImpossible);
				if (shortestLength < int.MaxValue)
					shortestLength++; //加上A[startIndex]这个元素


				if (shortestLength < int.MaxValue)
				{
					if (stickyMap[index] == null)
						stickyMap[index] = new Dictionary<int, int>();
					stickyMap[index].Add(K, shortestLength);
				}
				else
				{
					Debug.Assert(minimalImpossible[index] > K);
					minimalImpossible[index] = K;
				}
			}
			return l;
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

					LoopCount++;
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
