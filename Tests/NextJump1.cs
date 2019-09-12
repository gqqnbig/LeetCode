using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LeetCode.Tests
{
	[TestClass]
	public class NextJump1
	{
		public static List<int> meanderingArray(List<int> unsorted)
		{
			var desending = new List<int>(unsorted);
			unsorted.Sort((a, b) => b.CompareTo(a));
			var ascending = unsorted;
			desending.Sort((a, b) => a.CompareTo(b));

			var result = new List<int>(unsorted.Count);
			for (int i = 0; i < unsorted.Count; i++)
			{
				if (i % 2 == 1)
					result.Add(ascending[i]);
				else
					result.Add(desending[i]);
			}

			return result;
		}


		[DataTestMethod]
		[DataRow(new[] { 1, 3, 5, 2, 6, 7, 0 })]
		public void meanderingArrayTest(int[] input)
		{
			meanderingArray(new List<int>(input));
		}



		/*
		 * Complete the 'userDataWrong' function below.
		 *
		 * The function is expected to return a STRING_ARRAY.
		 * The function accepts 2D_STRING_ARRAY userInfo as parameter.
		 */

		public static List<string> userDataWrong(List<List<string>> userInfo)
		{
			int totalCount = userInfo.Count;

			var riskUserIds = (from u in userInfo
							   where u[1] == "-1" //has risk
							   select u[0]).ToList();

			int riskyUserCount = riskUserIds.Count;


			List<string> result = new List<string>(1 + 1 + riskyUserCount);
			result.Add(totalCount.ToString());
			result.Add(riskyUserCount.ToString());
			result.AddRange(riskUserIds);

			return result;
		}

		/// <summary>
		/// Complete the 'maxMoves' function below.
		///
		/// The function is expected to return an INTEGER.
		/// The function accepts following parameters:
		///  1. STRING s
		///  2. STRING t
		/// </summary>
		/// <param name="s"></param>
		/// <param name="t">a short string. We need to remove t from s.</param>
		/// <returns></returns>
		public static int maxMoves(string s, string t)
		{
			return maxMoves(s, t, 0, new Dictionary<string, int>());
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="s"></param>
		/// <param name="t"></param>
		/// <param name="usedSteps"></param>
		/// <param name="maxLeftSteps">Key is s, value is the maximum number of steps used to achieve the string.</param>
		/// <returns></returns>
		public static int maxMoves(string s, string t, int usedSteps, Dictionary<string, int> maxLeftSteps)
		{
			if (maxLeftSteps.TryGetValue(s, out var n))
				return n;

			var index1 = s.IndexOf(t);
			if (index1 == -1)
			{
				maxLeftSteps[s] = 0;
				return 0;
			}

			//remove the first occurrence
			var s1 = s.Substring(0, index1) + s.Substring(index1 + t.Length);
			var steps1 = maxMoves(s1, t, usedSteps + 1, maxLeftSteps) + 1;


			var index2 = s.LastIndexOf(t);
			if (index1 == index2) //may not need this if we have cache.
				return steps1;

			var s2 = s.Substring(0, index2) + s.Substring(index2 + t.Length);
			if (s1 == s2)  //may not need this if we have cache.
				return steps1;
			var steps2 = maxMoves(s2, t, usedSteps + 1, maxLeftSteps) + 1;
			var max = steps1 > steps2 ? steps1 : steps2;

			maxLeftSteps[s] = max;
			return max;
		}

		[DataTestMethod]
		[DataRow(3, "bcbbc", "b")]
		[DataRow(2, "aabb", "ab")]
		[DataRow(1, "aabcbdc", "abc")]
		[DataRow(0, "abab", "aa")]
		public void maxMovesTest(int expected, string s, string t)
		{
			Assert.AreEqual(expected, maxMoves(s, t));
		}

		public void maxMovesSpeedTest()
		{

		}


		[TestMethod]
		public void maxMovesGenerationTest()
		{
			int length = 1000;
			StringBuilder sb = new StringBuilder();
			var letters = new[] { 'a', 'b' };
			Random rand = new Random();
			for (int i = 0; i < length; i++)
				sb.Append(letters[rand.Next(letters.Length)]);

			var needleLength = rand.Next(5);
			var t = "";
			for (int i = 0; i < needleLength; i++)
				t += letters[rand.Next(letters.Length)];

			var s = sb.ToString();
			Console.WriteLine($"s= {s}");
			Console.WriteLine($"t= {t}");
			Stopwatch sw = Stopwatch.StartNew();

			var actual = maxMoves(s, t);
			sw.Stop();
			Console.WriteLine($"{sw.ElapsedMilliseconds}ms");
			Console.WriteLine(actual);

		}

	}
}
