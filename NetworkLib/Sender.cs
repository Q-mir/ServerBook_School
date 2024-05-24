using TransportLib;
using System.Text.Json;

namespace NetworkLib;

public class Sender : ISender
{
    private readonly StreamWriter _writer;

    public Sender(StreamWriter writer)
    {
        ArgumentNullException.ThrowIfNull(writer);
         _writer = writer;  
    }

    public void SendMessage(Message message)
    {
        string mail = JsonSerializer.Serialize(message);
        _writer.WriteLine(mail);
        _writer.Flush();
    }
}
