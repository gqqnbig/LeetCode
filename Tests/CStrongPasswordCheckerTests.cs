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
		[InlineData(0,"1aA234567890")]
		[InlineData(1, "1aA234567888")]
		[InlineData(1, "Abcdefghijklmnopqr111")]
		public void StrongPasswordCheckerTest(int modifications, string password)
		{
			output.WriteLine(modifications.ToString());
			Assert.Equal(modifications, new CStrongPasswordChecker().StrongPasswordChecker(password));
		}

		[Theory]
		[InlineData(0,"1234")]
		[InlineData(0, "")]
		[InlineData(0, "a")]
		[InlineData(0, "aa")]
		[InlineData(1, "aaa")]
		[InlineData(1, "aaaa")]
		[InlineData(1, "aaaaa")]
		[InlineData(2, "aaaaaa")]
		[InlineData(2, "aaa1bbb")]
		public void ModifyToBreakTest(int modifications, string password)
		{
			Assert.Equal(modifications, new CStrongPasswordChecker().ModifyToBreak(password));

		}

		[Fact]
		public void StrongPasswordCheckerGenerationTest()
		{
			char[] symbols = {'0', 'a', 'A', 'b', 'c'};
			Random rand=new Random();
			var checker = new CStrongPasswordChecker();
			for (int iteration = 0; iteration < 20; iteration++)
			{
				int length= rand.Next(1, 30);
				StringBuilder sb=new StringBuilder(length);
				for (int i = 0; i < length; i++)
				{
					sb.Append(symbols[rand.Next(symbols.Length)]);
				}

				string password = sb.ToString();
				int modifications = checker.StrongPasswordChecker(password);
				output.WriteLine("{0} needs to modify {1} times.", password,modifications);
			}
		}


	}
}
