using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Win32.SafeHandles;
using Newtonsoft.Json;

namespace LeetCode.Tests
{
	class ImportantReversePairs
	{
		public int ReversePairs2(int[] nums)
		{
			int res = 0;
			int[] copy = (int[])nums.Clone();
			Array.Sort(copy);
			BinaryIndexedArray bit = new BinaryIndexedArray(copy);


			foreach (int ele in nums)
			{
				res += bit.GetSum(BinarySearchFirstIndex(copy, 2L * ele + 1));
				bit.Update(BinarySearchFirstIndex(copy, ele), 1);
			}

			return res;
		}

		public int ReversePairs(int[] nums)
		{
			int res = 0;
			int[] sorted = (int[])nums.Clone();
			Array.Sort(sorted);
			int[] bit = new int[sorted.Length + 1];


			foreach (int ele in nums)
			{
				res += GetSum(bit, BinarySearchFirstIndex(sorted, 2L * ele + 1));
				Update(bit, BinarySearchFirstIndex(sorted, ele), 1);
			}

			return res;
		}

		/// <summary>
		/// 在bit蕴含的数组中，求索引为i的前缀和。
		/// </summary>
		/// <param name="bit"></param>
		/// <param name="i">0基</param>
		/// <returns></returns>
		private int GetSum(int[] bit, int i)
		{
			i++;
			int sum = 0;

			while (i < bit.Length)
			{
				sum += bit[i];
				i += i & -i;
			}

			return sum;
		}

		/// <summary>
		/// 在bit蕴含的数组中，更新指定位置的值。
		/// </summary>
		/// <param name="bit"></param>
		/// <param name="index">0基</param>
		private void Update(int[] bit, int index, int delta)
		{
			index++;
			while (index > 0)
			{
				bit[index] += delta;
				index -= index & -index;
			}
		}

		/// <summary>
		/// 给定一个递增数组，返回第一个大于等于val的元素的索引。如果所有元素都小于val，则返回arr.Length。
		/// </summary>
		static int BinarySearchFirstIndex(int[] arr, long val)
		{
			Debug.Assert(arr[0] <= arr[arr.Length - 1], "数组必须是递增的。");

			int i;
			if (val > int.MaxValue)
			{
				if (arr[arr.Length - 1] == int.MaxValue)
					i = arr.Length - 1;
				else
					i = Array.BinarySearch(arr, int.MaxValue);
			}
			else
				i = Array.BinarySearch(arr, (int)val);

			if (i < 0)
				return ~i;

			while (i >= 0 && arr[i] == val)
			{
				i--;
			}

			return i + 1;
		}


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

		public int ReversePairsFilterByValue(int[] nums)
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

	/// <summary>
	/// 可以快速计算前缀和的数组，以Binary Indexed Tree实现，
	/// 单次更新的时间复杂度为O(log n)，
	/// 单次和查询的时间复杂度为O(log n)。
	/// </summary>
	public class BinaryIndexedArray
	{
		private readonly int[] bitArr;
		private readonly int[] originalArr;

		/// <summary>
		/// 初始化的时间复杂度为O(n)，n为数组大小。
		/// </summary>
		/// <param name="list"></param>
		public BinaryIndexedArray(int[] list)
		{
			originalArr = list;

			//不使用bitArr[0]。
			this.bitArr = new int[list.Length + 1];
			Array.Copy(list, 0, bitArr, 1, list.Length);

			for (int i = 1; i < this.bitArr.Length; i++)
			{
				int j = i + (i & -i);
				if (j < this.bitArr.Length)
					this.bitArr[j] += this.bitArr[i];
			}
		}

		/// <summary>
		/// 在指定索引处获取或设置数组元素的值。设置值时，时间复杂度为O(log n)，n为数组大小。
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		public int this[int index]
		{
			get => originalArr[index];
			set
			{
				var old = originalArr[index];
				originalArr[index] = value;
				Update(index, value - old);
			}
		}

		///<summary>Add `delta` to elements in `idx` of original array。时间复杂度是O(log n)，n为数组大小。</summary>
		public void Update(int index, int delta)
		{
			index += 1;
			while (index < this.bitArr.Length)
			{
				this.bitArr[index] += delta;
				index = index + (index & -index);
			}
		}

		///<summary>获取数组从0开始到索引(length-1)的和。时间复杂度是O(log n)，n为数组大小。
		///如果length小于等于0，则返回0。
		/// </summary>
		public int GetSum(int length)
		{
			int result = 0;
			while (length > 0)
			{
				result += this.bitArr[length];
				length = length - (length & -length);
			}

			return result;
		}

		///<summary>Get the range sum of elements from original array from index `from_idx` to `to_idx`。
		/// 时间复杂度是O(log n)，n为数组大小。</summary>
		public int GetSum(int startIndex, int length)
		{
			return GetSum(startIndex + length) - GetSum(startIndex);
		}

	}
}
