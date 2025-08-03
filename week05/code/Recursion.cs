using System.Collections;

public static class Recursion
{
    /// <summary>
    /// Problem 1: Sum of squares from 1^2 to n^2 using recursion
    /// </summary>
    public static int SumSquaresRecursive(int n)
    {
        if (n <= 0)
            return 0;
        return n * n + SumSquaresRecursive(n - 1);
    }

    /// <summary>
    /// Problem 2: Permutations of specified size from unique letters using recursion
    /// </summary>
    public static void PermutationsChoose(List<string> results, string letters, int size, string word = "")
    {
        if (word.Length == size)
        {
            results.Add(word);
            return;
        }

        for (int i = 0; i < letters.Length; i++)
        {
            char c = letters[i];
            string remaining = letters.Remove(i, 1);
            PermutationsChoose(results, remaining, size, word + c);
        }
    }

    private static string Remove(this string str, int index, int count = 1)
    {
        return str.Substring(0, index) + str.Substring(index + count);
    }

    /// <summary>
    /// Problem 3: Count ways to climb stairs with 1, 2, or 3 steps at a time using memoization
    /// </summary>
    public static decimal CountWaysToClimb(int s, Dictionary<int, decimal>? remember = null)
    {
        if (s < 0)
            return 0;
        if (s == 0)
            return 1;

        if (remember == null)
            remember = new Dictionary<int, decimal>();

        if (remember.ContainsKey(s))
            return remember[s];

        decimal ways = CountWaysToClimb(s - 1, remember) +
                       CountWaysToClimb(s - 2, remember) +
                       CountWaysToClimb(s - 3, remember);

        remember[s] = ways;
        return ways;
    }

    /// <summary>
    /// Problem 4: Generate all binary strings for wildcard patterns
    /// </summary>
    public static void WildcardBinary(string pattern, List<string> results)
    {
        int index = pattern.IndexOf('*');
        if (index == -1)
        {
            results.Add(pattern);
            return;
        }

        WildcardBinary(pattern[..index] + "0" + pattern[(index + 1)..], results);
        WildcardBinary(pattern[..index] + "1" + pattern[(index + 1)..], results);
    }

    /// <summary>
    /// Problem 5: Find all paths in a maze using DFS-style recursion
    /// </summary>
    public static void SolveMaze(List<string> results, Maze maze, int x = 0, int y = 0, List<ValueTuple<int, int>>? currPath = null)
    {
        if (currPath == null)
            currPath = new List<(int, int)>();

        if (!maze.IsValidMove(currPath, x, y))
            return;

        currPath.Add((x, y));

        if (maze.IsEnd(x, y))
        {
            results.Add(currPath.AsString());
        }
        else
        {
            SolveMaze(results, maze, x + 1, y, new List<(int, int)>(currPath)); // right
            SolveMaze(results, maze, x - 1, y, new List<(int, int)>(currPath)); // left
            SolveMaze(results, maze, x, y + 1, new List<(int, int)>(currPath)); // down
            SolveMaze(results, maze, x, y - 1, new List<(int, int)>(currPath)); // up
        }
    }
}