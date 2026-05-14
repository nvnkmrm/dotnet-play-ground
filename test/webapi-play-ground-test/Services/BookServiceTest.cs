using Microsoft.Extensions.Time.Testing;
using Moq;
using webapi_play_ground.Services;

namespace webapi_play_ground_test.Services;

public class BookServiceTest
{
    private readonly FakeTimeProvider _fakeTimeProvider;
    private readonly BookService _bookService;

    public BookServiceTest()
    {
        _fakeTimeProvider = new FakeTimeProvider();
        _bookService = new BookService(_fakeTimeProvider,
            new Mock<ITestService1>().Object,
            new Mock<ITestService2>().Object,
            new Mock<ITestService3>().Object,
            new Mock<ITestService>().Object,
            new Mock<ITestService>().Object);
    }

    [Fact]
    public void ShouldReturnAllBooks()
    {
        var createdAt = DateTime.UtcNow;
        _fakeTimeProvider.SetUtcNow(createdAt);

        var result = _bookService.GetAllBooks();

        var book = result.First();
        Assert.Equivalent(createdAt.AddHours(1), book.CreatedAt);
    }
}