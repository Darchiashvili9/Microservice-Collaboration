using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;


Console.WriteLine("******************** Version 2.0 Version 2.0 Version 2.0 Version 2.0");
Console.WriteLine("EVENT CONSUMER ***************** Version 2.0 Version 2.0 Version 2.0");


var start = await GetStartIdFromDatastore();
var end = 100;
var client = new HttpClient();
client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
//using var resp = await client.GetAsync(new Uri($"http://host.docker.internal:5002/events?start={start}&end={end}"));

using var resp = await client.GetAsync(new Uri($"http://20.166.179.207:5002/events?start={start}&end={end}"));

await ProcessEvents(await resp.Content.ReadAsStreamAsync());
await SaveStartIdToDataStore(start);

// fake implementation. Should get from a real database
Task<long> GetStartIdFromDatastore() => Task.FromResult(0L);


// fake implementation. Should apply business rules to events
async Task ProcessEvents(Stream content)
{
    var options = new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true
    };

    var events = await JsonSerializer.DeserializeAsync<SpecialOfferEvent[]>(content, options) ?? new SpecialOfferEvent[0];
    foreach (var @event in events)
    {
        Console.WriteLine(@event);
        start = Math.Max(start, @event.SequenceNumber + 1);
    }
}

Task SaveStartIdToDataStore(long startId) => Task.CompletedTask;

public record SpecialOfferEvent(long SequenceNumber, DateTimeOffset OccuredAt, string Name, object Content);