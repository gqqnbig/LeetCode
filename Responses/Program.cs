using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{
	public class Program
	{
		public static void Main(string[] args)
		{
			Console.WriteLine(int.MinValue);
			Console.WriteLine(int.MinValue >> 2);
			int v = -536870912;
			Console.WriteLine(v << 2);
			//Console.WriteLine(int.MinValue >> 2);

			Console.WriteLine("int.MinValue: " + Convert.ToString(int.MinValue, 2));

			Console.WriteLine("int.MinValue >> 1: " + Convert.ToString(int.MinValue >> 1, 2));
			Console.ReadKey();
		}
	}
}
