using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{
	public class MaxPointsOnALine
	{
		public int MaxPoints(int[][] points)
		{
			if (points.Length == 0)
				return 0;

			int max = 0;
			for (int i = 0; i < points.Length; i++)
			{
				int[] p1 = points[i];
				int samePointCount = 0;
				Dictionary<double, int> dic = new Dictionary<double, int>(points.Length);
				for (int j = i + 1; j < points.Length; j++)
				{
					int[] p2 = points[j];
					//如果p2等于p1，求斜率会得出NaN。
					if (p1[0] == p2[0] && p1[1] == p2[1])
					{
						samePointCount++;
						continue;
					}

					//求斜率
					double k = ((double)p1[0] - p2[0]) / ((double)p1[1] - p2[1]);

					Debug.Assert(double.IsNaN(k) == false);
					if (double.IsNegativeInfinity(k))
						k = double.PositiveInfinity;
					if (dic.ContainsKey(k))
						dic[k] += 1;
					else
						dic[k] = 1;
				}

				int localMax = samePointCount;
				if (dic.Values.Count > 0)
					localMax += dic.Values.Max();

				max = Math.Max(max, localMax);
			}

			return max + 1;
		}

	}
}
