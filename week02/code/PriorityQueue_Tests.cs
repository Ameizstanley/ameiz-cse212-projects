using Microsoft.VisualStudio.TestTools.UnitTesting;

// TODO Problem 2 - Write and run test cases and fix the code to match requirements.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Enqueue items with different priorities and dequeue them.
    // Items should be dequeued in order of priority (highest first).
    // Expected Result: "High" (priority 3), "Medium" (priority 2), "Low" (priority 1)
    // Defect(s) Found: Dequeue may not correctly identify the highest priority item,
    // or may not remove items in priority order.
    public void TestPriorityQueue_DifferentPriorities()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("Low", 1);
        priorityQueue.Enqueue("Medium", 2);
        priorityQueue.Enqueue("High", 3);

        Assert.AreEqual("High", priorityQueue.Dequeue());
        Assert.AreEqual("Medium", priorityQueue.Dequeue());
        Assert.AreEqual("Low", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Enqueue multiple items with the SAME priority.
    // Items with the same priority should follow FIFO order (first in, first out).
    // Expected Result: "First" (added first), then "Second", then "Third"
    // Defect(s) Found: When multiple items have the same priority, the implementation
    // may not follow FIFO order and might return them in wrong order or always return the last one.
    public void TestPriorityQueue_SamePriority_FIFO()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("First", 1);
        priorityQueue.Enqueue("Second", 1);
        priorityQueue.Enqueue("Third", 1);

        Assert.AreEqual("First", priorityQueue.Dequeue());
        Assert.AreEqual("Second", priorityQueue.Dequeue());
        Assert.AreEqual("Third", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Mix of priorities with duplicates. When there are multiple items
    // with the highest priority, the first one enqueued should be dequeued first.
    // Expected Result: "High1" (priority 5, added first), "High2" (priority 5, added second),
    // "Medium" (priority 3), "Low" (priority 1)
    // Defect(s) Found: May not properly handle FIFO order for items with same high priority.
    public void TestPriorityQueue_MixedWithDuplicatePriorities()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("Low", 1);
        priorityQueue.Enqueue("High1", 5);
        priorityQueue.Enqueue("Medium", 3);
        priorityQueue.Enqueue("High2", 5);

        Assert.AreEqual("High1", priorityQueue.Dequeue());
        Assert.AreEqual("High2", priorityQueue.Dequeue());
        Assert.AreEqual("Medium", priorityQueue.Dequeue());
        Assert.AreEqual("Low", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Try to dequeue from an empty queue.
    // Expected Result: InvalidOperationException with message "The queue is empty."
    // Defect(s) Found: May not throw an exception, may throw wrong exception type,
    // or may have incorrect error message.
    public void TestPriorityQueue_EmptyQueue_ThrowsException()
    {
        var priorityQueue = new PriorityQueue();

        try
        {
            priorityQueue.Dequeue();
            Assert.Fail("Exception should have been thrown.");
        }
        catch (InvalidOperationException e)
        {
            Assert.AreEqual("The queue is empty.", e.Message);
        }
        catch (AssertFailedException)
        {
            throw;
        }
        catch (Exception e)
        {
            Assert.Fail(
                string.Format("Unexpected exception of type {0} caught: {1}",
                    e.GetType(), e.Message)
            );
        }
    }

    [TestMethod]
    // Scenario: Enqueue and dequeue in alternating pattern to ensure queue maintains
    // correct state throughout operations.
    // Expected Result: Should dequeue items in priority order even when interleaved.
    // Defect(s) Found: Queue state may become corrupted with alternating operations,
    // or priorities may not be correctly maintained.
    public void TestPriorityQueue_InterleavedOperations()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("First", 1);
        priorityQueue.Enqueue("High", 3);
        
        Assert.AreEqual("High", priorityQueue.Dequeue()); // Remove highest
        
        priorityQueue.Enqueue("Medium", 2);
        priorityQueue.Enqueue("VeryHigh", 5);
        
        Assert.AreEqual("VeryHigh", priorityQueue.Dequeue()); // Remove highest
        Assert.AreEqual("Medium", priorityQueue.Dequeue());
        Assert.AreEqual("First", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Use negative and zero priorities to ensure they work correctly.
    // Expected Result: Items should still be dequeued by highest priority (0 > -1 > -5).
    // Defect(s) Found: May not handle negative priorities correctly, or may have
    // comparison issues with negative numbers.
    public void TestPriorityQueue_NegativeAndZeroPriorities()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("VeryLow", -5);
        priorityQueue.Enqueue("Zero", 0);
        priorityQueue.Enqueue("Low", -1);

        Assert.AreEqual("Zero", priorityQueue.Dequeue());
        Assert.AreEqual("Low", priorityQueue.Dequeue());
        Assert.AreEqual("VeryLow", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Enqueue items, dequeue all, then try to dequeue from empty queue.
    // Expected Result: After emptying queue, dequeue should throw exception.
    // Defect(s) Found: May not properly detect when queue becomes empty after operations.
    public void TestPriorityQueue_EmptyAfterDequeuing()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("Item1", 1);
        priorityQueue.Enqueue("Item2", 2);

        priorityQueue.Dequeue();
        priorityQueue.Dequeue();

        try
        {
            priorityQueue.Dequeue();
            Assert.Fail("Exception should have been thrown when queue is empty.");
        }
        catch (InvalidOperationException e)
        {
            Assert.AreEqual("The queue is empty.", e.Message);
        }
    }

    [TestMethod]
    // Scenario: Large number of items with the same priority to stress test FIFO behavior.
    // Expected Result: Items should be dequeued in exact order they were enqueued.
    // Defect(s) Found: FIFO ordering may break with larger queues.
    public void TestPriorityQueue_ManySamePriority()
    {
        var priorityQueue = new PriorityQueue();
        
        for (int i = 1; i <= 10; i++)
        {
            priorityQueue.Enqueue($"Item{i}", 5);
        }

        for (int i = 1; i <= 10; i++)
        {
            Assert.AreEqual($"Item{i}", priorityQueue.Dequeue());
        }
    }

    [TestMethod]
    // Scenario: Complex scenario with multiple priority levels and FIFO within each level.
    // Expected Result: High priority items first (in FIFO order), then medium, then low.
    // Defect(s) Found: May not maintain FIFO order within each priority level in complex scenarios.
    public void TestPriorityQueue_ComplexScenario()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("Low1", 1);
        priorityQueue.Enqueue("High1", 10);
        priorityQueue.Enqueue("Medium1", 5);
        priorityQueue.Enqueue("Low2", 1);
        priorityQueue.Enqueue("High2", 10);
        priorityQueue.Enqueue("Medium2", 5);

        Assert.AreEqual("High1", priorityQueue.Dequeue());
        Assert.AreEqual("High2", priorityQueue.Dequeue());
        Assert.AreEqual("Medium1", priorityQueue.Dequeue());
        Assert.AreEqual("Medium2", priorityQueue.Dequeue());
        Assert.AreEqual("Low1", priorityQueue.Dequeue());
        Assert.AreEqual("Low2", priorityQueue.Dequeue());
    }
}