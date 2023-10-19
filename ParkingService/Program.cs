using ParkingService.Services;
using Polly;
using ParkingService.Events;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IParkingStore, ParkingStore>();
builder.Services.AddScoped<IEventStore, EventStore>();

builder.Services.AddHttpClient<IParkingStore, ParkingStore>().AddTransientHttpErrorPolicy(p =>
p.WaitAndRetryAsync(3, attempt => TimeSpan.FromMilliseconds(100 * Math.Pow(2, attempt))));

builder.Services.Scan(selector => selector.FromAssemblyOf<IParkingStore>().AddClasses(classes => classes.AssignableTo<IParkingStore>()).AsImplementedInterfaces());

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
