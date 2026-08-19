using System.Threading.Tasks.Dataflow;

// https://learn.microsoft.com/en-us/dotnet/standard/parallel-programming/dataflow-task-parallel-library
// hese dataflow components are useful when you have multiple operations that must communicate with one
// another asynchronously or when you want to process data as it becomes available. 
//
// The TPL defines three kinds of dataflow blocks: source blocks, target blocks, and propagator blocks.
// A source block acts as a source of data and can be read from.
// A target block acts as a receiver of data and can be written to.
// A propagator block acts as both a source block and a target block, and can be read from and written to.

// The TPL defines the System.Threading.Tasks.Dataflow.ISourceBlock<TOutput> interface to represent sources,
// System.Threading.Tasks.Dataflow.ITargetBlock<TInput> to represent targets,
// and System.Threading.Tasks.Dataflow.IPropagatorBlock<TInput,TOutput> to represent propagators.
// IPropagatorBlock<TInput,TOutput> inherits from both ISourceBlock<TOutput>, and ITargetBlock<TInput>.

// The TPL Dataflow Library provides several predefined dataflow block types.
// These types are divided into three categories: buffering blocks, execution blocks, and grouping blocks. 

// The TPL Dataflow Library provides three join block types: BatchBlock<T>, JoinBlock<T1,T2>, and BatchedJoinBlock<T1,T2>.

namespace DataFlow.Library;

public static class Tutorial
{
    public static void Method1()
    {
        // Create an ActionBlock<int> object that prints its input
        // and throws ArgumentOutOfRangeException if the input
        // is less than zero.
        var throwIfNegative = new ActionBlock<int>(n =>
        {
            Console.WriteLine($"n = {n}");
            if (n < 0)
            {
                throw new ArgumentOutOfRangeException();
            }
        });

        // Post values to the block.
        throwIfNegative.Post(0);
        throwIfNegative.Post(-1);
        throwIfNegative.Post(1);
        throwIfNegative.Post(-2);
        throwIfNegative.Complete();

        // Wait for completion in a try/catch block.
        try
        {
            throwIfNegative.Completion.Wait();
        }
        catch (AggregateException ae)
        {
            // If an unhandled exception occurs during dataflow processing, all
            // exceptions are propagated through an AggregateException object.
            ae.Handle(e =>
            {
                Console.WriteLine($"Encountered {e.GetType().Name}: {e.Message}");
                return true;
            });
        }

        /* Output:
        n = 0
        n = -1
        Encountered ArgumentOutOfRangeException: Specified argument was out of the range
         of valid values.
        */
    }

    public static void Method2()
    {
        // Create an ActionBlock<int> object that prints its input
        // and throws ArgumentOutOfRangeException if the input
        // is less than zero.
        var throwIfNegative = new ActionBlock<int>(n =>
        {
            Console.WriteLine($"n = {n}");
            if (n < 0)
            {
                throw new ArgumentOutOfRangeException();
            }
        });

        // Create a continuation task that prints the overall
        // task status to the console when the block finishes.
        throwIfNegative.Completion.ContinueWith(task =>
        {
            Console.WriteLine($"The status of the completion task is '{task.Status}'.");
        });

        // Post values to the block.
        throwIfNegative.Post(0);
        throwIfNegative.Post(-1);
        throwIfNegative.Post(1);
        throwIfNegative.Post(-2);
        throwIfNegative.Complete();

        // Wait for completion in a try/catch block.
        try
        {
            throwIfNegative.Completion.Wait();
        }
        catch (AggregateException ae)
        {
            // If an unhandled exception occurs during dataflow processing, all
            // exceptions are propagated through an AggregateException object.
            ae.Handle(e =>
            {
                Console.WriteLine($"Encountered {e.GetType().Name}: {e.Message}");
                return true;
            });
        }

        /* Output:
        n = 0
        n = -1
        The status of the completion task is 'Faulted'.
        Encountered ArgumentOutOfRangeException: Specified argument was out of the range
         of valid values.
        */
    }

    public static void Method3()
    {
        // Create a BufferBlock<int> object (FIFO).
        var bufferBlock = new BufferBlock<int>();

        // Post several messages to the block.
        for (int i = 0; i < 3; i++)
        {
            bufferBlock.Post(i);
        }

        // Receive the messages back from the block.
        for (int i = 0; i < 3; i++)
        {
            Console.WriteLine(bufferBlock.Receive());
        }

        /* Output:
           0
           1
           2
         */
    }

    public static void Method4()
    {
        // The BroadcastBlock<T> class is useful when you must pass multiple messages to another component,
        // but that component needs only the most recent value.
        // This class is also useful when you want to broadcast a message to multiple components.

        // Create a BroadcastBlock<double> object.
        var broadcastBlock = new BroadcastBlock<double>(null);

        // Post a message to the block.
        broadcastBlock.Post(Math.PI);

        // Receive the messages back from the block several times.
        for (int i = 0; i < 3; i++)
        {
            Console.WriteLine(broadcastBlock.Receive());
        }

        /* Output:
           3.14159265358979
           3.14159265358979
           3.14159265358979
         */
    }

    public static void Method5()
    {
        // The WriteOnceBlock<T> class is useful when you want to propagate only the first of multiple messages.

        // Create a WriteOnceBlock<string> object.
        var writeOnceBlock = new WriteOnceBlock<string>(null);

        // Post several messages to the block in parallel. The first
        // message to be received is written to the block.
        // Subsequent messages are discarded.
        Parallel.Invoke(
           () => writeOnceBlock.Post("Message 1"),
           () => writeOnceBlock.Post("Message 2"),
           () => writeOnceBlock.Post("Message 3"));

        // Receive the message from the block.
        Console.WriteLine(writeOnceBlock.Receive());
        Console.WriteLine(writeOnceBlock.Receive());
        Console.WriteLine(writeOnceBlock.Receive());

        /* Sample output:
           Message 2
         */
    }

    public static void Method6()
    {
        // Create an ActionBlock<int> object that prints values to the console.
        var actionBlock = new ActionBlock<int>(n =>
        {
            Console.WriteLine(n);
        });

        // Post several messages to the block.
        for (int i = 0; i < 3; i++)
        {
            actionBlock.Post(i * 10);
        }

        // Set the block to the completed state and wait for all tasks to finish.
        actionBlock.Complete();
        actionBlock.Completion.Wait();

        /* Output:
           0
           10
           20
         */
    }

    public static void Method7()
    {
        // Create a TransformBlock<int, double> object that
        // computes the square root of its input.
        var transformBlock = new TransformBlock<int, double>(n =>
        {
            return Math.Sqrt(n);
        });

        // Post several messages to the block.
        transformBlock.Post(10);
        transformBlock.Post(20);
        transformBlock.Post(30);

        // Read the output messages from the block.
        for (int i = 0; i < 3; i++)
        {
            Console.WriteLine(transformBlock.Receive());
        }

        /* Output:
           3.16227766016838
           4.47213595499958
           5.47722557505166
         */

        transformBlock.Complete();
        transformBlock.Completion.Wait();
    }

    public static void Method8()
    {
        // Create a TransformManyBlock<string, char> object that splits
        // a string into its individual characters.
        var transformManyBlock = new TransformManyBlock<string, char>(s =>
           {
               return s.ToCharArray();
           });

        // Post two messages to the first block.
        transformManyBlock.Post("Hello");
        transformManyBlock.Post("World");

        // Receive all output values from the block.
        for (int i = 0; i < ("Hello" + "World").Length; i++)
        {
            Console.WriteLine(transformManyBlock.Receive());
        }

        /* Output:
           H
           e
           l
           l
           o
           W
           o
           r
           l
           d
         */
    }

    public static void Method9() {
        // Create a BatchBlock<int> object that holds ten
        // elements per batch.
        var batchBlock = new BatchBlock<int>(10);

        // Post several values to the block.
        for (int i = 0; i < 13; i++)
        {
            batchBlock.Post(i);
        }
        // Set the block to the completed state. This causes
        // the block to propagate out any remaining
        // values as a final batch.
        batchBlock.Complete();

        // Print the sum of both batches.

        Console.WriteLine($"The sum of the elements in batch 1 is {batchBlock.Receive().Sum()}.");

        Console.WriteLine($"The sum of the elements in batch 2 is {batchBlock.Receive().Sum()}.");

        /* Output:
           The sum of the elements in batch 1 is 45.
           The sum of the elements in batch 2 is 33.
         */
    }

    public static void Method10()
    {
        var bufferBlock = new BufferBlock<int>();

        // Post more messages to the block.
        for (int i = 0; i < 3; i++)
        {
            bufferBlock.Post(i);
        }

        // Receive the messages back from the block.
        while (bufferBlock.TryReceive(out int value))
        {
            Console.WriteLine(value);
        }

        // Output:
        //   0
        //   1
        //   2
    }

    public static async Task Method11()
    {
        var bufferBlock = new BufferBlock<int>();

        // Write to and read from the message block concurrently.
        var post01 = Task.Run(() =>
        {
            bufferBlock.Post(0);
            bufferBlock.Post(1);
        });
        var receive = Task.Run(() =>
        {
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine(bufferBlock.Receive());
            }
        });
        var post2 = Task.Run(() =>
        {
            bufferBlock.Post(2);
        });

        await Task.WhenAll(post01, receive, post2);

        // Output:
        //   0
        //   1
        //   2
    }

    public static async Task Method12()
    {
        var bufferBlock = new BufferBlock<int>();

        // Post more messages to the block asynchronously.
        for (int i = 0; i < 3; i++)
        {
            await bufferBlock.SendAsync(i);
        }

        // Asynchronously receive the messages back from the block.
        for (int i = 0; i < 3; i++)
        {
            Console.WriteLine(await bufferBlock.ReceiveAsync());
        }

        // Output:
        //   0
        //   1
        //   2
    }

    public static async Task Method13()
    {
        var bufferBlock = new BufferBlock<int>();

        // Demonstrate asynchronous dataflow operations.
        await DataflowReadWrite.AsyncSendReceive(bufferBlock);
    }

}


// Demonstrates a how to write to and read from a dataflow block.
public static class DataflowReadWrite
{
    // Demonstrates asynchronous dataflow operations.
    public static async Task AsyncSendReceive(BufferBlock<int> bufferBlock)
    {
        // Post more messages to the block asynchronously.
        for (int i = 0; i < 3; i++)
        {
            await bufferBlock.SendAsync(i);
        }

        // Asynchronously receive the messages back from the block.
        for (int i = 0; i < 3; i++)
        {
            Console.WriteLine(await bufferBlock.ReceiveAsync());
        }

        // Output:
        //   0
        //   1
        //   2
    }
}