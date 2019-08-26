using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{
	public class DivideTwoIntegers
	{
		public int Divide(int dividend, int divisor)
		{
			//特殊情形
			if (divisor == 1)
				return dividend;
			if (divisor == -1)
				return NegatePossibleOverflow(dividend);


			//负数部分范围比较大，所以除数和被除数都转到负数进行处理
			if (dividend >= 0 && divisor < 0)
				return NegativeDivide(-dividend, divisor);
			else if (dividend >= 0 && divisor > 0)
				return NegatePossibleOverflow(NegativeDivide(-dividend, -divisor));
			else if (dividend < 0 && divisor < 0)
				return NegatePossibleOverflow(NegativeDivide(dividend, divisor));
			else //if (dividend < 0 && divisor > 0)
				return NegativeDivide(dividend, -divisor);
		}

		int NegatePossibleOverflow(int v)
		{
			Debug.Assert(int.MinValue < -int.MaxValue);

			if (v < -int.MaxValue)
				return int.MaxValue;
			return -v;
		}


		/// <summary>
		/// 返回负商
		/// </summary>
		/// <param name="dividend"></param>
		/// <param name="divisor"></param>
		/// <returns></returns>
		public int NegativeDivide(int dividend, int divisor)
		{
			Debug.Assert(dividend <= 0, $"dividend {dividend}应该小于等于0。");
			Debug.Assert(divisor < 0, $"divisor {divisor}应该小于0。");

			//也把商转到负数进行处理
			int quotient = 0;
			while (dividend <= divisor)
			{
				dividend -= divisor;
				quotient--;
			}

			return quotient;
		}
	}
}
