using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Add three items with different priorities.
    // Expected Result: Highest priority item is dequeued first.
    // Defect(s) Found: Original implementation dequeued in insertion order, ignoring priority.
    public void TestPriorityQueue_HighestPriorityFirst()
    {
        var pq = new PriorityQueue();
        pq.Enqueue("A", 1);
        pq.Enqueue("B", 3); // highest
        pq.Enqueue("C", 2);

        Assert.AreEqual("B", pq.Dequeue());
        Assert.AreEqual("C", pq.Dequeue());
        Assert.AreEqual("A", pq.Dequeue());
    }

    [TestMethod]
    // Scenario: Add multiple items with the same priority.
    // Expected Result: Items with equal priority are dequeued in FIFO order.
    // Defect(s) Found: Items with same priority were previously returned out of insertion order.
    public void TestPriorityQueue_TieBreakerFIFO()
    {
        var pq = new PriorityQueue();
        pq.Enqueue("A", 2);
        pq.Enqueue("B", 2);
        pq.Enqueue("C", 2);

        Assert.AreEqual("A", pq.Dequeue());
        Assert.AreEqual("B", pq.Dequeue());
        Assert.AreEqual("C", pq.Dequeue());
    }

    [TestMethod]
    // Scenario: Attempt to dequeue from an empty queue.
    // Expected Result: InvalidOperationException with specific message.
    // Defect(s) Found: No exception was originally thrown on empty dequeue.
    public void TestPriorityQueue_EmptyQueueThrows()
    {
        var pq = new PriorityQueue();

        try
        {
            pq.Dequeue();
            Assert.Fail("Expected exception was not thrown.");
        }
        catch (InvalidOperationException ex)
        {
            Assert.AreEqual("The queue is empty.", ex.Message);
        }
    }

    [TestMethod]
    // Scenario: Mix of high and low priority with some duplicates.
    // Expected Result: Highest priorities first, tie-breaking by insertion order.
    // Defect(s) Found: Order of dequeue was incorrect when handling mixed priorities with ties.
    public void TestPriorityQueue_MixedWithTies()
    {
        var pq = new PriorityQueue();
        pq.Enqueue("Low1", 1);
        pq.Enqueue("High1", 5);
        pq.Enqueue("Low2", 1);
        pq.Enqueue("High2", 5);
        pq.Enqueue("Mid", 3);

        Assert.AreEqual("High1", pq.Dequeue());
        Assert.AreEqual("High2", pq.Dequeue());
        Assert.AreEqual("Mid", pq.Dequeue());
        Assert.AreEqual("Low1", pq.Dequeue());
        Assert.AreEqual("Low2", pq.Dequeue());
    }
}