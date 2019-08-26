using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LeetCode
{
	public class ValidNumber
	{
		public bool IsNumberLimitedRange(string s)
		{
			if (int.TryParse(s, out _))
				return true;

			if (double.TryParse(s, out _))
				return true;

			
			return false;
		}

		public bool IsNumber(string s)
		{
			s = s.Trim();
			int p = s.IndexOf('e');
			Regex plainNumberReg=new Regex(@"^(\+|-)?(\.\d+|\d+(\.\d*)?)$");
			if (p == -1)
			{
				return plainNumberReg.IsMatch(s);
			}
			else if (p == s.Length)
				return false;
			else
			{
				string coefficient = s.Substring(0, p);
				if (plainNumberReg.IsMatch(coefficient) == false)
					return false;

				string exponent = s.Substring(p + 1);
				if (Regex.IsMatch(exponent, @"^(\+|-)?\d+$") == false)
					return false;
				return true;
			}
			
		}
	}
}
