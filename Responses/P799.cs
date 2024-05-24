using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Numerics;

namespace LeetCode
{
	class Cup
	{
		/// <summary>
		/// cups dominating the target cup
		/// </summary>
		static Cup[] cups;

		/// <summary>
		/// This is the main direction.
		/// </summary>
		static int nwse;
		static int nesw;

		public static bool compactPrinting = false;

		/// <summary>
		/// Initialize this number of cups.
		/// </summary>
		/// <param name="count"></param>
		public static void InitCups(int targetRow, int targetGlass)
		{
			nwse = targetGlass + 1;
			nesw = targetRow - targetGlass + 1;
			cups = new Cup[nwse * nesw];

			for (int i = 0; i < cups.Length; i++)
			{
				int step = Math.DivRem(i, nwse, out int remain);
				cups[i] = new Cup(step + remain, remain);
			}
		}

		public static Cup GetCup(int row, int glass)
		{
			int index = GetIndex(row, glass);
			if (index == -1)
				return null;

			return cups[index];

		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="row">0-based</param>
		/// <param name="glass">0-based</param>
		/// <returns>0-based. -1 means out of range</returns>
		public static int GetIndex(int row, int glass)
		{
			if (glass >= Cup.nwse)
				return -1;
			int index = (row - glass) * Cup.nwse + glass;
			if (index >= cups.Length)
				return -1;
			return index;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="index">0-based</param>
		/// <returns>0-based row and glass</returns>
		public static int[] GetLocation(int index)
		{
			int step = Math.DivRem(index, nwse, out int remain);

			var res = new int[] { step + remain, remain };


			Debug.Assert(GetIndex(res[0], res[1]) == index);
			return res;
		}

		public static void PrintStack()
		{
			string wastingStr = "({0:f3}) ";
			string regularStr = "[{0:f3}] ";
			string irrelevantStr = "[  x  ] ";
			int cupWidth = 8;
			if (compactPrinting)
			{
				wastingStr = "W ";
				regularStr = "R ";
				irrelevantStr = "  ";
				cupWidth = 2;
			}


			int maxRowIndex = GetLocation(cups.Length - 1)[0];

			for (int r = 0; r <= maxRowIndex; r++)
			{
				Console.Write("r{0:d2} ", r);
				Console.Write(new string(' ', Math.Abs(r + 1 - nesw) * cupWidth / 2));

				for (int j = 0; j <= r; j++)
				{
					int i = GetIndex(r, j);
					if (i != -1 && cups[i] != null)
					{
						if (cups[i].IsWasting)
							Console.Write(wastingStr, cups[i].Load);
						else
							Console.Write(regularStr, cups[i].Load);
					}
					else
					{
						//Console.Write(irrelevantStr);
					}
					//if (i >= cups.Length)
					//	goto outside;
				}
				Console.Write("\n");
			}
			//outside:
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

		/// <summary>
		/// If all dessendants are full, adding waster to this cup is wasting.
		/// </summary>
		public bool IsWasting { get; private set; }

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

		/// <summary>
		/// 
		/// </summary>
		/// <param name="w"></param>
		public void AddWater(double w)
		{
			if (IsWasting)
			{
				Console.WriteLine("All descendants of C_{0},{1} are full. Water {2} wasted.", Row, Glass, w);
				return;
			}

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
				bool wl;
				if (l != null)
				{
					l.AddWater(w / 2);
					wl = l.IsWasting;
				}
				else
				{
					Console.WriteLine("water {0} wasted on the left.", w / 2);
					wl = true;
				}


				var r = GetRight();
				bool wr;
				if (r != null)
				{
					r.AddWater(w / 2);
					wr = r.IsWasting;
				}
				else
				{
					Console.WriteLine("water {0} wasted on the right.", w / 2);
					wr = true;
				}
				IsWasting = wl && wr;
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


			Cup.InitCups(query_row, query_glass);
			//if (query_row > 50 || query_glass > 50)
			//	Cup.compactPrinting = true;

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
			//Console.WriteLine(p.ChampagneTower(5, 2, 1));
			//Console.WriteLine(p.ChampagneTower(0.1, 6, 1));
			//Console.WriteLine(p.ChampagneTower(1, 6, 1));
			//Console.WriteLine(p.ChampagneTower(25, 6, 1));

			//Console.WriteLine(p.ChampagneTower(1, 1, 1) == 0);
			//Console.WriteLine(p.ChampagneTower(2, 1, 1) == 0.5);

			//Console.WriteLine(p.ChampagneTower(100000009, 26, 17));
			//Console.WriteLine(p.ChampagneTower(100000009, 33, 17));
			Console.WriteLine(p.ChampagneTower(1000000000, 99, 50));




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
