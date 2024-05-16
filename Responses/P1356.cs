using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace LeetCode
{
	class P1356
	{
		public int[] SortByBits(int[] arr)
		{
			Array.Sort(arr, new BinaryComparer());

			return arr;
		}

		public static void Main(string[] args)
		{
			Console.WriteLine(BinaryComparer.CountOneInBinary(0));
			Console.WriteLine(BinaryComparer.CountOneInBinary(7));
			Console.WriteLine(BinaryComparer.CountOneInBinary(100));
		}
	}


	class BinaryComparer : IComparer<int>
	{
		/// <summary>
		/// Cache the number of bit 1 in an integer of base 10.
		/// </summary>
		static Dictionary<int, int> bits = new Dictionary<int, int>();

		public static int CountOneInBinary(int n)
		{
			if (n == 0)
				return 0;
			if (n == 1)
				return 1;

			int count;
			if (bits.TryGetValue(n, out count))
				return count;

			int remainder;
			int q = Math.DivRem(n, 2, out remainder);
			if (remainder == 1)
				count = 1 + CountOneInBinary(q);
			else
				count = CountOneInBinary(q);
			bits.Add(n, count);
			return count;
		}

		public int Compare([AllowNull] int x, [AllowNull] int y)
		{
			int cx = CountOneInBinary(x);
			int cy = CountOneInBinary(y);

			if (cx < cy)
				return -1;
			else if (cx > cy)
				return 1;
			else
			{
				//revert to regular integer comparsion.
				if (x < y)
					return -1;
				else if (x > y)
					return 1;
				else
					return 0;
			}
		}
	}
}
