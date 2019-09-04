using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
		public bool ContainsNearbyAlmostDuplicate(int[] nums, int k, int t)
		{
			if (k > t)
				return ContainsNearbyAlmostDuplicateSortByValue(nums, k, t);
			else
				return ContainsNearbyAlmostDuplicateSortByIndex(nums, k, t);
		}

		bool ContainsNearbyAlmostDuplicateSortByIndex(int[] nums, int k, int t)
		{
			for (int i = 0; i < nums.Length; i++)
			{
				var indexLimit = (long)i + k;
				for (int j = i + 1; j <= indexLimit && j < nums.Length; j++)
				{
					if (Math.Abs((long)nums[i] - nums[j]) <= t)
						return true;
				}
			}

			return false;
		}


		bool ContainsNearbyAlmostDuplicateSortByValue(int[] nums, int k, int t)
		{
			List<KeyValuePair<int, int>> list = new List<KeyValuePair<int, int>>(nums.Length);
			for (int i = 0; i < nums.Length; i++)
				list.Add(new KeyValuePair<int, int>(i, nums[i]));

			list.Sort((a, b) => a.Value.CompareTo(b.Value));

			for (int i = 0; i < list.Count; i++)
			{
				var value = (long)list[i].Value;
				for (int j = i + 1; j < list.Count && Math.Abs(value - list[j].Value) <= t; j++)
				{
					if (Math.Abs((long)list[i].Key - list[j].Key) <= k)
						return true;
				}
			}

			return false;
		}
	}
}
