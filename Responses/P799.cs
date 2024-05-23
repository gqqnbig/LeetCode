﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Numerics;

namespace LeetCode
{
	class Cup
	{
		static Cup[] cups;

		/// <summary>
		/// Initialize this number of cups.
		/// </summary>
		/// <param name="count"></param>
		public static void InitCups(int count)
		{
			cups = new Cup[count];
		}

		public static Cup GetCup(int row, int glass)
		{
			int index = GetIndex(row, glass);
			if (index >= cups.Length)
				return null;

			if (cups[index] == null)
			{
				cups[index] = new Cup(row, glass);

				//Console.WriteLine("Cup stack updated");
				//PrintStack();
			}

			return cups[index];

		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="row">0-based</param>
		/// <param name="glass">0-based</param>
		/// <returns>0-based</returns>
		public static int GetIndex(int row, int glass)
		{
			return (1 + row) * row / 2 + glass;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="index">0-based</param>
		/// <returns>0-based row and glass</returns>
		public static int[] GetLocation(int index)
		{
			if (index == 0)
				return new int[] { 0, 0 };

			int row = (int)(Math.Ceiling(Math.Sqrt(2 * index))) - 1;
			int glass = index - ((1 + row) * row / 2);
			if (glass < 0)
			{
				glass += row;
				row--;
			}

			Debug.Assert(GetIndex(row, glass) == index);
			return new int[] { row, glass };
		}

		public static void PrintStack()
		{
			int rowIndex = GetLocation(cups.Length - 1)[0];

			int i = 0;
			for (int r = 0; r <= rowIndex; r++)
			{
				Console.Write("r{0:d2} " + new string(' ', (rowIndex - r) * 8 / 2), r);
				for (int j = 0; j <= r; j++)
				{
					if (cups[i] != null)
						Console.Write("[{0:f3}] ", cups[i].Load);
					else
						Console.Write("[     ] ");
					i++;
					if (i >= cups.Length)
						goto outside;
				}
				Console.Write("\n");
			}
		outside:
			Console.Write("\n");
		}


		public int Row { get; private set; }
		public int Glass { get; private set; }
		//public int Index
		//{
		//	get
		//	{
		//		return (1 + Row) * Row / 2 + Glass + 1;
		//	}
		//}

		public double Load { get; private set; }

		private Cup(int row, int glass)
		{
			if (row < 0 || glass < 0)
				throw new ArgumentException($"{nameof(row)} and {nameof(glass)} cannot be negative.");
			if (glass > row)
				throw new ArgumentException($"Row {row} has {row + 1} glasses. Glass index {glass} doesn't exist.");

			Row = row;
			Glass = glass;
		}

		private Cup GetLeft()
		{
			int row = Row + 1;
			int glass = Glass;
			return Cup.GetCup(row, glass);
		}

		public Cup GetRight()
		{
			int row = Row + 1;
			int glass = Glass + 1;
			return Cup.GetCup(row, glass);
		}

		public void AddWater(double w)
		{
			if (1 - Load <= w)
			{
				if (Load < 1)
				{
					w -= 1 - Load;
					Load = 1;

					Console.WriteLine(this);
					Cup.PrintStack();
				}
				else
					Console.WriteLine("{0} is already full.", this);
				var l = GetLeft();
				if (l != null)
					l.AddWater(w / 2);
				else
					Console.WriteLine("water wasted " + (w / 2));


				var r = GetRight();
				if (r != null)
					r.AddWater(w / 2);
				else
					Console.WriteLine("water wasted " + (w / 2));
			}
			else
			{
				Load += w;
				Console.WriteLine(this);
				Cup.PrintStack();
			}
		}

		public override string ToString()
		{
			return $"Cup_{Row},{Glass} = {Load}";
		}

	}

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
			return ChampagneTower((double)poured, query_row, query_glass);
		}

		public double ChampagneTower(double poured, int query_row, int query_glass)
		{
			//if (poured == 0)
			//	return 0;
			int levels = (int)(Math.Ceiling(Math.Log(poured + 1, 2)) - 1);
			Console.WriteLine($"{poured} glasses of water fills up {levels} levels. That is, glasses of row index {levels - 1} potentially have water.");

			if (query_row > levels)
				return 0;


			Cup.InitCups(Cup.GetIndex(query_row, query_glass) + 1);
			Debug.Assert(Cup.GetCup(query_row, query_glass) != null);
			Cup root = Cup.GetCup(0, 0);
			Cup.PrintStack();

			//for (int i = 0; i < poured; i++)
			root.AddWater(poured);

			Cup targetCup = Cup.GetCup(query_row, query_glass);
			return targetCup.Load;

		}

		public static void Main(string[] args)
		{
			//for (int i = 0; i < 10; i++)
			//{
			//	for (int j = 0; j <= i; j++)
			//	{
			//		int index = Cup.GetIndex(i, j);
			//		//Console.WriteLine(index);
			//		int[] lo = Cup.GetLocation(index);
			//		Debug.Assert(lo[0] == i);
			//		Debug.Assert(lo[1] == j);
			//	}
			//}






			var p = new P799();
			//Console.WriteLine(p.ChampagneTower(0.1, 6, 1));
			//Console.WriteLine(p.ChampagneTower(1, 6, 1));
			//Console.WriteLine(p.ChampagneTower(25, 6, 1));

			//Console.WriteLine(p.ChampagneTower(1, 1, 1) == 0);
			//Console.WriteLine(p.ChampagneTower(2, 1, 1) == 0.5);

			Console.WriteLine(p.ChampagneTower(100000009, 26, 17));
			Console.WriteLine(p.ChampagneTower(100000009, 33, 17));




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
