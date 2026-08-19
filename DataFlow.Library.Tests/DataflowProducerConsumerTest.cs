namespace DataFlow.Library.Tests
{
    public class DataflowProducerConsumerTest
    {
        [Fact]
        public async Task TestRun()
        {
            await DataflowProducerConsumer.Run();
        }
    }
}
