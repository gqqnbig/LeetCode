using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Priority_Queue;

namespace LeetCode
{
	public class WordLadder2
	{
		public IList<IList<string>> FindLadders(string beginWord, string endWord, IList<string> wordList)
		{
			var enhancedList = wordList.Select(w => new Problem(w, GetDifferences(w, endWord))).ToList();
			var words = new List<Problem>(enhancedList);
			var transforms = FindLadders(new Problem(beginWord, GetDifferences(beginWord, endWord)), endWord,
										 words,
										 wordList.Count);
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
		/// <param name="beginWord"></param>
		/// <param name="endWord"></param>
		/// <param name="wordList"></param>
		/// <param name="usedTransformLimit">还能对beginWord变形几次</param>
		/// <returns></returns>
		List<LinkedList<string>> FindLadders(Problem beginWord, string endWord, IList<Problem> wordList, int usedTransformLimit)
		{
			Debug.Assert(usedTransformLimit >= 0);
			SimplePriorityQueue<Problem, int> queue = new SimplePriorityQueue<Problem, int>();
			queue.Enqueue(beginWord, 0);
			List<LinkedList<string>> shortestTransformations = new List<LinkedList<string>>();
			Dictionary<string, int> minTransformToWord = new Dictionary<string, int>();
			Dictionary<string, IList<Problem>> expansionMap = new Dictionary<string, IList<Problem>>(wordList.Count - 1 + 1);//-1表示减去endWord，+1表示加上beginWord
			while (queue.Count > 0)
			{
				var problem = queue.Dequeue();


				//Console.WriteLine($"expandLimit={expandLimit}, DifferenceFromSolution={beginWord.DifferenceFromSolution}");
				if (problem.Word == endWord)
				{
					var transform = ChainTransform(problem);
					if (shortestTransformations.Count == 0)
						shortestTransformations.Add(transform);
					else if (transform.Count < shortestTransformations[0].Count)
					{
						shortestTransformations.Clear();
						shortestTransformations.Add(transform);
					}
					else
					{
						Debug.Assert(transform.Count == shortestTransformations[0].Count);
						shortestTransformations.Add(transform);
					}

					Debug.Assert(problem.UsedTransform <= usedTransformLimit);
					usedTransformLimit = problem.UsedTransform;
				}

				Debug.Assert(problem.UsedTransform <= usedTransformLimit);
				if (problem.UsedTransform == usedTransformLimit)
					continue;

				if (expansionMap.TryGetValue(problem.Word, out var nextWords) == false)
				{
					nextWords = (from w in wordList
								 where IsDifference1(w.Word, problem.Word)
								 select w).ToList();
					expansionMap[problem.Word] = nextWords;
				}

				foreach (var w in nextWords)
				{
					var p = new Problem(w.Word, w.DifferenceFromSolution, problem);
					if (minTransformToWord.TryGetValue(p.Word, out int v) == false || v >= p.UsedTransform)
					{
						queue.Enqueue(p, p.Heuristic);
						minTransformToWord[p.Word] = p.UsedTransform;
					}
#if DEBUG
					else
						Console.WriteLine($"用了{p.UsedTransform}步扩展到{p.Word}，但其他路径只需要{minTransformToWord[p.Word]}步。");
#endif
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

		int GetDifferences(string w1, string w2)
		{
			Debug.Assert(w1.Length == w2.Length);

			//对特殊情况进行优化
			if (w1 == w2)
				return 0;

			int hasDifference = 0;
			for (int i = 0; i < w1.Length; i++)
			{
				if (w1[i] != w2[i])
					hasDifference++;
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

		[DebuggerDisplay("Word={Word}, d={DifferenceFromSolution}")]
		class Problem
		{
			public Problem(string word, int differenceFromSolution, Problem parent = null)
			{
				Word = word;
				DifferenceFromSolution = differenceFromSolution;
				Parent = parent;
				if (parent != null)
					UsedTransform = parent.UsedTransform + 1;
			}
			public string Word { get; }

			public int DifferenceFromSolution { get; }

			/// <summary>
			/// 从初始状态到达此状态已经使用的变换数量
			/// </summary>
			public int UsedTransform { get; }

			public int Heuristic => DifferenceFromSolution + UsedTransform;

			public Problem Parent { get; }
		}
	}
}
