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
	public class NonDecreasingArrayTests
	{

		[DataTestMethod]
		[DataRow(true, new int[] { 0 })]
		public void CheckPossibilityTest(bool expected, int[] nums)
		{
			Assert.AreEqual(expected, CheckPossibilityBruteForce(nums), "输入为[{0}]", string.Join(",", nums));
		}

		[TestMethod]
		public void CheckPossibilityGenerationTest()
		{
			Random rand = new Random();
			var solution = new NonDecreasingArray();
			for (int i = 0; i < 10; i++)
			{
				int length = rand.Next(10);
				int[] nums = new int[length];
				for (int j = 0; j < length - 1; j++)
					nums[j] = rand.Next(100);

				var actual = CheckPossibilityBruteForce(nums);
				Console.WriteLine("[{0}]{1}变成非递减序列", string.Join(",", nums), (actual ? "可以" : "不可以"));
			}
		}

		public bool CheckPossibilityBruteForce(int[] nums, int startIndex = 0, bool canChange = true)
		{
			if (startIndex < 0)
				startIndex = 0;
			for (int i = startIndex + 1; i < nums.Length; i++)
			{
				if (nums[i - 1] > nums[i])
				{
					if (canChange == false)
						return false;

					var temp = nums[i - 1];
					nums[i - 1] = nums[i];
					var r = CheckPossibilityBruteForce(nums, i - 2, false);
					nums[i - 1] = temp;
					if (r)
						return true;

					temp = nums[i];
					nums[i] = nums[i - 1];
					r = CheckPossibilityBruteForce(nums, i - 1, false);
					nums[i] = temp;
					if (r)
						return true;

					return false;
				}
			}

			return true;
		}
	}
}
