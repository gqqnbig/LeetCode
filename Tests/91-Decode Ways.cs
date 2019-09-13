using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode.Tests
{
	class DecodeWays
	{
		public int NumDecodings(string s)
		{
			var waysCache = new int[s.Length + 1];
			waysCache[s.Length] = 1;
			for (int i = s.Length - 1; i >= 0; i--)
			{
				if (IsChar(s[i]))
					waysCache[i]+=waysCache[i+1];

				if (i - 1 >= 0 && IsChar(s[i - 1], s[i]))
					waysCache[i - 1] += waysCache[i + 1];

			}

			return waysCache[0];
		}

		static bool IsChar(char a)
		{
			return a > '0' && a <= '9';
		}

		static bool IsChar(char a, char b)
		{
			if (a == '1')
				return true;
			if (a == '2' && b >= '0' && b <= '6')
				return true;

			return false;
		}
	}
}
