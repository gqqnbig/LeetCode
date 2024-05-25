using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace LeetCode.Tests
{
	class DecodedStringAtIndex
	{

		public string DecodeAtIndex(string S, int K)
		{
			return DecodeAtIndexCore(S, K).ToString();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="S"></param>
		/// <param name="K">starts from 1</param>
		/// <returns></returns>
		public char DecodeAtIndexCore(string S, int K)
		{
			var repeatings = FindRepeatings(S, K);
			Debug.Assert(repeatings[repeatings.Count - 1].Length >= K && (repeatings.Count == 1 || repeatings[repeatings.Count - 2].Length < K),
						 "最后一个repeatings的Length必须>=K，倒数第二个repeatings的Length必须<K。第二个分句是为了及时终止。");
			return DecodeAtIndex(S, K, repeatings);
		}

		private char DecodeAtIndex(string S, int K, List<Repeating> repeatings)
		{
			Debug.Assert(K > 0, "K从1开始。");

			if (K > repeatings.Last().BaseLength)
				return DecodeAtIndex(S, (K - 1) % repeatings.Last().BaseLength + 1, repeatings);

			for (int i = repeatings.Count - 2; i >= 0; i--)
			{
				Debug.Assert(repeatings[i].Length < repeatings[i + 1].BaseLength);
				if (repeatings[i].Length < K)
				{
					if (K <= repeatings[i + 1].BaseLength)
						return S[repeatings[i].EndIndex + 1 + K - repeatings[i].Length];
					else
						return DecodeAtIndex(S, (K - 1) % repeatings[i + 1].BaseLength + 1, repeatings);
				}
			}

			Debug.Assert(K <= repeatings[0].Length);
			//K是1基的，改为0基
			return S[(K - 1) % repeatings[0].BaseLength];
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
					Repeating r;
					if (segmentLength > 0)
					{
						int l = segmentLength;
						if (repeatings.Count > 0)
							l += repeatings.Last().Length;
						r = new Repeating(l, count, i - 1);
						repeatings.Add(r);
						segmentLength = 0;
					}
					else
					{
						Debug.Assert(repeatings.Count > 0);
						r = repeatings[repeatings.Count - 1];
						r.Count *= count;
						repeatings[repeatings.Count - 1] = r;
					}

					if (r.Length >= K)
						break;
				}
			}
			if (repeatings.Count == 0)
				repeatings.Add(new Repeating(input.Length, 1, input.Length - 1));

			Debug.Assert(repeatings.All(r => char.IsLetter(input[r.EndIndex])));
			Debug.Assert(repeatings.Count > 0);

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
			public int Count { get; set; }

			public int Length => BaseLength * Count;

			public int EndIndex { get; }
		}
	}
}
