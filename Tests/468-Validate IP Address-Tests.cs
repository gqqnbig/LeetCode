using Xunit;
using Xunit.Sdk;

namespace LeetCode.Tests
{
	public class ValidateIPAddressTests
	{
		[Theory]
		[InlineData("Neither", "172.16.254.01")]
		[InlineData("Neither", "12.12.12")]
		[InlineData("Neither", "2001:0db8:85a3::8A2E:0370:7334")]//特殊规则：禁止省略
		[InlineData("IPv6", "2001:0db8:85a3:0:0:8A2E:0370:7334")]
		[InlineData("Neither", "02001:0db8:85a3:0000:0000:8a2e:0370:7334")]//02001不行
		[InlineData("IPv6", "2001:0db8:85a3:0:0:8A2E:0370:7334")]
		public void ValidIPAddressTest(string expected, string input)
		{
			Assert.That.AreEqual(expected, () => new ValidateIPAddress().ValidIPAddress(input));
		}
	}
}
