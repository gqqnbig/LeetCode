using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

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
			int toDelete = 0;
			if (s.Length > 20)
				toDelete = s.Length - 20; //要删除这么多字符

			int toAdd = 0;
			int freeCharacters = 0;
			if (s.Length < 6)
			{
				toAdd = 6 - s.Length; //要补上这么多字符
				freeCharacters = toAdd; //有这么多字符可以用来满足大小写要求。
			}

			Debug.Assert(toDelete == 0 || toAdd == 0);

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

			return toDelete + toAdd + ModifyToBreak(s);
		}


		bool IsStrong(string password)
		{
			if (password.Length < 6 || password.Length > 20)
				return false;

			if ((password.Any(char.IsLower) && password.Any(char.IsUpper) && password.Any(char.IsDigit)) == false)
				return false;

			Regex repeatingCheck = new Regex(@"(.)(\1){2,}");
			if (repeatingCheck.IsMatch(password))
				return false;

			return true;
		}

		/// <summary>
		/// 返回最少的修改次数，以便断开一个连续的相同字符串
		/// </summary>
		/// <param name="s"></param>
		/// <returns></returns>
		public int ModifyToBreak(string s)
		{
			Regex repeatingCheck = new Regex(@"(.)(\1){2,}");
			MatchCollection matchCollection = repeatingCheck.Matches(s);
			if (matchCollection.Count == 0)
				return 0;
			else
			{
				return (from Match m in matchCollection
						select m.Value.Length / 3).Sum();
			}
		}

	}
}
