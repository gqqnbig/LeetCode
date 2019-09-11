using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xunit.Sdk;



namespace LeetCode.Tests
{
	[TestClass]
	public class Amazon
	{
		public int minimumTime(int[] parts)
		{
			return minimumTime(parts.Length, parts);
		}

		public int minimumTime(int numOfParts, int[] parts)
		{
			SortedSet<int> list = new SortedSet<int>();
			for (int i = 0; i < parts.Length; i++)
			{
				list.Add(parts[i]);
			}

			var sum = 0;
			while (list.Count > 1)
			{
				var t1 = list.Min;
				list.Remove(t1);
				var t2 = list.Min;
				list.Remove(t2);

				sum += t1 + t2;
				list.Add(t1 + t2);
			}

			return sum;
		}


		[DataTestMethod]
		[DataRow(53, new[] { 3, 4, 5, 4, 7 })]
		[DataRow(0, new int[0])]
		[DataRow(58, new[] { 8, 4, 6, 12 })]
		public void minimumTimeTest(int expected, int[] parts)
		{
			Assert.AreEqual(expected, new Amazon().minimumTime(parts));
		}
	}
}
