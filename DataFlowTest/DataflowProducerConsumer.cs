using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks.Dataflow;

namespace DataFlow.Library;

/// <summary>
/// Implement a producer-consumer dataflow pattern
/// </summary>
public static class DataflowProducerConsumer
{
    private static void Produce(ITargetBlock<byte[]> target)
    {
        var rand = new Random();

        for (int i = 0; i < 100; ++i)
        {
            var buffer = new byte[1024];
            rand.NextBytes(buffer);
            target.Post(buffer);
        }

        target.Complete();
    }

    /// <summary>
    /// The preceding example uses just one consumer to process the source data.
    /// If you have multiple consumers in your application, use the TryReceive method to read data from the source block
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    private static async Task<int> ConsumeAsync_NotRobust(ISourceBlock<byte[]> source)
    {
        int bytesProcessed = 0;

        while (await source.OutputAvailableAsync())
        {
            byte[] data = await source.ReceiveAsync();
            bytesProcessed += data.Length;
        }

        return bytesProcessed;
    }

    /// <summary>
    /// The TryReceive method returns False when no data is available. 
    /// When multiple consumers must access the source block concurrently, 
    /// this mechanism guarantees that data is still available after the call to OutputAvailableAsync.
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    private static async Task<int> ConsumeAsync(IReceivableSourceBlock<byte[]> source)
    {
        int bytesProcessed = 0;
        while (await source.OutputAvailableAsync())
        {
            while (source.TryReceive(out byte[]? data))
            {
                bytesProcessed += data.Length;
            }
        }
        return bytesProcessed;
    }

    public static async Task Run()
    {
        var buffer = new BufferBlock<byte[]>();
        var consumerTask = ConsumeAsync(buffer);
        Produce(buffer);

        var bytesProcessed = await consumerTask;

        Console.WriteLine($"Processed {bytesProcessed:#,#} bytes.");

        // Sample  output:
        //     Processed 102,400 bytes.
    }
}