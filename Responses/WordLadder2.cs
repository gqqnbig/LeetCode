using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{
	public class WordLadder2
	{
		public IList<IList<string>> FindLadders(string beginWord, string endWord, IList<string> wordList)
		{
			var transforms = FindLadders(beginWord, endWord, new LinkedList<string>(wordList), wordList.Count);
			//如果找不到解，按规定返回空列表
			if (transforms == null)
				return new List<IList<string>>();
			else
			{
				foreach (var t in transforms)
					t.AddFirst(beginWord);
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
		/// <param name="expandLimit">还能对beginWord变形几次</param>
		/// <returns></returns>
		internal List<LinkedList<string>> FindLadders(string beginWord, string endWord, LinkedList<string> wordList, int expandLimit)
		{
			Debug.Assert(expandLimit >= 0);
			if (beginWord == endWord)
			{
				var r = new List<LinkedList<string>>();
				r.Add(new LinkedList<string>());
				return r;
			}

			if (expandLimit == 0)
				return null;
			Debug.Assert(wordList.Count > 0);

			var nextWords = wordList.Where(w => IsDifference1(w, beginWord)).ToList();
			List<LinkedList<string>> shortestTransformations = null;

			LinkedListNode<string> node = wordList.First;
			while (node != null)
			{
				if (IsDifference1(node.Value, beginWord) == false)
				{
					node = node.Next;
					continue;
				}

				string word = node.Value;
				var next = node.Next;
				Debug.Assert(next == null || next.List != null);
				wordList.Remove(node);
				var transformations = FindLadders(word, endWord, wordList, expandLimit - 1);
				if (next == null)
					wordList.AddLast(node);
				else
				{
					Debug.Assert(next.List != null);
					wordList.AddBefore(next, node);
				}


				if (transformations != null)
				{
					//找到了解
					foreach (var t in transformations)
						t.AddFirst(word);
					//transformations里面每个transformation的长度都必须相同。
					Debug.Assert(transformations.Skip(1).All(t => t.Count == transformations[0].Count));
					if (shortestTransformations == null || transformations[0].Count < shortestTransformations[0].Count)
					{
						shortestTransformations = transformations;
						Debug.Assert(shortestTransformations[0].Count <= expandLimit);
						expandLimit = shortestTransformations[0].Count;
					}
					else
					{
						Debug.Assert(transformations[0].Count == shortestTransformations[0].Count);
						shortestTransformations.AddRange(transformations);
					}

				}

				node = next;
			}
			Debug.Assert(shortestTransformations == null || shortestTransformations[0].Count - 1 <= expandLimit); //强制搜索层数限制
			return shortestTransformations;
		}

		bool IsDifference1(string w1, string w2)
		{
			Debug.Assert(w1.Length == w2.Length);

			//对特殊情况进行优化
			if (w1 == w2)
				return false;

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
	}
}
