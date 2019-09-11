using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xunit.Sdk;



namespace LeetCode.Tests
{
	[TestClass]
	public class Amazon
	{
		public int minimumTime(int[] parts)
		{
			return minimumTime(parts.Length, parts);
		}

		public int minimumTime(int numOfParts, int[] parts)
		{
			MinHeap heap=new MinHeap(parts.Length);

			for (int i = 0; i < parts.Length; i++)
			{
				heap.Add(parts[i]);
			}

			var sum = 0;
			while (heap.Count > 1)
			{
				var t1 = heap.Pop();
				var t2 = heap.Pop();

				sum += t1 + t2;
				heap.Add(t1 + t2);
			}

			return sum;
		}

		public class MinHeap
		{
			private readonly int[] _elements;
			private int _size;

			public MinHeap(int size)
			{
				_elements = new int[size];
			}

			public int Count
			{
				get { return _size; }
			}

			private int GetLeftChildIndex(int elementIndex) => 2 * elementIndex + 1;
			private int GetRightChildIndex(int elementIndex) => 2 * elementIndex + 2;
			private int GetParentIndex(int elementIndex) => (elementIndex - 1) / 2;

			private bool HasLeftChild(int elementIndex) => GetLeftChildIndex(elementIndex) < _size;
			private bool HasRightChild(int elementIndex) => GetRightChildIndex(elementIndex) < _size;
			private bool IsRoot(int elementIndex) => elementIndex == 0;

			private int GetLeftChild(int elementIndex) => _elements[GetLeftChildIndex(elementIndex)];
			private int GetRightChild(int elementIndex) => _elements[GetRightChildIndex(elementIndex)];
			private int GetParent(int elementIndex) => _elements[GetParentIndex(elementIndex)];

			private void Swap(int firstIndex, int secondIndex)
			{
				var temp = _elements[firstIndex];
				_elements[firstIndex] = _elements[secondIndex];
				_elements[secondIndex] = temp;
			}

			public bool IsEmpty()
			{
				return _size == 0;
			}

			public int Peek()
			{
				if (_size == 0)
					throw new IndexOutOfRangeException();

				return _elements[0];
			}

			public int Pop()
			{
				if (_size == 0)
					throw new IndexOutOfRangeException();

				var result = _elements[0];
				_elements[0] = _elements[_size - 1];
				_size = _size - 1;

				ReCalculateDown();

				return result;
			}

			public void Add(int element)
			{
				if (_size == _elements.Length)
					throw new IndexOutOfRangeException();

				_elements[_size] = element;
				_size = _size + 1;

				ReCalculateUp();
			}

			private void ReCalculateDown()
			{
				int index = 0;
				while (HasLeftChild(index))
				{
					var smallerIndex = GetLeftChildIndex(index);
					if (HasRightChild(index) && GetRightChild(index) < GetLeftChild(index))
					{
						smallerIndex = GetRightChildIndex(index);
					}

					if (_elements[smallerIndex] >= _elements[index])
					{
						break;
					}

					Swap(smallerIndex, index);
					index = smallerIndex;
				}
			}

			private void ReCalculateUp()
			{
				var index = _size - 1;
				while (!IsRoot(index) && _elements[index] < GetParent(index))
				{
					var parentIndex = GetParentIndex(index);
					Swap(parentIndex, index);
					index = parentIndex;
				}
			}
		}


		[DataTestMethod]
		[DataRow(53, new[] { 3, 4, 5, 4, 7 })]
		[DataRow(0, new int[0])]
		[DataRow(58, new[] { 8, 4, 6, 12 })]
		public void minimumTimeTest(int expected, int[] parts)
		{
			Assert.AreEqual(expected, new Amazon().minimumTime(parts));
		}
	}
}
