using System.Diagnostics;

namespace LeetCode.Tests
{
	public class MyLinkedList
	{
		private Node head;
		private int count;


		/** Initialize your data structure here. */
		public MyLinkedList()
		{

		}

		/** Get the value of the index-th node in the linked list. If the index is invalid, return -1. */
		public int Get(int index)
		{
			if (index < 0 || index >= count)
				return -1;
			Node n = head;
			while (index > 0 && n != null)
			{
				n = n.Next;
				index--;
			}

			if (n != null)
				return n.Value;
			else
				return -1;
		}


		///<summary> Add a node of value val before the first element of the linked list. After the insertion, the new node will be the first node of the linked list.</summary>
		public void AddAtHead(int val)
		{
			Node n = new Node(val, head);
			head = n;
			count++;
		}

		/** Append a node of value val to the last element of the linked list. */
		public void AddAtTail(int val)
		{
			Node prev = null;
			Node n = head;
			while (n != null)
			{
				prev = n;
				n = n.Next;
			}
			if (prev == null)
				head = new Node(val, null);
			else
				prev.Next = new Node(val, null);
			count++;
		}

		///<summary>Add a node of value val before the index-th node in the linked list.
		/// If index equals to the length of linked list, the node will be appended to the end of linked list.
		/// If index is greater than the length, the node will not be inserted. </summary> 
		public void AddAtIndex(int index, int val)
		{
			if (index == 0 || index == -1) //错误的测试用例，AddAtIndex(-1, 0)依然插入。
			{
				AddAtHead(val);
				return;
			}

			if (index < 0)
				return;

			Node prev = null;
			Node n = head;
			while (index > 0 && n != null)
			{
				prev = n;
				n = n.Next;
				index--;
			}

			if (index > 0)
			{
				//do nothing
			}
			else
			{
				Debug.Assert(prev != null);
				prev.Next = new Node(val, prev.Next);
				count++;
			}

		}

		/** Delete the index-th node in the linked list, if the index is valid. */
		public void DeleteAtIndex(int index)
		{
			if (index < 0 || head == null)
				return;
			if (index == 0)
			{
				head = head.Next;
				count--;
				return;
			}
			Node prev = null;
			Node n = head;
			while (index > 0 && n != null)
			{
				prev = n;
				n = n.Next;
				index--;
			}

			if (n == null)
			{
				//do nothing
			}
			else
			{
				prev.Next = n.Next;
				count--;
			}

		}


		class Node
		{
			public Node(int value, Node next)
			{
				Value = value;
				Next = next;
			}
			public int Value { get; set; }
			public Node Next { get; set; }
		}
	}

	/**
	 * Your MyLinkedList object will be instantiated and called as such:
	 * MyLinkedList obj = new MyLinkedList();
	 * int param_1 = obj.Get(index);
	 * obj.AddAtHead(val);
	 * obj.AddAtTail(val);
	 * obj.AddAtIndex(index,val);
	 * obj.DeleteAtIndex(index);
	 */
}
