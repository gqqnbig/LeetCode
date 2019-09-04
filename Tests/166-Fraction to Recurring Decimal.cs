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

			//令运行时间从104ms减少到84。
			var d = GetCommonDivisor(numerator, denominator);
			numerator /= d;
			denominator /= d;

			const int maxInitialCapacity = 50000;
			var arr = new Dictionary<int, int>(denominator < -maxInitialCapacity || denominator > maxInitialCapacity ? maxInitialCapacity : Math.Abs(denominator));

			if (numerator == int.MinValue)
			{
				if (denominator == 1)
					return numerator.ToString();
				else if (denominator == -1)
					return numerator.ToString().Substring(1);
			}
			var quotient = Math.DivRem(numerator, denominator, out var remainder);
			Debug.Assert(int.MinValue < quotient);
			sb.Append(Math.Abs(quotient));
			numerator = remainder;

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

				//float的精度不足够表示 -2147483648/10。
				//float会计算成         -214748368

				int q= Multiply10ThenDiv(numerator, denominator, out var r);

				Debug.Assert(-10 < q && q < 10);
				Debug.Assert(q == numerator * 10L / denominator);
				sb.Append(Math.Abs(q));
				Debug.Assert(r == numerator * 10L % denominator, "", "{0}/{1}的进位求余出错。", numerator, denominator);
				numerator = r;
				if (denominator > 0)
					Debug.Assert(Math.Abs(numerator) < denominator);
				else
					Debug.Assert(denominator < -Math.Abs(numerator));
				////把整除和求余和为一部的方法为Math.DivRem，但该方法速度还不如用两个原始运算符。
				////见https://github.com/dotnet/coreclr/issues/757
				//long l = Math.Abs(numerator * 10L);
				//sb.Append(Math.Abs(l / denominator));
				//numerator = (int)(l % denominator);
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

		internal static int Multiply10ThenDiv(int numerator, int denominator, out int remainder)
		{
			int a = numerator;
			int additions = 0;
			int q = 0;
			while (additions < 9)
			{
				if (a > 0 && denominator > 0 || a < 0 && denominator < 0)
				{
					a -= denominator;
					q++;
				}

				a += numerator;
				additions++;
			}

			if (a < 0 && denominator > 0 || a > 0 && denominator < 0)
			{
				q--;
				remainder = a + denominator;
			}
			else
				remainder = a;

			return q;
		}
	}
}
