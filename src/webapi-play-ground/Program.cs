using webapi_play_ground.Extensions;
using webapi_play_ground.Filters;
using webapi_play_ground.Services;

var builder = WebApplication.CreateBuilder(args);

Console.WriteLine("Executing Program.cs file");

builder.Services.AddTransient<TestAsyncActionFilterOne>();
builder.Services.AddControllers(options =>
{
    options.Filters.Add(typeof(TestAsyncActionFilterTwo));
}).AddNewtonsoftJson();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<ITestService1, TestService1>();
builder.Services.AddScoped<ITestService2, TestService2>();
builder.Services.AddSingleton<ITestService3, TestService3>();
builder.Services.AddKeyedScoped<ITestService, TestServiceKey1>("key1");
builder.Services.AddKeyedScoped<ITestService, TestServiceKey2>("key2");
builder.Services.AddTransient<IService, Service>();
builder.Services.AddTransient<IBookService, BookService>();
builder.Services.AddSingleton(TimeProvider.System);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCustomMiddleware();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();

app.Run();