using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Numerics;

namespace LeetCode
{
	class P799
	{


		/// <summary>
		/// Choose pick from total
		/// </summary>
		/// <param name="total"></param>
		/// <param name="pick"></param>
		/// <returns></returns>
		public BigInteger Binomial(int total, int pick)
		{
			// Binomial[100,50] has 30 digits. Even the data type long can't hold it.


			if (pick > total / 2)
				pick = total - pick;

			BigInteger numerator = 1;
			for (int i = 0; i < pick; i++)
				numerator *= total - i;

			BigInteger denominator = 1;
			for (int i = 1; i <= pick; i++)
				denominator *= i;

			return numerator / denominator;
		}

		double GetIncome(int query_row, int query_glass)
		{
			const int precision = 5;

			var b = Binomial(query_row, query_glass) * (int)Math.Pow(10, precision);
			for (int i = 0; i < query_row; i++)
				b = b / 2;

			double income = (double)b / Math.Pow(10, precision);
			return income;
		}

		public double ChampagneTower(int poured, int query_row, int query_glass)
		{
			Debug.Assert(query_glass <= query_row, "n-th row at most has n glasses.");

			// Minimal number of cups to fill
			double minWater = 0;
			for (int d = 1; query_row - d >= 0; d++)
			{
				int start = Math.Max(query_glass - d, 0);
				int end = Math.Min(query_row - d, query_glass);
				//int count = end - start + 1;
				//minWater += count;

				Console.WriteLine("The {0} row has {1} cups affecting the target cup.", query_row - d, end - start + 1);

				for (; start <= end; start++)
				{
					double inc = GetIncome(query_row - d, start);
					double count = 1 / inc;
					Console.WriteLine(count);
					minWater += count;
				}

			}

			Console.WriteLine("For C_{0},{1} to have water, {2} glass of water must be poured.", query_row, query_glass, minWater);
			if (poured < minWater)
				return 0;

			// Next we calculate 1 / NumberService.Power(2, query_row) * Binomial(query_row, query_glass)
			// But we have to use precision and big number to work it around.

			double income = GetIncome(query_row, query_glass);

			Console.WriteLine("I_{0},{1} = B({0},{1}) / 2^{0} * I_0,0 = {2} * I_{0,0}", query_row, query_glass, income);

			double load = income * (poured - minWater);
			if (load > 1)
				return 1;
			else
				return load;
		}

		public static void Main(string[] args)
		{
			var p = new P799();
			//Console.WriteLine(p.ChampagneTower(1, 1, 1) == 0);
			//Console.WriteLine(p.ChampagneTower(2, 1, 1) == 0.5);

			//Console.WriteLine(p.ChampagneTower(100000009, 33, 17));

			Console.WriteLine(p.ChampagneTower(25, 6, 1));



			//Console.WriteLine(p.Binomial(5, 0));
			//Console.WriteLine(p.Binomial(5, 1));
			//Console.WriteLine(p.Binomial(5, 2));
			//Console.WriteLine(p.Binomial(5, 3));


			//Console.WriteLine(p.Binomial(10, 0));
			//Console.WriteLine(p.Binomial(10, 3));
			//Console.WriteLine(p.Binomial(10, 6));
			//Console.WriteLine(p.Binomial(10, 10));

			//Console.WriteLine(p.Binomial(20, 10));
			//Console.WriteLine(p.Binomial(100, 50));

			//Console.WriteLine(p.Binomial(50, 1));
			//Console.WriteLine(p.Binomial(50, 2));
			//Console.WriteLine(p.Binomial(50, 3));

		}
	}
}
