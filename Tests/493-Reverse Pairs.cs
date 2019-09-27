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

			var subList = CreateSubList(arr);

			int count = 0;
			for (int i = 0; i < arr.Length; i++)
			{
				var n = arr[i].Value / 2f;
				var index = arr[i].Index;

				var subListCurrent = subList.First;
				while (subListCurrent != null && subListCurrent.Value.Value >= n)
				{
					var next = subListCurrent.Next;
					subList.Remove(subListCurrent);
					subListCurrent = next;
				}


				subListCurrent = subList.First;
				//排除索引不合要求的项目
				while (subListCurrent != null)
				{
					var next = subListCurrent.Next;
					if (subListCurrent.Value.Index >= index)
						count++;
					subListCurrent = next;
				}
			}

			return count;
		}

		private static LinkedList<Item> CreateSubList(Item[] arr)
		{
			var subListStart = 1;

			var n = arr[0].Value / 2f;
			for (; subListStart < arr.Length && arr[subListStart].Value >= n; subListStart++) { }

			LinkedList<Item> subList = new LinkedList<Item>(arr.Skip(subListStart));
			return subList;
		}

		/// <summary>
		/// 从链接表的指定节点开始复制
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="node"></param>
		/// <returns></returns>
		LinkedList<T> Clone<T>(LinkedListNode<T> node)
		{
			var list = new LinkedList<T>();
			while (node != null)
			{
				list.AddLast(node.Value);
				node = node.Next;
			}

			return list;
		}

		[DebuggerDisplay("[{Index}]={Value}")]
		struct Item
		{
			public int Value;
			public int Index;
		}
	}
}
