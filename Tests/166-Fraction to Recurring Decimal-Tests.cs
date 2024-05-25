using System;
using System.Collections.Generic;
using Xunit;

namespace LeetCode.Tests
{
	
	public class FractionToRecurringDecimalTests
	{
		[Theory]
		[InlineData("-6.25", -50, 8)]
		[InlineData("0", 0, 3)]
		[InlineData("2147483648", -2147483648, -1)]
		[InlineData("0.0000000004656612873077392578125", -1, -2147483648)]
		[InlineData("-0.58(3)", 7, -12)]
		[InlineData("2", 2, 1)]
		[InlineData("0.(9900)", 100, 101)]
		[InlineData("0.(857142)", 6, 7)]
		[InlineData("0.(999725802029064984919111598574170551137921579380312585686865917192212777625445571702769399506443652316972854400877433506992048258842884562654236358650945982)", 3646, 3647)]
		//[InlineData("", int.MaxValue - 1, int.MaxValue)]//Mathematica都算不出
		[InlineData("1", int.MaxValue, int.MaxValue)]
		[InlineData("2.(3)", 7, 3)]
		public void FractionToDecimalTest(string expected, int numerator, int denominator)
		{
			Assert.Equal(expected, new FractionToRecurringDecimal().FractionToDecimal(numerator, denominator));
		}


		[Theory]
		[InlineData(21, 1071, 462)]
		[InlineData(21, 1071, -462)]
		[InlineData(21, -1071, -462)]
		[InlineData(21, -1071, 462)]
		public void GetCommonDivisorTest(int expected, int a, int b)
		{
			Assert.Equal(expected, FractionToRecurringDecimal.GetCommonDivisor(a, b));
		}


		[Theory]
		[InlineData(10, 11)]
		[InlineData(33, 95)]
		[InlineData(int.MinValue + 1, int.MinValue)]
		[InlineData(int.MaxValue - 1, int.MaxValue)]
		[InlineData(1, int.MaxValue)]
		[InlineData(1, int.MinValue)]
		//[InlineData(490753113, 2034414257)]
		public void Multiply10ThenDivTest(int numerator, int denominator)
		{
			int expectedQ = (int)(numerator * 10L / denominator);
			int expectedR = (int)(numerator * 10L % denominator);

			int actualQ = FractionToRecurringDecimal.Multiply10ThenDiv(numerator, denominator, out int actualR);
			Assert.Equal(expectedQ, actualQ);
			Assert.Equal(expectedR, actualR);
		}

		[Fact]
		public void Multiply10ThenDivGenerationTest()
		{
			List<Tuple<int, int>> inputs = new List<Tuple<int, int>>();
			Random rand = new Random();
			for (int i = 0; i < 10; i++)
			{
				var denominator = rand.Next(100);
				inputs.Add(new Tuple<int, int>(rand.Next(denominator), denominator));
			}


			foreach (var input in inputs)
			{
				int expectedQ = (int)(input.Item1 * 10L / input.Item2);
				int expectedR = (int)(input.Item1 * 10L % input.Item2);

				int actualQ = FractionToRecurringDecimal.Multiply10ThenDiv(input.Item1, input.Item2, out int actualR);
				Assert.Equal(expectedQ, actualQ, "{0} * 10L / {1}", input.Item1, input.Item2);
				Assert.Equal(expectedR, actualR, "{0} * 10L / {1}", input.Item1, input.Item2);

			}
		}
	}
}
