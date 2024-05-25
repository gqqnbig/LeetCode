using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace LeetCode.Tests
{



	
	public class MyLinkedListTests
	{

		[Fact]
		public void GetTest1()
		{
			var list = new MyLinkedList();
			Assert.Equal(-1, list.Get(-1));

			list.AddAtHead(1);
			list.AddAtTail(3);
			list.AddAtIndex(1, 2);

			Assert.That.AreEqual(-1, () => list.Get(-1));
		}


		[Fact]
		public void GetTest2()
		{
			var list = new MyLinkedList();
			Assert.Equal(-1, list.Get(-1));

			list.AddAtIndex(-1, 0);
			Assert.That.AreEqual(0, () => list.Get(0));

			list.DeleteAtIndex(-1);
		}

		[Fact]
		public void AddAtHeadTest()
		{
			var list = new MyLinkedList();
			list.AddAtHead(1);
			list.AddAtHead(2);
			list.AddAtHead(3);

			Assert.Equal(list.Get(0), 3);
			Assert.Equal(list.Get(1), 2);
			Assert.Equal(list.Get(2), 1);
		}

		[Fact]
		public void AddAtTailTest()
		{
			var list = new MyLinkedList();
			list.AddAtTail(1);
			list.AddAtTail(2);
			list.AddAtTail(3);

			Assert.Equal(list.Get(0), 1);
			Assert.Equal(list.Get(1), 2);
			Assert.Equal(list.Get(2), 3);
		}

		[Fact]
		public void AddAtIndexTest()
		{

			var list = new MyLinkedList();
			list.AddAtIndex(0, 1);
			list.AddAtIndex(1, 2);
			list.AddAtIndex(2, 3);
			list.AddAtIndex(2, 4);
			list.AddAtIndex(1, 5);
			list.AddAtIndex(0, 6);
			list.AddAtIndex(10, 7);


			Assert.Equal(6, list.Get(0));
			Assert.Equal(1, list.Get(1));
			Assert.Equal(5, list.Get(2));
			Assert.Equal(2, list.Get(3));
			Assert.Equal(4, list.Get(4));
			Assert.Equal(3, list.Get(5));
			Assert.Equal(-1, list.Get(10));
			Assert.Equal(-1, list.Get(6));
		}

		[Fact]
		public void DeleteAtIndexTest()
		{
			var list = new MyLinkedList();
			list.AddAtIndex(0, 1);
			list.AddAtIndex(1, 2);
			list.AddAtIndex(2, 3);
			list.AddAtIndex(2, 4);
			list.AddAtIndex(1, 5);
			list.AddAtIndex(0, 6);
			list.AddAtIndex(10, 7);
			//{6,1,5,2,4,3}
			list.DeleteAtIndex(0);
			list.DeleteAtIndex(1);
			list.DeleteAtIndex(3);
			//{1,2,4}

			Assert.Equal(1, list.Get(0));
			Assert.Equal(2, list.Get(1));
			Assert.Equal(4, list.Get(2));
			Assert.Equal(-1, list.Get(3));
		}

		[Fact]
		public void GenerationTest()
		{
			var rand = new Random();
			for (int i = 0; i < 10; i++)
			{
				var sequence = GenerateOperationSequence(rand);
				Console.WriteLine("sequence={0}", sequence);
				ExecuteOperationSequence(sequence);
			}
		}

		[Theory]
		[InlineData("T T T T")]
		[InlineData("T T T T H -3 +6 +12 T H")]
		[InlineData("H --2 +2 +6 -6 +12 -7 -5 T T")]
		[InlineData("H T +1 -1")]
		[InlineData("-9 -5 +1 +8 -0 +6 --1 -3 H +11")]
		public void OperationSequenceTest(string sequence)
		{
			ExecuteOperationSequence(sequence);
		}

		private void ExecuteOperationSequence(string sequence)
		{
			var list = new MyLinkedList();
			var compare = new List<int>();
			int c = 0;
			for (int i = 0; i < sequence.Length; i += 2)//一个操作符后面有一个空格
			{
				char op = sequence[i];
				int index;
				int l;
				switch (op)
				{
					case '+':
						index = PickLeadingNumber(sequence, i + 1, out l);
						i += l;
						list.AddAtIndex(index, c);
						if (index >= 0 && index <= compare.Count)
							compare.Insert(index, c);
						c++;
						break;
					case '-':
						index = PickLeadingNumber(sequence, i + 1, out l);
						i += l;
						list.DeleteAtIndex(index);

						if (index >= 0 && index < compare.Count)
							compare.RemoveAt(index);
						c++;
						break;
					case 'H':
						list.AddAtHead(c);
						compare.Insert(0, c);
						c++;
						break;
					case 'T':
						list.AddAtTail(c);
						compare.Add(c);
						c++;
						break;
					default:
						throw new FormatException($"{sequence}不是合法的操作序列，在索引{i}处，{op}不是一个操作符。");
				}
			}

			StringBuilder sb1 = new StringBuilder();
			StringBuilder sb2 = new StringBuilder();


			for (int i = 0; i < compare.Count; i++)
			{
				sb1.Append(compare[i] + " ");
				sb2.Append(list.Get(i) + " ");
			}

			var v = list.Get(compare.Count);
			if (v > -1)
				sb2.Append(v + " ");
			v = list.Get(compare.Count + 1);
			if (v > -1)
				sb2.Append(v + " ");

			Assert.Equal(sb1.ToString().Trim(), sb2.ToString().Trim());

		}

		private static string GenerateOperationSequence(Random random)
		{
			char[] operations = { '+', '-', 'H', 'T' };
			StringBuilder operationSequence = new StringBuilder();
			const int listLength = 10;
			for (int c = 0; c < listLength; c++)
			{
				char op = operations[random.Next(operations.Length)];
				int index;
				switch (op)
				{
					case '+':
						index = random.Next(listLength + 5) - 2;
						operationSequence.Append(" +" + index);
						break;
					case '-':
						index = random.Next(listLength + 5) - 2;
						operationSequence.Append(" -" + index);
						break;
					case 'H':
						operationSequence.Append(" H");
						break;
					case 'T':
						operationSequence.Append(" T");
						break;
				}
			}

			return operationSequence.ToString().Trim();
		}

		private int PickLeadingNumber(string s, int startIndex, out int length)
		{
			int n = 0;
			int i = startIndex;
			bool isNegative = false;
			for (; i < s.Length; i++)
			{
				if (s[i] == '-')
					isNegative = true;
				else
				{
					var d = s[i] - '0';
					if (d < 0 || d > 9)
						break;

					n = n * 10 + d;
				}
			}

			length = i - startIndex;
			if (length == 0)
				throw new FormatException();
			return isNegative ? -n : n;
		}

	}
}
