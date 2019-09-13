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
			string longestDup = "";
			int lowerBound = longestDup.Length;//成功的length
			int upperBound = S.Length;

			while (lowerBound < upperBound - 1)
			{
				int probingLength = (lowerBound + upperBound) / 2;
				var v = FindDupWithLength(S, probingLength);
				if (v.Length > 0)
				{
					longestDup = v;

					lowerBound = probingLength;
				}
				else
				{
					upperBound = probingLength;
				}
			}

			return longestDup;

		}
		/// <summary>
		/// Check if there is a substring with subLength in S
		/// </summary>
		/// <param name="S"></param>
		/// <param name="subLength"></param>
		/// <returns></returns>
		static string FindDupWithLength(string S, int subLength)
		{
			HashSet<string> set = new HashSet<string>(S.Length - subLength + 1);
			for (int i = 0; i < S.Length - subLength + 1; i++)
			{
				var sub = S.Substring(i, subLength);
				if (set.Contains(sub))
					return sub;
				else
					set.Add(sub);
			}

			return string.Empty;
		}

		public string LongestDupSubstringMine(string S)
		{
			string maxSub = "";
			int max = 0;

			//HashSet<string> notFoundStrings=new HashSet<string>();
			//Dictionary<string, int> stringIndexOf = new Dictionary<string, int>();
			for (int i = 0; i < S.Length; i++)
			{
				//var remaining = S.Substring(i + 1);
				int lastFoundIndex = i + 1;
				for (int l = max + 1; l < S.Length + 1 - i; l++)
				{
					var sub = S.Substring(i, l);//有可能反复查找同一个子字符串
												//Debug.Assert(notFoundStrings.Contains(sub) == false);
												//notFoundStrings.Add(sub);

					//Console.WriteLine(sub);
					var n = S.IndexOf(sub, lastFoundIndex);

					if (n > -1)
					{
						//因为其实条件 l = Math.Max(1, max)
						// sub.Length一定大于max
						Debug.Assert(l > max);
						max = l;
						maxSub = sub;
						lastFoundIndex = n;
					}
					else
						break; //如果已经找不到一个短字符串了，把这个字符串变长，就更找不到了
				}
			}

			return maxSub;

		}
	}
}
