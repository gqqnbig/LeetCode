using System.Collections.Generic;
using Xunit;
using Xunit.Sdk;

namespace LeetCode.Tests
{
	public class Amazon2
	{
		public int removeObstacle(int[,] lot)
		{
			return removeObstacle(lot.GetLength(0), lot.GetLength(1), lot);
		}

		public int removeObstacle(int numRows, int numColumns, int[,] lot)
		{
			//Debug.Assert(lot.Cast<int>().Count(v => v == 9) == 1, "There must be only 1 obstacle.");

			Queue<Problem> queue = new Queue<Problem>();
			queue.Enqueue(new Problem(new Point(0, 0), 0, new Point(-1, -1)));
			int[,] cache = new int[lot.GetLength(0), lot.GetLength(1)];
			while (queue.Count > 0)
			{
				var p = queue.Dequeue();
				if (lot[p.Location.Y, p.Location.X] == 9) //cleaned the obstacle
					return p.UsedStep;

				var location = p.Location;
				var avoid = p.Avoid;
				if (cache[location.Y, location.X] != 0 && cache[location.Y, location.X] < p.UsedStep)
					continue;
				else
					cache[location.Y, location.X] = p.UsedStep;

				if (location.X + 1 < lot.GetLength(1) && location.X + 1 != avoid.X && lot[location.Y, location.X + 1] != 0)
					queue.Enqueue(new Problem(new Point(location.X + 1, location.Y), p.UsedStep + 1, location));

				//move left
				if (location.X - 1 >= 0 && location.X - 1 != avoid.X && lot[location.Y, location.X - 1] != 0)
					queue.Enqueue(new Problem(new Point(location.X - 1, location.Y), p.UsedStep + 1, location));

				//move down
				if (location.Y + 1 < lot.GetLength(0) && location.Y + 1 != avoid.Y && lot[location.Y + 1, location.X] != 0)
					queue.Enqueue(new Problem(new Point(location.X, location.Y + 1), p.UsedStep + 1, location));

				//move up
				if (location.Y - 1 >= 0 && location.Y - 1 != avoid.Y && lot[location.Y - 1, location.X] != 0)
					queue.Enqueue(new Problem(new Point(location.X, location.Y - 1), p.UsedStep + 1, location));
			}

			return -1;
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
			public Problem(Point location, int usedStep, Point avoid)
			{
				Location = location;
				UsedStep = usedStep;
				Avoid = avoid;
			}
			public Point Location { get; }

			/// <summary>
			/// The number of steps used to reach <see cref="Location"/>.
			/// </summary>
			public int UsedStep { get; }

			public Point Avoid { get; }

		}


		[Fact]
		public void removeObstacleTest()
		{
			var data = new int[3, 3]
			{
				{1, 0, 0},
				{1, 0, 0},
				{1, 9, 1}
			};

			Assert.Equal(3, new Amazon2().removeObstacle(data));
		}

		[Fact]
		public void removeObstacleTest2()
		{
			var data = new int[,]
			{
				{1,0,1, 0, 0},
				{1,1,0, 0, 0},
				{0,1,1, 9, 1}
			};

			Assert.Equal(5, new Amazon2().removeObstacle(data));
		}

		//[DataTestMethod]
		//[InlineData(new int[3, 3]
		//{
		//	{1, 0, 0},
		//	{1, 0, 0},
		//	{1, 9, 1}
		//})]
		//public void removeObstacleTest2(int[,] lot)
		//{
		//	var data = 

		//	Assert.Equal(3, new Amazon2().removeObstacle(lot));
		//}
	}
}
