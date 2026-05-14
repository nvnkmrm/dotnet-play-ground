namespace webapi_play_ground.Services;

public class TestServiceKey2: BaseTestService
{
    protected override Task HandleCore()
    {
        Console.WriteLine("HandleCore TestServiceKey2");
        return Task.CompletedTask;
    }
}