using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace LeetCode
{
	class Stack
	{
		/// <summary>
		/// cups dominating the target cup
		/// </summary>
		Cup[,] cups;

		/// <summary>
		/// length. This is the main direction, north west to south east.
		/// </summary>
		int mainDiagonal;
		/// <summary>
		/// length
		/// </summary>
		int antiDiagonal;

		public bool compactPrinting = false;

		public Stack(int targetRow, int targetGlass)
		{
			mainDiagonal = targetGlass + 1;
			antiDiagonal = targetRow - targetGlass + 1;
			cups = new Cup[antiDiagonal, mainDiagonal];

			for (int i = 0; i < antiDiagonal; i++)
			{
				for (int j = 0; j < mainDiagonal; j++)
				{
					cups[i, j] = new Cup(i + j, j);
				}
			}
		}


		public Cup GetCup(int row, int glass)
		{
			if (row - glass >= antiDiagonal || glass >= mainDiagonal)
				return null;

			return cups[row - glass, glass];

		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="index">0-based</param>
		/// <returns>0-based row and glass</returns>
		public int[] GetLocation(int index)
		{
			int step = Math.DivRem(index, mainDiagonal, out int remain);

			var res = new int[] { step + remain, remain };


			return res;
		}



		/// <summary>
		/// Optionally highlight a cup
		/// </summary>
		/// <param name="highlightRow"></param>
		/// <param name="highlightGlass"></param>
		public void PrintStack(int highlightRow = -1, int highlightGlass = -1)
		{
			//return;
			//string wastingStr = "({0:f3}) ";
			string regularStr = "[{0:f3}] ";
			//string irrelevantStr = "[  x  ] ";
			int cupWidth = 8;
			if (compactPrinting)
			{
				//wastingStr = "W ";
				regularStr = "R ";
				//irrelevantStr = "  ";
				cupWidth = 2;
			}


			int maxRowIndex = mainDiagonal + antiDiagonal - 2;

			for (int r = 0; r <= maxRowIndex; r++)
			{
				if (r % 2 == 0)
					Console.BackgroundColor = ConsoleColor.Black;
				else
					Console.BackgroundColor = ConsoleColor.DarkGray;

				Console.Write("r{0:d2} ", r);
				Console.Write(new string(' ', Math.Abs(r + 1 - antiDiagonal) * cupWidth / 2));


				List<Cup> cupsOnRow = new List<Cup>();
				for (int j = 0; j <= r; j++)
				{
					var c = GetCup(r, j);
					if (c != null)
						cupsOnRow.Add(c);
				}

				if (cupsOnRow.TrueForAll(c => c.Load == 0))
					Console.Write("...");
				else
				{
					cupsOnRow.ForEach(c =>
					{
						if (c.Row == highlightRow && c.Glass == highlightGlass)
							Console.ForegroundColor = ConsoleColor.Red;

						Console.Write(regularStr, c.Load);
						Console.ForegroundColor = ConsoleColor.White;
					});
				}
				Console.BackgroundColor = ConsoleColor.Black;
				Console.Write("\n");
			}
			Console.BackgroundColor = ConsoleColor.Black;
			Console.Write("\n");
		}


		private static IEnumerable<HashSet<T>> Pick<T>(IEnumerable<T> items, int count)
		{
			int i = 0;
			foreach (var item in items)
			{
				if (count == 1)
				{
					var c = new HashSet<T>();
					c.Add(item);
					yield return c;
				}
				else
				{
					foreach (var result in Pick(items.Skip(i + 1), count - 1))
					{
						result.Add(item);
						yield return result;
					}
				}

				++i;
			}
		}

		public static IEnumerable<HashSet<T>> Pick<T>(HashSet<T> items, int count)
		{
			return Pick<T>(items.ToList(), count);
		}


		Cup GetRight(Cup c)
		{
			return GetCup(c.Row + 1, c.Glass + 1);
		}

		Cup GetLeft(Cup c)
		{
			return GetCup(c.Row + 1, c.Glass);
		}

		double AddWaterDownPath(double water, HashSet<int> rights)
		{
			double wasterToOtherPath = 0;
			Cup root = GetCup(0, 0);
			Cup c = root;
			for (int step = 0; step <= antiDiagonal + mainDiagonal - 2; step++)
			{

				if (1 - c.Load <= water)
				{
					if (c.Load < 1)
					{
						double loadBefore = c.Load;
						double waterBefore = water;

						water -= 1 - c.Load;
						c.Load = 1;

						Console.Write("Cup_{0},{1} Load = {2} -> {3}.", c.Row, c.Glass, loadBefore, c.Load);
						Console.WriteLine(" Water = {0} -> {1}", waterBefore, water);

						PrintStack(c.Row, c.Glass);
					}
					else
						Console.WriteLine("{0} is already full.", c);


					water /= 2;
					var lCup = GetLeft(c);
					if (lCup == null)
						Console.WriteLine("water {0} wasted on the left.", water);


					var rCup = GetRight(c);
					if (rCup == null)
						Console.WriteLine("water {0} wasted on the right.", water);


					if (rights.Contains(step))
					{
						//Move right
						if (lCup != null)
							wasterToOtherPath += water;

						Debug.Assert(rCup != null);
						c = rCup;
					}
					else
					{
						//Move left
						if (rCup != null)
							wasterToOtherPath += water;

						Debug.Assert(lCup != null);
						c = lCup;
					}
				}
				else
				{
					double l = c.Load;
					c.Load += water;
					Console.WriteLine("Cup_{0},{1} Load = {2} -> {3}. Water = 0", c.Row, c.Glass, l, c.Load);

					return wasterToOtherPath;
				}
			}
			return wasterToOtherPath;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="water"></param>
		public void AddWater(double water)
		{
			//generate paths
			var total = NumberService.Binomial(antiDiagonal + mainDiagonal - 2, mainDiagonal - 1);

			while (true)
			{
				Console.WriteLine("Water is {1:f3}. We are about to loop {0} times.", total, water);
				//if water is not enough to fill the path or reach the bottom, stop looping.
				foreach (var rights in Pick(Enumerable.Range(0, antiDiagonal + mainDiagonal - 2), mainDiagonal - 1))
				{
					water = AddWaterDownPath(water, rights);
					Cup target = cups[antiDiagonal - 1, mainDiagonal - 1];
					if (target.Load >= 1)
						return;
					if (water <= 0)
						return;
				}
			}
		}

	}

	class Cup
	{

		public int Row { get; private set; }
		public int Glass { get; private set; }

		///// <summary>
		///// If all dessendants are full, adding waster to this cup is wasting.
		///// </summary>
		//public bool IsWasting { get; private set; }

		public double Load { get; set; }

		//public double Capacity { get; private set; }


		public Cup(int row, int glass)
		{
			if (row < 0 || glass < 0)
				throw new ArgumentException($"{nameof(row)} and {nameof(glass)} cannot be negative.");
			if (glass > row)
				throw new ArgumentException($"Row {row} has {row + 1} glasses. Glass index {glass} doesn't exist.");

			Row = row;
			Glass = glass;

		}

		//private Cup GetLeft()
		//{
		//	int row = Row + 1;
		//	int glass = Glass;
		//	return Cup.GetCup(row, glass);
		//}

		//public Cup GetRight()
		//{
		//	int row = Row + 1;
		//	int glass = Glass + 1;
		//	return Cup.GetCup(row, glass);
		//}


		public override string ToString()
		{
			return $"Cup_{Row},{Glass} = {Load}";
		}

	}

	class P799
	{

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
				Console.WriteLine("However, this only talks about cups at the edge. Cups in the center have multiple paths to be filled.");


			Stack stack = new Stack(query_row, query_glass);
			//if (query_row > 50 || query_glass > 50)
			//	Cup.compactPrinting = true;
			stack.AddWater(poured);

			//Debug.Assert(Cup.GetCup(query_row, query_glass) != null);
			//Cup root = Cup.GetCup(0, 0);
			//Cup.PrintStack(0, 0);

			//root.AddWater(poured);

			Cup targetCup = stack.GetCup(query_row, query_glass);
			return targetCup.Load;

		}

		public static void Main(string[] args)
		{
			var res = Stack.Pick(new HashSet<int>(new int[] { 1, 2, 3, 4, 5 }), 3);
			foreach (var path in res)
			{
				Console.WriteLine(string.Join(", ", path));
			}






			var p = new P799();
			//Console.WriteLine(p.ChampagneTower(5, 2, 1));
			//Console.WriteLine(p.ChampagneTower(0.1, 6, 1));
			//Console.WriteLine(p.ChampagneTower(1, 6, 1));
			Console.WriteLine(p.ChampagneTower(25, 6, 1));

			//Console.WriteLine(p.ChampagneTower(1, 1, 1) == 0);
			//Console.WriteLine(p.ChampagneTower(2, 1, 1) == 0.5);

			//Console.WriteLine(p.ChampagneTower(100000009, 26, 17));
			//Console.WriteLine(p.ChampagneTower(100000009, 33, 17));
			//Console.WriteLine(p.ChampagneTower(1000000, 50, 25));




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
