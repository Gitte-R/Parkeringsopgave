// See https://aka.ms/new-console-template for more information
using ConsoleApp;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

Console.WriteLine("Hello, World!");
long start = 0;
var end = 100;
var client = new HttpClient();
client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
Console.ReadKey();
using var resp = await client.GetAsync(new Uri($"https://localhost:32768/events/test/?start={start}&end={end}"));

await ProcessEvents(await resp.Content.ReadAsStreamAsync());
await SaveStartIdToDataStore(start);




// fake implementation. Should apply business rules to events
async Task ProcessEvents(Stream content)
{
    Gateway gat1 = new Gateway(new HttpClient());

    //var events = await JsonSerializer.DeserializeAsync<ParkingServiceEvent[]>(content);
    var events = await JsonSerializer.DeserializeAsync<EventFeedEvent[]>(content) ?? new EventFeedEvent[0];
    foreach (var @event in events)
    {
        Console.WriteLine(@event);
        start = Math.Max(start, @event.sequenceNumber + 1);
        await gat1.SendSMS(@event.content.phonenumber, @event.content.licensplate);
        await gat1.SendSMS("+4521970411", "CN17870");

    }
}



Task SaveStartIdToDataStore(long startId) => Task.CompletedTask;

public record EventFeedEvent(long sequenceNumber, DateTimeOffset occuredAt, string name, Parking content);
public record Parking (string licensplate, DateTimeOffset time, string parkinglot, string? phonenumber, string? email);



