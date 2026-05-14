namespace webapi_play_ground.Services;

public class TestServiceKey1 : BaseTestService
{
    protected override Task HandleCore()
    {
        Console.WriteLine("HandleCore TestServiceKey1");
        return Task.CompletedTask;
    }
}