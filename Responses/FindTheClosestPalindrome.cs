using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{
	public class FindTheClosestPalindrome
	{
		public string NearestPalindromic(string n)
		{
			var num = Convert.ToInt64(n);
			return NearestPalindromic(num).ToString();
		}

		public long NearestPalindromic(long num)
		{
			var v1 = NearestPalindromicUp(num + 1);
			var v2 = NearestPalindromicDown(num - 1);

			if (v1 == null)
			{
				Debug.Assert(v2 != null);
				return v2.Value;
			}
			else if (v2 == null)
			{
				Debug.Assert(v1 != null);
				return v1.Value;

			}
			else if (Math.Abs(v1.Value - num) < Math.Abs(v2.Value - num))
				return v1.Value;
			else
				return v2.Value;
		}

		long? NearestPalindromicUp(long n)
		{
			long max = Convert.ToInt64(new string('9', 18));
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

					if (max - n < toAdd)//会溢出
						return null;

					n += toAdd;
					s = n.ToString();
				}
			}

			return n;
		}

		long? NearestPalindromicDown(long n)
		{
			string s = n.ToString();
			for (int i = 0; i < s.Length / 2; i++)
			{
				while (s[i] != s[s.Length - 1 - i])
				{
					//修改较小的一位
					long toSubtract;
					if (s[i] < s[s.Length - 1 - i])
					{
						toSubtract = (s[s.Length - 1 - i] - s[i]) * (int)Math.Pow(10, i);
					}
					else
					{
						toSubtract = (10 + s[s.Length - 1 - i] - s[i]) * (int)Math.Pow(10, i);
					}

					if ((n - toSubtract).ToString().Length != s.Length)
					{
						toSubtract = (n % 10) + 1;
					}

					n -= toSubtract;

					s = n.ToString();
					if (n < 0)
						return null;
				}
			}

			return n;
		}
	}
}
