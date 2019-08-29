using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{
	public class ReverseWordsInAString
	{
		public string ReverseWords(string s)
		{
			var words = s.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
			Array.Reverse(words);
			return string.Join(" ", words);
		}
	}
}
