using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32.SafeHandles;
using Newtonsoft.Json;

namespace LeetCode.Tests
{
	class ImportantReversePairs
	{
		public int ReversePairsBruteForce(int[] nums)
		{
			int count = 0;
			for (int i = 0; i < nums.Length; i++)
			{
				var n = nums[i] / 2.0f;
				for (int j = i + 1; j < nums.Length; j++)
				{
					if (n > nums[j])
						count++;
				}
			}

			return count;
		}

		public int ReversePairs(int[] nums)
		{
			Item[] arr = new Item[nums.Length];
			for (int i = 0; i < nums.Length; i++)
				arr[i] = new Item { Index = i, Value = nums[i] };

			Array.Sort(arr, Comparer<Item>.Create((a, b) => b.Value.CompareTo(a.Value)));
			Debug.Assert(arr[0].Value >= arr[arr.Length - 1].Value, "降序排列");

			var subListIndex = FindSubList(arr, arr[0].Value / 2f);


			int count = 0;
			for (int i = 0; i < arr.Length; i++)
			{
				var n = arr[i].Value / 2f;
				var index = arr[i].Index;

				for (; subListIndex < arr.Length && arr[subListIndex].Value >= n; subListIndex++)
				{ }

				//排除索引不合要求的项目
				//count += arr.Skip(subListIndex).Count(a => a.Index >= index);
				for (int j = subListIndex; j < arr.Length; j++)
				{
					if (arr[j].Index >= index)
						count++;
				}
			}

			return count;
		}

		/// <summary>
		/// 给定一个降序数组，查找第一个值小于f的索引
		/// </summary>
		/// <param name="arr"></param>
		/// <param name="f"></param>
		/// <returns></returns>
		private static int FindSubList(Item[] arr, float f)
		{
			int start = 0;
			int end = arr.Length - 1;
			while (start < end - 1)
			{
				int middle = (start + end) / 2;
				if (arr[middle].Value > f)
					start = middle;
				else
					end = middle;
			}

			return end;
		}

		[DebuggerDisplay("[{Index}]={Value}")]
		struct Item
		{
			public int Value;
			public int Index;
		}
	}
}
