﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode.Tests
{
	class CPrimePalindrome
	{
		public int PrimePalindrome(int n)
		{
			if (n <= 1)
				n = 2;
			for (; ; )
			{

				n = NearestPalindromicUp(n);
				var r = IsPrime(n);
				if (r != 0)
					n += r - 1;
				else
					break;
			}
			return n;
		}


		/// <summary>
		/// 返回最小的因子。如果n为素数，则返回0。
		/// </summary>
		/// <param name="n"></param>
		/// <returns></returns>
		public int IsPrime(int n)
		{
			if (n == 1)
				return 1;

			for (int i = 2; i <= n / 2; i++)
			{
				if (n % i == 0)
					return i;
			}

			return 0;
		}

		/// <summary>
		/// 返回n或比n大且与n最接近的回文数
		/// </summary>
		/// <param name="n"></param>
		/// <param name="isPalindromic">如果已经是回文数</param>
		/// <returns></returns>
		int NearestPalindromicUp(int n)
		{
			string s = n.ToString();
			for (int i = 0; i < s.Length / 2; i++)
			{

				while (s[i] != s[s.Length - 1 - i])
				{
					//修改较小的一位
					int toAdd;
					if (s[i] < s[s.Length - 1 - i])
					{
						toAdd = (10 + s[i] - s[s.Length - 1 - i]) * (int)Math.Pow(10, i);
					}
					else
					{
						toAdd = (s[i] - s[s.Length - 1 - i]) * (int)Math.Pow(10, i);
					}

					n += toAdd;
					s = n.ToString();
				}
			}

			return n;
		}
	}
}
