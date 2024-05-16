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

		public static int CountOneInBinary(int n)
		{
			if (n == 0)
				return 0;
			if (n == 1)
				return 1;

			int remainder;
			n = Math.DivRem(n, 2, out remainder);
			if (remainder == 1)
				return 1 + CountOneInBinary(n);
			else
				return CountOneInBinary(n);
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
