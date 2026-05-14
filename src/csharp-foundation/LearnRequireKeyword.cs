namespace csharp_foundation;

public class LearnRequireKeyword
{
    public required string Id { get; init; }

    public required string Name { get; init; }

    public required int Age { get; init; }

    public override string ToString()
    {
        return $"Id: {Id}, Name: {Name}, Age: {Age}";
    }
}