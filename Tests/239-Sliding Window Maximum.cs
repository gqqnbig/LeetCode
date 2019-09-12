using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode.Tests
{
	class SlidingWindowMaximum
	{
		public int[] MaxSlidingWindow(int[] nums, int k)
		{
			if (nums.Length == 0)
				return new int[0];
			int[] result = new int[nums.Length - k + 1];
			for (int i = 0; i < result.Length; i++)
				result[i] = nums.Skip(i).Take(k).Max();

			return result;
		}

		public int[] MaxSlidingWindowQueue(int[] nums, int k)
		{
			if (nums.Length == 0)
				return new int[0];
			int[] result = new int[nums.Length - k + 1];
			//用链表实现单调队列，因为需要从两端移除元素。
			//设尾端为peek。
			//设该队列为单调递减队列，First为最大值，Last为最大值。
			var monotoneQueue = new LinkedList<Data>();

			//初始化单调队列，同时也是处理初始窗口
			for (int i = 0; i < k - 1; i++)
				Enqueue(monotoneQueue, new Data(i, nums[i]));

			for (int i = 0; i < result.Length; i++)
			{
				Enqueue(monotoneQueue, new Data(i + k - 1, nums[i + k - 1]));
				result[i] = monotoneQueue.First.Value.Value;

				Debug.Assert(monotoneQueue.First.Value.Index >= i);
				if (monotoneQueue.First.Value.Index == i)
					monotoneQueue.RemoveFirst();
			}

			return result;
		}

		/// <summary>
		/// Add an item to monotonic decreasing queue. If the item is larger than <see cref="LinkedList{T}.Last"/>,
		/// <see cref="LinkedList{T}.Last"/> will be removed.
		/// </summary>
		/// <param name="monotonicDecreasingQueue"></param>
		/// <param name="item"></param>
		void Enqueue(LinkedList<Data> monotonicDecreasingQueue, Data item)
		{
			while (monotonicDecreasingQueue.Last?.Value.Value < item.Value)
				monotonicDecreasingQueue.RemoveLast();

			monotonicDecreasingQueue.AddLast(item);
		}

		[DebuggerDisplay("Index={Index}, Value={Value}")]
		struct Data
		{
			public Data(int index, int value)
			{
				Index = index;
				Value = value;
			}
			public int Index { get; }
			public int Value { get; }
		}
	}



	public class MonotoneQueue : IEnumerable<int>
	{
		public int FixedSize { get; }
		LinkedList<int> list = new LinkedList<int>();
		/// <summary>
		/// 若为true，则 <see cref="Peek"/> 是全队列最大的。
		/// </summary>
		public bool PeekMax { get; }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="peekMax">队列只允许在队尾进行插入操作。若rearMax为true，则后端元素是全队列最大的。</param>
		public MonotoneQueue(bool peekMax) : this(peekMax, int.MaxValue)
		{
		}

		public MonotoneQueue(bool peekMax, int fixedSize)
		{
			FixedSize = fixedSize;
			this.PeekMax = peekMax;
		}

		public void Enqueue(int item)
		{
			while (list.Count > 0 && (PeekMax && list.Last.Value >= item || PeekMax == false && list.Last.Value <= item))
				list.RemoveLast();
			if (list.Count >= FixedSize)
				list.RemoveFirst();

			list.AddLast(item);
		}

		public void Dequeue()
		{
			list.RemoveFirst();
		}
		/// <summary>
		/// 若 <see cref="PeekMax"/> 为true，则Peek是全队列最大的。
		/// </summary>
		/// <returns></returns>
		public int Peek => list.Last.Value;

		public IEnumerator<int> GetEnumerator()
		{
			return list.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return list.GetEnumerator();
		}
	}

}
