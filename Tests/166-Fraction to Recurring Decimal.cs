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

			BitArray arr = new BitArray(denominator);
			StringBuilder sb = new StringBuilder();

			var quotient = numerator / denominator;
			sb.Append(quotient);
			numerator = numerator % denominator;

			//进入小数部分
			if (numerator != 0)
				sb.Append(".");

			while (numerator != 0)
			{
				if (arr[numerator])
				{
					sb.Append(")");
					return sb.ToString();
				}
				else
					arr[numerator] = true;

				numerator *= 10;
				sb.Append(numerator / denominator);
				numerator = numerator % denominator;
			}

			return sb.ToString();

		}

		int GetCommonDivisor(int num1, int num2)
		{
			//辗转相除法
			int remainder = num1;
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
