namespace webapi_play_ground.Services;

public abstract class BaseTestService : ITestService
{
    protected abstract Task HandleCore();
    
    public void Test()
    {
        HandleCore();
    }
}