namespace TransportLib;
public class Message
{
    public object Data { get; set; } = null!;
    public TypeMessage Type { get; set; }
    public Guid? Guid { get; set; } 
}
