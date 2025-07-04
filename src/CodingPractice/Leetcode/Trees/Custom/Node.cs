﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingPractice.Leetcode.Trees.Custom
{
	public class Node
	{
		public int val;
		public Node left;
		public Node right;
		public Node parent;
	}

	/*
	* Definition for a binary tree node.
	*/
	public class TreeNode
	{
		public int val;
		public TreeNode left;
		public TreeNode right;
		public TreeNode(int val = 0, TreeNode left = null, TreeNode right = null)
		{
			this.val = val;
			this.left = left;
			this.right = right;
		}
	}
}
