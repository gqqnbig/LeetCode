using System.Collections.Generic;
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
			int toDelete = s.Length > 20 ? s.Length - 20 : 0; //要删除这么多字符
			var sortedLength = FindRepeatingGroups(s).ToList();
			//可以通过删除重复字符，即缩短密码，又断开连续字符
			sortedLength.Sort((a, b) => -a.CompareTo(b));
			int freeDeletion = toDelete;
			while (freeDeletion > 0 && sortedLength.Count > 0)
			{
				//找到长度为3的倍数且至少为6的重复组，它本来要求修改2次，删除一个字后只需修改1次。
				for (int i = 0; i < sortedLength.Count && freeDeletion > 0; i++)
				{
					if (sortedLength[i] / 3 * 3 == sortedLength[i])
					{
						sortedLength[i] -= 1;
						freeDeletion--;
					}
				}

				//把剩余的freeDeletion分配给所有重复组
				for (int i = 0; i < sortedLength.Count && freeDeletion > 0; i++)
				{
					if (sortedLength[i] >= 3)
					{
						sortedLength[i] -= 1;
						freeDeletion--;
					}
				}

				sortedLength = sortedLength.Where(v => v > 2).ToList();
			}


			//repeatingGroupIndex指向尚未处理的字符组，现在需要使用modify
			int toModify = sortedLength.Select(v => v / 3).Sum();
			int freeModify = toModify;
			int toAdd = 0;

			if (s.Any(char.IsLower) == false)
			{
				if (freeModify > 0)
					freeModify--; //其中一个新增的字符必须是小写
				else
					toAdd++;
			}

			if (s.Any(char.IsUpper) == false)
			{
				if (freeModify > 0)
					freeModify--; //其中一个新增的字符必须是大写
				else
					toAdd++;
			}

			if (s.Any(char.IsDigit) == false)
			{
				if (freeModify > 0)
					freeModify--; //其中一个新增的字符必须是数字
				else
					toAdd++;
			}

			//添加了toAdd个字符，但长度可能还是不足6，要补足。
			if (s.Length + toAdd < 6)
				toAdd = 6 - s.Length;

			return toDelete + toModify + toAdd;
		}

		/// <summary>
		/// 返回最少的修改次数，以便断开一个连续的相同字符串
		/// </summary>
		/// <param name="s"></param>
		/// <returns></returns>
		internal int ModifyToBreak(string s)
		{
			return FindRepeatingGroups(s).Select(v => v / 3).Sum();
		}

		internal int RemoveToBreak(string s)
		{
			return FindRepeatingGroups(s).Select(v => v - 2).Sum();
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
