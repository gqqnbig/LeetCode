using System;
using System.Collections.Generic;
using System.Text;

namespace Responses
{
	class P509
	{
		public int Fib(int n)
		{
			double sq5 = Math.Sqrt(5);

			double term1 = 1 / sq5 * Math.Pow((1 + sq5) / 2, n);
			double term2 = -1 / sq5 * Math.Pow((1 - sq5) / 2, n);

			return (int)Math.Round(term1 + term2);
		}


		public static void Main(string[] args)
		{
			var p = new P509();
			Console.WriteLine(p.Fib(1));
			Console.WriteLine(p.Fib(2));
			Console.WriteLine(p.Fib(3));
			Console.WriteLine(p.Fib(4));
			Console.WriteLine(p.Fib(5));


		}
	}
}
