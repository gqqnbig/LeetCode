using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode.Tests
{
	class DecodedStringAtIndex
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="S"></param>
		/// <param name="K">starts from 1</param>
		/// <returns></returns>
		public string DecodeAtIndex(string S, int K)
		{
			var repeatings = FindRepeatings(S, K);
			return DecodeAtIndex(S, K, repeatings);
		}

		private string DecodeAtIndex(string S, int K, List<Repeating> repeatings)
		{
			Debug.Assert(K > 0,"K从1开始。");

			if (K > repeatings.Last().BaseLength)
				return DecodeAtIndex(S, (K - 1) % repeatings.Last().BaseLength, repeatings);

			for (int i = repeatings.Count - 2; i >= 0; i--)
			{
				if (repeatings[i].Length < K && K <= repeatings[i + 1].BaseLength)
				{
					return S[repeatings[i].EndIndex + 1 + K - repeatings[i].Length].ToString();
				}
			}

			Debug.Assert(K < repeatings[0].Length);
			return S[K % repeatings[0].BaseLength].ToString();
		}

		private static List<Repeating> FindRepeatings(string input, int K)
		{
			List<Repeating> repeatings = new List<Repeating>();

			int segmentLength = 0;
			for (int i = 0; i < input.Length; i++)
			{
				if (char.IsLetter(input[i]))
					segmentLength++;
				else
				{
					Debug.Assert(char.IsNumber(input[i]));
					int count = input[i] - '0';
					int l = segmentLength;
					if (repeatings.Count > 0)
						l += repeatings.Last().Length;
					var r = new Repeating(l, count, i - 1);
					repeatings.Add(r);

					if (r.Length > K)
						break;

					segmentLength = 0;
				}
			}

			Debug.Assert(repeatings.All(r => char.IsLetter(input[r.EndIndex])));


			return repeatings;
		}

		[DebuggerDisplay("{BaseLength}*{Count}={Length}, End={EndIndex}")]
		struct Repeating
		{
			public Repeating(int baseLength, int count, int endIndex)
			{
				BaseLength = baseLength;
				Count = count;
				EndIndex = endIndex;
			}

			public int BaseLength { get; }
			public int Count { get; }

			public int Length => BaseLength * Count;

			public int EndIndex { get; }
		}

		public static int DecodeAtIndex(string S)
		{
			int totalLength = 0;
			int segmentLength = 0;
			for (int i = 0; i < S.Length; i++)
			{
				//if (K == 0)
				//	return S[i].ToString();


				if (char.IsLetter(S[i]))
				{
					segmentLength++;
					//K--;
				}
				else
				{
					Debug.Assert(char.IsNumber(S[i]));
					int count = S[i] - '0';
					totalLength = (totalLength + segmentLength) * count;


					segmentLength = 0;
				}
			}

			return totalLength;
		}


	}
}
