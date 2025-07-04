using System.Collections.Generic;

namespace CodingPractice.Leetcode.Graphs.Custom
{
	// Needed for #133. Clone Graph
	public class Node
	{
		public int val;
		public IList<Node> neighbors;

		public Node()
		{
			val = 0;
			neighbors = new List<Node>();
		}

		public Node(int _val)
		{
			val = _val;
			neighbors = new List<Node>();
		}

		public Node(int _val, List<Node> _neighbors)
		{
			val = _val;
			neighbors = _neighbors;
		}
	}
}
