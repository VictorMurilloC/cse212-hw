using System;
using System.Collections.Generic;

/// <summary>
/// A priority queue where each item has a priority. Higher values mean higher priority.
/// If multiple items have the same priority, the one added first is removed first (FIFO).
/// </summary>
public class PriorityQueue
{
    private List<PriorityItem> _queue = new();

    /// <summary>
    /// Adds an item to the end of the queue with an associated priority.
    /// </summary>
    public void Enqueue(string value, int priority)
    {
        _queue.Add(new PriorityItem(value, priority));
    }

    /// <summary>
    /// Removes and returns the value with the highest priority.
    /// If multiple items have the same priority, removes the earliest one (FIFO).
    /// </summary>
    public string Dequeue()
    {
        if (_queue.Count == 0)
        {
            throw new InvalidOperationException("The queue is empty.");
        }

        int highPriorityIndex = 0;

        for (int i = 1; i < _queue.Count; i++)
        {
            if (_queue[i].Priority > _queue[highPriorityIndex].Priority)
            {
                highPriorityIndex = i;
            }
        }

        string result = _queue[highPriorityIndex].Value;
        _queue.RemoveAt(highPriorityIndex);
        return result;
    }

    public override string ToString()
    {
        return $"[{string.Join(", ", _queue)}]";
    }
}

internal class PriorityItem
{
    internal string Value { get; }
    internal int Priority { get; }

    internal PriorityItem(string value, int priority)
    {
        Value = value;
        Priority = priority;
    }

    public override string ToString()
    {
        return $"{Value} (Pri:{Priority})";
    }
}