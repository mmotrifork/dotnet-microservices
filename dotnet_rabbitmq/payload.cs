using System.Text.Json;

namespace dotnet_rabbitmq;

public class Payload
{
    public DateTime TimeStamp { get; set; }
    public int Count { get; set; }
    
    public Payload(DateTime timeStamp, int count)
    {
        TimeStamp = timeStamp;
        Count = count;
    }
    
    // Parameterless constructor for deserialization
    public Payload() { }
    
    public Payload(string json)
    {
        try
        {
            var payload = JsonSerializer.Deserialize<Payload>(json);
            if (payload != null)
            {
                TimeStamp = payload.TimeStamp;
                Count = payload.Count;
            }
            else
            {
                Console.WriteLine("Deserialization returned null.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Deserialization failed: {ex.Message}");
        }
    }
    
    public string ToJson()
    {
        return JsonSerializer.Serialize(this);
    }

    public override string ToString()
    {
        return $"TimeStamp: {TimeStamp}, Count: {Count}";
    }
}