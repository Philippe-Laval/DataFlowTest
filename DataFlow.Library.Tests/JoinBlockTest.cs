namespace DataFlow.Library.Tests;

public class JoinBlockTest
{
    [Fact]
    public async Task TestRun()
    {
        await JoinBlock.Run();
        Console.WriteLine("test");
    }
}


