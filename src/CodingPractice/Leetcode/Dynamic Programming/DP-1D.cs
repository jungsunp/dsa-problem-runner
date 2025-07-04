using System;
using System.Collections.Generic;

namespace CodingPractice.Leetcode
{
	public class DP_1D
	{
		// #1137. N-th Tribonacci Number
		// Time: O(n)
		// Space: O(1)
		public int Tribonacci(int n)
		{
			if (n == 0) { return 0; }

			int ret = 1; // T(n+3)
			int prev0 = 0; // T(n)
			int prev1 = 1; // T(n+1)
			int prev2 = 1; // T(n+2)

			for (int i = 3; i <= n; i++)
			{
				ret = prev0 + prev1 + prev2;

				prev0 = prev1;
				prev1 = prev2;
				prev2 = ret;
			}

			return ret;
		}

		// #746. Min Cost Climbing Stairs
		// Time: O(n)
		// Space: O(1)
		public int MinCostClimbingStairs(int[] cost)
		{
			for (int i = 2; i < cost.Length; i++)
			{
				cost[i] += Math.Min(cost[i - 2], cost[i - 1]);
			}
			return Math.Min(cost[cost.Length - 2], cost[cost.Length - 1]);
		}

		// #198. House Robber
		// Time: O(n)
		// Space: O(1)
		public int Rob(int[] nums)
		{
			if (nums.Length == 1) { return nums[0]; }

			if (nums.Length > 2)
			{
				nums[2] += nums[0]; // base case for at least 3 elements array
			}

			// Run DP - dynamically update with max amount
			for (int i = 3; i < nums.Length; i++)
			{
				nums[i] += Math.Max(nums[i - 3], nums[i - 2]);
			}

			return Math.Max(nums[nums.Length - 2], nums[nums.Length - 1]);
		}

		// #790. Domino and Tromino Tiling
		// Time: O(n)
		// Space: O(1)
		private const int MOD = 1000000007; // 10 ^ 9 + 7
		public int NumTilings(int n)
		{
			if (n < 3) { return n; }

			// f: full covered tiling & p: partially covered tiling
			long fn1 = 2; // base case for f(n-1)
			long fn2 = 1; // base case for f(n-2)
			long pn1 = 1; // base case for p(n-1)
			long fn = fn1;
			long pn = pn1;

			// Run DP with following formula
			// f(n) = f(n-1) + f(n-2) + 2 * p(n-1)
			// p(n) = p(n-1) + f(n-2)
			for (int i = 3; i <= n; i++)
			{
				fn = (fn1 + fn2 + 2 * pn1) % MOD;
				pn = (pn1 + fn2) % MOD;

				(fn1, fn2, pn1) = (fn, fn1, pn);
			}

			return (int)fn1;
		}

		// #53. Maximum Subarray
		// Time: O(n)
		// Space: O(1)
		public int MaxSubArray(int[] nums)
		{
			int currentSum = 0;
			int maxSum = int.MinValue;

			// Run Kadane's Algorithm
			foreach (int num in nums)
			{
				currentSum = Math.Max(currentSum + num, num);
				maxSum = Math.Max(maxSum, currentSum);
			}

			return maxSum;
		}

		// #3434. Maximum Frequency After Subarray Operation
		// Time: O(n)
		// Space: O(1)
		public int MaxFrequency(int[] nums, int k)
		{
			int kCount = 0;
			int min = 1;
			int max = 50;

			// count K occurences
			foreach (int num in nums)
			{
				if (num == k)
				{
					kCount++;
				}
				else
				{
					min = Math.Min(min, num);
					max = Math.Max(max, num);
				}
			}

			// iterate and perform Kadane's Algorithm
			// 1 - if == i, -1 if == k, 0 otherwise
			int maxFreq = 0;
			for (int i = min; i <= max; i++)
			{
				if (i == k) { continue; }
				int currSum = 0;
				foreach (int num in nums)
				{
					int point = 0;
					if (num == i) { point = 1; }
					else if (num == k) { point = -1; }

					currSum = Math.Max(currSum + point, point);
					maxFreq = Math.Max(maxFreq, currSum);
				}
			}

			return maxFreq + kCount;
		}

		// #121. Best Time to Buy and Sell Stock
		// Time: O(n)
		// Space: O(1)
		public int MaxProfit(int[] prices)
		{
			int maxProfit = 0;
			int minPrice = prices[0];

			// Run DP
			for (int i = 1; i < prices.Length; i++)
			{
				minPrice = Math.Min(minPrice, prices[i]);
				maxProfit = Math.Max(maxProfit, prices[i] - minPrice);
			}

			return maxProfit;
		}

		// #122. Best Time to Buy and Sell Stock II
		// Time: O(n)
		// Space: O(1)
		// Note: Simple variation to Peak Valley Appraoch
		public int MaxProfit2(int[] prices)
		{
			int res = 0;
			for (int i = 1; i < prices.Length; i++)
			{
				res += Math.Max(prices[i] - prices[i - 1], 0);
			}
			return res;
		}

		// #45. Jump Game II
		// Time: O(n)
		// Space: O(n)
		// Note: There is a greedy solution with O(1) space
		public int Jump(int[] nums)
		{
			// Run DP
			int n = nums.Length;
			int[] dp = new int[n]; // keep track of min jumps
			int pointer = 1; // keep track where to fill in dp array
			for (int i = 0; i < n; i++)
			{
				while (pointer < n && pointer <= i + nums[i])
				{
					dp[pointer] = dp[i] + 1;
					pointer++;
				}

				if (pointer == n)
				{
					return dp[n - 1];
				}
			}

			return dp[n - 1];
		}

		// #322. Coin Change
		// Time: O(S * n)
		// Space: O(S) - S = amount of coin
		// Note: There is also simpler DP solution.
		public int CoinChange(int[] coins, int amount)
		{
			if (amount == 0) return 0;

			// Define initial dp array and queeu for BFS
			int[] dp = new int[amount + 1]; // 0, 1, 2, .... , amount
			Queue<int> bfsQueue = new(); // stores indexes in dp
			bfsQueue.Enqueue(0);

			// Run BFS
			while (bfsQueue.Count > 0)
			{
				int levelCnt = bfsQueue.Count;
				for (int i = 0; i < levelCnt; i++)
				{
					int prevAmount = bfsQueue.Dequeue();
					foreach (int coin in coins)
					{
						int nextAmount = prevAmount + coin;
						if (nextAmount == amount)
						{
							return dp[prevAmount] + 1; // found target
						}

						if (nextAmount < dp.Length && dp[nextAmount] == 0)
						{
							dp[nextAmount] = dp[prevAmount] + 1;
							bfsQueue.Enqueue(nextAmount);
						}
					}
				}
			}

			return -1; // invalid
		}
	}
}
