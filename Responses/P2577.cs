using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace LeetCode
{
	class P2577
	{
		struct Node
		{
			public int X { get; private set; }
			public int Y { get; private set; }

			public Node(int x, int y)
			{
				X = x;
				Y = y;
			}

			public bool IsInRange(int[][] graph)
			{
				if (X < 0 || Y < 0)
					return false;

				return Y < graph.Length && X < graph[Y].Length;
			}

			public override int GetHashCode()
			{
				return HashCode.Combine(X, Y);
			}

			public override bool Equals(object obj)
			{
				if (obj is Node n)
					return X == n.X && Y == n.Y;
				return false;
			}

			public override string ToString()
			{
				return $"Node({X}, {Y})";
			}

		}



		/// <summary>
		/// If quantity is 1, returns the quantity and the unit.
		/// If quantity is more than 1, returns the quantity and the plural form of the unit.
		/// </summary>
		/// <param name="quantity"></param>
		/// <param name="unit"></param>
		/// <param name="units">If omitted, the plural form is <c>unit</c> plus "s".</param>
		/// <returns></returns>
		static string MakeUnitPhrase(int quantity, string unit, string units = null)
		{
			if (quantity == 1)
				return quantity.ToString() + " " + unit;
			else
			{
				if (units == null)
					units = unit + "s";
				return quantity.ToString() + " " + units;
			}

		}

		public int MinimumTime(int[][] graph)
		{
			if (graph == null || graph.Length == 0)
				throw new Exception($"{nameof(graph)} cannot be empty.");
			int width = graph[0].Length;
			Debug.Assert(graph.All(row => row.Length == width), "The input matrix cannot be rugged.");


			Node startingNode = new Node(0, 0);
			if (graph[startingNode.Y][startingNode.X] > 0)
				return -1;
			// As long as any adjacent node to the starting node is 1, we will never return -1
			// because we can go back and forth to increase time.

			//distance from every node to the starting node.
			int[][] timeTo = Enumerable.Range(0, graph.Length).Select(_ => Enumerable.Repeat(int.MaxValue, width).ToArray()).ToArray();
			timeTo[startingNode.Y][startingNode.X] = 0;
			Console.WriteLine("The time to visit {1} from {0} is 0.", startingNode, startingNode);

			Dictionary<Node, Node> links = new Dictionary<Node, Node>();
			HashSet<Node> queue = new HashSet<Node>();
			queue.Add(startingNode);

			List<int[]> directions = new List<int[]> {
					new int[]{ 0, -1},//up
					new int[]{ 0, 1}, // down
					new int[]{ -1, 0}, //left
					new int[]{ 1, 0}, //right			
				};

			while (queue.Count > 0)
			{
				var en = queue.GetEnumerator();
				en.MoveNext();
				Node n = en.Current;
				if (queue.Remove(n) == false)
					throw new NotSupportedException("We must be able to remove element retrieved from a hashset.");
				en.Dispose();

				// check four directions
				foreach (int[] d in directions)
				{
					Node n2 = new Node(n.X + d[0], n.Y + d[1]);
					if (n2.IsInRange(graph) == false)
						continue;

					Debug.Assert(timeTo[n.Y][n.X] < int.MaxValue);
					int nextTick = timeTo[n.Y][n.X] + 1;
					if (nextTick >= graph[n2.Y][n2.X])
					{
						// We can visit n2.

						if (nextTick < timeTo[n2.Y][n2.X])
						{
							timeTo[n2.Y][n2.X] = nextTick;
							links[n2] = n;
							if (queue.Contains(n2) == false)
								queue.Add(n2);

							Console.WriteLine("The distance from {0} to {1}, via {2} is {3}.", startingNode, n2, n, timeTo[n2.Y][n2.X]);
						}
					}
					else if (links.TryGetValue(n, out Node prev))
					{
						// We can perform roundtrip to waste time.

						/* t
						 * [] -> [n]
						 * 
						 * Let n be the time constraint of the target node,
						 * and t+1 < n.
						 * 
						 * If n-(t+1) is even (ie. n-t is odd),
						 * we do (n-(t+1))/2 roundtrips.
						 * 
						 * Therefore, t_n = t + (n-(t+1))/2*2 + 1 = n.
						 * 
						 * If n-(t+1) is odd (ie. n-t is even),
						 * we do (n-(t+1)+1)/2 roundtrips.
						 * 
						 * Therefore, t_n = t+ (n-(t+1)+1)/2*2 + 1 = n+1
						 * 
						 */

						int neededTime = graph[n2.Y][n2.X] - nextTick;
						if (neededTime % 2 == 0)
							nextTick = graph[n2.Y][n2.X] + 1;
						else
							nextTick = graph[n2.Y][n2.X];

						if (nextTick < timeTo[n2.Y][n2.X])
						{
							timeTo[n2.Y][n2.X] = nextTick;
							links[n2] = n;
							if (queue.Contains(n2) == false)
								queue.Add(n2);

							Console.WriteLine($"Perform {MakeUnitPhrase((neededTime + 1) / 2, "roundtrip")} between {n} and {prev}, then go to {n2}. The time is now {nextTick}.");
						}

					}

				}
			}

			if (timeTo[timeTo.Length - 1][width - 1] == int.MaxValue)
				return -1;
			else
				return timeTo[timeTo.Length - 1][width - 1];
		}



		public static void Main(string[] args)
		{
			var p = new P2577();

			int[][] grid = {
				new int[]{0, 1, 3, 2 },
				new int[]{5,1,2,5 },
				new int[]{4,3,8,6 }
			};

			Console.WriteLine(p.MinimumTime(grid) == 7);

			grid = new int[][] {
				new int[]{0, 2, 4 },
				new int[]{3,2,1},
				new int[]{1,0,4 }
			};

			//Console.WriteLine(p.MinimumTime(grid));
		}

	}
}
