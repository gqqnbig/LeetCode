using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LeetCode.Tests
{
	[TestClass]
	public class NextJump1
	{
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

	}
}
