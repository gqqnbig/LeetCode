using System;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace LeetCode.Tests
{
	public class CStrongPasswordCheckerTests
	{
		private readonly ITestOutputHelper output;

		public CStrongPasswordCheckerTests(ITestOutputHelper output)
		{
			this.output = output;
		}


		[Theory]
		[InlineData(3, "...")]
		[InlineData(0, "1aA234567890")]
		[InlineData(1, "1aA234567888")]
		[InlineData(1, "Abcdefghijklmnopqr111")]
		[InlineData(2, "aaaaaa")]
		[InlineData(5, "a")]
		[InlineData(6, "")]
		[InlineData(7, "aaaaaaaaaaaaaaaaaaaaa")]
		[InlineData(9, "a00abb0Aa0A0aa0abA0bAb00cca0b")]
		[InlineData(9, "aaaaaaaa11aAa1a1aaaa1aaa1aaa")]
		[InlineData(11, "aaaaaaaa11aAa1a1aaaa1aaa1aaaCDE")]
		[InlineData(3, "aaaaabbbb1234567890ABA")]
		[InlineData(6, "aaaaaaaaaaaa1aa111aaa")]
		public void StrongPasswordCheckerTest(int modifications, string password)
		{
			output.WriteLine(modifications.ToString());
			Assert.Equal(modifications, new CStrongPasswordChecker().StrongPasswordChecker(password));
		}

		[Fact]
		public void StrongPasswordCheckerGenerationTest()
		{
			char[] symbols = { '0', 'a', 'A', 'b', 'c' };
			Random rand = new Random();
			var checker = new CStrongPasswordChecker();
			for (int iteration = 0; iteration < 20; iteration++)
			{
				int length = rand.Next(1, 30);
				StringBuilder sb = new StringBuilder(length);
				for (int i = 0; i < length; i++)
				{
					sb.Append(symbols[rand.Next(symbols.Length)]);
				}

				string password = sb.ToString();
				output.WriteLine("Testing {0}", password);
				int modifications = checker.StrongPasswordChecker(password);
				output.WriteLine("{0} needs to modify {1} times.", password, modifications);
			}
		}

		[Fact]
		public void StrongPasswordCheckerSameCharacterTest()
		{
			var checker = new CStrongPasswordChecker();
			Random rand = new Random();
			for (int iteration = 0; iteration < 30; iteration++)
			{
				StringBuilder sb = new StringBuilder(new string('a', iteration));

				int insert = rand.Next(iteration / 3 * 2);
				for (int i = insert; i > 0; i--)
				{
					sb.Insert(rand.Next(sb.Length), '1');
				}

				if (rand.Next(100) > 80)
					sb.Insert(rand.Next(sb.Length), 'A');

				string password = sb.ToString();
				output.WriteLine("Testing {0}", password);
				int modifications = checker.StrongPasswordChecker(password);
				output.WriteLine("{0}({2}) needs to modify {1} times.", password, modifications, password.Length);
			}
		}


	}
}
