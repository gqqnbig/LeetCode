using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode.Tests
{
	class FractionToRecurringDecimal
	{
		public string FractionToDecimal(int numerator, int denominator)
		{
			if (numerator == 0)
				return "0";

			StringBuilder sb = new StringBuilder();
			if (numerator <= 0 && denominator > 0 || numerator >= 0 && denominator < 0)
				sb.Append("-");

			var d = GetCommonDivisor(numerator, denominator);
			numerator /= d;
			denominator /= d;

			const int maxInitialCapacity = 50000;
			var arr = new Dictionary<int, int>(denominator < -maxInitialCapacity || denominator > maxInitialCapacity ? maxInitialCapacity : Math.Abs(denominator));

			long quotient = Math.DivRem(Math.Abs((long)numerator), Math.Abs((long)denominator), out var remainder);
			sb.Append(quotient);
			numerator = (int)remainder;

			//进入小数部分
			if (numerator != 0)
				sb.Append(".");
			//denominator = Math.Abs(denominator);
			Debug.Assert(numerator < int.MaxValue);
			numerator = Math.Abs(numerator);

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

				long l = Math.Abs(numerator * 10L);
				sb.Append(Math.Abs(l / denominator));
				numerator = (int)(l % denominator);
			}

			return sb.ToString();

		}

		/// <summary>
		/// 返回值总是为正。
		/// </summary>
		/// <param name="num1"></param>
		/// <param name="num2"></param>
		/// <returns></returns>
		/// <remarks>程序应尽量返回正值，以符合常识。
		/// 但有一个特殊情况，如果输入值是<see cref="int.MinValue"/>、<see cref="int.MinValue"/>，且负数范围大于正数，方法返回正值就导致溢出。</remarks>
		internal static int GetCommonDivisor(long num1, long num2)
		{
			//辗转相除法
			long remainder = num2;
			while (num1 % num2 != 0)
			{
				remainder = num1 % num2;
				num1 = num2;
				num2 = remainder;
			}
			return (int)Math.Abs(remainder);
		}
	}
}
