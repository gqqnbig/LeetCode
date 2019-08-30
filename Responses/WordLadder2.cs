using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace LeetCode
{
	public class WordLadder2
	{
		public IList<IList<string>> FindLadders(string beginWord, string endWord, IList<string> wordList)
		{
			Func<string, List<Problem>> generateNewWords;
			if (wordList.Count < 26 * beginWord.Length)
			{
				//设W为单词长度，L为wordList长度。
				//如果wordList较短，迭代wordList。
				//迭代次数为L，IsDifference1的复杂度为W，所以总复杂度为O(WL)。
				var enhancedList = wordList.Select(w => new Problem(w)).ToList();
				var words = new List<Problem>(enhancedList);
				generateNewWords = begingWord => (from w in words
												  where IsDifference1(w.Word, begingWord)
												  select w).ToList();

			}
			else
			{
				//如果wordList较长，变换迭代起始单词的字母，然后检查是否存在于wordList。
				//迭代次数26W，IsDifference1的复杂度为W，所以总复杂度为O(26W^2) -> O(W^2)。
				var dic = wordList.ToHashSet();
				generateNewWords = word =>
				{
					List<Problem> list = new List<Problem>();
					var charArr = word.ToCharArray();
					for (int j = 0; j < charArr.Length; j++)
					{
						char ch = charArr[j];
						for (int k = 0; k < 26; k++)
						{
							char nextCh = (char)(k + 'a');
							if (nextCh == ch)
								continue;

							charArr[j] = nextCh;
							string nextWord = new string(charArr);
							if (dic.Contains(nextWord) && IsDifference1(nextWord, word))
								list.Add(new Problem(nextWord));
						}
						charArr[j] = ch;
					}
					return list;
				};
			}
			//至此，generateNewWords的复杂度为O(min(WL,W^2))
			var transforms = FindLadders(new Problem(beginWord), endWord, generateNewWords, wordList.Count);
			//如果找不到解，按规定返回空列表
			if (transforms.Count == 0)
				return new List<IList<string>>();
			else
			{
				var result = (from t in transforms
							  select (IList<string>)t.ToList()).ToList();
				Debug.Assert(result.All(t => t[t.Count - 1] == endWord), "最后一个元素必须是endWord。");
				Debug.Assert(result.All(t => t[0] == beginWord), "第一个元素必须是beginWord。");
				return result;
			}
		}

		/// <summary>
		/// 如果beginWord等于endWord，变换长度为0。
		/// 如果beginWord无法变换到endWord，返回null。
		/// </summary>
		/// <returns></returns>
		/// <remarks>
		/// 设generateNewWords的度杂复为g
		/// 最坏情况产生L^L个节点，每个节点比较是否为解，复杂度W，若不是，产生子节点，复杂度g。
		/// 所以总复杂度为O((W+g)L^L)。
		/// </remarks>
		List<LinkedList<string>> FindLadders(Problem beginWord, string endWord, Func<string, List<Problem>> generateNewWords, int wordListCount)
		{
			//还能对beginWord变形几次
			int usedTransformLimit = wordListCount;
			Debug.Assert(usedTransformLimit >= 0);
			var queue = new Queue<Problem>();
			queue.Enqueue(beginWord);
			List<LinkedList<string>> shortestTransformations = new List<LinkedList<string>>();
			var expansionMap = new Dictionary<string, Tuple<int, IList<Problem>>>(wordListCount - 1 + 1);//-1表示减去endWord，+1表示加上beginWord
			while (queue.Count > 0)
			{
				var problem = queue.Dequeue();
				if (problem.Word == endWord)
				{
					var transform = ChainTransform(problem);
					Debug.Assert(shortestTransformations.Count == 0 || transform.Count == shortestTransformations[0].Count);
					shortestTransformations.Add(transform);

					Debug.Assert(problem.UsedTransform <= usedTransformLimit);
					usedTransformLimit = problem.UsedTransform;
				}

				Debug.Assert(problem.UsedTransform <= usedTransformLimit);
				if (problem.UsedTransform == usedTransformLimit)
					continue;

				if (expansionMap.TryGetValue(problem.Word, out var nextWords) == false || nextWords.Item2 == null)
				{
					nextWords = new Tuple<int, IList<Problem>>(nextWords?.Item1 ?? problem.UsedTransform + 1, generateNewWords(problem.Word));
					expansionMap[problem.Word] = nextWords;
				}

				foreach (var w in nextWords.Item2)
				{
					var p = new Problem(w.Word, problem);
					if (expansionMap.TryGetValue(p.Word, out var v) == false || v.Item1 >= p.UsedTransform)
					{
						queue.Enqueue(p);
						expansionMap[p.Word] = new Tuple<int, IList<Problem>>(p.UsedTransform, v?.Item2);
					}
				}
			}
			//Debug.Assert(shortestTransformations == null || shortestTransformations[0].Count - 1 <= usedTransformLimit); //强制搜索层数限制
			return shortestTransformations;
		}

		bool IsDifference1(string w1, string w2)
		{
			Debug.Assert(w1.Length == w2.Length);

			bool hasDifference = false;
			for (int i = 0; i < w1.Length; i++)
			{
				if (w1[i] != w2[i])
				{
					if (hasDifference)
						return false;
					else
						hasDifference = true;
				}
			}

			return hasDifference;
		}

		LinkedList<string> ChainTransform(Problem p)
		{
			var list = new LinkedList<string>();
			while (p != null)
			{
				list.AddFirst(p.Word);
				p = p.Parent;
			}

			return list;
		}

		class Problem
		{
			public Problem(string word, Problem parent = null)
			{
				Word = word;
				Parent = parent;
				if (parent != null)
					UsedTransform = parent.UsedTransform + 1;
			}
			public string Word { get; }

			/// <summary>
			/// 从初始状态到达此状态已经使用的变换数量
			/// </summary>
			public int UsedTransform { get; }

			public Problem Parent { get; }
		}


	}
}
