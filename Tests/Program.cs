using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeetCode.Tests;

namespace LeetCode
{
	public class Program
	{
		public static void Main(string[] args)
		{
			//var solution = new CPrimePalindrome();
			//for (int i = 0; i < 50; i++)
			//{
			//	Console.WriteLine("{0}{1} prime.", i, solution.IsPrime(i) == 0 ? " is" : " is not");
			//}

			Console.ReadKey();
		}



		static bool IsPalindromic(string n)
		{
			for (int i = 0; i < n.Length / 2; i++)
			{
				if (n[i] != n[n.Length - 1 - i])
					return false;
			}

			return true;
		}

	}
}
