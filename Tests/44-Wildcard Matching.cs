using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LeetCode.Tests
{
	class WildcardMatching
	{
		const byte FixedMatch = 3;
		const byte PossibleMatch = 2;
		const byte NoMatch = 1;

		public bool IsMatch(string s, string pattern)
		{
			string p = pattern;
			do
			{
				pattern = p;
				p = pattern.Replace("**", "*");
			} while (p != pattern);

			//table[x,y]表示p[x]在s[y]的左边是否可以匹配
			byte[,] table = new byte[p.Length + 1, s.Length + 1];
			table[p.Length, s.Length] = FixedMatch;
			int lastFixedMatch = s.Length;
			for (int pi = p.Length - 1; pi >= 0; pi--)
			{
				char c = pattern[pi];

				switch (c)
				{
					case '?':
						for (int i = lastFixedMatch - 1; i >= 0; i--)
						{
							if (table[pi + 1, i + 1] >= PossibleMatch)
								table[pi, i] = FixedMatch;
						}
						lastFixedMatch = FixLastFixedMatch(pi, lastFixedMatch - 1, table);

						break;
					case '*':
						for (int i = lastFixedMatch; i >= 0; i--)
							table[pi, i] = PossibleMatch;
						break;
					default:
						//c是字符
						for (int i = lastFixedMatch - 1; i >= 0; i--)
						{
							if (table[pi + 1, i + 1] >= PossibleMatch && c == s[i])
								table[pi, i] = FixedMatch;
						}
						lastFixedMatch = FixLastFixedMatch(pi, lastFixedMatch - 1, table);
						break;
				}

				if (lastFixedMatch == -1)
					break;
			}

			return table[0, 0] >= PossibleMatch;

		}

		static int FixLastFixedMatch(int pi, int startIndex, byte[,] table)
		{
			for (int i = startIndex; i >= 0; i--)
			{
				if (table[pi, i] == FixedMatch)
					return i;
			}

			return -1;
		}

		public bool IsMatchReg(string s, string pattern)
		{
			string p = pattern;
			do
			{
				pattern = p;
				p = pattern.Replace("**", "*");
			} while (p != pattern);


			string regPattern = p.Replace("?", "[a-z]").Replace("*", "[a-z]*");
			Regex regex = new Regex("^" + regPattern + "$", RegexOptions.Singleline | RegexOptions.Compiled);
			return regex.IsMatch(s);
		}
	}
}
