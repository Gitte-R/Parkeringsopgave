using CarTypeService.Services;
using Polly;
using System.Net.Http;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

//builder.Services.AddScoped<IMotorApiService, MotorApiService>();
//builder.Services.AddHttpClient<IMotorApiService, MotorApiService>();

builder.Services.AddHttpClient<IMotorApiService, MotorApiService>().AddTransientHttpErrorPolicy(p =>
p.WaitAndRetryAsync(3, attempt => TimeSpan.FromMilliseconds(100 * Math.Pow(2, attempt))));

var MotorApiKey = builder.Configuration["Parking:MotorApiKey"];
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
