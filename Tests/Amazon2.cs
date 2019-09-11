using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xunit.Sdk;

namespace LeetCode.Tests
{
	[TestClass]
	public class Amazon2
	{
		public int removeObstacle(int[,] lot)
		{
			return removeObstacle(lot.GetLength(0), lot.GetLength(1), lot);
		}

		public int removeObstacle(int numRows, int numColumns, int[,] lot)
		{
			Debug.Assert(lot.Cast<int>().Count(v => v == 9) == 1, "There must be only 1 obstacle.");
			var r = removeObstacle(new Point(0, 0), lot, new Point(-1, -1), new int[lot.GetLength(0), lot.GetLength(1)]);
			if (r == int.MaxValue)
				return -1;
			else
				return r;
		}

		int removeObstacle(Point location, int[,] lot, Point avoid, int[,] cache)
		{
			if (lot[location.Y, location.X] == 9) //cleaned the obstacle
				return 0;
			else if (lot[location.Y, location.X] == 0)
				throw new InvalidOperationException("Robot should never be moved to trenches.");

			if (cache[location.Y, location.X] != 0)
				return cache[location.Y, location.X];

			int minStep = int.MaxValue;
			//move right
			if (location.X + 1 < lot.GetLength(1) && location.X + 1 != avoid.X && lot[location.Y, location.X + 1] != 0)
			{
				var s = removeObstacle(new Point(location.X + 1, location.Y), lot, location, cache);
				minStep = Math.Min(minStep, s);
			}

			//move left
			if (location.X - 1 >= 0 && location.X - 1 != avoid.X && lot[location.Y, location.X - 1] != 0)
			{
				var s = removeObstacle(new Point(location.X - 1, location.Y), lot, location, cache);
				minStep = Math.Min(minStep, s);
			}

			//move down
			if (location.Y + 1 < lot.GetLength(0) && location.Y + 1 != avoid.Y && lot[location.Y + 1, location.X] != 0)
			{
				var s = removeObstacle(new Point(location.X, location.Y + 1), lot, location, cache);
				minStep = Math.Min(minStep, s);
			}

			//move up
			if (location.Y - 1 >= 0 && location.Y - 1 != avoid.Y && lot[location.Y - 1, location.X] != 0)
			{
				var s = removeObstacle(new Point(location.X, location.Y - 1), lot, location, cache);
				minStep = Math.Min(minStep, s);
			}

			if (minStep < int.MaxValue)
				minStep++; //Add the step of this method itself.
			cache[location.Y, location.X] = minStep;
			return minStep;
		}

		struct Point
		{
			public Point(int x, int y)
			{
				X = x;
				Y = y;
			}
			public int X { get; }
			public int Y { get; }
		}

		struct Problem
		{
			public Point Location { get; }
			public int UsedStep { get; }

			public Point Avoid { get; }

		}


		[TestMethod]
		public void removeObstacleTest()
		{
			var data = new int[3, 3]
			{
				{1, 0, 0},
				{1, 0, 0},
				{1, 9, 1}
			};

			Assert.AreEqual(3, new Amazon2().removeObstacle(data));
		}

		[TestMethod]
		public void removeObstacleTest2()
		{
			var data = new int[,]
			{
				{1,0,1, 0, 0},
				{1,1,0, 0, 0},
				{0,1,1, 9, 1}
			};

			Assert.AreEqual(5, new Amazon2().removeObstacle(data));
		}

		//[DataTestMethod]
		//[DataRow(new int[3, 3]
		//{
		//	{1, 0, 0},
		//	{1, 0, 0},
		//	{1, 9, 1}
		//})]
		//public void removeObstacleTest2(int[,] lot)
		//{
		//	var data = 

		//	Assert.AreEqual(3, new Amazon2().removeObstacle(lot));
		//}
	}
}
