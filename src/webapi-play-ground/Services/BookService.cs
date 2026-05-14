using webapi_play_ground.Models;

namespace webapi_play_ground.Services;

public class BookService(TimeProvider timeProvider,
    ITestService1 testService1,
    ITestService2 testService2,
    ITestService3 testService3,
    [FromKeyedServices("key1")]ITestService testServiceKey1,
    [FromKeyedServices("key2")]ITestService testServiceKey2) : IBookService
{
    public List<Book> GetAllBooks()
    {
        Console.WriteLine(testService1.GetHashCode());
        Console.WriteLine(testService2.GetHashCode());
        Console.WriteLine(testService3.GetHashCode());
        testServiceKey1.Test();
        testServiceKey2.Test();
        
        List<Book> books = [];

        
        for (var i = 0; i < 10; i++)
        {
            books.Add(new Book()
            {
                Id = Guid.NewGuid().ToString(),
                Title = $"Book Title {i}",
                BookType = $"Book Type {i % 3}",
                CreatedAt = timeProvider.GetUtcNow().UtcDateTime.AddHours(1),
                UpdatedAt = DateTime.UtcNow.AddHours(1)
            });
        }

        return books;
    }
}