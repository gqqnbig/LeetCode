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
	}


	class BinaryComparer : IComparer<int>
	{
		static int CountOneInBinary(int n)
		{
			string s = Convert.ToString(n, 2);
			return s.Count(c => c == '1');
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
