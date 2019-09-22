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
			byte[,] table = new byte[p.Length, s.Length];
			int lastFixedMatch = s.Length;
			for (int pi = p.Length - 1; pi >= 0; pi--)
			{
				char c = pattern[pi];

				switch (c)
				{
					case '?':
						if (pi == p.Length - 1)
						{
							if (s.Length - 1 >= 0)
								table[pi, s.Length - 1] = FixedMatch;
							else
								return false;
						}
						else
						{
							for (int i = lastFixedMatch - 1; i >= 0; i--)
							{
								if (i + 1 < s.Length && table[pi + 1, i + 1] >= PossibleMatch)
									table[pi, i] = FixedMatch;
							}
						}
						lastFixedMatch = FixLastFixedMatch(pi, lastFixedMatch - 1, table);

						break;
					case '*':
						for (int i = Math.Min(lastFixedMatch, s.Length - 1); i >= 0; i--)
							table[pi, i] = PossibleMatch;
						break;
					default:
						//c是字符
						if (pi == p.Length - 1)
						{
							if (s.Length - 1 >= 0 && c == s[s.Length - 1])
								table[pi, s.Length - 1] = FixedMatch;
							else
								return false;
						}
						else
						{
							for (int i = lastFixedMatch - 1; i >= 0; i--)
							{
								if (i + 1 < s.Length && table[pi + 1, i + 1] >= PossibleMatch && c == s[i])
									table[pi, i] = FixedMatch;
								else if (table[pi + 1, i] == FixedMatch)
								{
									if (i - 1 >= 0 && c == s[i - 1])
										table[pi, i - 1] = FixedMatch;
									else
										return false;
								}
							}

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
