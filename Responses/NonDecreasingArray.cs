namespace LeetCode.Tests
{
	class NonDecreasingArray
	{
		public bool CheckPossibility(int[] nums)
		{
			int violations = 0;
			for (int i = 1; i < nums.Length; i++)
			{
				if (nums[i - 1] > nums[i])
				{
					violations++;
				}
			}

			return violations <= 1;
		}

	}
}
