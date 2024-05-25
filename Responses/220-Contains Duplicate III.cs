using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace LeetCode.Tests
{
	class ContainsDuplicate3
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="nums"></param>
		/// <param name="k">index difference</param>
		/// <param name="t">value difference</param>
		/// <returns></returns>
		public bool ContainsNearbyAlmostDuplicateMe(int[] nums, int k, int t)
		{
			if (k > t)
				return ContainsNearbyAlmostDuplicateSortByValue(nums, k, t);
			else
				return ContainsNearbyAlmostDuplicateSortByIndex(nums, k, t);
		}

		public bool ContainsNearbyAlmostDuplicate(int[] nums, int k, int t)
		{
			if (t < 0 || k < 1) return false;
			Dictionary<long, long> dict = new Dictionary<long, long>();
			//本地函数比lambda表达式更好
			int GetBucket(int val)
			{
				//设桶大小为10
				//数值   -20  -10  0  10  20
				//桶编号    -2   -1  0   1

				int bucket = (int)(val / ((long)t + 1));
				if (val < 0)
					bucket -= 1;
				return bucket;
			}

			for (int i = 0; i < nums.Length; ++i)
			{
				int bucket = GetBucket(nums[i]);
				if (dict.ContainsKey(bucket)
					|| (dict.ContainsKey(bucket - 1) && Math.Abs(dict[bucket - 1] - nums[i]) <= t)
					|| (dict.ContainsKey(bucket + 1) && Math.Abs(dict[bucket + 1] - nums[i]) <= t))
				{
					return true;
				}

				dict[bucket] = nums[i];
				if (dict.Count >= (long)k + 1)
					dict.Remove(GetBucket(nums[i - k]));

			}

			return false;
		}

		public bool ContainsNearbyAlmostDuplicateSortedSet(int[] nums, int k, int t)
		{
			//总体复杂度 O(nlogk)

			if (t < 0 || k < 1)
				return false;
			SortedSet<long> ss = new SortedSet<long>();
			for (int i = 0; i < nums.Length; i++)
			{
				//构建可能的数字范围，看集合里在不在
				if (ss.GetViewBetween((long)nums[i] - t, (long)nums[i] + t).Count > 0)
					return true;
				ss.Add(nums[i]); //复杂度log k

				//删掉索引超出范围的数值。如果不删，该数值会影响上面的GetViewBetween。
				if (i >= k)
					ss.Remove(nums[i - k]);//二分查找删除，复杂度log k。

				Debug.Assert(ss.Count <= k, "SortedSet里最多有k个元素");
			}
			return false;
		}

		bool ContainsNearbyAlmostDuplicateSortByIndex(int[] nums, int k, int t)
		{
			//复杂度O(nk)
			for (int i = 0; i < nums.Length; i++) //循环n次
			{
				var indexLimit = (long)i + k;
				var value = (long)nums[i];
				for (int j = i + 1; j <= indexLimit && j < nums.Length; j++) //循环k次
				{
					if (Math.Abs(value - nums[j]) <= t)
						return true;
				}
			}

			return false;
		}


		bool ContainsNearbyAlmostDuplicateSortByValue(int[] nums, int k, int t)
		{
			//总体复杂度为O(nlogn+nt)

			List<KeyValuePair<int, int>> list = new List<KeyValuePair<int, int>>(nums.Length);
			for (int i = 0; i < nums.Length; i++)
				list.Add(new KeyValuePair<int, int>(i, nums[i]));

			//从小到大排列
			list.Sort((a, b) => a.Value.CompareTo(b.Value)); //复杂度O(nlogn）

			for (int i = 0; i < list.Count; i++) //循环n次
			{
				var value = (long)list[i].Value;
				var index = (long)list[i].Key;
				for (int j = i + 1; j < list.Count && list[j].Value - value <= t; j++) //循环t次
				{
					if (Math.Abs(index - list[j].Key) <= k)
						return true;
				}
			}

			return false;
		}
	}
}
