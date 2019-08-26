using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LeetCode
{
	public class StringToInteger
	{
		public int MyAtoi(string str)
		{
			str = str.Trim(' ');

			// "-.1" -> 0
			if (Regex.IsMatch(str, @"^(\+|-)?\.\d+", RegexOptions.IgnoreCase))
				return 0;


			Regex numberReg = new Regex(@"^(\+|-)?\d+", RegexOptions.IgnoreCase);
			Match m = numberReg.Match(str);
			if (m.Success == false)
				return 0;
			else if (int.TryParse(m.Value, out var i))
				return i;
			else
			{
				if (m.Value[0] == '-')
					return (int)(-Math.Pow(2, 31));
				else
					return (int)(Math.Pow(2, 31) - 1);
			}

		}
	}
}
