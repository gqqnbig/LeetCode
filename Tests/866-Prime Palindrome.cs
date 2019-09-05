using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode.Tests
{
	class CPrimePalindrome
	{
		public int NearestPalindromicUpEntry = 0;
		public int IsPrimeEntry = 0;

		public int PrimePalindrome(int n)
		{
			if (n <= 1)
				n = 2;
			for (; ; )
			{

				n = NextPalindrome(n);
				var r = IsPrime(n);
				if (r == false)
					n++;
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
		public bool IsPrime(int n)
		{
			if (n == 1)
				return false;

			int limit = (int)Math.Sqrt(n);
			for (int i = 2; i <= limit; i++)
			{
				if (n % i == 0)
					return false;
			}

			return true;
		}

		/// <summary>
		/// 返回n或比n大且与n最接近的回文数
		/// </summary>
		/// <param name="n"></param>
		/// <param name="isPalindromic">如果已经是回文数</param>
		/// <returns></returns>
		int NearestPalindromicUp(int n)
		{
			NearestPalindromicUpEntry++;

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

		static int NextPalindrome(int n)
		{
			int nSize = GetSize(n);
			int root = GetRoot(n);

			var palindrome = FormPalindrome(root, nSize);
			if (palindrome < n)
			{
				root++;
				palindrome = FormPalindrome(root, nSize);
			}

			return palindrome;
		}

		/// <summary>
		/// 根据root组成回文数
		/// </summary>
		/// <param name="root"></param>
		/// <param name="size"></param>
		/// <returns></returns>
		public static int FormPalindrome(int root, int size)
		{
#if DEBUG
			int length = GetSize(root);
			if (length != (size + 1) / 2)
				throw new ArgumentException($"root={root}，长度为{length}。本方法只能合成长度为{length * 2}或{length * 2 - 1}的回文数，而指定的size为{size}。");
#endif
			var palindrome = root;

			if (size % 2 == 1)
				root /= 10;

			while (root > 0)
			{
				var digit = root % 10;
				root /= 10;
				palindrome = palindrome * 10 + digit;
			}
			return palindrome;
		}


		static int GetRoot(int n)
		{
			var size = GetSize(n);
			var rootSize = (size + 1) / 2;
			var root = n;
			for (var i = 0; i < size - rootSize; i++)
			{
				root /= 10;
			}

			return root;
		}

		/// <summary>
		/// 获得数字的位数
		/// </summary>
		/// <param name="n"></param>
		static int GetSize(int n)
		{
			if (n < 10) return 1;
			var digit = 0;
			do
			{
				digit++;
				n /= 10;
			}
			while (n != 0);
			return digit;
		}
	}
}
