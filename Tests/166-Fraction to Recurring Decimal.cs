using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode.Tests
{
	class FractionToRecurringDecimal
	{
		public string FractionToDecimal(int numerator, int denominator)
		{
			var d = GetCommonDivisor(numerator, denominator);
			numerator /= d;
			denominator /= d;

			const int maxInitialCapacity = 50000;
			var arr = new Dictionary<int, int>(denominator > maxInitialCapacity ? maxInitialCapacity : denominator);
			StringBuilder sb = new StringBuilder();

			var quotient = numerator / denominator;
			sb.Append(quotient);
			numerator = numerator % denominator;

			//进入小数部分
			if (numerator != 0)
				sb.Append(".");

			while (numerator != 0)
			{
				if (arr.TryGetValue(numerator, out var index))
				{
					sb.Insert(index, "(");
					sb.Append(")");
					return sb.ToString();
				}
				else
					arr[numerator] = sb.Length;

				long l = numerator * 10L;
				sb.Append(l / denominator);
				numerator = (int)(l % denominator);
			}

			return sb.ToString();

		}

		int GetCommonDivisor(int num1, int num2)
		{
			//辗转相除法
			int remainder = num2;
			while (num1 % num2 > 0)
			{
				remainder = num1 % num2;
				num1 = num2;
				num2 = remainder;
			}
			return remainder;
		}
	}
}
