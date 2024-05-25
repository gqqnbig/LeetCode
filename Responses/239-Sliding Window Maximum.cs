using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

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

			//通过比较器设置队列为单调递减
			var comparer = Comparer<Data>.Create((a, b) => b.Value.CompareTo(a.Value));
			var monotoneQueue = new MonotoneQueue<Data>(comparer);

			//初始化单调队列，同时也是处理初始窗口
			for (int i = 0; i < k - 1; i++)
				monotoneQueue.Enqueue(new Data(i, nums[i]));

			for (int i = 0; i < result.Length; i++)
			{
				monotoneQueue.Enqueue(new Data(i + k - 1, nums[i + k - 1]));
				result[i] = monotoneQueue.Head.Value;

				Debug.Assert(monotoneQueue.Head.Index >= i);
				if (monotoneQueue.Head.Index == i)
					monotoneQueue.Dequeue();
			}

			return result;
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



	public class MonotoneQueue<T> : IEnumerable<T>
	{
		private readonly IComparer<T> comparer;
		private readonly LinkedList<T> list = new LinkedList<T>();

		/// <summary>
		/// 
		/// </summary>
		/// <param name="comparer">用comparer的方法来决定单调递增还是单调递减</param>
		public MonotoneQueue(IComparer<T> comparer)
		{
			this.comparer = comparer;
		}

		public void Enqueue(T item)
		{
			while (list.Count > 0 && comparer.Compare(list.Last.Value, item) > 0)
				list.RemoveLast();

			list.AddLast(item);
		}

		public void Dequeue()
		{
			list.RemoveFirst();
		}


		public T Head => list.First.Value;


		public IEnumerator<T> GetEnumerator()
		{
			return list.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return list.GetEnumerator();
		}
	}

}
