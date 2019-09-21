using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LeetCode.Tests
{
	class WildcardMatching
	{
		public bool IsMatch(string s, string pattern)
		{
			string p = pattern;
			do
			{
				pattern = p;
				p = pattern.Replace("**", "*");
			} while (p != pattern);


			string regPattern = p.Replace("?", "[a-z]").Replace("*", "[a-z]*");
			Regex regex = new Regex("^" + regPattern + "$",RegexOptions.Singleline);
			return regex.IsMatch(s);
		}
	}
}
