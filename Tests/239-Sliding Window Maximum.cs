using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode.Tests
{
	class SlidingWindowMaximum
	{
		public int[] MaxSlidingWindow(int[] nums, int k)
		{
			int[] result = new int[nums.Length - k + 1];
			//用链表实现单调队列，因为需要从两端移除元素。
			//设尾端为peek。
			//设该队列为单调递增队列，peek为最大值。
			var monotoneQueue = new LinkedList<int>();

			//初始化单调队列，同时也是处理初始窗口

			for (int j = 0; j < result.Length; j++)
			{
				for (int i = 0; i < k; i++)
					Enqueue(monotoneQueue, nums[i]);

				result[j] = monotoneQueue.Last.Value;

				monotoneQueue.RemoveFirst();
			}

			return result;
		}

		/// <summary>
		/// Add an item to monotonic increasing queue. If the item is smaller than the peek, ie. <see cref="LinkedList{T}.Last"/>,
		/// then peek will be added instead.
		/// </summary>
		/// <param name="monotonicIncreasingQueue"></param>
		/// <param name="item"></param>
		void Enqueue(LinkedList<int> monotonicIncreasingQueue, int item)
		{
			if (monotonicIncreasingQueue.Last?.Value > item)
				monotonicIncreasingQueue.AddLast(monotonicIncreasingQueue.Last.Value);
			else
				monotonicIncreasingQueue.AddLast(item);
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
