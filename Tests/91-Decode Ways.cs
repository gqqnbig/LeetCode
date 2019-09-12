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
			var waysCache = Enumerable.Repeat(-1, s.Length + 1).ToArray();
			waysCache[s.Length] = 1;
			NumDecodings(s, 0, waysCache);
			return waysCache[0];
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="s"></param>
		/// <param name="index"></param>
		/// <param name="waysCache"></param>
		/// <returns>s从index起，是不是一个合法的编码</returns>
		bool NumDecodings(string s, int index, int[] waysCache)
		{
			//if (s.Length == index)
			//	return true;

			if (waysCache[index] > -1)
				return true;

			//开始处理waysCache[index]
			waysCache[index] = 0;
			if (index == s.Length - 1) //只能尝试一种解释
			{
				if (IsChar(s[index]))
					waysCache[index] = 1;
			}
			else
			{
				if (IsChar(s[index]) && NumDecodings(s, index + 1, waysCache))
					waysCache[index] += waysCache[index + 1];

				if (IsChar(s[index], s[index + 1]) && NumDecodings(s, index + 2, waysCache))
					waysCache[index] +=  waysCache[index + 2];
			}

			return waysCache[index] > 0;
		}

		bool IsChar(char a, char b = '\0')
		{
			if (b == '\0' && a > '0' && a <= '9')
				return true;

			if (a == '1')
				return true;
			if (a == '2' && b >= '0' && b <= '6')
				return true;

			return false;
		}
	}
}
