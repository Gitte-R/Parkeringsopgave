// See https://aka.ms/new-console-template for more information
using ConsoleApp;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

Console.WriteLine("Hello, World!");
long start = 0;
var end = 100;
var client = new HttpClient();
//client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
Console.ReadKey();
using var resp = await client.GetAsync(new Uri($"https://localhost:32770/events/test?start={start}&end={end}"));
//using var resp = await client.GetAsync(new Uri($"https://localhost:32770/events/test?start=1&end=2"));
//using var resp = await client.GetAsync(new Uri($"https://localhost:32770/events/test?start=1&end=2"));
//https://localhost:32770/events/test?start=1&end=2
//var respAsJson = await resp.Content.ReadFromJsonAsync<ParkingServiceEvent>();
await ProcessEvents(await resp.Content.ReadAsStreamAsync());
await SaveStartIdToDataStore(start);




// fake implementation. Should apply business rules to events
async Task ProcessEvents(Stream content)
{
    var events = await JsonSerializer.DeserializeAsync<ParkingServiceEvent[]>(content) ?? new ParkingServiceEvent[0];
    foreach (var @event in events)
    {
        Console.WriteLine(@event);
        start = Math.Max(start, @event.SequenceNumber + 1);
    }
}


//Gateway gat1 = new Gateway(new HttpClient());
//await gat1.SendSMS("+4521970411", "CN17870");

Task SaveStartIdToDataStore(long startId) => Task.CompletedTask;

public record ParkingServiceEvent(long SequenceNumber, DateTimeOffset OccuredAt, string Name, object Content);



