﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpressionToCodeLib;
using LeetCode.Tests;

namespace LeetCode
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var actual = new LongestDuplicateSubstring().LongestDupSubstring(Resource1.P1044LongTestData);
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
