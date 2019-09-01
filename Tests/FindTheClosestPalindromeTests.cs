using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LeetCode.Tests
{
	[TestClass]
	public class FindTheClosestPalindromeTests
	{

		[DataTestMethod]
		[DataRow("1", "0")]
		[DataRow("999999998899999999", "999999999999999999")]
		[DataRow("2147447412", "2147483647")]
		[DataRow("999", "1001")]
		[DataRow("1394334931", "1394375405")]
		[DataRow("121", "123")]
		[DataRow("1221", "1234")]
		public void NearestPalindromicTest(string expected, string input)
		{
			Assert.AreEqual(expected, new FindTheClosestPalindrome().NearestPalindromic(input), $"input={input}");
		}

		[TestMethod]
		public void NearestPalindromicGenerationTest()
		{
			var solution = new FindTheClosestPalindrome();
			List<long> inputList = new List<long>();
			Random rand = new Random();
			for (int i = 0; i < 10; i++)
			{
				var input = 0L;
				var length = rand.Next(19);
				for (int j = 0; j < length; j++)
				{
					input = input * 10 + rand.Next(10);
				}
				inputList.Add(input);
			}
			//inputList.Add(Convert.ToInt64(new string('9', 16)));
			//inputList.Add(Convert.ToInt64("2147483647"));


			Parallel.ForEach(inputList, input =>
			 {
				 var actualResult = solution.NearestPalindromic(input);
				 Assert.AreNotEqual(actualResult, input);

				 var d = Math.Abs(input - actualResult);

				 var max = long.MaxValue - input < d ? long.MaxValue : input + d - 1;
				 var value = input - d + 1;
				 if (value < 0)
					 value = 0;
				 for (; value <= max; value++)
				 {
					 if (value != input && IsPalindromic(value.ToString()))
						 Assert.Fail("输入为{0}，正确的结果应为{1}，但实际得到{2}。", input, value, actualResult);
				 }

				 Console.WriteLine("input={0}，测试通过，正确的结果为{1}", input, actualResult);
			 });
		}

		[TestMethod]
		public void NearestPalindromicSpeedTest()
		{
			new FindTheClosestPalindrome().NearestPalindromic("2147483647");
		}

		[TestMethod]
		public void IsPalindromicSpeedTest()
		{
			const int count = 10000;

			Stopwatch sw=new Stopwatch();
			sw.Start();
			for (int i = 0; i < count; i++)
			{
				IsPalindromic(123001);
				IsPalindromic(2147483647);
				IsPalindromic(999999999999999999);
			}
			sw.Stop();
			Console.WriteLine("数学方法: {0}",sw.ElapsedMilliseconds);


			sw.Restart();
			for (int i = 0; i < count; i++)
			{
				IsPalindromic(123001);
				IsPalindromic(2147483647);
				IsPalindromic(999999999999999999);
			}
			sw.Stop();
			Console.WriteLine("文本方法: {0}", sw.ElapsedMilliseconds);
		}


		static bool IsPalindromic(string n)
		{
			for (int i = 0; i < n.Length / 2; i++)
			{
				if (n[i] != n[n.Length - 1 - i])
					return false;
			}

			return true;
		}


		static bool IsPalindromic(long value)
		{
			long t = value;
			long reversedValue = 0;
			//如果输入是123001，reversedValue最后将是100321。
			while (t > 0)
			{
				reversedValue = 10 * reversedValue + t % 10;
				t /= 10;
			}
			return reversedValue == value;
		}


		[TestMethod]
		public void GetDigitTest()
		{
			Random rand = new Random();
			for (int i = 0; i < 10; i++)
			{
				var number = rand.Next();
				var str = number.ToString();
				var length = str.Length;
				var index = rand.Next(length);
				Assert.AreEqual(GetDigit(number, index).ToString()[0], str[index]);
			}
		}

		[TestMethod]
		public void GetDigitSpeedTest()
		{
			List<int> numbers = new List<int>();
			List<int> indexes = new List<int>();
			List<string> strs = new List<string>();

			Random rand = new Random();
			for (int i = 0; i < 500000; i++)
			{
				var number = rand.Next();
				numbers.Add(number);
				var str = number.ToString();
				strs.Add(str);
				var length = str.Length;
				var index = rand.Next(length);
				indexes.Add(index);
			}

			Stopwatch sw = new Stopwatch();
			sw.Start();
			for (int i = 0; i < numbers.Count; i++)
			{
				var s = FindTheClosestPalindrome.GetDigit(numbers[i], indexes[i]);
			}
			sw.Stop();
			Console.WriteLine($"GetDigit用时{sw.ElapsedMilliseconds}");

			sw.Restart();
			for (int i = 0; i < numbers.Count; i++)
			{
				var s = strs[i][indexes[i]];
			}
			sw.Stop();
			Console.WriteLine($"string[int]用时{sw.ElapsedMilliseconds}");

			sw.Restart();
			for (int i = 0; i < numbers.Count; i++)
			{
				var s = Convert.ToInt32(strs[i]);
			}
			sw.Stop();
			Console.WriteLine($"Convert.ToInt32用时{sw.ElapsedMilliseconds}");

			sw.Restart();
			for (int i = 0; i < numbers.Count; i++)
			{
				var s = numbers[i].ToString();
			}
			sw.Stop();
			Console.WriteLine($"ToString用时{sw.ElapsedMilliseconds}");

		}


		static int GetDigit(int number, int index, int length = 0)
		{
			if (length == 0)
				length = (int)(Math.Floor(Math.Log10(number)) + 1);
			if (index >= length)
				throw new ArgumentException($"index {index}大于length {length}。");

			int divisor = (int)Math.Pow(10, length - 1 - index);
			return (number / divisor) % 10;
		}

	}
}
