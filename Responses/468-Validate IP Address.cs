using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;

namespace LeetCode.Tests
{
	class ValidateIPAddress
	{
		static readonly Regex leading0Check = new Regex(@"(^0\d|\.0\d)");

		public string ValidIPAddress(string IP)
		{
			if (IP.IndexOf("::") > -1)
				return "Neither";
			if (IPAddress.TryParse(IP, out var address) == false)
				return "Neither";

			if (address.AddressFamily == AddressFamily.InterNetworkV6)
				return "IPv6";
			else if (IP.Count(c => c == '.') != 3)
				return "Neither";
			else if (leading0Check.IsMatch(IP)) //特殊要求，IPv4地址不能以0开头
				return "Neither";
			else
				return "IPv4";
		}
	}
}
