using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode.Tests
{
	class LongestDuplicateSubstring
	{
		public string LongestDupSubstring(string S)
		{
			string maxSub = "";
			int max = 0;

			Dictionary<string, int> stringIndexOf = new Dictionary<string, int>();
			for (int i = 0; i < S.Length; i++)
			{
				var remaining = S.Substring(i + 1);
				for (int l = max + 1; l < S.Length + 1 - i; l++)
				{
					var sub = S.Substring(i, l);//有可能反复查找同一个子字符串
					if (stringIndexOf.TryGetValue(sub, out var n) == false ||
						n != -1 && n <= i) //n是以前的记录值
					{
						n = remaining.IndexOf(sub);

						//Console.WriteLine(sub);

						stringIndexOf[sub] = n;
					}

					if (n > -1)
					{
						//因为其实条件 l = Math.Max(1, max)
						// sub.Length一定大于max
						Debug.Assert(l > max);
						max = l;
						maxSub = sub;
					}
					else
						break; //如果已经找不到一个短字符串了，把这个字符串变长，就更找不到了
				}
			}

			return maxSub;

		}
	}
}
