
using csharp_foundation.Excercise;
using System.Text.Json;
using csharp_foundation;

new SwitchExploration().Run();

Console.WriteLine(Guid.Parse("9d1d2c26-b253-4cd9-850e-0bcd0a79cc96"));

var learnRequireKeyword = ParseLearnRequireKeyword();
foreach (var item in learnRequireKeyword)
{
	Console.WriteLine(item);
}

return;

static IEnumerable<LearnRequireKeyword> ParseLearnRequireKeyword()
{
	var jsonFilePath = Path.Combine(AppContext.BaseDirectory, "LearnRequireKeyword.json");
	var jsonContent = File.ReadAllText(jsonFilePath);

	return JsonSerializer.Deserialize<IEnumerable<LearnRequireKeyword>>(jsonContent)
		   ?? throw new InvalidOperationException("Failed to parse LearnRequireKeyword list from JSON.");
}