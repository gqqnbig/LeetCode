using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace LeetCode
{
	public class CStrongPasswordChecker
	{
		/// <summary>
		/// Return the minimum changes requires to make a password strong.
		/// </summary>
		/// <param name="s"></param>
		/// <returns></returns>
		public int StrongPasswordChecker(string s)
		{
			if (s.Length > 20)
				return CheckerTooLong(s);
			else if (s.Length < 6)
				return CheckerTooShort(s);
			else
				return Checker(s);
		}


		private static int Checker(string s)
		{
			var sortedLength = FindRepeatingGroups(s).ToList();
			//repeatingGroupIndex指向尚未处理的字符组，现在需要使用modify
			int toModify = sortedLength.Select(v => v / 3).Sum();
			int toAdd = AddCharacters(s, toModify);

			return toModify + toAdd;
		}

		private static int CheckerTooShort(string s)
		{
			var sortedLength = FindRepeatingGroups(s).ToList();
			//通过插入字符打断连续字符
			int toAdd = sortedLength.Select(v => (int)Math.Ceiling(v / 2.0) - 1).Sum();
			toAdd += AddCharacters(s, toAdd);

			//添加了toAdd个字符，但长度可能还是不足6，要补足。
			if (s.Length + toAdd < 6)
				toAdd = 6 - s.Length;

			return toAdd;
		}

		private static int CheckerTooLong(string s)
		{
			int toDelete = s.Length - 20; //要删除这么多字符
			var sortedLength = FindRepeatingGroups(s).ToList();
			//可以通过删除重复字符，即缩短密码，又断开连续字符
			int freeDeletion = toDelete;
			while (freeDeletion > 0 && sortedLength.Count > 0)
			{
				//找到长度为3的倍数的重复组，删除一个字后可少修改1次。
				for (int i = 0; i < sortedLength.Count && freeDeletion > 0; i++)
				{
					if (sortedLength[i] % 3 == 0)
					{
						sortedLength[i]--;
						freeDeletion--;
					}
				}
				//如果freeDeletion足够多，上面循环一遍整个数组，则下面条件成立。
				//Debug.Assert(sortedLength.All(v => v != 3));

				//把剩余的freeDeletion分配给长度大于5的组。长度为5的组可通过modify断开。
				for (int i = 0; i < sortedLength.Count && freeDeletion > 0; i++)
				{
					if (sortedLength[i] > 5)
					{
						sortedLength[i] -= 1;
						freeDeletion--;
					}
				}

				sortedLength = sortedLength.Where(v => v > 2).ToList();

				if (freeDeletion > 0 && sortedLength.Count > 0)
				{
					int minIndex = 0;
					int min = int.MaxValue;
					for (int i = 0; i < sortedLength.Count; i++)
					{
						min = Math.Min(min, sortedLength[i]);
						minIndex = i;
					}

					sortedLength[minIndex]--;
					freeDeletion--;
				}

				Debug.Assert(sortedLength.All(v => v >= 2));
			}


			int toModify = sortedLength.Select(v => v / 3).Sum();
			int toAdd = AddCharacters(s, toModify);

			return toDelete + toModify + toAdd;
		}

		/// <summary>
		/// 优先使用freeCharacters满足复杂度要求。当freeCharacters用完，才新增字符。
		/// 返回需要新增的字符数量。
		/// </summary>
		/// <param name="s"></param>
		/// <param name="freeCharacters">未指定的字符数量。这些字符可用来满足复杂度要求。</param>
		/// <returns></returns>
		private static int AddCharacters(string s, int freeCharacters)
		{
			int toAdd = 0;
			if (s.Any(char.IsLower) == false)
			{
				if (freeCharacters > 0)
					freeCharacters--; //其中一个新增的字符必须是小写
				else
					toAdd++;
			}

			if (s.Any(char.IsUpper) == false)
			{
				if (freeCharacters > 0)
					freeCharacters--; //其中一个新增的字符必须是大写
				else
					toAdd++;
			}

			if (s.Any(char.IsDigit) == false)
			{
				if (freeCharacters > 0)
					freeCharacters--; //其中一个新增的字符必须是数字
				else
					toAdd++;
			}

			return toAdd;
		}

		private static IEnumerable<int> FindRepeatingGroups(string s)
		{
			Regex repeatingCheck = new Regex(@"(.)(\1){2,}");
			MatchCollection matchCollection = repeatingCheck.Matches(s);
			return from Match m in matchCollection
				   select m.Value.Length;
		}
	}
}
