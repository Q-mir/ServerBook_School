using System;
using System.Text.Json;
using TransportLib;


namespace NetworkLib;
public class Reader : IReceive
{
    private readonly StreamReader _reader;

    public Reader(StreamReader reader)
    {
        ArgumentNullException.ThrowIfNull(reader);
        _reader = reader;
    }

    public Message ReadMessage()
    {
        string message = _reader.ReadLine();
        if (message == null)
            return null;
        
        return JsonSerializer.Deserialize<Message>(message);
    }
}
