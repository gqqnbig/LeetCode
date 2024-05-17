using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Linq;

namespace LeetCode
{
	class P649
	{
		static int CountConsecutive(string str)
		{
			int i = 1;
			for (; i < str.Length && str[i] == str[0]; i++) { }
			return i;
		}

		public string PredictPartyVictory(string senate)
		{
			var arr = senate.ToCharArray();

			int banR = 0;
			int banD = 0;
			while (true)
			{
				for (int i = 0; i < arr.Length; i++)
				{
					Debug.Assert(banD == 0 || banR == 0, "We cannot ban both D and R.");


					if (banR == 0 && arr[i] == 'R')
						banD++;
					else if (banD == 0 && arr[i] == 'D')
						banR++;
					else if (banR > 0 && arr[i] == 'R')
					{
						banR--;
						arr[i] = '\0';
					}
					else if (banD > 0 && arr[i] == 'D')
					{
						banD--;
						arr[i] = '\0';
					}
					else
					{

					}

				}

				if (arr.All(c => c == 'R' || c == '\0'))
					return "Radiant";
				if (arr.All(c => c == 'D' || c == '\0'))
					return "Dire";
			}

		}

		public string PredictPartyVictoryM(string senate)
		{
			System.Diagnostics.Debug.Assert(senate != null && senate.Length > 0);

			int voters = CountConsecutive(senate);
			if (voters == senate.Length)
			{
				// move to beginning
			}
			int others = CountConsecutive(senate.Substring(voters));

			int index = 0;
			if (voters >= others)
			{
				senate = new string(senate[0], voters) + senate.Substring(voters + others);
				index = 2 * others;
			}
			else // if (voters < others)
			{
				senate = new string(senate[0], voters) + senate.Substring(voters + voters);
				index = 2 * voters;
			}




			int countR = senate.Count(c => c == 'R');
			int countD = senate.Length - countR;

			if (countR > countD)
				return "Radiant";
			else if (countR < countD)
				return "Dire";
			else
			{
				if (senate[0] == 'R')
					return "Radiant";
				else
					return "Dire";
			}
		}

		public static void Main(string[] args)
		{
			var program = new P649();
			Console.WriteLine(program.PredictPartyVictory("RRDRRDDDD") == "Radiant");
			Console.WriteLine(program.PredictPartyVictory("DDRRR") == "Dire");
		}
	}
}
