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
	public class ValidateIPAddressTests
	{
		[DataTestMethod]
		[DataRow("Neither", "172.16.254.01")]
		[DataRow("Neither", "12.12.12")]
		[DataRow("Neither", "2001:0db8:85a3::8A2E:0370:7334")]//特殊规则：禁止省略
		[DataRow("IPv6", "2001:0db8:85a3:0:0:8A2E:0370:7334")]
		[DataRow("Neither", "02001:0db8:85a3:0000:0000:8a2e:0370:7334")]//02001不行
		[DataRow("IPv6", "2001:0db8:85a3:0:0:8A2E:0370:7334")]
		public void ValidIPAddressTest(string expected, string input)
		{
			Assert.That.AreEqual(expected, () => new ValidateIPAddress().ValidIPAddress(input));
		}
	}
}
