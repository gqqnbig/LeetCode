using System;
using System.Collections.Generic;
using System.Text;

namespace LeetCode
{
	class P2144
	{
		public int MinimumCost(int[] cost)
		{
			Array.Sort(cost);


			int bought = 0;
			int spend = 0;
			for (int i = cost.Length - 1; i >= 0; i--)
			{
				if (bought == 2)
				{
					//take this candy for free.
					bought = 0;
				}
				else
				{
					spend += cost[i];
					bought++;
				}
			}

			return spend;
		}

		public static void Main(string[] args)
		{
			var p = new P2144();
			Console.WriteLine(p.MinimumCost(new int[] { 1, 2, 3 }));
			Console.WriteLine(p.MinimumCost(new int[] { 6, 5, 7, 9, 2, 2 }));

		}
	}
}
