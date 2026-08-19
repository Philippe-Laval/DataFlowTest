namespace DataFlow.Library.Tests;

public class DataflowExecutionBlocksTest
{
    [Fact]
    public void TestRun()
    {
        DataflowExecutionBlocks.Run();
    }

    [Fact]
    public void TestRunAsync1()
    {
        DataflowExecutionBlocksAsync1.Run();
    }

    [Fact]
    public void TestRunAsync2()
    {
        DataflowExecutionBlocksAsync2.Run();
    }
}

