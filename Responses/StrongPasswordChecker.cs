using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace LeetCode
{
	public class CStrongPasswordChecker
	{
		private static readonly Regex Regex = new Regex(@"(.)(\1){2,}");

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
			var sortedLength = FindRepeatingGroups(s);
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
			LinkedList<int> sortedLength = new LinkedList<int>(FindRepeatingGroups(s));
			//可以通过删除重复字符，即缩短密码，又断开连续字符
			int freeDeletion = toDelete;
			//找到长度为3的倍数的重复组，删除一个字后可少修改1次。

			LinkedListNode<int> next = null;
			for (var n = sortedLength.First; n != null && freeDeletion > 0; n = next)
			{
				next = n.Next;
				if (n.Value % 3 == 0)
				{
					n.Value--;
					freeDeletion--;
					//if (n.Value == 2)
					//	sortedLength.Remove(n);
				}
			}
			//如果freeDeletion足够多，上面循环一遍整个数组，则下面条件成立。
			//Debug.Assert(sortedLength.All(v => v != 3));

			for (var n = sortedLength.First; n != null && freeDeletion >= 2; n = next)
			{
				next = n.Next;
				if (n.Value % 3 == 1)
				{
					//把4变成2，不用modify
					//把7变成5，从2次modify变成1次
					n.Value -= 2;
					freeDeletion -= 2;

					//if (n.Value == 2)
					//	sortedLength.Remove(n);
				}
			}

			for (var n = sortedLength.First; n != null && freeDeletion >= 3; n = n.Next)
			{
				if (n.Value >= 3)
				{
					int m = Math.Min(n.Value / 3, freeDeletion / 3);
					n.Value -= 3 * m;
					freeDeletion -= 3 * m;
				}
			}

			//Debug.Assert(sortedLength.All(v => v >= 2));


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
			MatchCollection matchCollection = Regex.Matches(s);
			return from Match m in matchCollection
				   select m.Value.Length;
		}
	}
}
