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
		internal static string FindDupWithLength(string S, int subLength)
		{
			HashSet<int> set = new HashSet<int>(S.Length - subLength + 1);
			int hash = GetHash(S, 0, subLength);
			const int @base = 26;
			set.Add(hash);

			int p = 1;
			bool hasOverflow;
			//防止溢出
			if (Math.Log(int.MaxValue, @base) >= subLength - 1)
			{
				p = (int)Math.Pow(@base, subLength - 1);
				hasOverflow = false;
			}
			else
			{
				for (int i = 0; i < subLength - 1; i++)
					p = unchecked(p * @base);
				hasOverflow = true;
			}

			for (int i = subLength; i < S.Length; i++)
			{
				/* i-1是子字符串的末尾，把子字符串头的第一个字符的hash减掉
				 *
				 * abcd
				 *    |
				 *    i
				*/
				hash = unchecked(hash - (S[i - subLength] - 'a') * p);
				Debug.Assert(hash == GetHash(S, i - subLength + 1, subLength - 1));

				hash = unchecked(hash * @base + S[i] - 'a');
				/* 设subLength=3，abc为已经处理过的字符串。
				 *
				 * abcd
				 * ~~~|
				 *    i
				*/
				if (set.Contains(hash))
				{
					var sub = S.Substring(i - subLength + 1, subLength);
					if (hasOverflow == false || S.Substring(0, i).IndexOf(sub) > -1)
						return sub;
					else
					{
						Console.WriteLine("hash collision");
						set.Add(hash);
					}
				}
				else
					set.Add(hash);
			}

			return string.Empty;
		}

		static int GetHash(string s, int start, int length)
		{
			int hash = 0;
			const int @base = 26;
			for (int i = start; i < start + length; i++)
				hash = unchecked(hash * @base + s[i] - 'a');
			return hash;
		}
	}
}
